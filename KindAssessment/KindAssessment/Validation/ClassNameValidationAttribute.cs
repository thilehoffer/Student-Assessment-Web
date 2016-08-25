using AssessmentApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentApp.WebClient.Validation
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ClassNameValidationAttribute : ValidationAttribute, IClientValidatable
    {
        internal readonly IRepository DataRepository = Code.Services.DataRepository();
    

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (DataRepository.CheckIfClassAlreadyExists(SessionItems.TeacherId.Value, (string)value))
            {
                return new ValidationResult($"{(string)value} already exists");
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage,
                ValidationType = "classname"
            }; 

            yield return rule;
        }
    }
     
}