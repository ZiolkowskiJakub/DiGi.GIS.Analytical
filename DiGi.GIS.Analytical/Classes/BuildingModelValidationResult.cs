using DiGi.Core.Classes;
using DiGi.GIS.Analytical.Enums;
using DiGi.GIS.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Classes
{
    /// <summary>
    /// Holds the outcome of validating a single <see cref="DiGi.Analytical.Building.Classes.BuildingModel"/>.
    /// <para>The counts and extents are kept next to the validation codes on purpose: a model failing on enclosure is only actionable once it is known how many spaces it holds and how far off the closing tolerance it was, which is what tells a shell that never had a boundary apart from one whose source geometry is merely imprecise.</para>
    /// </summary>
    public class BuildingModelValidationResult : SerializableObject, IGISSerializableObject
    {
        [JsonInclude, JsonPropertyName(nameof(Code))]
        private readonly string? code = null;

        [JsonInclude, JsonPropertyName(nameof(ComponentCount))]
        private readonly int componentCount = 0;

        [JsonInclude, JsonPropertyName(nameof(EnclosedShellCount))]
        private readonly int enclosedShellCount = 0;

        [JsonInclude, JsonPropertyName(nameof(MaxZ))]
        private readonly double maxZ = double.NaN;

        [JsonInclude, JsonPropertyName(nameof(MinEnclosingTolerance))]
        private readonly double minEnclosingTolerance = double.NaN;

        [JsonInclude, JsonPropertyName(nameof(MinZ))]
        private readonly double minZ = double.NaN;

        [JsonInclude, JsonPropertyName(nameof(Reference))]
        private readonly string? reference = null;

        [JsonInclude, JsonPropertyName(nameof(ShellCount))]
        private readonly int shellCount = 0;

        [JsonInclude, JsonPropertyName(nameof(SpaceCount))]
        private readonly int spaceCount = 0;

        [JsonInclude, JsonPropertyName(nameof(Tolerance))]
        private readonly double tolerance = double.NaN;

        [JsonInclude, JsonPropertyName(nameof(ValidationCodes))]
        private readonly List<BuildingModelValidationCode>? validationCodes = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingModelValidationResult"/> class.
        /// </summary>
        /// <param name="reference">The reference of the 2D building the validated model belongs to.</param>
        /// <param name="code">The administrative area code the validated model belongs to.</param>
        /// <param name="tolerance">The distance tolerance the enclosure was required to hold at.</param>
        /// <param name="spaceCount">The number of spaces held by the model.</param>
        /// <param name="componentCount">The number of components held by the model.</param>
        /// <param name="shellCount">The number of shells that could be assembled from those spaces.</param>
        /// <param name="enclosedShellCount">The number of those shells closing at <paramref name="tolerance"/>.</param>
        /// <param name="minEnclosingTolerance">The smallest tolerance at which every shell of the model closes, or not a number when at least one of them never closes.</param>
        /// <param name="minZ">The lowest elevation of the model.</param>
        /// <param name="maxZ">The highest elevation of the model.</param>
        /// <param name="validationCodes">The ways in which the model failed validation. An empty or null collection means it passed.</param>
        public BuildingModelValidationResult(string? reference, string? code, double tolerance, int spaceCount, int componentCount, int shellCount, int enclosedShellCount, double minEnclosingTolerance, double minZ, double maxZ, IEnumerable<BuildingModelValidationCode>? validationCodes)
            : base()
        {
            this.reference = reference;
            this.code = code;
            this.tolerance = tolerance;
            this.spaceCount = spaceCount;
            this.componentCount = componentCount;
            this.shellCount = shellCount;
            this.enclosedShellCount = enclosedShellCount;
            this.minEnclosingTolerance = minEnclosingTolerance;
            this.minZ = minZ;
            this.maxZ = maxZ;
            this.validationCodes = validationCodes is null ? null : new List<BuildingModelValidationCode>(validationCodes);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingModelValidationResult"/> class by copying an existing one.
        /// </summary>
        /// <param name="buildingModelValidationResult">The source result to copy data from. If null, a default instance is initialized.</param>
        public BuildingModelValidationResult(BuildingModelValidationResult? buildingModelValidationResult)
            : base(buildingModelValidationResult)
        {
            if (buildingModelValidationResult != null)
            {
                reference = buildingModelValidationResult.reference;
                code = buildingModelValidationResult.code;
                tolerance = buildingModelValidationResult.tolerance;
                spaceCount = buildingModelValidationResult.spaceCount;
                componentCount = buildingModelValidationResult.componentCount;
                shellCount = buildingModelValidationResult.shellCount;
                enclosedShellCount = buildingModelValidationResult.enclosedShellCount;
                minEnclosingTolerance = buildingModelValidationResult.minEnclosingTolerance;
                minZ = buildingModelValidationResult.minZ;
                maxZ = buildingModelValidationResult.maxZ;
                validationCodes = buildingModelValidationResult.validationCodes is null ? null : new List<BuildingModelValidationCode>(buildingModelValidationResult.validationCodes);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingModelValidationResult"/> class using the specified <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the result data, or null if no data is provided.</param>
        public BuildingModelValidationResult(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Gets the administrative area code the validated model belongs to.
        /// </summary>
        [JsonIgnore]
        public string? Code
        {
            get
            {
                return code;
            }
        }

        /// <summary>
        /// Gets the number of components held by the model.
        /// </summary>
        [JsonIgnore]
        public int ComponentCount
        {
            get
            {
                return componentCount;
            }
        }

        /// <summary>
        /// Gets the number of shells closing at <see cref="Tolerance"/>.
        /// </summary>
        [JsonIgnore]
        public int EnclosedShellCount
        {
            get
            {
                return enclosedShellCount;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the model passed every check.
        /// </summary>
        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                return validationCodes is null || validationCodes.Count == 0;
            }
        }

        /// <summary>
        /// Gets the highest elevation of the model.
        /// </summary>
        [JsonIgnore]
        public double MaxZ
        {
            get
            {
                return maxZ;
            }
        }

        /// <summary>
        /// Gets the smallest tolerance at which every shell of the model closes, or not a number when at least one of them never closes.
        /// </summary>
        [JsonIgnore]
        public double MinEnclosingTolerance
        {
            get
            {
                return minEnclosingTolerance;
            }
        }

        /// <summary>
        /// Gets the lowest elevation of the model.
        /// </summary>
        [JsonIgnore]
        public double MinZ
        {
            get
            {
                return minZ;
            }
        }

        /// <summary>
        /// Gets the reference of the 2D building the validated model belongs to.
        /// </summary>
        [JsonIgnore]
        public string? Reference
        {
            get
            {
                return reference;
            }
        }

        /// <summary>
        /// Gets the number of shells that could be assembled from the spaces of the model.
        /// </summary>
        [JsonIgnore]
        public int ShellCount
        {
            get
            {
                return shellCount;
            }
        }

        /// <summary>
        /// Gets the number of spaces held by the model.
        /// </summary>
        [JsonIgnore]
        public int SpaceCount
        {
            get
            {
                return spaceCount;
            }
        }

        /// <summary>
        /// Gets the distance tolerance the enclosure was required to hold at.
        /// </summary>
        [JsonIgnore]
        public double Tolerance
        {
            get
            {
                return tolerance;
            }
        }

        /// <summary>
        /// Gets the ways in which the model failed validation. An empty list means it passed.
        /// </summary>
        [JsonIgnore]
        public List<BuildingModelValidationCode>? ValidationCodes
        {
            get
            {
                return validationCodes is null ? null : new List<BuildingModelValidationCode>(validationCodes);
            }
        }
    }
}
