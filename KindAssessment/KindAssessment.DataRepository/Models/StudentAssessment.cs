using AssessmentApp.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AssessmentApp.Data.Models
{
    public class StudentAssessment
    {
        public int Id { get; set; }
        [Display(Name = "Student:")]
        [Required]
        public int? StudentId { get; set; }

        [Display(Name = "Assessment Type:")]
        [Required]
        public int? AssessmentTypeId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public bool Complete => CompletedOn.HasValue;
        public String AssessmentTypeName { get; set; }
        public String StudentName { get; set; }

        public AssessmentTypeEnum? AssessmentType => AssessmentTypeId.HasValue ? (AssessmentTypeEnum)AssessmentTypeId.Value : (AssessmentTypeEnum?)null ;


    }
}
