using DiGi.CityGML.Classes;
using DiGi.CityGML.Enums;
using DiGi.Core;
using DiGi.GIS.Classes;
using DiGi.CityGML;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DiGi.GIS.Analytical
{
    public static partial class Query
    {
        public static Building Building(this Building2D building2D, IEnumerable<CityModel> cityModels)
        {
            string reference = building2D?.Reference;
            if (reference == null)
            {
                return null;
            }

            List<Tuple<LOD?, int?, Building>> tuples = new List<Tuple<LOD?, int?, Building>>();
            foreach (CityModel cityModel in cityModels)
            {
                LOD? lOD = null;
                int? year = null;

                if (cityModel.TryGetValue(CityModelParameter.LOD, out LOD lOD_Temp))
                {
                    lOD = lOD_Temp;
                }

                if (cityModel.TryGetValue(CityModelParameter.Year, out int year_Temp))
                {
                    year = year_Temp;
                }


                Building building = cityModel.Buildings.Find(x => x.Reference() == reference);
                if (building == null)
                {
                    continue;
                }

                tuples.Add(new Tuple<LOD?, int?, Building>(lOD, year, building));
            }

            if (tuples == null || tuples.Count == 0)
            {
                return null;
            }

            List<Tuple<LOD?, int?, Building>> tuples_Temp;

            tuples_Temp = tuples.FindAll(x => x.Item1 == LOD.LOD2);
            if(tuples_Temp != null && tuples_Temp.Count > 0)
            {
                List<Tuple<LOD?, int?, Building>> tuples_Temp_Year = tuples_Temp.FindAll(x => x.Item2 != null && x.Item2.HasValue);
                if(tuples_Temp_Year == null && tuples_Temp_Year.Count == 0)
                {
                    return tuples_Temp[0].Item3;
                }

                tuples_Temp_Year.Sort((x, y) => x.Item2.Value.CompareTo(y.Item2.Value));

                return tuples_Temp_Year.Last().Item3;
            }

            tuples_Temp = tuples.FindAll(x => x.Item1 == LOD.LOD1);
            if (tuples_Temp != null && tuples_Temp.Count > 0)
            {
                List<Tuple<LOD?, int?, Building>> tuples_Temp_Year = tuples_Temp.FindAll(x => x.Item2 != null && x.Item2.HasValue);
                if (tuples_Temp_Year == null && tuples_Temp_Year.Count == 0)
                {
                    return tuples_Temp[0].Item3;
                }

                tuples_Temp_Year.Sort((x, y) => x.Item2.Value.CompareTo(y.Item2.Value));

                return tuples_Temp_Year.Last().Item3;
            }

            return tuples[0].Item3;
        }
    }
}
