using DiGi.Analytical.Building.Classes;
using DiGi.Core.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Spatial.Classes;

namespace DiGi.GIS.Analytical
{
    public static partial class Modify
    {
        /// <summary>
        /// Sets the WGS 84 coordinates and the Polish standard time zone on the model's <see cref="BuildingModel.BuildingInformation"/>.
        /// <para>The coordinates are the WGS 84 conversion of the centre of the model's bounding box, whose geometry is in EPSG:2180. The UTC offset is fixed to <see cref="Core.Enums.UTC.Plus0100"/> (CET), deliberately without daylight saving: a shading model holds one offset for its whole life, so a solve over a date range would be wrong by one hour for half of every year if the offset switched with the season. Every time given to the solver is local standard time; a caller holding wall-clock summer time subtracts one hour before solving.</para>
        /// <para>The method is idempotent: it always overwrites and never reads the previous values, so the unlocated default of (0, 0) is never treated as "already set". The <see cref="BuildingInformation.Address"/> is preserved through the copy constructor.</para>
        /// </summary>
        /// <param name="buildingModel">The building model to locate.</param>
        /// <returns><see langword="false"/> when the model is null, has no bounding box, or the bounding-box centre converts to a WGS 84 position outside the Polish range (latitude [48.9, 55.0], longitude [14.05, 24.25]) - in which case the geometry is not in EPSG:2180 and the model is left unchanged.</returns>
        public static bool UpdateBuildingInformation(this BuildingModel? buildingModel)
        {
            if (buildingModel is null)
            {
                return false;
            }

            BoundingBox3D? boundingBox3D = buildingModel.GetBoundingBox();
            if (boundingBox3D is null)
            {
                return false;
            }

            Point2D point2D_Centre = new((boundingBox3D.Min.X + boundingBox3D.Max.X) / 2, (boundingBox3D.Min.Y + boundingBox3D.Max.Y) / 2);
            Coordinates? coordinates = GIS.Query.Coordinates(point2D_Centre);
            if (coordinates is null)
            {
                return false;
            }

            // Poland spans 48.95-54.85 N and 14.12-24.15 E. The margin keeps border and coastal
            // geometry in, while geometry near the origin (local scenes, models built from a Polyhedron)
            // converts far outside the range - the EPSG:2180 origin maps to Austria - and is left unlocated.
            const double latitudeMin = 48.9;
            const double latitudeMax = 55.0;
            const double longitudeMin = 14.05;
            const double longitudeMax = 24.25;
            if ((coordinates.Latitude < latitudeMin) || (coordinates.Latitude > latitudeMax) || (coordinates.Longitude < longitudeMin) || (coordinates.Longitude > longitudeMax))
            {
                return false;
            }

            BuildingInformation buildingInformation = new(buildingModel.BuildingInformation);
            buildingInformation.Coordinates = coordinates;
            buildingInformation.UTC = Core.Enums.UTC.Plus0100;
            buildingModel.BuildingInformation = buildingInformation;

            return true;
        }
    }
}