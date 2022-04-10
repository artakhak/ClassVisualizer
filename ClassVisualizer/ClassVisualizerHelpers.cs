// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public static class ClassVisualizerHelpers
    {
        public static bool IsPrimitiveTypeForVisualization([NotNull] Type type)
        {
            return type.IsPrimitive || type.IsEnum || type == typeof(string);
        }

        [CanBeNull]
        public static Type GetCollectionTypeItemType([NotNull] Type collectionType)
        {
            foreach (var interfaceType in collectionType.GetInterfaces())
                if (interfaceType.IsGenericType
                    && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    return interfaceType.GetGenericArguments()[0];

            return null;
        }

        /// <summary>
        ///     Returns the lowest interface in implemented interface by <paramref name="objectImplementingInterface" />.GetType(),
        ///     that is a subclass of
        ///     <paramref name="implementedInterfaceType" />
        /// </summary>
        [NotNull]
        internal static Type GetInterfaceTypeForDisplay([NotNull] object objectImplementingInterface, [NotNull] Type implementedInterfaceType)
        {
            if (!implementedInterfaceType.IsInterface)
                return implementedInterfaceType;

            var allInterfaces = objectImplementingInterface.GetType().GetInterfaces().Where(implementedInterfaceType.IsAssignableFrom).ToList();

            if (allInterfaces == null || allInterfaces.Count == 0)
                throw new ArgumentException($"Parameter '{nameof(implementedInterfaceType)}' should be an instance of a type that implements interface '{implementedInterfaceType.FullName}'.");

            foreach (var interfaceType in allInterfaces)
                if (!allInterfaces.Any(potentialChildInterface => potentialChildInterface != interfaceType &&
                                                                  interfaceType.IsAssignableFrom(potentialChildInterface)))
                    return interfaceType;

            throw new Exception("No interface was found. This should normally not happen.");
        }
    }
}