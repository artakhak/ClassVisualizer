using System.Web;

namespace ClassVisualizer
{
    /// <summary>
    /// Attribute value sanitizer that uses <see cref="HttpUtility.HtmlEncode(string)"/> to sanitize the visualized object values in attributes.
    /// Use this class if the visualization will be viewed in Html.
    /// Otherwise, use <see cref="AttributeValueSanitizer"/>
    /// </summary>
    public class HtmlEncodeBasedAttributeValueSanitizer : IAttributeValueSanitizer
    {
        public string SanitizeAttributeValue(string attributeValue)
        {
            return HttpUtility.HtmlEncode(attributeValue);
        }
    }
}