namespace DiGi.GIS.Analytical.Constants
{
    public static class FileFilter
    {
        public static Core.IO.Classes.FileFilter BuildingModelsFile { get; } = Core.IO.Create.FileFilter(FileTypeName.BuildingModelsFile, FileExtension.BuildingModelsFile);
    }
}