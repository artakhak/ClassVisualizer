// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClassVisualizer
{
    public class InterfacePropertyVisualizationHelper : IInterfacePropertyVisualizationHelper
    {
        /// <inheritdoc />
        public IReadOnlyList<PropertyInfo> GetAllInterfaceProperties(Type interfaceType)
        {
            var interfaces = new List<Type>(5) {interfaceType};

            interfaces.AddRange(interfaceType.GetInterfaces());

            var allProperties = new List<PropertyInfo>();

            var addedProperties = new HashSet<string>(StringComparer.Ordinal);
            foreach (var implementedInterface in interfaces)
            foreach (var interfaceProperty in implementedInterface.GetProperties())
            {
                if (addedProperties.Contains(interfaceProperty.Name))
                    continue;

                allProperties.Add(interfaceProperty);
                addedProperties.Add(interfaceProperty.Name);
            }

            return allProperties;
        }
    }
}