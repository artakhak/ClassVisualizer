// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Text;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface IClassVisualizer
    {
        void Visualize([NotNull] StringBuilder visualizedText, int level);
    }
}