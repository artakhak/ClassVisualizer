// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public static class PropertyCategoryExtensionMethods
    {
        public static void AddCategoryProperties(this IPropertyCategory propertyCategory, [NotNull] [ItemNotNull] IEnumerable<IInterfacePropertyInfo> interfaceProperties)
        {
            foreach (var interfacePropertyInfo in interfaceProperties)
                propertyCategory.Properties.Add(interfacePropertyInfo);
        }

        public static void AddChildCategories(this IPropertyCategory propertyCategory, [NotNull] [ItemNotNull] IEnumerable<IPropertyCategory> interfacePropertyCategories)
        {
            foreach (var childPropertyCategory in interfacePropertyCategories)
                propertyCategory.ChildCategories.Add(childPropertyCategory);
        }
    }
}