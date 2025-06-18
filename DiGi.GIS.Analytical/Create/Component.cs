using DiGi.Analytical.Building.Classes;
using DiGi.Analytical.Building.Interfaces;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using System.Collections.Generic;

namespace DiGi.GIS.Analytical
{
    public static partial class Create
    {
        public static IComponent Component(this IPolygonalFace3D polygonalFace3D, Polyhedron polyhedron, double tolerance = Core.Constans.Tolerance.Distance)
        {
            Vector3D normal = polygonalFace3D?.Plane?.Normal;
            if (normal == null)
            {
                return null;
            }

            if(Query.Horizontal(normal))
            {
                return new SurfaceWall(polygonalFace3D);
            }

            if (polyhedron != null)
            {
                Point3D point3D = polygonalFace3D.GetInternalPoint(tolerance);

                IntersectionResult3D intersectionResult3D = Geometry.Spatial.Create.IntersectionResult3D(polyhedron, new Line3D(point3D, Geometry.Spatial.Constans.Vector3D.WorldZ));
                if(intersectionResult3D != null && intersectionResult3D.Intersect)
                {
                    List<Point3D> point3Ds = intersectionResult3D.GetGeometry3Ds<Point3D>();
                    if(point3Ds != null || point3Ds.Count == 0)
                    {
                        point3Ds.RemoveAll(x => point3D.Z >= x.Z || point3D.AlmostEquals(x, tolerance));
                    }

                    if(point3Ds == null || point3Ds.Count == 0)
                    {
                        return new SurfaceRoof(polygonalFace3D);
                    }
                    else
                    {
                        return new FaceFloor(polygonalFace3D);
                    }

                }
            }

            if(Geometry.Spatial.Constans.Vector3D.WorldZ.GetInversed().Angle(normal) < Core.Constans.Tolerance.Angle)
            {
                return new FaceFloor(polygonalFace3D);
            }

            return new SurfaceRoof(polygonalFace3D);
        }

    }
}
