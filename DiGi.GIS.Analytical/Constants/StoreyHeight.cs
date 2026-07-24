namespace DiGi.GIS.Analytical.Constants
{
    /// <summary>
    /// Provides the storey height values used when building models are created from 2D buildings.
    /// </summary>
    public static class StoreyHeight
    {
        /// <summary>
        /// The storey height in meters assumed when a building has to be extruded from its footprint.
        /// </summary>
        public const double Default = 3.0;

        /// <summary>
        /// The minimal plausible storey height in meters. A storey height derived from the extents of a building model below this value is treated as unreliable and the model is left unsplit.
        /// </summary>
        public const double Min = 2.4;

        /// <summary>
        /// The maximal plausible storey height in meters. A storey height derived from the extents of a non residential building model is clamped to this value.
        /// </summary>
        public const double Max = 4.0;

        /// <summary>
        /// The rounding step in meters applied to a storey height derived from the extents of a building model.
        /// </summary>
        public const double Precision = 0.1;
    }
}
