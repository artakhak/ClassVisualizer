// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace ClassVisualizer
{
    public abstract class NonNullValueInitializer : ClassVisualizerBase
    {
        protected NonNullValueInitializer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects,
            [NotNull] IObjectVisualizationContext objectVisualizationContext,
            [NotNull] object visualizedObject,
            [NotNull] string visualizedElementName)
        {
            ValueVisualizerDependencyObjects = valueVisualizerDependencyObjects;
            ObjectVisualizationContext = objectVisualizationContext;
            VisualizedObject = visualizedObject;
            VisualizedElementName = visualizedElementName;
        }

        [NotNull] protected IValueVisualizerDependencyObjects ValueVisualizerDependencyObjects { get; }

        [NotNull] protected IObjectVisualizationContext ObjectVisualizationContext { get; }

        [NotNull] protected object VisualizedObject { get; }

        [NotNull] protected string VisualizedElementName { get; }
    }
}