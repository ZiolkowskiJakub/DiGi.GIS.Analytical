using DiGi.Analytical.Building.Classes;
using DiGi.Analytical.Building.Interfaces;
using DiGi.CityGML;
using DiGi.CityGML.Classes;
using DiGi.CityGML.Interfaces;
using DiGi.Geometry.Spatial.Classes;
using DiGi.Geometry.Spatial.Interfaces;
using DiGi.GIS.Analytical.Enums;
using DiGi.GIS.Classes;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.Analytical
{
    public static partial class Create
    {
        public static BuildingModel BuildingModel(this Building2D building2D, IEnumerable<CityModel> cityModels, double tolerance = Core.Constans.Tolerance.Distance)
        {
            if (building2D == null)
            {
                return null;
            }

            Building building = Query.Building(building2D, cityModels);
            if (building == null)
            {
                return null;
            }

            BuildingModel result = BuildingModel(building, tolerance);

            return result;
        }

        public static BuildingModel BuildingModel(this Building building, double tolerance = Core.Constans.Tolerance.Distance)
        {
            IEnumerable<ISurface> surfaces = building?.Surfaces;
            if (surfaces == null || surfaces.Count() == 0)
            {
                return null;
            }

            Polyhedron polyhedron = building.Polyhedron();

            BuildingModel result = new BuildingModel();
            LOD lOD = LOD.LOD2;

            List<IComponent> components = new List<IComponent>();
            foreach (ISurface surface in surfaces)
            {
                IComponent component = surface.ToAnalytical();
                if(component == null)
                {
                    component = Component(surface.Geometry, polyhedron, tolerance);
                    if(component == null)
                    {
                        continue;
                    }

                    lOD = LOD.LOD1;
                }

                if(result.Update(component))
                {
                    components.Add(component);
                }
            }

            Space space = new Space(polyhedron.GetInternalPoint(), building.UniqueId);
            result.Update(space);
            foreach(IComponent component in components)
            {
                result.Assign(component, space);
            }

            result.SetValue(BuildingModelParameter.LOD, lOD, new Core.Parameter.Classes.SetValueSettings() { TryConvert = true, CheckAccessType = false });

            return result;
        }

        public static BuildingModel BuildingModel(this Polyhedron polyhedron, double tolerance = Core.Constans.Tolerance.Distance)
        {
            IEnumerable<IPolygonalFace3D> polygonalFace3Ds = polyhedron?.PolygonalFaces;
            if (polygonalFace3Ds == null || polygonalFace3Ds.Count() == 0)
            {
                return null;
            }

            BuildingModel result = new BuildingModel();
            List<IComponent> components = new List<IComponent>();
            foreach (IPolygonalFace3D polygonalFace3D in polygonalFace3Ds)
            {
                IComponent component = Component(polygonalFace3D, polyhedron, tolerance);
                if (component == null)
                {
                    continue;
                }

                if(result.Update(component))
                {
                    components.Add(component);
                }
            }

            Space space = new Space(polyhedron.GetInternalPoint(), "Building");
            result.Update(space);
            foreach (IComponent component in components)
            {
                result.Assign(component, space);
            }

            result.SetValue(BuildingModelParameter.LOD, LOD.Undefined, new Core.Parameter.Classes.SetValueSettings() { TryConvert = true, CheckAccessType = false });

            return result;
        }

    }
}
