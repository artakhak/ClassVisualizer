// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;

namespace ClassVisualizer
{
    public class PropertyCategory : IPropertyCategory
    {
        public PropertyCategory(string name, bool doNotRenderIfEmpty = true)
        {
            Name = name;
            DoNotRenderIfEmpty = doNotRenderIfEmpty;
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public bool DoNotRenderIfEmpty { get; set; }

        /// <inheritdoc />
        public IList<IPropertyCategory> ChildCategories { get; } = new List<IPropertyCategory>();

        /// <inheritdoc />
        public IList<IInterfacePropertyInfo> Properties { get; } = new List<IInterfacePropertyInfo>();
    }
}