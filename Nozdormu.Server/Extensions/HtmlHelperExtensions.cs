using System.Composition;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NuGet.Packaging;

namespace Nozdormu.Server.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent ActionLink(this IHtmlHelper helper, string linkText, string actionName, string controllerName, HttpMethod httpMethod, object routeValues, object htmlAttributes)
        {
            var htmlContent = new TagBuilder("button");

            foreach (var prop in htmlAttributes.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                htmlContent.Attributes.Add(prop.Name, prop.GetValue(htmlAttributes, null).ToString());
            }

            var url = $"/{controllerName}/{actionName}";

            foreach (var prop in routeValues.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if(prop.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                {
                    url += $"/{prop.GetValue(routeValues, null)}";
                }
            }

            htmlContent.Attributes.Add("onclick", $"handleActionLink('{url}', '{httpMethod}')");

            htmlContent.InnerHtml.SetHtmlContent(linkText);

            return htmlContent;
        }
    }
}
