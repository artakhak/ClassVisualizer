// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Text;
using JetBrains.Annotations;

namespace ClassVisualizer
{
    /// <inheritdoc />
    public abstract class ClassVisualizerBase : IClassVisualizer
    {
        /// <inheritdoc />
        public abstract void Visualize(StringBuilder visualizedText, int level);

        /// <summary>
        /// Returns an indent used at level <paramref name="level"/>.
        /// </summary>
        /// <param name="level">Level.</param>
        protected string GetIndent(int level)
        {
            return new string('\t', level);
        }

        /// <summary>
        /// Adds an indent and a line break.
        /// </summary>
        protected void AddIndentedLineBreak([NotNull] StringBuilder visualizedText, int level, bool addAdditionalTabs = false)
        {
            var indent = GetIndent(level);

            visualizedText.AppendLine();
            visualizedText.Append(indent);

            if (addAdditionalTabs)
                visualizedText.Append(" ");
        }
    }
}