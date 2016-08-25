using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentApp.Data.Models.Assessments
{
    public abstract class AssessmentBase
    {
        public StudentAssessment StudentAssessment { get; set; }

        public AssessmentBase() {
          
        }
    }
}
