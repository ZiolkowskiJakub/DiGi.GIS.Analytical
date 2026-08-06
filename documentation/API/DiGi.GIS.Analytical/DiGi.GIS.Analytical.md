#### [DiGi\.GIS\.Analytical](DiGi.GIS.Analytical.Overview.md 'DiGi\.GIS\.Analytical\.Overview')

## DiGi\.GIS\.Analytical Namespace
### Classes

<a name='DiGi.GIS.Analytical.Convert'></a>

## Convert Class

```csharp
public static class Convert
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Convert
### Methods

<a name='DiGi.GIS.Analytical.Convert.ToAnalytical(thisDiGi.CityGML.Interfaces.ISurface)'></a>

## Convert\.ToAnalytical\(this ISurface\) Method

Converts a CityGML surface to its corresponding analytical building component based on the surface type\.

```csharp
public static DiGi.Analytical.Building.Interfaces.IComponent? ToAnalytical(this DiGi.CityGML.Interfaces.ISurface? surface);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Convert.ToAnalytical(thisDiGi.CityGML.Interfaces.ISurface).surface'></a>

`surface` [DiGi\.CityGML\.Interfaces\.ISurface](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.interfaces.isurface 'DiGi\.CityGML\.Interfaces\.ISurface')

The surface object to be converted\.

#### Returns
[DiGi\.Analytical\.Building\.Interfaces\.IComponent](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.interfaces.icomponent 'DiGi\.Analytical\.Building\.Interfaces\.IComponent')  
An [DiGi\.Analytical\.Building\.Interfaces\.IComponent](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.interfaces.icomponent 'DiGi\.Analytical\.Building\.Interfaces\.IComponent') representing the analytical version of the surface, or `null` if the input is null, the geometry is missing, or the surface type is not supported for conversion\.

<a name='DiGi.GIS.Analytical.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,DiGi.GIS.Classes.Building2D,double,System.Collections.Generic.IEnumerable_double_)'></a>

## Create\.BuildingModel\(this Building, Building2D, double, IEnumerable\<double\>\) Method

Creates a [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') from a 3D building and refines it with the data carried by the matching 2D building\.

The model is built from the 3D geometry of [building](DiGi.GIS.Analytical.md#DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,DiGi.GIS.Classes.Building2D,double,System.Collections.Generic.IEnumerable_double_).building 'DiGi\.GIS\.Analytical\.Create\.BuildingModel\(this DiGi\.CityGML\.Classes\.Building, DiGi\.GIS\.Classes\.Building2D, double, System\.Collections\.Generic\.IEnumerable\<double\>\)\.building'). When that geometry is missing or cannot be converted, the model is extruded from the footprint of [building2D](DiGi.GIS.Analytical.md#DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,DiGi.GIS.Classes.Building2D,double,System.Collections.Generic.IEnumerable_double_).building2D 'DiGi\.GIS\.Analytical\.Create\.BuildingModel\(this DiGi\.CityGML\.Classes\.Building, DiGi\.GIS\.Classes\.Building2D, double, System\.Collections\.Generic\.IEnumerable\<double\>\)\.building2D') at [Default](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.StoreyHeight.Default 'DiGi\.GIS\.Analytical\.Constants\.StoreyHeight\.Default').

The storey count of [building2D](DiGi.GIS.Analytical.md#DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,DiGi.GIS.Classes.Building2D,double,System.Collections.Generic.IEnumerable_double_).building2D 'DiGi\.GIS\.Analytical\.Create\.BuildingModel\(this DiGi\.CityGML\.Classes\.Building, DiGi\.GIS\.Classes\.Building2D, double, System\.Collections\.Generic\.IEnumerable\<double\>\)\.building2D') is used to cut the model into storeys. The storey height is derived from the extents of the model, rounded down to [Precision](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.StoreyHeight.Precision 'DiGi\.GIS\.Analytical\.Constants\.StoreyHeight\.Precision'), and the cutting planes are measured downwards from the top of the model so that the rounding remainder is left to the lowest storey. Nothing is cut when the derived storey height is below [Min](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.StoreyHeight.Min 'DiGi\.GIS\.Analytical\.Constants\.StoreyHeight\.Min').

A storey height above [Max](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.StoreyHeight.Max 'DiGi\.GIS\.Analytical\.Constants\.StoreyHeight\.Max') is handled by the function of the building. For a non residential building the storey height is clamped to [Max](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.StoreyHeight.Max 'DiGi\.GIS\.Analytical\.Constants\.StoreyHeight\.Max') and the storey count is kept, so the whole remainder is left to the lowest storey. For a residential building the storey count is treated as unreliable instead and recalculated from the extents of the model at [Default](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.StoreyHeight.Default 'DiGi\.GIS\.Analytical\.Constants\.StoreyHeight\.Default'), the storey height being derived again from that count - the resulting model may therefore hold a different number of storeys than [DiGi\.GIS\.Classes\.Building2D\.Storeys](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d.storeys 'DiGi\.GIS\.Classes\.Building2D\.Storeys'). When even the recalculated storey height stays above [Max](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.StoreyHeight.Max 'DiGi\.GIS\.Analytical\.Constants\.StoreyHeight\.Max') the model is returned unsplit.

The building reference ([DiGi\.GIS\.Classes\.GISGuidObject2D\.Reference](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gisguidobject2d.reference 'DiGi\.GIS\.Classes\.GISGuidObject2D\.Reference')) is carried over to [Reference](DiGi.GIS.Analytical.Enums.md#DiGi.GIS.Analytical.Enums.BuildingModelParameter.Reference 'DiGi\.GIS\.Analytical\.Enums\.BuildingModelParameter\.Reference').

The split does not re-host openings - windows and doors stay assigned to the fragment inheriting the identifier of the component they were hosted by, not to the fragment geometrically containing them.

The tolerance defaults to [Coordinate](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.Tolerance.Coordinate 'DiGi\.GIS\.Analytical\.Constants\.Tolerance\.Coordinate') rather than [DiGi\.Core\.Constants\.Tolerance\.Distance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.distance 'DiGi\.Core\.Constants\.Tolerance\.Distance') because the coordinates of the national 3D building model carry two decimal places - at a finer tolerance the storey split leaves the ring assembled on the cutting plane open at the corners and no cut is made. Pass an explicit tolerance when the building comes from a more precise source.

```csharp
public static DiGi.Analytical.Building.Classes.BuildingModel? BuildingModel(this DiGi.CityGML.Classes.Building? building, DiGi.GIS.Classes.Building2D? building2D, double tolerance=0.01, System.Collections.Generic.IEnumerable<double>? candidateTolerances=null);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,DiGi.GIS.Classes.Building2D,double,System.Collections.Generic.IEnumerable_double_).building'></a>

`building` [DiGi\.CityGML\.Classes\.Building](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.classes.building 'DiGi\.CityGML\.Classes\.Building')

The 3D building object\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,DiGi.GIS.Classes.Building2D,double,System.Collections.Generic.IEnumerable_double_).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The 2D building representation providing the storey count, the function and the reference\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,DiGi.GIS.Classes.Building2D,double,System.Collections.Generic.IEnumerable_double_).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,DiGi.GIS.Classes.Building2D,double,System.Collections.Generic.IEnumerable_double_).candidateTolerances'></a>

`candidateTolerances` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

Optional candidate tolerances to attempt if the polyhedron is not closed at the specified tolerance\.

#### Returns
[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')  
A [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') if successful; otherwise, null\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,double)'></a>

## Create\.BuildingModel\(this Building, double\) Method

Creates a [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') from a 3D building object\.

The tolerance defaults to [Coordinate](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.Tolerance.Coordinate 'DiGi\.GIS\.Analytical\.Constants\.Tolerance\.Coordinate') rather than [DiGi\.Core\.Constants\.Tolerance\.Distance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.distance 'DiGi\.Core\.Constants\.Tolerance\.Distance') because the coordinates of the national 3D building model carry two decimal places - joining its boundary surfaces at a finer tolerance leaves the assembled rings open at the corners. Pass an explicit tolerance when the building comes from a more precise source.

```csharp
public static DiGi.Analytical.Building.Classes.BuildingModel? BuildingModel(this DiGi.CityGML.Classes.Building? building, double tolerance=0.01);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,double).building'></a>

`building` [DiGi\.CityGML\.Classes\.Building](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.classes.building 'DiGi\.CityGML\.Classes\.Building')

The 3D building object\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.CityGML.Classes.Building,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

#### Returns
[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')  
A [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') if successful; otherwise, null\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.Geometry.Spatial.Classes.Polyhedron,double)'></a>

## Create\.BuildingModel\(this Polyhedron, double\) Method

Creates a [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') from a polyhedron representation\.

```csharp
public static DiGi.Analytical.Building.Classes.BuildingModel? BuildingModel(this DiGi.Geometry.Spatial.Classes.Polyhedron? polyhedron, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.Geometry.Spatial.Classes.Polyhedron,double).polyhedron'></a>

`polyhedron` [DiGi\.Geometry\.Spatial\.Classes\.Polyhedron](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.polyhedron 'DiGi\.Geometry\.Spatial\.Classes\.Polyhedron')

The polyhedron representing the building geometry\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.Geometry.Spatial.Classes.Polyhedron,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

#### Returns
[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')  
A [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') if successful; otherwise, null\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,ushort,double,double)'></a>

## Create\.BuildingModel\(this IPolygonalFace3D, ushort, double, double\) Method

Creates a [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') by extruding a polygonal face into the specified number of storeys\.

```csharp
public static DiGi.Analytical.Building.Classes.BuildingModel? BuildingModel(this DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D? polygonalFace3D, ushort storeys, double storeyHeight=3.0, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,ushort,double,double).polygonalFace3D'></a>

`polygonalFace3D` [DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.ipolygonalface3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D')

The base polygonal face to extrude\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,ushort,double,double).storeys'></a>

`storeys` [System\.UInt16](https://learn.microsoft.com/en-us/dotnet/api/system.uint16 'System\.UInt16')

The number of storeys to generate\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,ushort,double,double).storeyHeight'></a>

`storeyHeight` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The height of each storey in meters\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,ushort,double,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

#### Returns
[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')  
A [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') if successful; otherwise, null\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.GIS.Classes.Building2D,double,double)'></a>

## Create\.BuildingModel\(this Building2D, double, double\) Method

Creates a [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') from a 2D building representation by extruding it storey by storey\.

The building reference ([DiGi\.GIS\.Classes\.GISGuidObject2D\.Reference](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gisguidobject2d.reference 'DiGi\.GIS\.Classes\.GISGuidObject2D\.Reference')) is carried over to [Reference](DiGi.GIS.Analytical.Enums.md#DiGi.GIS.Analytical.Enums.BuildingModelParameter.Reference 'DiGi\.GIS\.Analytical\.Enums\.BuildingModelParameter\.Reference').

```csharp
public static DiGi.Analytical.Building.Classes.BuildingModel? BuildingModel(this DiGi.GIS.Classes.Building2D? building2D, double storeyHeight=3.0, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.GIS.Classes.Building2D,double,double).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The 2D building representation\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.GIS.Classes.Building2D,double,double).storeyHeight'></a>

`storeyHeight` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The height of a single storey in meters used for the extrusion\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.GIS.Classes.Building2D,double,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

#### Returns
[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')  
A [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') instance if successful; otherwise, null\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_,double)'></a>

## Create\.BuildingModel\(this Building2D, IEnumerable\<CityModel\>, double\) Method

Creates a [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') based on a 2D building and a collection of city models\.

The model is built from the 3D building found in [cityModels](DiGi.GIS.Analytical.md#DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_,double).cityModels 'DiGi\.GIS\.Analytical\.Create\.BuildingModel\(this DiGi\.GIS\.Classes\.Building2D, System\.Collections\.Generic\.IEnumerable\<DiGi\.CityGML\.Classes\.CityModel\>, double\)\.cityModels'), so the tolerance defaults to [Coordinate](DiGi.GIS.Analytical.Constants.md#DiGi.GIS.Analytical.Constants.Tolerance.Coordinate 'DiGi\.GIS\.Analytical\.Constants\.Tolerance\.Coordinate'), the coordinate precision of the national 3D building model. Pass an explicit tolerance when the city models come from a more precise source.

```csharp
public static DiGi.Analytical.Building.Classes.BuildingModel? BuildingModel(this DiGi.GIS.Classes.Building2D? building2D, System.Collections.Generic.IEnumerable<DiGi.CityGML.Classes.CityModel>? cityModels, double tolerance=0.01);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_,double).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The 2D building representation\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_,double).cityModels'></a>

`cityModels` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.CityGML\.Classes\.CityModel](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.classes.citymodel 'DiGi\.CityGML\.Classes\.CityModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of city models used to find the corresponding 3D building\.

<a name='DiGi.GIS.Analytical.Create.BuildingModel(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

#### Returns
[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')  
A [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') if successful; otherwise, null\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisDiGi.GIS.Classes.GISModelFile,string,double)'></a>

## Create\.BuildingModels\(this GISModelFile, string, double\) Method

Creates a list of building models by correlating 2D building data from a GIS model file with corresponding 3D CityGML data found in the specified directory\.

```csharp
public static System.Collections.Generic.List<DiGi.Analytical.Building.Classes.BuildingModel>? BuildingModels(this DiGi.GIS.Classes.GISModelFile? gISModelFile, string directory_CityGML, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisDiGi.GIS.Classes.GISModelFile,string,double).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The source GIS model file containing the building data\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisDiGi.GIS.Classes.GISModelFile,string,double).directory_CityGML'></a>

`directory_CityGML` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The directory path where the CityGML zip files are stored\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisDiGi.GIS.Classes.GISModelFile,string,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance used for geometric projections and matching operations\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') objects if successful; otherwise, null if the input file is invalid or no buildings are found\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.Building_,double)'></a>

## Create\.BuildingModels\(this IEnumerable\<Building2D\>, IEnumerable\<Building\>, double\) Method

Creates a list of building models from a collection of 2D buildings and CityGML buildings, matching them by reference, with fallback logic for buildings that cannot be directly matched\.

Unlike the city model overload the match is a straight reference join of the 2D building reference against the CityGML building reference, so the caller is expected to have resolved any level of detail or year ranking beforehand.

```csharp
public static System.Collections.Generic.List<DiGi.Analytical.Building.Classes.BuildingModel>? BuildingModels(this System.Collections.Generic.IEnumerable<DiGi.GIS.Classes.Building2D>? building2Ds, System.Collections.Generic.IEnumerable<DiGi.CityGML.Classes.Building>? buildings, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.Building_,double).building2Ds'></a>

`building2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The 2D building data to convert\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.Building_,double).buildings'></a>

`buildings` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.CityGML\.Classes\.Building](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.classes.building 'DiGi\.CityGML\.Classes\.Building')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The CityGML buildings used to find corresponding 3D geometry\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.Building_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') objects if successful; otherwise, null\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_,double)'></a>

## Create\.BuildingModels\(this IEnumerable\<Building2D\>, IEnumerable\<CityModel\>, double\) Method

Creates a list of building models from a collection of 2D buildings and city models, with fallback logic for buildings that cannot be directly matched\.

```csharp
public static System.Collections.Generic.List<DiGi.Analytical.Building.Classes.BuildingModel>? BuildingModels(this System.Collections.Generic.IEnumerable<DiGi.GIS.Classes.Building2D>? building2Ds, System.Collections.Generic.IEnumerable<DiGi.CityGML.Classes.CityModel> cityModels, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_,double).building2Ds'></a>

`building2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The 2D building data to convert\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_,double).cityModels'></a>

`cityModels` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.CityGML\.Classes\.CityModel](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.classes.citymodel 'DiGi\.CityGML\.Classes\.CityModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The city models used to find corresponding 3D buildings\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') objects if successful; otherwise, null\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels_Unidentified(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.Building_,double)'></a>

## Create\.BuildingModels\_Unidentified\(this IEnumerable\<Building2D\>, IEnumerable\<Building\>, double\) Method

Creates building models for 2D buildings that could not be matched to a CityGML building directly\.

Each 2D building is first matched spatially - its internal point is tested against the CityGML buildings projected onto the world XY plane - and the remainder are extruded from their footprint at an assumed storey height, with the base elevation interpolated from the nearest CityGML geometry.

```csharp
public static System.Collections.Generic.List<DiGi.Analytical.Building.Classes.BuildingModel>? BuildingModels_Unidentified(this System.Collections.Generic.IEnumerable<DiGi.GIS.Classes.Building2D>? building2Ds, System.Collections.Generic.IEnumerable<DiGi.CityGML.Classes.Building>? buildings, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.BuildingModels_Unidentified(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.Building_,double).building2Ds'></a>

`building2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The unmatched 2D building data to convert\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels_Unidentified(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.Building_,double).buildings'></a>

`buildings` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.CityGML\.Classes\.Building](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.classes.building 'DiGi\.CityGML\.Classes\.Building')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The CityGML buildings used for the spatial match and for the base elevation of the extruded models\.

<a name='DiGi.GIS.Analytical.Create.BuildingModels_Unidentified(thisSystem.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.Building_,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') objects for the 2D buildings that could be resolved; otherwise, null\.

<a name='DiGi.GIS.Analytical.Create.Component(thisDiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,DiGi.Geometry.Spatial.Classes.Polyhedron,double)'></a>

## Create\.Component\(this IPolygonalFace3D, Polyhedron, double\) Method

Creates a building component from a polygonal face 3D, determining whether it is a wall, floor, or roof based on its orientation and spatial relationship to an optional polyhedron\.

```csharp
public static DiGi.Analytical.Building.Interfaces.IComponent? Component(this DiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D? polygonalFace3D, DiGi.Geometry.Spatial.Classes.Polyhedron? polyhedron, double tolerance=1E-06);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Create.Component(thisDiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,DiGi.Geometry.Spatial.Classes.Polyhedron,double).polygonalFace3D'></a>

`polygonalFace3D` [DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.interfaces.ipolygonalface3d 'DiGi\.Geometry\.Spatial\.Interfaces\.IPolygonalFace3D')

The polygonal face 3D to convert into a component\.

<a name='DiGi.GIS.Analytical.Create.Component(thisDiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,DiGi.Geometry.Spatial.Classes.Polyhedron,double).polyhedron'></a>

`polyhedron` [DiGi\.Geometry\.Spatial\.Classes\.Polyhedron](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.polyhedron 'DiGi\.Geometry\.Spatial\.Classes\.Polyhedron')

An optional polyhedron used to determine if the face is a floor or roof based on vertical intersection analysis\.

<a name='DiGi.GIS.Analytical.Create.Component(thisDiGi.Geometry.Spatial.Interfaces.IPolygonalFace3D,DiGi.Geometry.Spatial.Classes.Polyhedron,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance for geometric calculations\.

#### Returns
[DiGi\.Analytical\.Building\.Interfaces\.IComponent](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.interfaces.icomponent 'DiGi\.Analytical\.Building\.Interfaces\.IComponent')  
A building component \([DiGi\.Analytical\.Building\.Interfaces\.IComponent](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.interfaces.icomponent 'DiGi\.Analytical\.Building\.Interfaces\.IComponent')\) such as a wall, floor, or roof; otherwise, `null` if the face's normal cannot be determined\.

<a name='DiGi.GIS.Analytical.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.GIS.Analytical.Query.Building(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_)'></a>

## Query\.Building\(this Building2D, IEnumerable\<CityModel\>\) Method

Retrieves the most appropriate [Building\(this Building2D, IEnumerable&lt;CityModel&gt;\)](DiGi.GIS.Analytical.md#DiGi.GIS.Analytical.Query.Building(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_) 'DiGi\.GIS\.Analytical\.Query\.Building\(this DiGi\.GIS\.Classes\.Building2D, System\.Collections\.Generic\.IEnumerable\<DiGi\.CityGML\.Classes\.CityModel\>\)') from a collection of city models based on a 2D building reference\.
The method prioritizes buildings with LOD2 over LOD1, and among those with the same LOD, it selects the one from the most recent year\.

```csharp
public static DiGi.CityGML.Classes.Building? Building(this DiGi.GIS.Classes.Building2D? building2D, System.Collections.Generic.IEnumerable<DiGi.CityGML.Classes.CityModel>? cityModels);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Query.Building(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The 2D building instance providing the reference for the search\.

<a name='DiGi.GIS.Analytical.Query.Building(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_).cityModels'></a>

`cityModels` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.CityGML\.Classes\.CityModel](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.classes.citymodel 'DiGi\.CityGML\.Classes\.CityModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of city models to be searched for a matching building\.

#### Returns
[DiGi\.CityGML\.Classes\.Building](https://learn.microsoft.com/en-us/dotnet/api/digi.citygml.classes.building 'DiGi\.CityGML\.Classes\.Building')  
The matching [Building\(this Building2D, IEnumerable&lt;CityModel&gt;\)](DiGi.GIS.Analytical.md#DiGi.GIS.Analytical.Query.Building(thisDiGi.GIS.Classes.Building2D,System.Collections.Generic.IEnumerable_DiGi.CityGML.Classes.CityModel_) 'DiGi\.GIS\.Analytical\.Query\.Building\(this DiGi\.GIS\.Classes\.Building2D, System\.Collections\.Generic\.IEnumerable\<DiGi\.CityGML\.Classes\.CityModel\>\)') based on priority rules, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if no match is found or inputs are null\.

<a name='DiGi.GIS.Analytical.Query.BuildingModelDictionary(DiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_)'></a>

## Query\.BuildingModelDictionary\(GISModelFile, IEnumerable\<string\>\) Method

Retrieves a dictionary of building models associated with the specified GIS model file based on the provided references\.

```csharp
public static System.Collections.Generic.Dictionary<string,DiGi.Analytical.Building.Classes.BuildingModel>? BuildingModelDictionary(DiGi.GIS.Classes.GISModelFile? gISModelFile, System.Collections.Generic.IEnumerable<string>? references);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Query.BuildingModelDictionary(DiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file used to determine the location and name of the corresponding building models file\.

<a name='DiGi.GIS.Analytical.Query.BuildingModelDictionary(DiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of reference strings used to identify the building models to retrieve\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
A dictionary mapping unique identifiers to [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') objects, or null if the GIS model file or references are null\.

<a name='DiGi.GIS.Analytical.Query.BuildingModelDictionary(string,System.Collections.Generic.IEnumerable_string_)'></a>

## Query\.BuildingModelDictionary\(string, IEnumerable\<string\>\) Method

Retrieves a dictionary of building models from the specified file path based on the provided references\.

```csharp
public static System.Collections.Generic.Dictionary<string,DiGi.Analytical.Building.Classes.BuildingModel>? BuildingModelDictionary(string? path, System.Collections.Generic.IEnumerable<string>? references);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Query.BuildingModelDictionary(string,System.Collections.Generic.IEnumerable_string_).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The file system path to the building models file\.

<a name='DiGi.GIS.Analytical.Query.BuildingModelDictionary(string,System.Collections.Generic.IEnumerable_string_).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

A collection of reference strings used to identify the building models to retrieve\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
A dictionary mapping unique identifiers to [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') objects, or null if the path is invalid or references are null\.

<a name='DiGi.GIS.Analytical.Query.Horizontal(thisDiGi.Geometry.Spatial.Classes.Vector3D,double)'></a>

## Query\.Horizontal\(this Vector3D, double\) Method

Determines whether the specified 3D vector is horizontal \(perpendicular to the World Z axis\) within a given tolerance\.

```csharp
public static bool Horizontal(this DiGi.Geometry.Spatial.Classes.Vector3D? vector3D, double tolerance=0.0349066);
```
#### Parameters

<a name='DiGi.GIS.Analytical.Query.Horizontal(thisDiGi.Geometry.Spatial.Classes.Vector3D,double).vector3D'></a>

`vector3D` [DiGi\.Geometry\.Spatial\.Classes\.Vector3D](https://learn.microsoft.com/en-us/dotnet/api/digi.geometry.spatial.classes.vector3d 'DiGi\.Geometry\.Spatial\.Classes\.Vector3D')

The 3D vector to evaluate\.

<a name='DiGi.GIS.Analytical.Query.Horizontal(thisDiGi.Geometry.Spatial.Classes.Vector3D,double).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The angular tolerance used to determine if the vector is horizontal\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the vector is horizontal within the specified tolerance; otherwise, `false`\.