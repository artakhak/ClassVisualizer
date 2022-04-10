// Copyright (c) IoC.Configuration Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace ClassVisualizer
{
    public interface IAttributeValueSanitizer
    {
        /// <summary>
        ///     Replaces some characters in attribute value. For example attribute value "This is un-escaped an attribute value
        ///     with apostrophe' "
        ///     might be replaced with "This is un-escaped an attribute value with apostrophe'' ".
        ///     Another example is replacing line breaks "\r\n" with some other text like "$line_break$".
        /// </summary>
        /// <param name="attributeValue">Attribute value to sanitize</param>
        /// <returns></returns>
        [NotNull]
        string SanitizeAttributeValue([NotNull] string attributeValue);
    }
}