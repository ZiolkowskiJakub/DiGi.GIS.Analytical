using System.ComponentModel;

namespace DiGi.GIS.Analytical.Enums
{
    /// <summary>
    /// Identifies a way in which a building model fails validation.
    /// <para>A model can carry several of these at once, so they are collected into a list rather than reduced to a single verdict - which of them a model carries is what says whether the data, the conversion or the upload is at fault.</para>
    /// </summary>
    [Description("Building Model Validation Codes")]
    public enum BuildingModelValidationCode
    {
        /// <summary>
        /// The model carries no reference, so it cannot be traced back to the 2D building it belongs to.
        /// </summary>
        [Description("Missing reference")] MissingReference,

        /// <summary>
        /// The model carries no administrative area code, so the county it belongs to cannot be resolved from the model itself.
        /// </summary>
        [Description("Missing code")] MissingCode,

        /// <summary>
        /// At least one component sits on a plane whose normal is not finite, which is what the last gate before the database rejects.
        /// </summary>
        [Description("Invalid component")] InvalidComponent,

        /// <summary>
        /// The model holds no space at all.
        /// </summary>
        [Description("No space")] NoSpace,

        /// <summary>
        /// The model holds no component, or one of its spaces is bounded by none.
        /// </summary>
        [Description("No component")] NoComponent,

        /// <summary>
        /// At least one space is not enclosed by its components at the requested tolerance.
        /// </summary>
        [Description("Not enclosed")] NotEnclosed,

        /// <summary>
        /// A space closes, but at least one of its edges is shared by more than two faces, so the shell is not a 2-manifold surface.
        /// </summary>
        [Description("Non manifold")] NonManifold,

        /// <summary>
        /// The internal point of a space lies outside the shell that bounds it, so anything classifying by that point resolves to the wrong space or to none.
        /// </summary>
        [Description("Space point outside shell")] SpacePointOutsideShell,

        /// <summary>
        /// The model sits at an elevation of zero, meaning the terrain elevation was never resolved and the building was placed at sea level.
        /// </summary>
        [Description("Sea level")] SeaLevel,

        /// <summary>
        /// The extents of the model are unusable - no height, or a coordinate that is not finite.
        /// </summary>
        [Description("Degenerate extent")] DegenerateExtent,
    }
}
