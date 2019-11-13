using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using DotNetNuke.Web.Mvc.Helpers;
using System.Web.Routing;

namespace NLDotNet.DNN.Modules.MVCTest.Components.Web.Helpers
{
    public static class HtmlLabelExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="labelText"></param>
        /// <returns></returns>
        public static MvcHtmlString DnnLabelFor<TModel, TValue>(this DnnHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText)
        {
            return DnnLabelFor(html, expression,labelText,null,"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString DnnLabelFor<TModel, TValue>(this DnnHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return DnnLabelFor(html, expression, "", new RouteValueDictionary(htmlAttributes),"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="labelText"></param>
        /// <param name="helpText"></param>
        /// <returns></returns>
        public static MvcHtmlString DnnLabelFor<TModel, TValue>(this DnnHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText , string helpText)
        {
            return DnnLabelFor(html, expression, labelText,null, helpText);
        }

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
        public static MvcHtmlString DnnLabelFor<TModel, TValue>(this DnnHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText = "", IDictionary<string, object> htmlAttributes = null, string helpText = "")
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            /* Text du label */
            //string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            //if (String.IsNullOrEmpty(labelText))
            //{
            //    return MvcHtmlString.Empty;
            //}
            if (string.IsNullOrWhiteSpace(labelText))
            {
                labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            }

            TagBuilder tagDiv = new TagBuilder("div");
            tagDiv.AddCssClass("dnnLabel");

            TagBuilder tagLbl = new TagBuilder("label");
            if(htmlAttributes != null)
                tagLbl.MergeAttributes(htmlAttributes);
            //var _for = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);
            //_for = metadata.PropertyName;
            //var _for = htmlFieldName.Split('.').Last();
            tagLbl.Attributes.Add("for", htmlFieldName);

            TagBuilder tagSpan = new TagBuilder("span");
            tagSpan.SetInnerText(labelText);
            
            // assign <span> to <label> inner html
            tagLbl.InnerHtml = tagSpan.ToString(TagRenderMode.Normal);
            
            TagBuilder tagA = new TagBuilder("a");
            tagA.AddCssClass("dnnFormHelp");
            
            TagBuilder _tagDiv = new TagBuilder("div");
            _tagDiv.AddCssClass("dnnTooltip");

            TagBuilder __tagDiv = new TagBuilder("div");
            __tagDiv.AddCssClass("dnnFormHelpContent dnnClear");

            TagBuilder _tagSpan = new TagBuilder("span");
            _tagSpan.AddCssClass("dnnHelpText");            
            if (!string.IsNullOrWhiteSpace(helpText))
                _tagSpan.SetInnerText(@"" + helpText);

            TagBuilder _tagA = new TagBuilder("a");
            _tagA.AddCssClass("pinHelp");

            __tagDiv.InnerHtml = string.Format("{0}{1}", _tagSpan.ToString(TagRenderMode.Normal),_tagA.ToString(TagRenderMode.Normal));
            _tagDiv.InnerHtml = __tagDiv.ToString(TagRenderMode.Normal);
            tagDiv.InnerHtml = string.Format("{0}{1}{2}", tagLbl.ToString(TagRenderMode.Normal), tagA.ToString(TagRenderMode.Normal), _tagDiv.ToString(TagRenderMode.Normal));

            return MvcHtmlString.Create(tagDiv.ToString(TagRenderMode.Normal));
        }
    }
}