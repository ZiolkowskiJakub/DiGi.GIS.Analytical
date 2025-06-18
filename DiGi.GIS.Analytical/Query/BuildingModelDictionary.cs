using DiGi.Analytical.Building.Classes;
using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.Analytical
{
    public static partial class Query
    {
        public static Dictionary<string, BuildingModel> BuildingModelDictionary(string path, IEnumerable<string> references)
        {
            if(string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path) || references == null)
            {
                return null;
            }

            HashSet<UniqueReference> uniqueReferences = new HashSet<UniqueReference>();
            foreach (string reference in references)
            {
                UniqueReference uniqueReference = OrtoDatasFile.GetUniqueReference(reference);
                if (uniqueReference == null)
                {
                    continue;
                }

                uniqueReferences.Add(uniqueReference);
            }

            Dictionary<string, BuildingModel> result = new Dictionary<string, BuildingModel>();

            if (uniqueReferences.Count == 0)
            {
                return result;
            }

            List<BuildingModel> buildingModels = null;
            using (BuildingModelsFile buildingModelsFile = new BuildingModelsFile(path))
            {
                buildingModelsFile.Open();

                buildingModels = buildingModelsFile.GetValues<BuildingModel>(uniqueReferences)?.ToList();
            }

            if (buildingModels == null)
            {
                return result;
            }

            for (int i = buildingModels.Count - 1; i >= 0; i--)
            {
                if (buildingModels[i] == null)
                {
                    continue;
                }

                UniqueReference uniqueReference = uniqueReferences.ElementAt(i);

                result[uniqueReference.UniqueId] = buildingModels[i];
                uniqueReferences.Remove(uniqueReference);

                if (uniqueReferences.Count == 0)
                {
                    return result;
                }
            }

            return result;
        }

        public static Dictionary<string, BuildingModel> BuildingModelDictionary(GISModelFile gISModelFile, IEnumerable<string> references)
        {
            if(gISModelFile == null || references == null)
            {
                return null;
            }

            string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(gISModelFile.Path), System.IO.Path.GetFileNameWithoutExtension(gISModelFile.Path) + "." + Constans.FileExtension.BuildingModelsFile);

            return BuildingModelDictionary(path, references);
        }
    }
}
