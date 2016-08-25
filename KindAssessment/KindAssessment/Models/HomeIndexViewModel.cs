using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AssessmentApp.WebClient.Models
{
    public class HomeIndexViewModel
    {
        public List<SelectListItem> Classes { get; set; }

        [Display(Name ="Select a class to display students")]
        public int ClassId { get; set; }
    }
}