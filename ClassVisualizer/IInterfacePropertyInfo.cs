// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface IInterfacePropertyInfo
    {
        [NotNull] string Name { get; set; }

        [NotNull] Type PropertyType { get; }

        PropertyVisualizationType VisualizationType { get; set; }

        [CanBeNull] object Value { get; }
    }
}