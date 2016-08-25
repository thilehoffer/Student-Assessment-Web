using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentApp.WebClient.Models
{
    public class AddAssessmentViewModel
    {
        public Data.Models.StudentAssessment StudentAssessment { get; set; }
        public List<Data.Models.ListItem> AssessmentTypes { get; set; }
        public List<Data.Models.ListItem> Students { get; set; }
    }

    public class EditAssessmentViewModel
    {
        public Data.Models.StudentAssessment StudentAssessment { get; set; }
        public EditAssessmentViewModel() {

        }       
    }

}