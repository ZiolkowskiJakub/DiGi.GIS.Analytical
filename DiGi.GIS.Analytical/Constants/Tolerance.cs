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
    }
}
