// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface IVisualizedObjectsHistory
    {
        /// <summary>
        ///     If object <paramref name="visualizedObject" /> was never assigned an identifier, one will be assigned and stored in
        ///     internal cache and will be returned, otherwise, previously assigned identifier will be returned.
        /// </summary>
        /// <param name="visualizedObject">Visualized object</param>
        /// <returns>
        ///     Returns a unique id assigned to the visualized object <paramref name="visualizedObject" />.
        /// </returns>
        long GetIdentifier([NotNull] object visualizedObject);

        /// <summary>
        ///     Returns true, if <paramref name="visualizedObject" /> was already visualized.
        ///     This information can be used to avoid getting into cycle when the same object is visualized endlessly.
        ///     This can happen if an object is in Children property of some object, and references that object via a property
        ///     called Parent.
        /// </summary>
        bool WasPreviouslyVisualized([NotNull] object visualizedObject);

        /// <summary>
        ///     Registers <paramref name="visualizedObject" /> as visualized.
        /// </summary>
        /// <returns>Returns a unique identifier assigned to visualized object.</returns>
        long OnVisualized([NotNull] object visualizedObject);
    }
}