using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentApp.Data.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "Class:")]
        [Required]
        public int? ClassId { get; set; }

        public string ClassName { get; set; }

        [Required]
        [Display(Name = "Full Name:")]
        [MaxLength(200)]
        public string StudentName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth:")]
        public DateTime? StudentDateOfBirth { get; set; }

        public string StudentDateOfBirthString => StudentDateOfBirth.HasValue ? StudentDateOfBirth.Value.ToShortDateString() : string.Empty;

        public bool Active { get; set; }                        
                            
    }
}