using DiGi.Analytical.Building.Classes;
using DiGi.Analytical.Building.Interfaces;
using DiGi.CityGML;
using DiGi.CityGML.Classes;
using DiGi.CityGML.Interfaces;
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
        /// <para>The building reference (<see cref="GISGuidObject2D.Reference"/>) is carried over to <see cref="BuildingModelParameter.Reference"/>.</para>
        /// </summary>
        /// <param name="building2D">The 2D building representation.</param>
        /// <param name="storeyHeight">The height of a single storey in meters used for the extrusion.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> instance if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building2D? building2D, double storeyHeight = 3.0, double tolerance = Core.Constants.Tolerance.Distance)
        {
            PolygonalFace3D? polygonalFace3D = Geometry.Spatial.Constants.Plane.WorldZ.Convert(building2D?.PolygonalFace2D);
            if (polygonalFace3D is null)
            {
                return null;
            }

            BuildingModel? result = BuildingModel(polygonalFace3D, building2D!.Storeys, storeyHeight, tolerance);

            if (result is not null && !string.IsNullOrWhiteSpace(building2D.Reference))
            {
                result.SetValue(BuildingModelParameter.Reference, building2D.Reference, new Core.Parameter.Classes.SetValueSettings(true, false));
            }

            return result;
        }

        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> by extruding a polygonal face into the specified number of storeys.
        /// </summary>
        /// <param name="polygonalFace3D">The base polygonal face to extrude.</param>
        /// <param name="storeys">The number of storeys to generate.</param>
        /// <param name="storeyHeight">The height of each storey in meters.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this IPolygonalFace3D? polygonalFace3D, ushort storeys, double storeyHeight = 3.0, double tolerance = Core.Constants.Tolerance.Distance)
        {
            if (polygonalFace3D?.GetBoundingBox()?.Min?.Z is not double minElevation)
            {
                return null;
            }

            if (storeys == 0)
            {
                storeys = 1;
            }

            BuildingModel result = new();
            Space? space_Last = null;

            for (int i = 0; i < storeys; i++)
            {
                double min = minElevation + i * storeyHeight;
                double max = minElevation + ((i + 1) * storeyHeight);

                Plane plane_Min = Geometry.Spatial.Create.Plane(min)!;

                IPolygonalFace3D? polygonalFace3D_Project = plane_Min.Project<IPolygonalFace3D>(polygonalFace3D);
                if (polygonalFace3D_Project is null)
                {
                    continue;
                }

                Point3D? internalPoint = polygonalFace3D_Project.GetInternalPoint();
                if (internalPoint is null)
                {
                    continue;
                }

                internalPoint.Move(new Vector3D(0, 0, (min + max) / 2));

                Space space = new(internalPoint, $"Storey {i + 1}");
                result.Update(space);

                FaceFloor? faceFloor = DiGi.Analytical.Building.Create.FaceFloor(polygonalFace3D_Project, tolerance);
                if (faceFloor is not null)
                {
                    result.Update(faceFloor);

                    if (space_Last is not null)
                    {
                        result.Assign(faceFloor, space, space_Last);
                    }
                    else
                    {
                        result.Assign(faceFloor, space);
                    }
                }

                space_Last = space;

                if (polygonalFace3D_Project.Edges is List<IPolygonal3D> edges)
                {
                    foreach (IPolygonal3D edge in edges)
                    {
                        if (edge?.GetSegments() is List<Segment3D> segment3Ds)
                        {
                            foreach (Segment3D segment3D in segment3Ds)
                            {
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

                IPolygonalFace3D? polygonalFace3D_Roof = plane_Max.Project<IPolygonalFace3D>(polygonalFace3D);
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