using DiGi.Analytical.Building.Classes;
using DiGi.Analytical.Building.Interfaces;
using DiGi.CityGML.Classes;
using DiGi.CityGML.Interfaces;
using DiGi.Geometry.Spatial.Interfaces;

namespace DiGi.GIS.Analytical
{
    public static partial class Convert
    {
        public static IComponent ToAnalytical(this ISurface surface)
        {
            if(surface == null)
            {
                return null;
            }

            IPolygonalFace3D polygonalFace3D = surface.Geometry;
            if(polygonalFace3D == null)
            {
                return null;
            }

            if(surface is WallSurface)
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
