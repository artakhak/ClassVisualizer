// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace ClassVisualizer
{
    public class ValueVisualizerDependencyObjects : IValueVisualizerDependencyObjects
    {
        public ValueVisualizerDependencyObjects(IValueVisualizerFactory valueVisualizerFactory, IInterfacePropertyVisualizationHelper interfacePropertyVisualizationHelper, IAttributeValueSanitizer attributeValueSanitizer)
        {
            ValueVisualizerFactory = valueVisualizerFactory;
            InterfacePropertyVisualizationHelper = interfacePropertyVisualizationHelper;
            AttributeValueSanitizer = attributeValueSanitizer;
        }

        /// <inheritdoc />
        public IValueVisualizerFactory ValueVisualizerFactory { get; }

        /// <inheritdoc />
        public IInterfacePropertyVisualizationHelper InterfacePropertyVisualizationHelper { get; }

        /// <inheritdoc />
        public IAttributeValueSanitizer AttributeValueSanitizer { get; }
    }
}