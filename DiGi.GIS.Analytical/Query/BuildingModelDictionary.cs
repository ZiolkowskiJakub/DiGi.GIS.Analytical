using DiGi.Analytical.Building.Classes;
using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.Analytical
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves a dictionary of building models from the specified file path based on the provided references.
        /// </summary>
        /// <param name="path">The file system path to the building models file.</param>
        /// <param name="references">A collection of reference strings used to identify the building models to retrieve.</param>
        /// <returns>A dictionary mapping unique identifiers to <see cref="BuildingModel"/> objects, or null if the path is invalid or references are null.</returns>
        public static Dictionary<string, BuildingModel>? BuildingModelDictionary(string? path, IEnumerable<string>? references)
        {
            if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path) || references == null)
            {
                return null;
            }

            HashSet<UniqueReference> uniqueReferences = [];
            foreach (string reference in references)
            {
                UniqueReference? uniqueReference = BuildingModelsFile.GetUniqueReference(reference);
                if (uniqueReference is null)
                {
                    continue;
                }

                uniqueReferences.Add(uniqueReference);
            }

            Dictionary<string, BuildingModel> result = [];

            if (uniqueReferences.Count == 0)
            {
                return result;
            }

            List<BuildingModel>? buildingModels = null;
            using (BuildingModelsFile buildingModelsFile = new(path))
            {
                buildingModelsFile.Open();

                buildingModels = Core.Query.FilterNulls(buildingModelsFile.GetValues<BuildingModel>(uniqueReferences));
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

                if (uniqueReference.UniqueId is not string uniqueId)
                {
                    continue;
                }

                result[uniqueId] = buildingModels[i];
                uniqueReferences.Remove(uniqueReference);

                if (uniqueReferences.Count == 0)
                {
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Retrieves a dictionary of building models associated with the specified GIS model file based on the provided references.
        /// </summary>
        /// <param name="gISModelFile">The GIS model file used to determine the location and name of the corresponding building models file.</param>
        /// <param name="references">A collection of reference strings used to identify the building models to retrieve.</param>
        /// <returns>A dictionary mapping unique identifiers to <see cref="BuildingModel"/> objects, or null if the GIS model file or references are null.</returns>
        public static Dictionary<string, BuildingModel>? BuildingModelDictionary(GISModelFile? gISModelFile, IEnumerable<string>? references)
        {
            if (gISModelFile == null || references == null)
            {
                return null;
            }

            string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(gISModelFile.Path), System.IO.Path.GetFileNameWithoutExtension(gISModelFile.Path) + "." + Constants.FileExtension.BuildingModelsFile);

            return BuildingModelDictionary(path, references);
        }
    }
}