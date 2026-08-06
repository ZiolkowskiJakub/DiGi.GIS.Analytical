using DiGi.Geometry.Spatial.Classes;

namespace DiGi.GIS.Analytical
{
    public static partial class Query
    {
        /// <summary>
        /// Reads the elevation carried by a point fetched from the terrain service.
        /// <para>A point that was never fetched, or one whose elevation the service reported as not a number, falls back to zero - the terrain service may not decide whether a building is created, so a model placed at sea level is preferred over no model at all.</para>
        /// </summary>
        /// <param name="point3D">The point returned by the terrain service.</param>
        /// <returns>The elevation of the point, or zero when it carries none.</returns>
        public static double Elevation(this Point3D? point3D)
        {
            if (point3D is null || double.IsNaN(point3D.Z))
            {
                return 0;
            }

            return point3D.Z;
        }
    }
}
