// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public class SaveVisualizedInterface : ISaveVisualizedInterface
    {
        [NotNull] private readonly IObjectVisualizationContextFactory _objectVisualizationContextFactory;

        [NotNull] private readonly IValueVisualizerFactory _valueVisualizerFactory;

        public SaveVisualizedInterface([NotNull] IValueVisualizerFactory valueVisualizerFactory,
            [NotNull] IObjectVisualizationContextFactory objectVisualizationContextFactory)
        {
            _valueVisualizerFactory = valueVisualizerFactory;
            _objectVisualizationContextFactory = objectVisualizationContextFactory;
        }

        /// <inheritdoc />
        public void Save(object visualizedObject, Type mainInterfaceType, string savedFilePath)
        {
            const string xmlFileHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            var visualizedText = new StringBuilder();

            visualizedText.Append(xmlFileHeader);

            var expressionItemVisualizer = _valueVisualizerFactory.CreateInterfaceVisualizer(
                _objectVisualizationContextFactory.CreateObjectVisualizationContext(),
                visualizedObject,
                mainInterfaceType.ToString() ?? "Invalid", mainInterfaceType, true);
            expressionItemVisualizer.Visualize(visualizedText, 0);

            using (var fileStream = new StreamWriter(savedFilePath, false))
            {
                fileStream.Write("");
                fileStream.Write(visualizedText.ToString());
            }
        }
    }
}