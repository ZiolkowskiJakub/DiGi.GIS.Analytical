using DiGi.Analytical.Building.Classes;
using DiGi.Analytical.Building.Interfaces;
using DiGi.CityGML.Classes;
using DiGi.CityGML.Interfaces;
using DiGi.Geometry.Spatial.Interfaces;

namespace DiGi.GIS.Analytical
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a CityGML surface to its corresponding analytical building component based on the surface type.
        /// </summary>
        /// <param name="surface">The surface object to be converted.</param>
        /// <returns>An <see cref="IComponent"/> representing the analytical version of the surface, or <c>null</c> if the input is null, the geometry is missing, or the surface type is not supported for conversion.</returns>
        public static IComponent? ToAnalytical(this ISurface? surface)
        {
            if (surface == null)
            {
                return null;
            }

            IPolygonalFace3D? polygonalFace3D = surface.Geometry;
            if (polygonalFace3D == null)
            {
                return null;
            }

            if (surface is WallSurface)
            {
                return new SurfaceWall(polygonalFace3D);
            }

            if (surface is RoofSurface)
            {
                return new SurfaceRoof(polygonalFace3D);
            }

            if (surface is GroundSurface)
            {
                return new FaceFloor(polygonalFace3D);
            }

            return null;
        }
    }
}