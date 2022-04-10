// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface IPropertyCategory
    {
        [NotNull] string Name { get; set; }

        bool DoNotRenderIfEmpty { get; set; }

        [NotNull] [ItemNotNull] IList<IPropertyCategory> ChildCategories { get; }

        [NotNull] [ItemNotNull] IList<IInterfacePropertyInfo> Properties { get; }
    }
}