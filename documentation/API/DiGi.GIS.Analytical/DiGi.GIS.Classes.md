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

<a name='DiGi.GIS.Classes.BuildingModelValidationResult'></a>

## BuildingModelValidationResult Class

Holds the outcome of validating a single [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')\.

The counts and extents are kept next to the validation codes on purpose: a model failing on enclosure is only actionable once it is known how many spaces it holds and how far off the closing tolerance it was, which is what tells a shell that never had a boundary apart from one whose source geometry is merely imprecise.

```csharp
public class BuildingModelValidationResult : DiGi.Core.Classes.SerializableObject, DiGi.GIS.Interfaces.IGISSerializableObject, DiGi.GIS.Interfaces.IGISObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → BuildingModelValidationResult

Implements [DiGi\.GIS\.Interfaces\.IGISSerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.igisserializableobject 'DiGi\.GIS\.Interfaces\.IGISSerializableObject'), [DiGi\.GIS\.Interfaces\.IGISObject](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.interfaces.igisobject 'DiGi\.GIS\.Interfaces\.IGISObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')
### Constructors

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(DiGi.GIS.Classes.BuildingModelValidationResult)'></a>

## BuildingModelValidationResult\(BuildingModelValidationResult\) Constructor

Initializes a new instance of the [BuildingModelValidationResult](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelValidationResult 'DiGi\.GIS\.Classes\.BuildingModelValidationResult') class by copying an existing one\.

```csharp
public BuildingModelValidationResult(DiGi.GIS.Classes.BuildingModelValidationResult? buildingModelValidationResult);
```
#### Parameters

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(DiGi.GIS.Classes.BuildingModelValidationResult).buildingModelValidationResult'></a>

`buildingModelValidationResult` [BuildingModelValidationResult](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelValidationResult 'DiGi\.GIS\.Classes\.BuildingModelValidationResult')

The source result to copy data from\. If null, a default instance is initialized\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_)'></a>

## BuildingModelValidationResult\(string, string, double, int, int, int, int, double, double, double, IEnumerable\<BuildingModelValidationCode\>\) Constructor

Initializes a new instance of the [BuildingModelValidationResult](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelValidationResult 'DiGi\.GIS\.Classes\.BuildingModelValidationResult') class\.

```csharp
public BuildingModelValidationResult(string? reference, string? code, double tolerance, int spaceCount, int componentCount, int shellCount, int enclosedShellCount, double minEnclosingTolerance, double minZ, double maxZ, System.Collections.Generic.IEnumerable<DiGi.GIS.Analytical.Enums.BuildingModelValidationCode>? validationCodes);
```
#### Parameters

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).reference'></a>

`reference` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The reference of the 2D building the validated model belongs to\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).code'></a>

`code` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The administrative area code the validated model belongs to\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).tolerance'></a>

`tolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The distance tolerance the enclosure was required to hold at\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).spaceCount'></a>

`spaceCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of spaces held by the model\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).componentCount'></a>

`componentCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of components held by the model\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).shellCount'></a>

`shellCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of shells that could be assembled from those spaces\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).enclosedShellCount'></a>

`enclosedShellCount` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of those shells closing at [tolerance](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).tolerance 'DiGi\.GIS\.Classes\.BuildingModelValidationResult\.BuildingModelValidationResult\(string, string, double, int, int, int, int, double, double, double, System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.Analytical\.Enums\.BuildingModelValidationCode\>\)\.tolerance')\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).minEnclosingTolerance'></a>

`minEnclosingTolerance` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The smallest tolerance at which every shell of the model closes, or not a number when at least one of them never closes\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).minZ'></a>

`minZ` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The lowest elevation of the model\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).maxZ'></a>

`maxZ` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The highest elevation of the model\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(string,string,double,int,int,int,int,double,double,double,System.Collections.Generic.IEnumerable_DiGi.GIS.Analytical.Enums.BuildingModelValidationCode_).validationCodes'></a>

`validationCodes` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[BuildingModelValidationCode](DiGi.GIS.Analytical.Enums.md#DiGi.GIS.Analytical.Enums.BuildingModelValidationCode 'DiGi\.GIS\.Analytical\.Enums\.BuildingModelValidationCode')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The ways in which the model failed validation\. An empty or null collection means it passed\.

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(System.Text.Json.Nodes.JsonObject)'></a>

## BuildingModelValidationResult\(JsonObject\) Constructor

Initializes a new instance of the [BuildingModelValidationResult](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelValidationResult 'DiGi\.GIS\.Classes\.BuildingModelValidationResult') class using the specified [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public BuildingModelValidationResult(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.BuildingModelValidationResult(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject') containing the result data, or null if no data is provided\.
### Properties

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.Code'></a>

## BuildingModelValidationResult\.Code Property

Gets the administrative area code the validated model belongs to\.

```csharp
public string? Code { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.ComponentCount'></a>

## BuildingModelValidationResult\.ComponentCount Property

Gets the number of components held by the model\.

```csharp
public int ComponentCount { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.EnclosedShellCount'></a>

## BuildingModelValidationResult\.EnclosedShellCount Property

Gets the number of shells closing at [Tolerance](DiGi.GIS.Classes.md#DiGi.GIS.Classes.BuildingModelValidationResult.Tolerance 'DiGi\.GIS\.Classes\.BuildingModelValidationResult\.Tolerance')\.

```csharp
public int EnclosedShellCount { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.IsValid'></a>

## BuildingModelValidationResult\.IsValid Property

Gets a value indicating whether the model passed every check\.

```csharp
public bool IsValid { get; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.MaxZ'></a>

## BuildingModelValidationResult\.MaxZ Property

Gets the highest elevation of the model\.

```csharp
public double MaxZ { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.MinEnclosingTolerance'></a>

## BuildingModelValidationResult\.MinEnclosingTolerance Property

Gets the smallest tolerance at which every shell of the model closes, or not a number when at least one of them never closes\.

```csharp
public double MinEnclosingTolerance { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.MinZ'></a>

## BuildingModelValidationResult\.MinZ Property

Gets the lowest elevation of the model\.

```csharp
public double MinZ { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.Reference'></a>

## BuildingModelValidationResult\.Reference Property

Gets the reference of the 2D building the validated model belongs to\.

```csharp
public string? Reference { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.ShellCount'></a>

## BuildingModelValidationResult\.ShellCount Property

Gets the number of shells that could be assembled from the spaces of the model\.

```csharp
public int ShellCount { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.SpaceCount'></a>

## BuildingModelValidationResult\.SpaceCount Property

Gets the number of spaces held by the model\.

```csharp
public int SpaceCount { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.Tolerance'></a>

## BuildingModelValidationResult\.Tolerance Property

Gets the distance tolerance the enclosure was required to hold at\.

```csharp
public double Tolerance { get; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.Classes.BuildingModelValidationResult.ValidationCodes'></a>

## BuildingModelValidationResult\.ValidationCodes Property

Gets the ways in which the model failed validation\. An empty list means it passed\.

```csharp
public System.Collections.Generic.List<DiGi.GIS.Analytical.Enums.BuildingModelValidationCode>? ValidationCodes { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[BuildingModelValidationCode](DiGi.GIS.Analytical.Enums.md#DiGi.GIS.Analytical.Enums.BuildingModelValidationCode 'DiGi\.GIS\.Analytical\.Enums\.BuildingModelValidationCode')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')