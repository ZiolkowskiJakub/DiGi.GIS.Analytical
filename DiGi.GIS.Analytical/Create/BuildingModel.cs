using DiGi.Analytical.Building;
using DiGi.Analytical.Building.Classes;
using DiGi.Analytical.Building.Interfaces;
using DiGi.CityGML;
using DiGi.CityGML.Classes;
using DiGi.CityGML.Interfaces;
using DiGi.Core.Parameter.Classes;
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
        /// <para>The model is built from the 3D building found in <paramref name="cityModels"/>, so the tolerance defaults to <see cref="Constants.Tolerance.Coordinate"/>, the coordinate precision of the national 3D building model. Pass an explicit tolerance when the city models come from a more precise source.</para>
        /// </summary>
        /// <param name="building2D">The 2D building representation.</param>
        /// <param name="cityModels">A collection of city models used to find the corresponding 3D building.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building2D? building2D, IEnumerable<CityModel>? cityModels, double tolerance = Constants.Tolerance.Coordinate)
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
        /// <para>The tolerance defaults to <see cref="Constants.Tolerance.Coordinate"/> rather than <see cref="Core.Constants.Tolerance.Distance"/> because the coordinates of the national 3D building model carry two decimal places - joining its boundary surfaces at a finer tolerance leaves the assembled rings open at the corners. Pass an explicit tolerance when the building comes from a more precise source.</para>
        /// </summary>
        /// <param name="building">The 3D building object.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building? building, double tolerance = Constants.Tolerance.Coordinate)
        {
            if (building is null)
            {
                return null;
            }

            IEnumerable<ISurface>? surfaces = building.Surfaces;
            if (surfaces == null || surfaces.Count() == 0)
            {
                return null;
            }

            Polyhedron? polyhedron = building.Polyhedron();

            BuildingModel result = new();

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
                }

                if (result.Update(component))
                {
                    components.Add(component);
                }
            }

            Space space = new(polyhedron?.GetInternalPoint(), building.UniqueId);
            result.Update(space);
            foreach (IComponent component in components)
            {
                result.Assign(component, space);
            }

            if (building.TryGetValue(BuildingParameter.Source, out string? source, new GetValueSettings(true, false)))
            {
                result.SetValue(BuildingModelParameter.Source, source);
            }

            if (building.TryGetValue(BuildingParameter.Code, out string? code, new GetValueSettings(true, false)))
            {
                result.SetValue(BuildingModelParameter.Code, code);
            }

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

                // The point already lies on the storey floor, so it is raised by half the storey rather than
                // moved by the absolute mid height - moving by the latter adds the base elevation a second
                // time and leaves the space point above the roof of every building not sitting at sea level.
                internalPoint.Move(new Vector3D(0, 0, (max - min) / 2));

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
                Plane plane_Max = Geometry.Spatial.Create.Plane(minElevation + (storeys * storeyHeight))!;

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

        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> from a 2D building representation by extruding it storey by storey from the given base elevation.
        /// <para>The building reference (<see cref="GISGuidObject2D.Reference"/>) is carried over to <see cref="BuildingModelParameter.Reference"/>.</para>
        /// </summary>
        /// <param name="building2D">The 2D building representation.</param>
        /// <param name="elevation">The base elevation in meters above sea level the footprint is extruded from. A not-a-number elevation means that no elevation is known and the method returns null rather than placing the building at a guessed height - the signal <see cref="BuildingModelAsync(System.Net.Http.HttpClient, Building, Building2D, double, IEnumerable{double})"/> relies on to decide whether the terrain service has to be queried.</param>
        /// <param name="storeyHeight">The height of a single storey in meters used for the extrusion.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> instance if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building2D? building2D, double elevation = 0, double storeyHeight = 3.0, double tolerance = Core.Constants.Tolerance.Distance)
        {
            if (double.IsNaN(elevation))
            {
                return null;
            }

            Plane? plane = Geometry.Spatial.Create.Plane(elevation);
            if (plane is null)
            {
                return null;
            }

            PolygonalFace3D? polygonalFace3D = plane.Convert(building2D?.PolygonalFace2D);
            if (polygonalFace3D is null)
            {
                return null;
            }

            BuildingModel? result = BuildingModel(polygonalFace3D, building2D!.Storeys, storeyHeight, tolerance);
            if (result is not null)
            {
                result.SetValue(BuildingModelParameter.Source, "PL.PZGiK.337.BDOT10k");

                if (!string.IsNullOrWhiteSpace(building2D.Reference))
                {
                    result.SetValue(BuildingModelParameter.Reference, building2D.Reference, new SetValueSettings(true, false));
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> from a 3D building and refines it with the data carried by the matching 2D building.
        /// <para>The model is built from the 3D geometry of <paramref name="building"/>. When that geometry is missing or cannot be converted, the model is extruded from the footprint of <paramref name="building2D"/> at <see cref="Constants.StoreyHeight.Default"/>, starting from <paramref name="elevation"/>.</para>
        /// <para>The storey count of <paramref name="building2D"/> is used to cut the model into storeys. The storey height is derived from the extents of the model, rounded down to <see cref="Constants.StoreyHeight.Precision"/>, and the cutting planes are measured downwards from the top of the model so that the rounding remainder is left to the lowest storey. Nothing is cut when the derived storey height is below <see cref="Constants.StoreyHeight.Min"/>.</para>
        /// <para>A storey height above <see cref="Constants.StoreyHeight.Max"/> is handled by the function of the building. For a non residential building the storey height is clamped to <see cref="Constants.StoreyHeight.Max"/> and the storey count is kept, so the whole remainder is left to the lowest storey. For a residential building the storey count is treated as unreliable instead and recalculated from the extents of the model at <see cref="Constants.StoreyHeight.Default"/>, the storey height being derived again from that count - the resulting model may therefore hold a different number of storeys than <see cref="Building2D.Storeys"/>. When even the recalculated storey height stays above <see cref="Constants.StoreyHeight.Max"/> the model is returned unsplit.</para>
        /// <para>The building reference (<see cref="GISGuidObject2D.Reference"/>) is carried over to <see cref="BuildingModelParameter.Reference"/>.</para>
        /// <para>The split does not re-host openings - windows and doors stay assigned to the fragment inheriting the identifier of the component they were hosted by, not to the fragment geometrically containing them.</para>
        /// <para>The tolerance defaults to <see cref="Constants.Tolerance.Coordinate"/> rather than <see cref="Core.Constants.Tolerance.Distance"/> because the coordinates of the national 3D building model carry two decimal places - at a finer tolerance the storey split leaves the ring assembled on the cutting plane open at the corners and no cut is made. Pass an explicit tolerance when the building comes from a more precise source.</para>
        /// </summary>
        /// <param name="building">The 3D building object.</param>
        /// <param name="building2D">The 2D building representation providing the storey count, the function and the reference.</param>
        /// <param name="elevation">The base elevation in meters above sea level. It is read only on the extruded fallback and is ignored when the 3D geometry converts, since that geometry carries its own elevations. A not-a-number elevation refuses the fallback and returns null instead of placing the building at a guessed height.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <param name="candidateTolerances">Optional candidate tolerances to attempt if the polyhedron is not closed at the specified tolerance.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building? building, Building2D? building2D, double elevation = 0, double tolerance = Constants.Tolerance.Coordinate, IEnumerable<double>? candidateTolerances = null)
        {
            if (building is null && building2D is null)
            {
                return null;
            }

            if (building2D is null)
            {
                return BuildingModel(building, tolerance);
            }

            if (building is null)
            {
                return BuildingModel(building2D, elevation, Constants.StoreyHeight.Default, tolerance);
            }

            double effectiveTolerance = tolerance;
            Polyhedron? polyhedron = building?.Polyhedron();
            if (polyhedron is not null && !polyhedron.IsClosed(effectiveTolerance))
            {
                candidateTolerances ??= [0.02, 0.05, 0.1];
                foreach (double candidateTolerance in candidateTolerances)
                {
                    if (polyhedron.IsClosed(candidateTolerance))
                    {
                        effectiveTolerance = candidateTolerance;
                        break;
                    }
                }
            }

            BuildingModel? result = BuildingModel(building, effectiveTolerance);
            if (result is null)
            {
                return BuildingModel(building2D, elevation, Constants.StoreyHeight.Default, effectiveTolerance);
            }

            if (!string.IsNullOrWhiteSpace(building2D.Reference))
            {
                result.SetValue(BuildingModelParameter.Reference, building2D.Reference, new SetValueSettings(true, false));
            }

            ushort storeys = building2D.Storeys;

            if (storeys > 1 && result.GetBoundingBox() is BoundingBox3D boundingBox3D)
            {
                List<double> elevations = [];

                double height = Core.Query.Round(boundingBox3D.Height / storeys, Constants.StoreyHeight.Precision, Core.Enums.RoundingMethod.Floor);
                if (height >= Constants.StoreyHeight.Min)
                {
                    if (GIS.Query.IsResidential(building2D))
                    {
                        if (height > Constants.StoreyHeight.Max)
                        {
                            storeys = System.Math.Max((ushort)1, System.Convert.ToUInt16(System.Math.Floor(boundingBox3D.Height / Constants.StoreyHeight.Default)));
                            height = Core.Query.Round(boundingBox3D.Height / storeys, Constants.StoreyHeight.Precision, Core.Enums.RoundingMethod.Floor);
                        }
                    }
                    else
                    {
                        if (height > Constants.StoreyHeight.Max)
                        {
                            height = Constants.StoreyHeight.Max;
                        }
                    }

                    if (height <= Constants.StoreyHeight.Max)
                    {
                        for (int i = 1; i < storeys; i++)
                        {
                            elevations.Add(boundingBox3D.MaxZ - (i * height));
                        }
                    }
                }

                if (elevations.Count > 0 && result.TrySplit(elevations, tolerance: effectiveTolerance))
                {
                    DiGi.Analytical.Building.Modify.ConvertAirs<IAir>(result);
                }
            }

            return result;
        }
    }
}