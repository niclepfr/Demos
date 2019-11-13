using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DotNetNuke.Web.Mvc.Helpers;
using System.Web.Routing;
using System.Text;
using NLDotNet.DNN.Modules.MVCTest.Components;

namespace NLDotNet.DNN.Modules.MVCTest.Components.Web.Helpers
{
    public static class HTMLTextBoxExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="format"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="helpText"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public static MvcHtmlString DnnTexBoxDateTimeFor<TModel, TValue>(
            this DnnHtmlHelper<TModel> html, Expression<Func<TModel
            , TValue>> expression
            , string format
            , IDictionary<string, object> htmlAttributes = null
            , bool readOnly = false
            )
        {

            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }

            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            if (modelMetadata == null)
            {
                if (readOnly)
                {
                    if (htmlAttributes.ContainsKey("readonly") == false)
                    {
                        htmlAttributes.Add("readonly", "read-only");
                    }
                }
            }
            else
            {
                if (htmlAttributes.ContainsKey("placeholder") == false)
                {
                    var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

                    var labelText =
                        modelMetadata.DisplayName ??
                        modelMetadata.PropertyName ??
                        htmlFieldName.Split('.').Last();

                    if (!string.IsNullOrWhiteSpace(labelText))
                    {
                        htmlAttributes.Add("placeholder", labelText);
                    }
                }

                if ((readOnly) || (modelMetadata.IsReadOnly))
                {
                    if (!htmlAttributes.ContainsKey("readonly"))
                    {
                        htmlAttributes.Add("readonly", "read-only");
                    }
                }
            }

            htmlAttributes.Add("class", "form-control");

            MemberExpression memberExpression = expression.Body as MemberExpression;

            if (memberExpression != null)
            {
                StringLengthAttribute stringLengthAttribute = 
                    memberExpression
                    .Member
                    .GetCustomAttributes(typeof(StringLengthAttribute), false)
                    .FirstOrDefault() as StringLengthAttribute;

                if (stringLengthAttribute != null)
                {
                    if (htmlAttributes.ContainsKey("maxlength") == false)
                    {
                        htmlAttributes.Add("maxlength", stringLengthAttribute.MaximumLength);
                    }
                }
            }

            return (html.TextBoxFor(expression, format, htmlAttributes));
        }
    }
}