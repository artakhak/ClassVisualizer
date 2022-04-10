// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Text;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public class NullValueVisualizer : ClassVisualizerBase
    {
        [NotNull] private readonly Type _mainInterfaceType;

        [NotNull] private readonly string _visualizedElementName;

        /// <inheritdoc />
        public NullValueVisualizer([NotNull] string visualizedElementName, [NotNull] Type mainInterfaceType)
        {
            _visualizedElementName = visualizedElementName;
            _mainInterfaceType = mainInterfaceType;
        }

        /// <inheritdoc />
        public override void Visualize(StringBuilder visualizedText, int level)
        {
            var indent = GetIndent(level);
            visualizedText.AppendLine();
            visualizedText.Append(indent);
            visualizedText.Append($"<{_visualizedElementName} value='null' interface='{_mainInterfaceType.FullName}' />");
        }
    }
}