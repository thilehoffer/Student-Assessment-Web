using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AssessmentApp.Data.Models
{
    public class Class 
    {
        public int? Id { get; set; }

        public int TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Class Name:")]
        public string ClassName { get; set; }

        
        public bool Active { get; set; }
       
    }

   
 

}