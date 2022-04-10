// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Reflection;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public class InterfacePropertyInfo : IInterfacePropertyInfo
    {
        public InterfacePropertyInfo([NotNull] string name, [NotNull] Type propertyType,
            PropertyVisualizationType visualizationType, [CanBeNull] object value)
        {
            Name = name;
            PropertyType = propertyType;
            VisualizationType = visualizationType;
            Value = value;
        }

        public InterfacePropertyInfo([NotNull] PropertyInfo propertyInfo, PropertyVisualizationType visualizationType, [CanBeNull] object value)
        {
            Name = propertyInfo.Name;
            PropertyType = propertyInfo.PropertyType;
            VisualizationType = visualizationType;
            Value = value;
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public Type PropertyType { get; }

        /// <inheritdoc />
        public PropertyVisualizationType VisualizationType { get; set; }

        /// <inheritdoc />
        public object Value { get; }
    }
}