/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy.Extend;
using Easy.Serializer;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using ZKEACMS.FormGenerator.Models;

namespace ZKEACMS.FormGenerator
{
    public static class HtmlHelperExt
    {
        public static IHtmlContent DisplayFieldValue(this IHtmlHelper html, FormField field)
        {
            HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();
            if ((field.Name == "Checkbox" || field.Name == "Radio" || field.Name == "Dropdown") && field.FieldOptions != null)
            {
                htmlContentBuilder.AppendHtml(string.Join("<br/>", field.FieldOptions.Where(m => m.Selected ?? false).Select(m => m.DisplayText)));
            }
            else if (field.Name == "Address" && field.Value.IsNotNullAndWhiteSpace())
            {
                htmlContentBuilder.Append(string.Join(" ", JsonConverter.Deserialize<string[]>(field.Value)));
            }
            else
            {
                htmlContentBuilder.Append(field.Value);
            }
            return htmlContentBuilder;
        }
    }
}
