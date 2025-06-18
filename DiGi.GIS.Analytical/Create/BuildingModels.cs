using DiGi.Analytical.Building.Classes;
using DiGi.CityGML;
using DiGi.CityGML.Classes;
using DiGi.CityGML.Enums;
using DiGi.CityGML.Interfaces;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Planar.Interfaces;
using DiGi.Geometry.Spatial;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.GIS.Analytical.Enums;
using DiGi.GIS.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiGi.GIS.Analytical
{
    public static partial class Create
    {
        public static List<BuildingModel> BuildingModels(this GISModelFile gISModelFile, string directory_CityGML, double tolerance = Core.Constans.Tolerance.Distance)
        {
            string path = gISModelFile?.Path;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            List<Building2D> building2Ds = gISModelFile.Value?.GetObjects<Building2D>();
            if (building2Ds == null || building2Ds.Count == 0)
            {
                return null;
            }

            string fileName = Path.GetFileNameWithoutExtension(path);
            if (fileName.EndsWith("_GML"))
            {
                fileName = fileName.Substring(0, fileName.Length - 4);
            }

            string[] paths_CityGML = Directory.GetFiles(directory_CityGML, string.Format("{0}.zip", fileName), SearchOption.AllDirectories);

            List<List<CityModel>> cityModelsList = Enumerable.Repeat<List<CityModel>>(null, paths_CityGML.Length).ToList();

            Parallel.For(0, paths_CityGML.Length, GIS.Query.DefaultParallelOptions() , i => 
            {
                cityModelsList[i] = CityGML.Create.CityModels(paths_CityGML[i]);
            });

            List<CityModel> cityModels = new List<CityModel>();
            foreach (List<CityModel> cityModels_Temp in cityModelsList)
            {
                if (cityModels_Temp != null && cityModels_Temp.Count != 0)
                {
                    cityModels.AddRange(cityModels_Temp);
                }
            }


            List<BuildingModel> result = new List<BuildingModel>();

            List<Building2D> building2Ds_Unidentified = new List<Building2D>();

            foreach (Building2D building2D in building2Ds)
            {
                BuildingModel buildingModel = BuildingModel(building2D, cityModels, tolerance);
                if (buildingModel == null)
                {
                    if(building2D?.PolygonalFace2D == null)
                    {
                        continue;
                    }

                    building2Ds_Unidentified.Add(building2D);
                }

                buildingModel.SetValue(BuildingModelParameter.Reference, building2D.Reference, new Core.Parameter.Classes.SetValueSettings(true, false));
                result.Add(buildingModel);
            }

            if (building2Ds_Unidentified != null && building2Ds_Unidentified.Count != 0)
            {
                Plane plane = Geometry.Spatial.Constans.Plane.WorldZ;

                List<Tuple<BoundingBox2D, List<IPolygonalFace2D>, Building>> tuples = new List<Tuple<BoundingBox2D, List<IPolygonalFace2D>, Building>>();
                foreach (CityModel cityModel in cityModels)
                {
                    IEnumerable<Building> buildings = cityModel.Buildings;
                    if(buildings == null || buildings.Count() == 0)
                    {
                        continue;
                    }

                    foreach (Building building in cityModel.Buildings)
                    {
                        List<IPolygonalFace2D> polygonalFace2Ds = new List<IPolygonalFace2D>();
                        foreach (ISurface surface in building.Surfaces)
                        {
                            IPolygonalFace3D polygonalFace3D = Geometry.Spatial.Query.Project<IPolygonalFace3D>(plane, surface.Geometry, tolerance);
                            if (polygonalFace3D == null)
                            {
                                continue;
                            }

                            IPolygonalFace2D polygonalFace2D = plane.Convert(polygonalFace3D);
                            if (polygonalFace2D == null)
                            {
                                continue;
                            }

                            double area = polygonalFace2D.GetArea();
                            if (area < tolerance)
                            {
                                continue;
                            }

                            polygonalFace2Ds.Add(polygonalFace2D);
                        }

                        if (polygonalFace2Ds == null || polygonalFace2Ds.Count == 0)
                        {
                            continue;
                        }

                        polygonalFace2Ds = Geometry.Planar.Query.Union(polygonalFace2Ds);

                        BoundingBox2D boundingBox2D = Geometry.Planar.Create.BoundingBox2D(polygonalFace2Ds);
                        if(boundingBox2D == null)
                        {
                            continue;
                        }

                        tuples.Add(new Tuple<BoundingBox2D, List<IPolygonalFace2D>, Building>(boundingBox2D, polygonalFace2Ds, building));
                    }
                }

                if(tuples != null && tuples.Count != 0)
                {
                    for(int i = building2Ds_Unidentified.Count - 1; i >= 0; i--)
                    {
                        Point2D point2D = building2Ds_Unidentified[i].PolygonalFace2D.GetInternalPoint();
                        if (point2D == null)
                        {
                            continue;
                        }

                        List<Tuple<BoundingBox2D, List<IPolygonalFace2D>, Building>> tuples_Temp = tuples.FindAll(x => x.Item1.InRange(point2D, tolerance));
                        if (tuples_Temp == null || tuples_Temp.Count == 0)
                        {
                            continue;
                        }

                        tuples_Temp = tuples_Temp.FindAll(x => x.Item2.Find(x => x.InRange(point2D, tolerance)) != null);
                        if (tuples_Temp == null || tuples_Temp.Count == 0)
                        {
                            continue;
                        }

                        BuildingModel buildingModel = BuildingModel(tuples_Temp[0].Item3, tolerance);
                        if (buildingModel == null)
                        {
                            continue;
                        }

                        buildingModel.SetValue(BuildingModelParameter.Reference, building2Ds_Unidentified[i].Reference, new Core.Parameter.Classes.SetValueSettings(true, false));
                        result.Add(buildingModel);
                        building2Ds_Unidentified.RemoveAt(i);
                    }

                    double storeyHeight = 3.0;

                    for (int i = building2Ds_Unidentified.Count - 1; i >= 0; i--)
                    {
                        IPolygonalFace2D polygonalFace2D = building2Ds_Unidentified[i].PolygonalFace2D;
                        List<Point2D> point2Ds = polygonalFace2D.ExternalEdge.GetPoints();
                        if(point2Ds == null || point2Ds.Count == 0)
                        {
                            continue;
                        }
                        double elevation = 0;
                        double distance = 0;
                        foreach(Point2D point2D in point2Ds)
                        {
                            double distance_Point2D = double.MaxValue;
                            Building building = null;
                            foreach(Tuple<BoundingBox2D, List<IPolygonalFace2D>, Building> tuple in tuples)
                            {
                                double distance_BoundingBox2D = tuple.Item1.Distance(point2D);
                                if(distance_BoundingBox2D < distance_Point2D)
                                {
                                    building = tuple.Item3;
                                    distance_Point2D = distance_BoundingBox2D;
                                }
                            }

                            if(building == null)
                            {
                                continue;
                            }

                            Point3D point3D = new Point3D(point2D.X, point2D.Y, building.BoundingBox().Min.Z);

                            Point3D point3D_Closest = building.ClosestPoint(point3D, tolerance);

                            distance_Point2D = point3D.Distance(point3D_Closest);

                            elevation += (point3D_Closest.Z * distance_Point2D);
                            distance += distance_Point2D;
                        }

                        elevation = elevation / distance;

                        plane = Geometry.Spatial.Create.Plane(elevation);

                        IPolygonalFace3D polygonalFace3D = plane.Convert(polygonalFace2D);

                        Polyhedron polyhedron = Geometry.Spatial.Create.Polyhedron(polygonalFace3D, plane.Normal * storeyHeight);
                        if(polyhedron == null)
                        {
                            continue;
                        }

                        BuildingModel buildingModel = BuildingModel(polyhedron, tolerance);
                        if (buildingModel == null)
                        {
                            continue;
                        }

                        buildingModel.SetValue(BuildingModelParameter.Reference, building2Ds_Unidentified[i].Reference, new Core.Parameter.Classes.SetValueSettings(true, false));
                        result.Add(buildingModel);
                        building2Ds_Unidentified.RemoveAt(i);
                    }
                }

            }

            return result;
        }
    }
}
