using DiGi.Analytical.Building.Classes;
using DiGi.Core.Parameter.Classes;
using System.ComponentModel;

namespace DiGi.GIS.Analytical.Enums
{
    /// <summary>
    /// Defines the available parameters for a building model.
    /// </summary>
    [AssociatedTypes(typeof(BuildingModel)), Description("Building Model Parameters")]
    public enum BuildingModelParameter
    {
        /// <summary>
        /// Gets the Level Of Detail (LOD) of the building model.
        /// </summary>
        [ParameterProperties("8286ccdc-1731-4d66-b8a2-de6f52c00a24", "LOD", "Level Of Detail", Core.Parameter.Enums.AccessType.Read), StringParameterValue()] LOD,

        /// <summary>
        /// Gets the reference identifier associated with the building model.
        /// </summary>
        [ParameterProperties("59029eab-aa99-4caa-9ae6-10613421ef1d", "Reference", "Reference", Core.Parameter.Enums.AccessType.Read), StringParameterValue(false)] Reference,
    }
}