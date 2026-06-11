namespace DiGi.GIS.Analytical.Constants
{
    /// <summary>
    /// Provides a set of predefined file filters used for filtering files within the GIS Analytical module.
    /// </summary>
    public static class FileFilter
    {
        /// <summary>
        /// Gets the file filter associated with building models files.
        /// </summary>
        public static Core.IO.Classes.FileFilter BuildingModelsFile { get; } = Core.IO.Create.FileFilter(FileTypeName.BuildingModelsFile, FileExtension.BuildingModelsFile);
    }
}