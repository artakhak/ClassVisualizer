// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface ICategoryToPropertiesMapper
    {
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<IInterfacePropertyInfo> GetCategoryProperties([NotNull] IPropertyCategory propertyCategory);
    }
}