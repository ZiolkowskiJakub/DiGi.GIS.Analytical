using DiGi.Geometry.Spatial.Classes;

namespace DiGi.GIS.Analytical
{
    public static partial class Query
    {
        public static bool Horizontal(this Vector3D? vector3D, double tolerance = Core.Constants.Tolerance.Angle)
        {
            if (vector3D == null)
            {
                return false;
            }

            if (vector3D?.Unit is not Vector3D unit)
            {
                return false;
            }

            // Compute angle between vector and Up (0, 0, 1)
            double angle = System.Math.Acos(System.Math.Abs(unit.DotProduct(Geometry.Spatial.Constants.Vector3D.WorldZ)));

            // 90 degrees in radians = π/2
            return System.Math.Abs(angle - System.Math.PI / 2) <= tolerance;
        }
    }
}