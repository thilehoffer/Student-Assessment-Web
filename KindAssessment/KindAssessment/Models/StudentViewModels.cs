using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AssessmentApp.WebClient.Models
{
    public class AddStudentViewModel
    {
        public int? SelectedClassId  {get; set;}
        public List<SelectListItem> Classes { get; set; }
        public Data.Models.Student Student {get; set;}
    }

    public class EditStudentViewModel
    {
        public int? SelectedClassId { get; set; }
        public List<SelectListItem> Classes { get; set; }
        public Data.Models.Student Student { get; set; }
    }

    public class StudentIndexViewModel {
        [Display(Name ="Select a Class to view students")]
        public int? SelectedClassId { get; set; }
        public List<SelectListItem> Classes { get; set; }
    }
}