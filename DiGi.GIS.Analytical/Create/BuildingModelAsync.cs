using DiGi.Analytical.Building.Classes;
using DiGi.CityGML.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Spatial.Classes;
using DiGi.GIS.Classes;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiGi.GIS.Analytical
{
    public static partial class Create
    {
        /// <summary>
        /// Asynchronously creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> from a 2D building representation by extruding it storey by storey from the terrain elevation of its footprint.
        /// <para>The elevation is queried once, for the internal point of the footprint, through <see cref="GIS.Query.ElevationAsync(HttpClient, Point2D)"/>.</para>
        /// <para>The elevation enriches the model, it is not a precondition - when the terrain service cannot be reached the model is still extruded, from an elevation of zero. Only a null client or a footprint that cannot be extruded yields null.</para>
        /// </summary>
        /// <param name="httpClient">The HTTP client used to query terrain elevation.</param>
        /// <param name="building2D">The 2D building representation.</param>
        /// <param name="storeyHeight">The height of a single storey in meters used for the extrusion.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> instance if successful; otherwise, null.</returns>
        public static async Task<BuildingModel?> BuildingModelAsync(this HttpClient? httpClient, Building2D? building2D, double storeyHeight = 3.0, double tolerance = Core.Constants.Tolerance.Distance)
        {
            if (httpClient is null || building2D?.PolygonalFace2D is not PolygonalFace2D polygonalFace2D)
            {
                return null;
            }

            Point3D? point3D_Elevation = await GIS.Query.ElevationAsync(httpClient, polygonalFace2D.GetInternalPoint());

            return BuildingModel(building2D, Query.Elevation(point3D_Elevation), storeyHeight, tolerance);
        }

        /// <summary>
        /// Asynchronously creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> from a 3D building, refines it with the data carried by the matching 2D building and queries the terrain elevation only when the footprint has to be extruded.
        /// <para>The model is attempted from the 3D geometry first, with the elevation withheld as a not-a-number, so the extruded fallback of <see cref="BuildingModel(Building, Building2D, double, double, IEnumerable{double})"/> refuses to run. A model returned by that attempt was therefore built from the 3D geometry, which carries its own elevations, and no terrain query is made. Only when it returns null is the elevation fetched and the creation repeated.</para>
        /// <para>The elevation enriches the model, it is not a precondition - when the terrain service cannot be reached the footprint is still extruded, from an elevation of zero.</para>
        /// </summary>
        /// <param name="httpClient">The HTTP client used to query terrain elevation.</param>
        /// <param name="building">The 3D building object.</param>
        /// <param name="building2D">The 2D building representation.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <param name="candidateTolerances">Optional candidate tolerances to attempt if the polyhedron is not closed.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static async Task<BuildingModel?> BuildingModelAsync(this HttpClient? httpClient, Building? building, Building2D? building2D, double tolerance = Constants.Tolerance.Coordinate, IEnumerable<double>? candidateTolerances = null)
        {
            if (building is null && building2D is null)
            {
                return null;
            }

            BuildingModel? result = BuildingModel(building, building2D, double.NaN, tolerance, candidateTolerances);
            if (result is not null)
            {
                return result;
            }

            Point3D? point3D_Elevation = await GIS.Query.ElevationAsync(httpClient, building2D?.PolygonalFace2D?.GetInternalPoint());

            return BuildingModel(building, building2D, Query.Elevation(point3D_Elevation), tolerance, candidateTolerances);
        }
    }
}
