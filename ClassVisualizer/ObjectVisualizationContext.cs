// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace ClassVisualizer
{
    public class ObjectVisualizationContext : IObjectVisualizationContext
    {
        public ObjectVisualizationContext(IVisualizedObjectsHistory visualizedObjectsHistory)
        {
            VisualizedObjectsHistory = visualizedObjectsHistory;
        }

        /// <inheritdoc />
        public IVisualizedObjectsHistory VisualizedObjectsHistory { get; }
    }
}