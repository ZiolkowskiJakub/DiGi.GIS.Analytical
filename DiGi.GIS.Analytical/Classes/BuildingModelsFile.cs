using DiGi.Analytical.Building.Classes;
using DiGi.Core.Classes;
using DiGi.GIS.Analytical.Enums;
using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.Classes
{
    /// <summary>
    /// Represents a storage file for building models, providing functionality to manage and retrieve 
    /// unique references associated with building model data.
    /// </summary>
    public class BuildingModelsFile : Core.IO.File.Classes.StorageFile<BuildingModel>, IGISObject
    {
        /// <summary>
        /// Gets the unique reference for the specified building model by extracting its reference parameter.
        /// </summary>
        /// <param name="buildingModel">The building model instance to retrieve the reference from.</param>
        /// <returns>A <see cref="UniqueReference"/> if a valid reference is found; otherwise, <c>null</c>.</returns>
        public override UniqueReference? GetUniqueReference(BuildingModel? buildingModel)
        {
            return GetUniqueReference(buildingModel?.GetValue<string>(BuildingModelParameter.Reference, new Core.Parameter.Classes.GetValueSettings(true, false)));
        }

        /// <summary>
        /// Creates a unique reference for a building model based on the provided string identifier.
        /// </summary>
        /// <param name="reference">The string representation of the reference.</param>
        /// <returns>A new <see cref="UniqueReference"/> instance if the reference is not null; otherwise, <c>null</c>.</returns>
        public static UniqueReference? GetUniqueReference(string? reference)
        {
            if (reference == null)
            {
                return null;
            }

            return new UniqueIdReference(typeof(BuildingModel), reference);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingModelsFile"/> class by copying an existing building models file.
        /// </summary>
        /// <param name="buildingModelsFile">The source <see cref="BuildingModelsFile"/> to copy.</param>
        public BuildingModelsFile(BuildingModelsFile? buildingModelsFile)
            : base(buildingModelsFile)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingModelsFile"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the building models file data.</param>
        public BuildingModelsFile(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingModelsFile"/> class from a specified file path.
        /// </summary>
        /// <param name="path">The system path to the building models file.</param>
        public BuildingModelsFile(string? path)
            : base(path)
        {
        }
    }
}