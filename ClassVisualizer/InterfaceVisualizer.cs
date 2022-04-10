// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public class InterfaceVisualizer : NonNullValueInitializer
    {
        private readonly bool _addChildren;

        [NotNull] private readonly Dictionary<IPropertyCategory, bool> _propertyCategoryToRenderMapping = new Dictionary<IPropertyCategory, bool>();

        public InterfaceVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects,
            [NotNull] IObjectVisualizationContext objectVisualizationContext,
            [NotNull] object visualizedObject,
            [NotNull] string visualizedElementName,
            [NotNull] Type mainInterfaceType, bool addChildren) :
            base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedObject, visualizedElementName)
        {
            MainInterfaceType = mainInterfaceType;

            if (!InterfaceVisualizerSettingsAmbientContext.Context.DoNotVisualizeDerivedInterface)
                MainInterfaceType = ClassVisualizerHelpers.GetInterfaceTypeForDisplay(visualizedObject, mainInterfaceType);

            _addChildren = addChildren;
        }

        [NotNull] protected Type MainInterfaceType { get; }

        public override void Visualize(StringBuilder visualizedText, int level)
        {
            Initialize();
            DoVisualizeElement(visualizedText, level);
        }

        protected virtual void Initialize()
        {
        }

        protected bool PropertyShouldBeIgnored([NotNull] string propertyName)
        {
            if (InterfaceVisualizerSettingsAmbientContext.Context.PropertyShouldBeIgnored(VisualizedObject, propertyName))
                return true;

            return PropertyShouldBeIgnoredVirtual(propertyName);
        }

        protected virtual bool PropertyShouldBeIgnoredVirtual([NotNull] string propertyName)
        {
            return false;
        }

        protected virtual (IList<IInterfacePropertyInfo> interfacePropertiesWithNoCategory, IList<IPropertyCategory> propertyCategories) GetVisualizedProperties()
        {
            var interfacePropertiesWithNoCategory = new List<IInterfacePropertyInfo>();

            var allInterfaceProperties = MainInterfaceType.IsInterface ? ValueVisualizerDependencyObjects.InterfacePropertyVisualizationHelper.GetAllInterfaceProperties(MainInterfaceType) : MainInterfaceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in allInterfaceProperties)
            {
                if (PropertyShouldBeIgnored(propertyInfo.Name))
                    continue;

                PropertyVisualizationType propertyVisualizationType;

                var propertyValue = propertyInfo.GetValue(VisualizedObject);

                if (propertyValue == null)
                    propertyVisualizationType = PropertyVisualizationType.VisualizePropertyOnlyInSeparateElement;
                else if (ClassVisualizerHelpers.IsPrimitiveTypeForVisualization(propertyInfo.PropertyType))
                    propertyVisualizationType = PropertyVisualizationType.VisualizePropertyOnlyInAttribute;
                else
                    propertyVisualizationType = PropertyVisualizationType.VisualizePropertyAndChildren;

                interfacePropertiesWithNoCategory.Add(new InterfacePropertyInfo(propertyInfo, propertyVisualizationType, propertyValue));
            }

            return (interfacePropertiesWithNoCategory, new List<IPropertyCategory>(0));
        }

        protected virtual void VisualizeCategory([NotNull] StringBuilder visualizedText,
            [NotNull] IPropertyCategory propertyCategory,
            int level)
        {
            var displayedProperties = propertyCategory.Properties.Where(x => !PropertyShouldBeIgnored(x.Name)).ToList();

            var isCategoryEmpty = displayedProperties.Count == 0 && propertyCategory.ChildCategories.Count == 0 ||
                                  !PropertyCategoryShouldBeRendered(propertyCategory);

            if (isCategoryEmpty && propertyCategory.DoNotRenderIfEmpty)
                return;

            AddIndentedLineBreak(visualizedText, level);

            visualizedText.Append($"<{propertyCategory.Name}");

            if (isCategoryEmpty)
            {
                visualizedText.Append("/>");
            }
            else
            {
                visualizedText.Append(">");
                foreach (var interfacePropertyInfo in displayedProperties)
                    if (!PropertyShouldBeIgnored(interfacePropertyInfo.Name))
                        VisualizeProperty(visualizedText, interfacePropertyInfo, level + 1);

                foreach (var childCategory in propertyCategory.ChildCategories)
                    VisualizeCategory(visualizedText, childCategory, level + 1);

                AddIndentedLineBreak(visualizedText, level);

                visualizedText.Append($"</{propertyCategory.Name}>");
            }
        }

        protected void VisualizeProperty([NotNull] StringBuilder visualizedText, IInterfacePropertyInfo interfacePropertyInfo, int level)
        {
            var interfaceVisualizerFactory = ValueVisualizerDependencyObjects.ValueVisualizerFactory;

            IClassVisualizer classVisualizer;

            if (interfacePropertyInfo.Value == null)
                classVisualizer = interfaceVisualizerFactory.CreateNullValueVisualizer(interfacePropertyInfo.Name, interfacePropertyInfo.PropertyType);
            else
                classVisualizer = interfaceVisualizerFactory.CreateInterfaceVisualizer(ObjectVisualizationContext, interfacePropertyInfo.Value,
                    interfacePropertyInfo.Name, interfacePropertyInfo.PropertyType,
                    interfacePropertyInfo.VisualizationType == PropertyVisualizationType.VisualizePropertyAndChildren);

            classVisualizer.Visualize(visualizedText, level);
        }

        private void DoVisualizeElement([NotNull] StringBuilder visualizedText, int level)
        {
            var wasPreviouslyVisualized = ObjectVisualizationContext.VisualizedObjectsHistory.WasPreviouslyVisualized(VisualizedObject);
            var objectId = ObjectVisualizationContext.VisualizedObjectsHistory.OnVisualized(VisualizedObject);

            var indent = GetIndent(level);

            string attributeValue;
            var totalLengthOfAttributeNameValuesOnCurrentLine = 0;

            visualizedText.AppendLine();

            visualizedText.Append(indent);
            visualizedText.Append($"<{VisualizedElementName}");

            if (!(InterfaceVisualizerSettingsAmbientContext.Context.PropertyShouldBeIgnored(VisualizedObject, nameof(SpecialVisualizedPropertyNames.ObjectId)) ||
                  PropertyShouldBeIgnored(SpecialVisualizedPropertyNames.ObjectId)))
            {
                attributeValue = objectId.ToString();
                visualizedText.Append($" {SpecialVisualizedPropertyNames.ObjectId}='{attributeValue}'");
                totalLengthOfAttributeNameValuesOnCurrentLine = SpecialVisualizedPropertyNames.ObjectId.Length + attributeValue.Length;
            }

            var (interfacePropertiesWithNoCategory, propertyCategories) = GetVisualizedProperties();
            var propertiesWithNoCategory2 = new List<IInterfacePropertyInfo>(interfacePropertiesWithNoCategory.Count);

            foreach (var interfacePropertyInfo in interfacePropertiesWithNoCategory)
            {
                if (PropertyShouldBeIgnored(interfacePropertyInfo.Name))
                    continue;

                switch (interfacePropertyInfo.VisualizationType)
                {
                    case PropertyVisualizationType.VisualizePropertyOnlyInAttribute:
                    case PropertyVisualizationType.VisualizePropertyOnlyInAttributeInNextLine:

                        attributeValue = interfacePropertyInfo.Value == null ? "null" : ValueVisualizerDependencyObjects.AttributeValueSanitizer.SanitizeAttributeValue(interfacePropertyInfo.Value.ToString());
                        var currentAttributeNameValueLength = interfacePropertyInfo.Name.Length + attributeValue.Length;

                        if (interfacePropertyInfo.VisualizationType == PropertyVisualizationType.VisualizePropertyOnlyInAttributeInNextLine ||
                            totalLengthOfAttributeNameValuesOnCurrentLine + currentAttributeNameValueLength >= InterfaceVisualizerSettingsAmbientContext.Context.MaxLengthOfAttributeNameValuePairsBeforeLineBreak)
                        {
                            AddIndentedLineBreak(visualizedText, level, true);
                            totalLengthOfAttributeNameValuesOnCurrentLine = currentAttributeNameValueLength;
                        }
                        else
                        {
                            totalLengthOfAttributeNameValuesOnCurrentLine += currentAttributeNameValueLength;
                        }

                        visualizedText.Append($" {interfacePropertyInfo.Name}='{attributeValue}'");
                        break;

                    default:

                        propertiesWithNoCategory2.Add(interfacePropertyInfo);
                        break;
                }
            }

            if (InterfaceVisualizerSettingsAmbientContext.Context.AddInterfaceTypeProperty &&
                !(VisualizedElementName.Equals(MainInterfaceType.FullName, StringComparison.Ordinal) ||
                  PropertyShouldBeIgnored(SpecialVisualizedPropertyNames.Interface)))
            {
                attributeValue = MainInterfaceType.ToString();
                var currentAttributeNameValueLength = SpecialVisualizedPropertyNames.Interface.Length + attributeValue.Length;

                if (totalLengthOfAttributeNameValuesOnCurrentLine + currentAttributeNameValueLength >= InterfaceVisualizerSettingsAmbientContext.Context.MaxLengthOfAttributeNameValuePairsBeforeLineBreak)
                    AddIndentedLineBreak(visualizedText, level, true);

                visualizedText.Append($" {SpecialVisualizedPropertyNames.Interface}='{MainInterfaceType}'");
            }

            if (_addChildren && !wasPreviouslyVisualized &&
                (propertiesWithNoCategory2.Count > 0 ||
                 propertyCategories.Any(PropertyCategoryShouldBeRendered)))
            {
                visualizedText.Append(">");

                foreach (var interfacePropertyInfo in propertiesWithNoCategory2)
                    VisualizeProperty(visualizedText, interfacePropertyInfo, level + 1);

                foreach (var propertyCategory in propertyCategories)
                    VisualizeCategory(visualizedText, propertyCategory, level + 1);

                AddIndentedLineBreak(visualizedText, level);
                visualizedText.Append($"</{VisualizedElementName}>");
            }
            else
            {
                visualizedText.Append("/>");
            }
        }

        private bool PropertyCategoryShouldBeRendered([NotNull] IPropertyCategory propertyCategory)
        {
            if (_propertyCategoryToRenderMapping.TryGetValue(propertyCategory, out var shouldBeRendered))
                return shouldBeRendered;

            bool PropertyCategoryShouldBeRendered2()
            {
                if (!propertyCategory.DoNotRenderIfEmpty || propertyCategory.Properties.Any(x => !PropertyShouldBeIgnored(x.Name)))
                    return true;

                foreach (var childPropertyCategory in propertyCategory.ChildCategories)
                    if (PropertyCategoryShouldBeRendered(childPropertyCategory))
                        return true;

                return false;
            }

            var propertyCategoryShouldBeRendered = PropertyCategoryShouldBeRendered2();
            _propertyCategoryToRenderMapping[propertyCategory] = propertyCategoryShouldBeRendered;

            return propertyCategoryShouldBeRendered;
        }
    }
}