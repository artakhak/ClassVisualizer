// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface IInterfacePropertyVisualizationHelper
    {
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<PropertyInfo> GetAllInterfaceProperties([NotNull] Type interfaceType);
    }
}