using AssessmentApp.WebClient.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessmentApp.Data.Models.Assessments;

namespace AssessmentApp.WebClient.Controllers
{
    [Authorize]
    public class AssessmentsController : BaseController
    {
        public ActionResult Index()
        {

            var model = DataRepository.GetRecentAssessments(SessionItems.TeacherId.Value);
            return View(model);
        }
        // GET: Assessments
        public ActionResult Add()
        {
            var model = new Models.AddAssessmentViewModel();
            model.AssessmentTypes = DataRepository.GetActiveAssessments(SessionItems.TeacherId.Value);
            model.Students = DataRepository.GetActiveStudents(SessionItems.TeacherId.Value);

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Models.AddAssessmentViewModel model)
        {
            var assessments = DataRepository.GetActiveAssessments(SessionItems.TeacherId.Value);
            var students = DataRepository.GetActiveStudents(SessionItems.TeacherId.Value);

            if (!ModelState.IsValid)
            {
                model.AssessmentTypes = assessments;
                model.Students = students;
                return View(model);
            }

            model.StudentAssessment = DataRepository.CreateStudentAssessment(model.StudentAssessment);
            return RedirectToAction("Edit", new { id = model.StudentAssessment.Id });
        }

        public ActionResult Edit(int id)
        {
            var assessment = DataRepository.GetStudentAssessment(id);
            switch (assessment.AssessmentType)
            {
                case Data.Models.Enums.AssessmentTypeEnum.LetterRecongitionUpperCase:
                    var data1 = DataRepository.GetAssmentData<Data.Models.Assessments.LetterRecognition.AssessmentData>(id);
                    if (data1 == null)
                        data1 = new Data.Models.Assessments.LetterRecognition.AssessmentData { IsUpperCase = true };

                    return View("LetterRecognition", new Data.Models.Assessments.LetterRecognition.Assessment
                    {
                        StudentAssessment = assessment,
                        AssessmentData = data1
                    });

                case Data.Models.Enums.AssessmentTypeEnum.LetterRecognitionLowerCase:
                    var data2 = DataRepository.GetAssmentData<Data.Models.Assessments.LetterRecognition.AssessmentData>(id);
                    if (data2 == null)
                        data2 = new Data.Models.Assessments.LetterRecognition.AssessmentData { IsUpperCase = false };

                    return View("LetterRecognition", new Data.Models.Assessments.LetterRecognition.Assessment
                    {
                        StudentAssessment = assessment,
                        AssessmentData = data2
                    });

            }

            throw new NotImplementedException();

        }



        [HttpPost]
        public ActionResult LetterRecognition(Data.Models.Assessments.LetterRecognition.Assessment model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            DataRepository.SaveAssessmentData(model.StudentAssessment.Id, model.AssessmentData);

            return RedirectToAction("Index");


        }
    }
}