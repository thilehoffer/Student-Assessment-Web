using System.ComponentModel.DataAnnotations;
namespace AssessmentApp.Data.Models.Assessments.LetterRecognition
{
    public class Response
    {
     
        public bool? Answer { get; set; }
        [StringLength(maximumLength:1)]
        public string InncorrectGuess { get; set; }

       
    }

    

   
}
