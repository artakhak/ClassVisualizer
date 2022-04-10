// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface IInterfaceVisualizerSettings
    {
        bool AddInterfaceTypeProperty { get; }

        int MaxLengthOfAttributeNameValuePairsBeforeLineBreak { get; }

        bool DoNotVisualizeDerivedInterface { get; }

        bool PropertyShouldBeIgnored([NotNull] object visualizedObject, [NotNull] string propertyName);
    }
}