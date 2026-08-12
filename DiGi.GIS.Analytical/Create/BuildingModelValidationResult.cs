using DiGi.Analytical.Building;
using DiGi.Analytical.Building.Classes;
using DiGi.Analytical.Building.Interfaces;
using DiGi.Analytical.Classes;
using DiGi.Core.Parameter.Classes;
using DiGi.Geometry.Spatial;
using DiGi.Geometry.Spatial.Classes;
using DiGi.GIS.Analytical.Enums;
using DiGi.GIS.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.Analytical
{
    public static partial class Create
    {
        /// <summary>
        /// Validates a building model and collects everything that is wrong with it into a single result.
        /// <para>Every check is run and every failure recorded - the method does not stop at the first one, because a model is usually broken in more than one way and knowing which combination it carries is what points at the stage that broke it.</para>
        /// <para>Enclosure is the check this exists for. It is evaluated per space through <see cref="DiGi.Analytical.Building.Classes.BuildingModel.GetShell(ISpace, DiGi.Geometry.Core.Enums.Side?, DiGi.Geometry.Core.Enums.Orientation?, DiGi.Geometry.Core.Enums.Orientation?, double)"/>, so a curve wall is resolved into the surface it sweeps and an extruded model is judged by the same rule as one converted from CityGML. Alongside the verdict the smallest tolerance at which the whole model closes is reported, which separates a model whose source geometry is merely imprecise from one that never had a boundary.</para>
        /// </summary>
        /// <param name="buildingModel">The building model to validate.</param>
        /// <param name="tolerance">The distance tolerance the enclosure is required to hold at.</param>
        /// <returns>A <see cref="BuildingModelValidationResult"/>, or null when <paramref name="buildingModel"/> is null.</returns>
        public static BuildingModelValidationResult? BuildingModelValidationResult(this BuildingModel? buildingModel, double tolerance = Constants.Tolerance.Enclosure)
        {
            if (buildingModel is null)
            {
                return null;
            }

            // Ascending, so the first entry closing a shell is the smallest tolerance that shell needs. The
            // ladder ends an order of magnitude above the acceptance value: past that, welding stops telling
            // vertices meant to coincide from ones that were not, so a shell needing more is reported as never
            // closing rather than handed a number that would invite raising the tolerance to match.
            double[] tolerances = [Core.Constants.Tolerance.Distance, 1E-05, 0.0001, Core.Constants.Tolerance.MacroDistance, Constants.Tolerance.Coordinate, Constants.Tolerance.Enclosure, 0.1, 0.2, 0.5];

            HashSet<BuildingModelValidationCode> buildingModelValidationCodes = [];

            GetValueSettings getValueSettings = new(true, false);

            buildingModel.TryGetValue(BuildingModelParameter.Reference, out string? reference, getValueSettings);
            if (string.IsNullOrWhiteSpace(reference))
            {
                buildingModelValidationCodes.Add(BuildingModelValidationCode.MissingReference);
            }

            buildingModel.TryGetValue(BuildingModelParameter.Code, out string? code, getValueSettings);
            if (string.IsNullOrWhiteSpace(code))
            {
                buildingModelValidationCodes.Add(BuildingModelValidationCode.MissingCode);
            }

            if (!buildingModel.IsValid())
            {
                buildingModelValidationCodes.Add(BuildingModelValidationCode.InvalidComponent);
            }

            List<IComponent>? components = buildingModel.GetComponents<IComponent>();
            int componentCount = components is null ? 0 : components.Count;
            if (componentCount == 0)
            {
                buildingModelValidationCodes.Add(BuildingModelValidationCode.NoComponent);
            }

            List<Space>? spaces = buildingModel.GetSpaces<Space>();
            int spaceCount = spaces is null ? 0 : spaces.Count;
            if (spaceCount == 0)
            {
                buildingModelValidationCodes.Add(BuildingModelValidationCode.NoSpace);
            }

            int shellCount = 0;
            int enclosedShellCount = 0;

            // The model closes at the coarsest of the tolerances its shells individually need, and at none at
            // all if any single one of them never closes.
            bool enclosed = spaceCount != 0;
            double minEnclosingTolerance = 0;

            for (int i = 0; i < spaceCount; i++)
            {
                Space? space = spaces![i];

                Shell? shell = buildingModel.GetShell(space, tolerance: tolerance);
                if (shell is null || shell.Count == 0)
                {
                    // A space bounded by nothing is dropped by GetShell, and Polyhedron keeps no face list
                    // below the four a solid needs, so both states arrive here as a shell holding no face.
                    buildingModelValidationCodes.Add(BuildingModelValidationCode.NoComponent);
                    buildingModelValidationCodes.Add(BuildingModelValidationCode.NotEnclosed);
                    enclosed = false;
                    continue;
                }

                shellCount++;

                double minEnclosingTolerance_Shell = double.NaN;
                for (int j = 0; j < tolerances.Length; j++)
                {
                    if (shell.IsClosed(tolerances[j]))
                    {
                        minEnclosingTolerance_Shell = tolerances[j];
                        break;
                    }
                }

                // A shell closing at a tolerance finer than the one asked for is enclosed - the requirement is
                // to close at the given tolerance or below it, not at that value in particular. The distinction
                // is not academic: welding is not transitive, so a coarser tolerance can merge vertices that
                // were meant to stay apart, collapse the edges between them and report a shell that is
                // genuinely closed as open. Measured on the stored data, models closing at 1E-06 do fail at
                // 0.05 this way.
                bool enclosed_Shell = !double.IsNaN(minEnclosingTolerance_Shell) && minEnclosingTolerance_Shell <= tolerance;
                if (!enclosed_Shell && shell.IsClosed(tolerance))
                {
                    // The requested tolerance need not sit on the ladder.
                    enclosed_Shell = true;
                    minEnclosingTolerance_Shell = tolerance;
                }

                if (enclosed_Shell)
                {
                    enclosedShellCount++;

                    // Both are judged at the tolerance the shell actually closes at, since that is the one at
                    // which it is a solid at all.
                    if (!shell.IsClosed(true, minEnclosingTolerance_Shell))
                    {
                        buildingModelValidationCodes.Add(BuildingModelValidationCode.NonManifold);
                    }

                    // Only meaningful on a closed shell - the ray cast behind Inside has nothing to count
                    // crossings against when the boundary has gaps. The band stays at the requested tolerance,
                    // the more forgiving of the two, so only a point genuinely away from the space is reported.
                    Point3D? point3D = space?.Geometry;
                    if (point3D is null || !shell.Inside(point3D, tolerance))
                    {
                        buildingModelValidationCodes.Add(BuildingModelValidationCode.SpacePointOutsideShell);
                    }
                }
                else
                {
                    buildingModelValidationCodes.Add(BuildingModelValidationCode.NotEnclosed);
                }

                if (double.IsNaN(minEnclosingTolerance_Shell))
                {
                    enclosed = false;
                }
                else if (minEnclosingTolerance_Shell > minEnclosingTolerance)
                {
                    minEnclosingTolerance = minEnclosingTolerance_Shell;
                }
            }

            double minZ = double.NaN;
            double maxZ = double.NaN;

            BoundingBox3D? boundingBox3D = buildingModel.GetBoundingBox();
            if (boundingBox3D is null)
            {
                buildingModelValidationCodes.Add(BuildingModelValidationCode.DegenerateExtent);
            }
            else
            {
                minZ = boundingBox3D.MinZ;
                maxZ = boundingBox3D.MaxZ;

                if (double.IsNaN(minZ) || double.IsNaN(maxZ) || double.IsInfinity(minZ) || double.IsInfinity(maxZ) || maxZ - minZ <= tolerance)
                {
                    buildingModelValidationCodes.Add(BuildingModelValidationCode.DegenerateExtent);
                }
                else if (System.Math.Abs(minZ) <= tolerance)
                {
                    // A building resting exactly on the zero level was not placed there, it was left there
                    // because the terrain elevation never resolved.
                    buildingModelValidationCodes.Add(BuildingModelValidationCode.SeaLevel);
                }
            }

            List<BuildingModelValidationCode> buildingModelValidationCodes_Sorted = [.. buildingModelValidationCodes];
            buildingModelValidationCodes_Sorted.Sort();

            return new BuildingModelValidationResult(reference, code, tolerance, spaceCount, componentCount, shellCount, enclosedShellCount, enclosed ? minEnclosingTolerance : double.NaN, minZ, maxZ, buildingModelValidationCodes_Sorted);
        }
    }
}
