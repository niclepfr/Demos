using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using DotNetNuke.Services.Localization;

namespace NLDotNet.DNN.Modules.MVCTest.Components.DataAnnotations
{
    public class DnnRequiredFieldValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var msg = Localization.GetString(validationContext.DisplayName + ".ErrorMessage");
            if(value != null)
            {
                if (!(string.IsNullOrWhiteSpace(value.ToString())))
                    return ValidationResult.Success;
                else
                    return new ValidationResult("" + validationContext.DisplayName + " is required");
            }       
            else
                return new ValidationResult("" + validationContext.DisplayName + " is required");
        }

    }
}