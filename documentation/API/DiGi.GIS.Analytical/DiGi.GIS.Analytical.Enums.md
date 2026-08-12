#### [DiGi\.GIS\.Analytical](DiGi.GIS.Analytical.Overview.md 'DiGi\.GIS\.Analytical\.Overview')

## DiGi\.GIS\.Analytical\.Enums Namespace
### Enums

<a name='DiGi.GIS.Analytical.Enums.BuildingModelParameter'></a>

## BuildingModelParameter Enum

Defines the available parameters for a building model\.

```csharp
public enum BuildingModelParameter
```
### Fields

<a name='DiGi.GIS.Analytical.Enums.BuildingModelParameter.Source'></a>

`Source` 0

Gets the data source information associated with the building model\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelParameter.Reference'></a>

`Reference` 1

Gets the reference identifier associated with the building model\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelParameter.Code'></a>

`Code` 2

Gets the administrative area code associated with the building model\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode'></a>

## BuildingModelValidationCode Enum

Identifies a way in which a building model fails validation\.

A model can carry several of these at once, so they are collected into a list rather than reduced to a single verdict - which of them a model carries is what says whether the data, the conversion or the upload is at fault.

```csharp
public enum BuildingModelValidationCode
```
### Fields

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.MissingReference'></a>

`MissingReference` 0

The model carries no reference, so it cannot be traced back to the 2D building it belongs to\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.MissingCode'></a>

`MissingCode` 1

The model carries no administrative area code, so the county it belongs to cannot be resolved from the model itself\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.InvalidComponent'></a>

`InvalidComponent` 2

At least one component sits on a plane whose normal is not finite, which is what the last gate before the database rejects\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.NoSpace'></a>

`NoSpace` 3

The model holds no space at all\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.NoComponent'></a>

`NoComponent` 4

The model holds no component, or one of its spaces is bounded by none\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.NotEnclosed'></a>

`NotEnclosed` 5

At least one space is not enclosed by its components at the requested tolerance\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.NonManifold'></a>

`NonManifold` 6

A space closes, but at least one of its edges is shared by more than two faces, so the shell is not a 2\-manifold surface\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.SpacePointOutsideShell'></a>

`SpacePointOutsideShell` 7

The internal point of a space lies outside the shell that bounds it, so anything classifying by that point resolves to the wrong space or to none\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.SeaLevel'></a>

`SeaLevel` 8

The model sits at an elevation of zero, meaning the terrain elevation was never resolved and the building was placed at sea level\.

<a name='DiGi.GIS.Analytical.Enums.BuildingModelValidationCode.DegenerateExtent'></a>

`DegenerateExtent` 9

The extents of the model are unusable \- no height, or a coordinate that is not finite\.

<a name='DiGi.GIS.Analytical.Enums.BuildingParameter'></a>

## BuildingParameter Enum

Parameters applicable to a Building\.

```csharp
public enum BuildingParameter
```
### Fields

<a name='DiGi.GIS.Analytical.Enums.BuildingParameter.LOD'></a>

`LOD` 0

Level of Detail\.

<a name='DiGi.GIS.Analytical.Enums.BuildingParameter.Year'></a>

`Year` 1

Model year\.

<a name='DiGi.GIS.Analytical.Enums.BuildingParameter.Code'></a>

`Code` 2

Area code\.

<a name='DiGi.GIS.Analytical.Enums.BuildingParameter.Source'></a>

`Source` 3

Source information\.

<a name='DiGi.GIS.Analytical.Enums.LOD'></a>

## LOD Enum

Specifies the Level of Detail \(LOD\) for GIS analytical objects\.

```csharp
public enum LOD
```
### Fields

<a name='DiGi.GIS.Analytical.Enums.LOD.Undefined'></a>

`Undefined` 0

The level of detail is undefined or not specified\.

<a name='DiGi.GIS.Analytical.Enums.LOD.LOD1'></a>

`LOD1` 1

Represents the first level of detail \(LOD1\)\.

<a name='DiGi.GIS.Analytical.Enums.LOD.LOD2'></a>

`LOD2` 2

Represents the second level of detail \(LOD2\)\.