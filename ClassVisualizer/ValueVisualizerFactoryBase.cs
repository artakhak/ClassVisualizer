// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public class ValueVisualizerFactoryBase : IValueVisualizerFactory
    {
        public ValueVisualizerFactoryBase([NotNull] IInterfacePropertyVisualizationHelper interfacePropertyVisualizationHelper,
            [NotNull] AttributeValueSanitizer attributeValueSanitizer)
        {
            ValueVisualizerDependencyObjects = new ValueVisualizerDependencyObjects(this,
                interfacePropertyVisualizationHelper, attributeValueSanitizer);
        }

        [NotNull] protected IValueVisualizerDependencyObjects ValueVisualizerDependencyObjects { get; }


        /// <inheritdoc />
        public virtual IClassVisualizer CreateInterfaceVisualizer([NotNull] IObjectVisualizationContext objectVisualizationContext,
            object visualizedObject,
            string visualizedElementName,
            Type mainInterfaceType, bool addChildren)
        {
            if (ClassVisualizerHelpers.IsPrimitiveTypeForVisualization(visualizedObject.GetType()))
                return new PrimitiveTypeVisualizer(ValueVisualizerDependencyObjects, objectVisualizationContext, visualizedObject, visualizedElementName);

            if (visualizedObject is IEnumerable enumerable)
                return new CollectionVisualizer(ValueVisualizerDependencyObjects, objectVisualizationContext, enumerable,
                    visualizedElementName, mainInterfaceType, addChildren);


            return new InterfaceVisualizer(ValueVisualizerDependencyObjects, objectVisualizationContext, visualizedObject, visualizedElementName, mainInterfaceType, addChildren);
        }

        /// <inheritdoc />
        public IClassVisualizer CreateNullValueVisualizer(string visualizedElementName, Type mainInterfaceType)
        {
            return new NullValueVisualizer(visualizedElementName, mainInterfaceType);
        }
    }
}