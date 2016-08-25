using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
usi

namespace AssessmentApp.Models
{
    public class TeacherClass : IValidatableObject
    {
        public int? Id { get; set; }

        
        public string ClassName { get; set; }
    }
}