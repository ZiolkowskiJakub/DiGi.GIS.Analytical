using DiGi.Analytical.Building.Classes;
using DiGi.Core.Parameter.Classes;
using System.ComponentModel;

namespace DiGi.GIS.Analytical.Enums
{
    [AssociatedTypes(typeof(BuildingModel)), Description("Building Model Parameters")]
    public enum BuildingModelParameter
    {
        [ParameterProperties("8286ccdc-1731-4d66-b8a2-de6f52c00a24", "LOD", "Level Of Detail", Core.Parameter.Enums.AccessType.Read), StringParameterValue()] LOD,
        [ParameterProperties("59029eab-aa99-4caa-9ae6-10613421ef1d", "Reference", "Reference", Core.Parameter.Enums.AccessType.Read), StringParameterValue(false)] Reference,
    }
}
