// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface IValueVisualizerFactory
    {
        /// <summary>
        ///     Creates a visualizer for non-null object
        /// </summary>
        [NotNull]
        IClassVisualizer CreateInterfaceVisualizer([NotNull] IObjectVisualizationContext objectVisualizationContext,
            [NotNull] object visualizedObject,
            [NotNull] string visualizedElementName,
            [NotNull] Type mainInterfaceType,
            bool addChildren);

        /// <summary>
        ///     Creates a visualizer for null object
        /// </summary>
        [NotNull]
        IClassVisualizer CreateNullValueVisualizer([NotNull] string visualizedElementName,
            [NotNull] Type mainInterfaceType);
    }
}