#### [DiGi\.GIS\.Analytical](DiGi.GIS.Analytical.Overview.md 'DiGi\.GIS\.Analytical\.Overview')

## DiGi\.GIS\.Analytical\.Constants Namespace
### Classes

<a name='DiGi.GIS.Analytical.Constants.FileExtension'></a>

## FileExtension Class

Provides constant values for supported file extensions used across the application\.

```csharp
public static class FileExtension
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → FileExtension
### Fields

<a name='DiGi.GIS.Analytical.Constants.FileExtension.BuildingModelsFile'></a>

## FileExtension\.BuildingModelsFile Field

The file extension associated with building models files \(\.bmsf\)\.

```csharp
public const string BuildingModelsFile = "bmsf";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.Analytical.Constants.FileFilter'></a>

## FileFilter Class

Provides a set of predefined file filters used for filtering files within the GIS Analytical module\.

```csharp
public static class FileFilter
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → FileFilter
### Properties

<a name='DiGi.GIS.Analytical.Constants.FileFilter.BuildingModelsFile'></a>

## FileFilter\.BuildingModelsFile Property

Gets the file filter associated with building models files\.

```csharp
public static DiGi.Core.IO.Classes.FileFilter BuildingModelsFile { get; }
```

#### Property Value
[DiGi\.Core\.IO\.Classes\.FileFilter](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.classes.filefilter 'DiGi\.Core\.IO\.Classes\.FileFilter')

<a name='DiGi.GIS.Analytical.Constants.FileTypeName'></a>

## FileTypeName Class

Provides constant definitions for file type names used across the GIS analytical system\.

```csharp
public static class FileTypeName
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → FileTypeName
### Fields

<a name='DiGi.GIS.Analytical.Constants.FileTypeName.BuildingModelsFile'></a>

## FileTypeName\.BuildingModelsFile Field

The display name or identifier for building models files\.

```csharp
public const string BuildingModelsFile = "Building Models File";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.Analytical.Constants.StoreyHeight'></a>

## StoreyHeight Class

Provides the storey height values used when building models are created from 2D buildings\.

```csharp
public static class StoreyHeight
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → StoreyHeight
### Fields

<a name='DiGi.GIS.Analytical.Constants.StoreyHeight.Default'></a>

## StoreyHeight\.Default Field

The storey height in meters assumed when a building has to be extruded from its footprint\.

```csharp
public const double Default = 3;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.Analytical.Constants.StoreyHeight.Max'></a>

## StoreyHeight\.Max Field

The maximal plausible storey height in meters\. A storey height derived from the extents of a non residential building model is clamped to this value\.

```csharp
public const double Max = 4;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.Analytical.Constants.StoreyHeight.Min'></a>

## StoreyHeight\.Min Field

The minimal plausible storey height in meters\. A storey height derived from the extents of a building model below this value is treated as unreliable and the model is left unsplit\.

```csharp
public const double Min = 2.4;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.Analytical.Constants.StoreyHeight.Precision'></a>

## StoreyHeight\.Precision Field

The rounding step in meters applied to a storey height derived from the extents of a building model\.

```csharp
public const double Precision = 0.1;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.Analytical.Constants.Tolerance'></a>

## Tolerance Class

Provides the tolerance values matching the precision of the source data the building models are created from\.

```csharp
public static class Tolerance
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Tolerance
### Fields

<a name='DiGi.GIS.Analytical.Constants.Tolerance.Coordinate'></a>

## Tolerance\.Coordinate Field

The distance tolerance in meters matching the coordinate precision of the national 3D building model, whose coordinates are written with two decimal places\.

Two vertices meant to coincide can therefore lie up to a centimetre apart, which is four orders of magnitude above [DiGi\.Core\.Constants\.Tolerance\.Distance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.distance 'DiGi\.Core\.Constants\.Tolerance\.Distance') and one above [DiGi\.Core\.Constants\.Tolerance\.MacroDistance](https://learn.microsoft.com/en-us/dotnet/api/digi.core.constants.tolerance.macrodistance 'DiGi\.Core\.Constants\.Tolerance\.MacroDistance'). Geometric operations joining the boundary surfaces of such a building - cutting a shell into storeys above all - have to be given this value, otherwise the rings they assemble stay open at the corners.

```csharp
public const double Coordinate = 0.01;
```

#### Field Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')