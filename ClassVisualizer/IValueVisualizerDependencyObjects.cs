// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface IValueVisualizerDependencyObjects
    {
        [NotNull] IValueVisualizerFactory ValueVisualizerFactory { get; }

        [NotNull] IInterfacePropertyVisualizationHelper InterfacePropertyVisualizationHelper { get; }

        [NotNull] IAttributeValueSanitizer AttributeValueSanitizer { get; }
    }
}