using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssessmentApp.WebClient.Models
{
    public class AddClassViewModel
    {
       
        public Data.Models.Class TeacherClass { get; set; }

        public bool FirstTime { get; set; }

        public AddClassViewModel() {
            TeacherClass = new Data.Models.Class();
        }

    }

    public class ClassIndexViewModel
    {
        public   List<Data.Models.Class> TableItems{get;set;}
    }

 
}