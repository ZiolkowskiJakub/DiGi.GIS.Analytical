#### [DiGi\.GIS\.Analytical](DiGi.GIS.Analytical.Overview.md 'DiGi\.GIS\.Analytical\.Overview')

## DiGi\.GIS\.Classes Namespace
### Classes

<a name='DiGi.GIS.Classes.BuildingModelsFile'></a>

## BuildingModelsFile Class

Represents a storage file for building models, providing functionality to manage and retrieve 
unique references associated with building model data\.

```csharp
public class BuildingModelsFile : DiGi.Core.IO.File.Classes.StorageFile<DiGi.Analytical.Building.Classes.BuildingModel>, DiGi.GIS.Interfaces.IGISObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.IO\.File\.Classes\.File](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.file.classes.file 'DiGi\.Core\.IO\.File\.Classes\.File') → [DiGi\.Core\.IO\.File\.Classes\.StorageFile&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.file.classes.storagefile-1 'DiGi\.Core\.IO\.File\.Classes\.StorageFile\`1')[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.file.classes.storagefile-1 'DiGi\.Core\.IO\.File\.Classes\.StorageFile\`1') → BuildingModelsFile

Implements [DiGi\.GIS\.Interfaces\.IGISObject](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.igisobject 'DiGi\.GIS\.Interfaces\.IGISObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.GIS.Classes.BuildingModelsFile.BuildingModelsFile(DiGi.GIS.Classes.BuildingModelsFile)'></a>

## BuildingModelsFile\(BuildingModelsFile\) Constructor

Initializes a new instance of the [BuildingModelsFile](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelsFile 'DiGi\.GIS\.Classes\.BuildingModelsFile') class by copying an existing building models file\.

```csharp
public BuildingModelsFile(DiGi.GIS.Classes.BuildingModelsFile? buildingModelsFile);
```
#### Parameters

<a name='DiGi.GIS.Classes.BuildingModelsFile.BuildingModelsFile(DiGi.GIS.Classes.BuildingModelsFile).buildingModelsFile'></a>

`buildingModelsFile` [BuildingModelsFile](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelsFile 'DiGi\.GIS\.Classes\.BuildingModelsFile')

The source [BuildingModelsFile](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelsFile 'DiGi\.GIS\.Classes\.BuildingModelsFile') to copy\.

<a name='DiGi.GIS.Classes.BuildingModelsFile.BuildingModelsFile(string)'></a>

## BuildingModelsFile\(string\) Constructor

Initializes a new instance of the [BuildingModelsFile](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelsFile 'DiGi\.GIS\.Classes\.BuildingModelsFile') class from a specified file path\.

```csharp
public BuildingModelsFile(string? path);
```
#### Parameters

<a name='DiGi.GIS.Classes.BuildingModelsFile.BuildingModelsFile(string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The system path to the building models file\.

<a name='DiGi.GIS.Classes.BuildingModelsFile.BuildingModelsFile(System.Text.Json.Nodes.JsonObject)'></a>

## BuildingModelsFile\(JsonObject\) Constructor

Initializes a new instance of the [BuildingModelsFile](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelsFile 'DiGi\.GIS\.Classes\.BuildingModelsFile') class from a JSON object\.

```csharp
public BuildingModelsFile(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.Classes.BuildingModelsFile.BuildingModelsFile(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the building models file data\.
### Methods

<a name='DiGi.GIS.Classes.BuildingModelsFile.GetUniqueReference(DiGi.Analytical.Building.Classes.BuildingModel)'></a>

## BuildingModelsFile\.GetUniqueReference\(BuildingModel\) Method

Gets the unique reference for the specified building model by extracting its reference parameter\.

```csharp
public override DiGi.Core.Classes.UniqueReference? GetUniqueReference(DiGi.Analytical.Building.Classes.BuildingModel? buildingModel);
```
#### Parameters

<a name='DiGi.GIS.Classes.BuildingModelsFile.GetUniqueReference(DiGi.Analytical.Building.Classes.BuildingModel).buildingModel'></a>

`buildingModel` [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')

The building model instance to retrieve the reference from\.

#### Returns
[DiGi\.Core\.Classes\.UniqueReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniquereference 'DiGi\.Core\.Classes\.UniqueReference')  
A [DiGi\.Core\.Classes\.UniqueReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniquereference 'DiGi\.Core\.Classes\.UniqueReference') if a valid reference is found; otherwise, `null`\.

<a name='DiGi.GIS.Classes.BuildingModelsFile.GetUniqueReference(string)'></a>

## BuildingModelsFile\.GetUniqueReference\(string\) Method

Creates a unique reference for a building model based on the provided string identifier\.

```csharp
public static DiGi.Core.Classes.UniqueReference? GetUniqueReference(string? reference);
```
#### Parameters

<a name='DiGi.GIS.Classes.BuildingModelsFile.GetUniqueReference(string).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The string representation of the reference\.

#### Returns
[DiGi\.Core\.Classes\.UniqueReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniquereference 'DiGi\.Core\.Classes\.UniqueReference')  
A new [DiGi\.Core\.Classes\.UniqueReference](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.uniquereference 'DiGi\.Core\.Classes\.UniqueReference') instance if the reference is not null; otherwise, `null`\.