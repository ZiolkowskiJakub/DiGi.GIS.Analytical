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
        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> based on a 2D building and a collection of city models.
        /// </summary>
        /// <param name="building2D">The 2D building representation.</param>
        /// <param name="cityModels">A collection of city models used to find the corresponding 3D building.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building2D? building2D, IEnumerable<CityModel>? cityModels, double tolerance = Core.Constants.Tolerance.Distance)
        {
            if (building2D == null)
            {
                return null;
            }

            Building? building = Query.Building(building2D, cityModels);
            if (building == null)
            {
                return null;
            }

            BuildingModel? result = BuildingModel(building, tolerance);

            return result;
        }

        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> from a 3D building object.
        /// </summary>
        /// <param name="building">The 3D building object.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Building? building, double tolerance = Core.Constants.Tolerance.Distance)
        {
            IEnumerable<ISurface>? surfaces = building?.Surfaces;
            if (surfaces == null || surfaces.Count() == 0)
            {
                return null;
            }

            Polyhedron? polyhedron = building.Polyhedron();

            BuildingModel result = new();
            LOD lOD = LOD.LOD2;

            List<IComponent> components = [];
            foreach (ISurface surface in surfaces)
            {
                IComponent? component = surface.ToAnalytical();
                if (component == null)
                {
                    component = Component(surface?.Geometry, polyhedron, tolerance);
                    if (component == null)
                    {
                        continue;
                    }

                    lOD = LOD.LOD1;
                }

                if (result.Update(component))
                {
                    components.Add(component);
                }
            }

            Space space = new(polyhedron?.GetInternalPoint(), building?.UniqueId);
            result.Update(space);
            foreach (IComponent component in components)
            {
                result.Assign(component, space);
            }

            result.SetValue(BuildingModelParameter.LOD, lOD, new Core.Parameter.Classes.SetValueSettings() { TryConvert = true, CheckAccessType = false });

            return result;
        }

        /// <summary>
        /// Creates a <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> from a polyhedron representation.
        /// </summary>
        /// <param name="polyhedron">The polyhedron representing the building geometry.</param>
        /// <param name="tolerance">The distance tolerance for geometric calculations.</param>
        /// <returns>A <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/> if successful; otherwise, null.</returns>
        public static BuildingModel? BuildingModel(this Polyhedron? polyhedron, double tolerance = Core.Constants.Tolerance.Distance)
        {
            IEnumerable<IPolygonalFace3D>? polygonalFace3Ds = polyhedron?.PolygonalFaces;
            if (polygonalFace3Ds == null || polygonalFace3Ds.Count() == 0)
            {
                return null;
            }

            BuildingModel result = new();
            List<IComponent> components = [];
            foreach (IPolygonalFace3D polygonalFace3D in polygonalFace3Ds)
            {
                IComponent? component = Component(polygonalFace3D, polyhedron, tolerance);
                if (component == null)
                {
                    continue;
                }

                if (result.Update(component))
                {
                    components.Add(component);
                }
            }

            Space space = new(polyhedron?.GetInternalPoint(), "Building");
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