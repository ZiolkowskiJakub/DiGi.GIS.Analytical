using DiGi.Analytical.Building.Classes;
using DiGi.Analytical.Building.Interfaces;
using DiGi.CityGML;
using DiGi.CityGML.Classes;
using DiGi.CityGML.Interfaces;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Planar.Interfaces;
using DiGi.Geometry.Spatial;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.GIS.Analytical.Enums;
using DiGi.GIS.Classes;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.Analytical
{
    public static partial class Create
    {
        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> based on a 2D building and a collection of city models.
        /// </summary>
        /// <param name="building2D">The 2D building representation.</param>
        /// <param name="cityModels">A collection of city models used to find the corresponding 3D building.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building2D? building2D, IEnumerable<CityModel>? cityModels, double tolerance = Core.Constants.Tolerance.Distance)
        {
            if (building2D == null)
            {
                return null;
            }

            Building? building = Query.Building(building2D, cityModels);
            if (building == null)
            {
                return null;
            }

            BuildingModel? result = BuildingModel(building, tolerance);

            return result;
        }

        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> from a 3D building object.
        /// </summary>
        /// <param name="building">The 3D building object.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building? building, double tolerance = Core.Constants.Tolerance.Distance)
        {
            IEnumerable<ISurface>? surfaces = building?.Surfaces;
            if (surfaces == null || surfaces.Count() == 0)
            {
                return null;
            }

            Polyhedron? polyhedron = building.Polyhedron();

            BuildingModel result = new();
            LOD lOD = LOD.LOD2;

            List<IComponent> components = [];
            foreach (ISurface surface in surfaces)
            {
                IComponent? component = surface.ToAnalytical();
                if (component == null)
                {
                    component = Component(surface?.Geometry, polyhedron, tolerance);
                    if (component == null)
                    {
                        continue;
                    }

                    lOD = LOD.LOD1;
                }

                if (result.Update(component))
                {
                    components.Add(component);
                }
            }

            Space space = new(polyhedron?.GetInternalPoint(), building?.UniqueId);
            result.Update(space);
            foreach (IComponent component in components)
            {
                result.Assign(component, space);
            }

            result.SetValue(BuildingModelParameter.LOD, lOD, new Core.Parameter.Classes.SetValueSettings() { TryConvert = true, CheckAccessType = false });

            return result;
        }

        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> from a polyhedron representation.
        /// </summary>
        /// <param name="polyhedron">The polyhedron representing the building geometry.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Polyhedron? polyhedron, double tolerance = Core.Constants.Tolerance.Distance)
        {
            IEnumerable<IPolygonalFace3D>? polygonalFace3Ds = polyhedron?.PolygonalFaces;
            if (polygonalFace3Ds == null || polygonalFace3Ds.Count() == 0)
            {
                return null;
            }

            BuildingModel result = new();
            List<IComponent> components = [];
            foreach (IPolygonalFace3D polygonalFace3D in polygonalFace3Ds)
            {
                IComponent? component = Component(polygonalFace3D, polyhedron, tolerance);
                if (component == null)
                {
                    continue;
                }

                if (result.Update(component))
                {
                    components.Add(component);
                }
            }

            Space space = new(polyhedron?.GetInternalPoint(), "Building");
            result.Update(space);
            foreach (IComponent component in components)
            {
                result.Assign(component, space);
            }

            result.SetValue(BuildingModelParameter.LOD, LOD.Undefined, new Core.Parameter.Classes.SetValueSettings() { TryConvert = true, CheckAccessType = false });

            return result;
        }

        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> from a 2D building representation by extruding it storey by storey.
        /// </summary>
        /// <param name="building2D">The 2D building representation.</param>
        /// <param name="storeyHeight">The height of a single storey in meters used for the extrusion.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> instance if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building2D? building2D, double storeyHeight = 3.0, double tolerance = Core.Constants.Tolerance.Distance)
        {
            if (building2D is null)
            {
                return null;
            }

            PolygonalFace2D? polygonalFace2D = building2D.PolygonalFace2D;
            if (polygonalFace2D is null)
            {
                return null;
            }

            ushort storeys = building2D.Storeys;
            if (storeys == 0)
            {
                storeys = 1;
            }

            BuildingModel result = new();
            Space? space_Last = null;

            for (int i = 0; i < storeys; i++)
            {
                double min = i * storeyHeight;
                double max = (i + 1) * storeyHeight;

                Plane plane_Min = Geometry.Spatial.Create.Plane(min)!;

                PolygonalFace3D? polygonalFace3D = plane_Min.Convert(polygonalFace2D);
                if (polygonalFace3D is null)
                {
                    continue;
                }

                Point3D? internalPoint = polygonalFace3D.GetInternalPoint();
                if (internalPoint is null)
                {
                    continue;
                }

                internalPoint.Move(new Vector3D(0, 0, (min + max) / 2));

                Space space = new(internalPoint, $"Storey {i + 1}");
                space_Last = space;

                result.Update(space);

                FaceFloor? faceFloor = DiGi.Analytical.Building.Create.FaceFloor(polygonalFace3D, tolerance);
                if (faceFloor is not null)
                {
                    result.Update(faceFloor);
                    result.Assign(faceFloor, space);
                }

                if (polygonalFace2D.Edges is List<IPolygonal2D> edges)
                {
                    foreach (IPolygonal2D edge in edges)
                    {
                        if (edge?.GetSegments() is List<Segment2D> segment2Ds)
                        {
                            foreach (Segment2D segment2D in segment2Ds)
                            {
                                if (plane_Min.Convert(segment2D) is not Segment3D segment3D)
                                {
                                    continue;
                                }

                                CurveWall? curveWall = DiGi.Analytical.Building.Create.CurveWall(segment3D, storeyHeight, tolerance);
                                if (curveWall is not null)
                                {
                                    result.Update(curveWall);
                                    result.Assign(curveWall, space);
                                }
                            }
                        }
                    }
                }
            }

            if (space_Last is not null)
            {
                Plane plane_Max = Geometry.Spatial.Create.Plane(storeys * storeyHeight)!;
                PolygonalFace3D? polygonalFace3D_Roof = plane_Max.Convert(polygonalFace2D);
                if (polygonalFace3D_Roof is not null)
                {
                    SurfaceRoof? surfaceRoof = DiGi.Analytical.Building.Create.SurfaceRoof(polygonalFace3D_Roof, tolerance);
                    if (surfaceRoof is not null)
                    {
                        result.Update(surfaceRoof);
                        result.Assign(surfaceRoof, space_Last);
                    }
                }
            }

            return result;
        }
    }
}