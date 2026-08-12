namespace DiGi.GIS.Analytical.Constants
{
    /// <summary>
    /// Provides the tolerance values matching the precision of the source data the building models are created from.
    /// </summary>
    public static class Tolerance
    {
        /// <summary>
        /// The distance tolerance in meters matching the coordinate precision of the national 3D building model, whose coordinates are written with two decimal places.
        /// <para>Two vertices meant to coincide can therefore lie up to a centimetre apart, which is four orders of magnitude above <see cref="Core.Constants.Tolerance.Distance"/> and one above <see cref="Core.Constants.Tolerance.MacroDistance"/>. Geometric operations joining the boundary surfaces of such a building - cutting a shell into storeys above all - have to be given this value, otherwise the rings they assemble stay open at the corners.</para>
        /// </summary>
        public const double Coordinate = 0.01;

        /// <summary>
        /// The distance tolerance in meters at which the shell of a space built from the national 3D building model is required to close.
        /// <para>It is five times <see cref="Coordinate"/>, and that margin is not slack: each boundary surface is projected onto its own best fit plane, so a source ring that is not exactly planar leaves a shared vertex in a slightly different place on each of the faces meeting at it, and the gap grows with the non-planarity of the ring rather than with the coordinate precision alone. Buildings detailed enough to carry rings out of plane by more than a centimetre are the reason this is the acceptance value rather than <see cref="Coordinate"/>.</para>
        /// <para>A shell needing more than this is reported rather than accepted - beyond a few centimetres the welding stops distinguishing vertices that were genuinely meant to coincide from ones that were not.</para>
        /// </summary>
        public const double Enclosure = 0.05;
    }
}
