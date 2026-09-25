using DiGi.Analytical.Building.Classes;
using DiGi.Core.Classes;

namespace DiGi.GIS.Analytical
{
    public static partial class Query
    {
        /// <summary>
        /// Checks that the building model carries a located <see cref="BuildingModel.BuildingInformation"/>.
        /// <para>True when all three conditions hold: the <see cref="BuildingInformation.Coordinates"/> is not null, it is not the unlocated (0, 0) default, and the <see cref="BuildingInformation.UTC"/> is not <see cref="Core.Enums.UTC.Undefined"/>. A model failing any one of the three still computes its sun path at (0, 0) or with a not-a-number offset, so it is unlocated even when the other conditions hold.</para>
        /// </summary>
        /// <param name="buildingModel">The building model to check.</param>
        /// <returns><see langword="true"/> when the building model carries located coordinates and a defined UTC offset; otherwise, <see langword="false"/>.</returns>
        public static bool IsLocated(this BuildingModel? buildingModel)
        {
            if (buildingModel is null)
            {
                return false;
            }

            Coordinates? coordinates = buildingModel.BuildingInformation.Coordinates;
            if (coordinates is null || ((coordinates.Latitude == 0) && (coordinates.Longitude == 0)))
            {
                return false;
            }

            return buildingModel.BuildingInformation.UTC != Core.Enums.UTC.Undefined;
        }
    }
}
