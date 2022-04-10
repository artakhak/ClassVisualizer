// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Text;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public class PrimitiveTypeVisualizer : NonNullValueInitializer
    {
        /// <inheritdoc />
        public PrimitiveTypeVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects,
            [NotNull] IObjectVisualizationContext objectVisualizationContext,
            [NotNull] object visualizedObject, [NotNull] string visualizedElementName) : base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedObject, visualizedElementName)
        {
        }

        /// <inheritdoc />
        public override void Visualize(StringBuilder visualizedText, int level)
        {
            AddIndentedLineBreak(visualizedText, level);
            visualizedText.Append($"<{VisualizedElementName}");

            var typeName = VisualizedObject.GetType().ToString();

            visualizedText.Append($" value='{ValueVisualizerDependencyObjects.AttributeValueSanitizer.SanitizeAttributeValue(VisualizedObject.ToString())}'");

            if (!string.Equals(VisualizedElementName, typeName, StringComparison.Ordinal))
                visualizedText.Append($" type='{typeName}'");

            visualizedText.Append(" />");
        }
    }
}