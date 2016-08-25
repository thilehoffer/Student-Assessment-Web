using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AssessmentApp.Data.Models.Assessments.LetterRecognition
{
    
    public class Assessment : AssessmentBase
    {
        public AssessmentData AssessmentData { get; set; }
        
       public Assessment( ) : base()
        { 
            
        }

        

    }

    [Serializable]
    public class AssessmentData {
        public bool IsUpperCase { get; set; }


        [ResponseRequiredAttribute(ErrorMessage = "A has no Answer")]
        public Response A { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "B has no Answer")]
        public Response B { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "C has no Answer")]
        public Response C { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "D has no Answer")]
        public Response D { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "E has no Answer")]
        public Response E { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "F has no Answer")]
        public Response F { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "G has no Answer")]
        public Response G { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "H has no Answer")]
        public Response H { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "I has no Answer")]
        public Response I { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "J has no Answer")]
        public Response J { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "K has no Answer")]
        public Response K { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "L has no Answer")]
        public Response L { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "M has no Answer")]
        public Response M { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "N has no Answer")]
        public Response N { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "O has no Answer")]
        public Response O { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "P has no Answer")]
        public Response P { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "Q has no Answer")]
        public Response Q { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "R has no Answer")]
        public Response R { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "S has no Answer")]
        public Response S { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "T has no Answer")]
        public Response T { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "U has no Answer")]
        public Response U { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "V has no Answer")]
        public Response V { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "W has no Answer")]
        public Response W { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "X has no Answer")]
        public Response X { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "Y has no Answer")]
        public Response Y { get; set; }

        [ResponseRequiredAttribute(ErrorMessage = "Z has no Answer")]
        public Response Z { get; set; }
        public AssessmentData() {

        }
    }

    public class ResponseRequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var r = (Response)value;
            
            if (r != null && !r.Answer.HasValue)
            {
                return false;
            }
            return true;
        }
    }
}
