using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using DotNetNuke.Web.Mvc.Helpers;
using System.Web.Routing;
using System.Text;

namespace NLDotNet.DNN.Modules.MVCTest.Components.Web.Helpers
{
    public static class HTMLRadioButtonListExtensions
    {
        /// <summary>
        /// Template Label pour les views Edit/Settings
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="labelText"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns>une chaine de caractères représentant un template html.</returns>
        public static MvcHtmlString DnnRadioButtonListFor<TModel, TValue>(this DnnHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Dictionary<string, string> listItems, string selectedValue, IDictionary<string, object> htmlAttributes = null, string helpText = "")
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            if (listItems == null)
                return MvcHtmlString.Empty;

            if (listItems.Count == 0)
                return MvcHtmlString.Empty;

            var sbHtml = new StringBuilder();
            try
            {
                var htmlCtrlName = (htmlFieldName.IndexOf(".")>=0) ? htmlFieldName.Split('.').Last() : htmlFieldName;
                var cssClass = (htmlAttributes == null) ? "" : ((htmlAttributes.Count == 0) ? "" : " " + String.Join(@" ", htmlAttributes));
                sbHtml.Append(@"<span class=""dnnFormRadioButtons display-table"">").AppendLine();
                for (var i = 0; i < listItems.Count; i++)
                {
                    var _value = listItems.Keys.ElementAt(i);
                    var _text = listItems.Values.ElementAt(i);
                    var _htmlCheckAttr = (selectedValue.Equals(_value, StringComparison.InvariantCultureIgnoreCase)) ? @" checked=""checked""" : "";
                    sbHtml.Append(@"<input type=""radio""")
                        .Append(@" id=""").Append(htmlCtrlName+i).Append(@"""")
                        .Append(@" name=""").Append(htmlFieldName).Append(@"""")
                        .Append(@" value=""").Append(_value).Append(@"""")
                        .Append(@" resourcekey=""").Append(_text).Append(@"""")
                        .Append(@"" + cssClass)
                        .Append(@"" + _htmlCheckAttr)
                        .Append(@" />").AppendLine()
                        .Append(@"<label for=""").Append(htmlCtrlName+i).Append(@""" class=""dnnBoxLabel"">").Append(@"" + _text).Append(@"</label>").AppendLine()
                        .Append(@"<br>").AppendLine();
                }
                sbHtml.Append(@"</span>").AppendLine();
            }
            catch(Exception ex)
            {
                
            }
            //return MvcHtmlString.Create(tagDiv.ToString(TagRenderMode.Normal));
            return MvcHtmlString.Create(@"" + sbHtml.ToString());
        }
    }
}