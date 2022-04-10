// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public class InterfaceVisualizerSettings : IInterfaceVisualizerSettings
    {
        private readonly HashSet<string> _notVisualizedProperties = new HashSet<string>(StringComparer.Ordinal);

        /// <summary>
        ///     Default constructor necessary in AmbientContext.
        /// </summary>
        public InterfaceVisualizerSettings() : this(new List<string>(0))
        {
        }

        public InterfaceVisualizerSettings([NotNull] [ItemNotNull] IEnumerable<string> notVisualizedProperties)
        {
            foreach (var propertyName in notVisualizedProperties)
                if (!_notVisualizedProperties.Contains(propertyName))
                    _notVisualizedProperties.Add(propertyName);
        }

        /// <inheritdoc />
        public bool AddInterfaceTypeProperty { get; set; } = true;

        /// <inheritdoc />
        public int MaxLengthOfAttributeNameValuePairsBeforeLineBreak { get; set; } = 300;

        /// <inheritdoc />
        public bool DoNotVisualizeDerivedInterface { get; set; } = true;

        /// <inheritdoc />
        public virtual bool PropertyShouldBeIgnored(object visualizedObject, string propertyName)
        {
            if (_notVisualizedProperties.Contains(propertyName))
                return true;

            return PropertyShouldBeIgnoredVirtual(visualizedObject, propertyName);
        }

        protected virtual bool PropertyShouldBeIgnoredVirtual(object visualizedObject, string propertyName)
        {
            return false;
        }
    }
}