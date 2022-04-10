// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface ISaveVisualizedInterface
    {
        void Save([NotNull] object visualizedObject,
            [NotNull] Type mainInterfaceType,
            [NotNull] string savedFilePath);
    }
}