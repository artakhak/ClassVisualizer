// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using OROptimizer.Diagnostics;

namespace ClassVisualizer
{
    public class VisualizedObjectsHistory : IVisualizedObjectsHistory
    {
        [NotNull] private readonly IObjectsCache<VisualizedObjectInfo> _objectsCache;

        public VisualizedObjectsHistory()
        {
            _objectsCache = new ObjectsCache<VisualizedObjectInfo>((obj, objectId) => new VisualizedObjectInfo(obj, objectId));
        }

        /// <inheritdoc />
        public long GetIdentifier(object visualizedObject)
        {
            return _objectsCache.GetOrCreateObjectInfo(visualizedObject).ObjectId;
        }

        /// <inheritdoc />
        public bool WasPreviouslyVisualized(object visualizedObject)
        {
            var objectInfo = _objectsCache.GetOrCreateObjectInfo(visualizedObject);

            return objectInfo != null && objectInfo.IsVisualized;
        }

        /// <inheritdoc />
        public long OnVisualized(object visualizedObject)
        {
            var objectInfo = _objectsCache.GetOrCreateObjectInfo(visualizedObject);
            objectInfo.IsVisualized = true;

            return objectInfo.ObjectId;
        }

        private class VisualizedObjectInfo : ObjectInfo
        {
            /// <inheritdoc />
            public VisualizedObjectInfo([NotNull] object obj, long objectId) : base(obj, objectId)
            {
            }

            public bool IsVisualized { get; set; }
        }
    }
}