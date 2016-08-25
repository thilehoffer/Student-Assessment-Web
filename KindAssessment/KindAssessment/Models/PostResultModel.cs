using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssessmentApp.WebClient.Models
{
    public class PostResultModel
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public List<ValidationResult> Errors { get; set; }

        public PostResultModel() {
            Errors = new List<ValidationResult>();
        }
        
    }
}