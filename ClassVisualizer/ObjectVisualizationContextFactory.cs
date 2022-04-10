// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace ClassVisualizer
{
    public class ObjectVisualizationContextFactory : IObjectVisualizationContextFactory
    {
        /// <inheritdoc />
        public IObjectVisualizationContext CreateObjectVisualizationContext()
        {
            return new ObjectVisualizationContext(new VisualizedObjectsHistory());
        }
    }
}