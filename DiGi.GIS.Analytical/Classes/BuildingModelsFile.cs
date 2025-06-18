using DiGi.Analytical.Building.Classes;
using DiGi.Core.Classes;
using DiGi.GIS.Analytical.Enums;
using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.Classes
{
    public class BuildingModelsFile : Core.IO.File.Classes.StorageFile<BuildingModel>, IGISObject
    {
        public override UniqueReference GetUniqueReference(BuildingModel buildingModel)
        {
            return GetUniqueReference(buildingModel.GetValue<string>(BuildingModelParameter.Reference, new Core.Parameter.Classes.GetValueSettings(true, false)));
        }

        public static UniqueReference GetUniqueReference(string reference)
        {
            if(reference == null)
            {
                return null;
            }

            return new UniqueIdReference(typeof(BuildingModel), reference);
        }

        public BuildingModelsFile(BuildingModelsFile buildingModelsFile)
            : base(buildingModelsFile)
        {

        }

        public BuildingModelsFile(JsonObject jsonObject)
            :base(jsonObject)
        {

        }

        public BuildingModelsFile(string path)
            : base(path) 
        {
            
        }
    }
}