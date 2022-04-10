// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public class CollectionVisualizer : InterfaceVisualizer
    {
        [NotNull] private readonly Type _collectionItemType;
        //private const string ItemsCategoryName = "items";

        [NotNull] private readonly List<object> _visualizedList;

        /// <inheritdoc />
        public CollectionVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects,
            [NotNull] IObjectVisualizationContext objectVisualizationContext,
            [NotNull] IEnumerable visualizedCollection,
            [NotNull] string visualizedElementName,
            [NotNull] Type mainInterfaceType, bool addChildren) : base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedCollection, visualizedElementName, mainInterfaceType, addChildren)
        {
            _visualizedList = new List<object>();

            foreach (var visualizedObject in visualizedCollection)
                _visualizedList.Add(visualizedObject);

            var collectionItemType = ClassVisualizerHelpers.GetCollectionTypeItemType(visualizedCollection.GetType());
            _collectionItemType = collectionItemType ??
                                  throw new ArgumentException("Visualizer '' can be used only with generic collections of type 'System.Collections.Generic.IEnumerable<T>' for some type T.");
        }

        protected override bool PropertyShouldBeIgnoredVirtual(string propertyName)
        {
            if (base.PropertyShouldBeIgnoredVirtual(propertyName))
                return true;

            return propertyName == "Item";
        }

        protected override (IList<IInterfacePropertyInfo> interfacePropertiesWithNoCategory, IList<IPropertyCategory> propertyCategories) GetVisualizedProperties()
        {
            var visualizedProperties = base.GetVisualizedProperties();

            IPropertyCategory otherPropertiesCategory = new PropertyCategory("OtherProperties");
            visualizedProperties.propertyCategories.Add(otherPropertiesCategory);


            var interfacePropertiesWithNoCategory = new List<IInterfacePropertyInfo>();
            //interfacePropertiesWithNoCategory.Add(new InterfacePropertyInfo(nameof(IList.Count), typeof(int), PropertyVisualizationType.VisualizePropertyOnlyInAttribute, _visualizedList.Count));

            foreach (var propertyInfo in visualizedProperties.interfacePropertiesWithNoCategory)
                switch (propertyInfo.VisualizationType)
                {
                    case PropertyVisualizationType.VisualizePropertyOnlyInAttribute:
                    case PropertyVisualizationType.VisualizePropertyOnlyInAttributeInNextLine:
                        interfacePropertiesWithNoCategory.Add(propertyInfo);
                        break;
                    default:
                        otherPropertiesCategory.Properties.Add(propertyInfo);
                        break;
                }
            /*if (visualizedProperties.interfacePropertiesWithNoCategory.Count > 0)
            {
                //IPropertyCategory otherPropertiesCategory = new PropertyCategory("OtherProperties");

                otherPropertiesCategory.AddCategoryProperties(visualizedProperties.interfacePropertiesWithNoCategory.Where(
                    x => x.Name != nameof(IList.Count) && x.Name != nameof(IList.IsReadOnly)));
                visualizedProperties.propertyCategories.Add(otherPropertiesCategory);
            }*/

            foreach (var collectionItem in _visualizedList)
            {
                IInterfacePropertyInfo interfacePropertyInfo =
                    new InterfacePropertyInfo(_collectionItemType.ToString(), _collectionItemType, PropertyVisualizationType.VisualizePropertyAndChildren, collectionItem);

                interfacePropertiesWithNoCategory.Add(interfacePropertyInfo);
            }

            return (interfacePropertiesWithNoCategory, visualizedProperties.propertyCategories);
        }
    }
}