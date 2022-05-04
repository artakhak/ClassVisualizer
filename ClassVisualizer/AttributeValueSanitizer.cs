// Copyright (c) ClassVisualizer Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Text;

namespace ClassVisualizer
{
    public class AttributeValueSanitizer : IAttributeValueSanitizer
    {
        /// <inheritdoc />
        public string SanitizeAttributeValue(string attributeValue)
        {
            var attributeValueStrBldr = new StringBuilder();

            const string lineBreak = "$line_break$";

            for (var i = 0; i < attributeValue.Length; ++i)
            {
                var currentChar = attributeValue[i];

                if (currentChar == '\'')
                {
                    attributeValueStrBldr.Append("&apos;");
                }
                else if (currentChar == '"')
                {
                    attributeValueStrBldr.Append("&quot;");
                }
                else if (currentChar == '\r')
                {
                    attributeValueStrBldr.Append(lineBreak);
                    if (i < attributeValue.Length - 1 && attributeValue[i + 1] == '\n')
                        ++i;
                }
                else if (currentChar == '\n')
                {
                    attributeValueStrBldr.Append(lineBreak);
                }
                else if (currentChar == '<')
                {
                    if (i < attributeValue.Length - 1 && attributeValue[i + 1] == '%')
                    {
                        attributeValueStrBldr.Append("< %");
                        ++i;
                    }
                    else
                    {
                        attributeValueStrBldr.Append(currentChar);
                    }
                }
                else if (currentChar == '\0')
                {
                    attributeValueStrBldr.Append("Na");
                }
                else
                {
                    attributeValueStrBldr.Append(currentChar);
                }
            }

            return attributeValueStrBldr.ToString();
        }
    }
}