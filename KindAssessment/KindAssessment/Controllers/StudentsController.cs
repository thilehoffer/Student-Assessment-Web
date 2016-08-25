using AssessmentApp.WebClient.Code;
using AssessmentApp.WebClient.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AssessmentApp.WebClient.Controllers
{
    [Authorize]
    public class StudentsController : BaseController
    {
        private IEnumerable<SelectListItem> GetClasses(int? classId)
        {
            return DataRepository.GetActiveClasses(SessionItems.TeacherId.Value).ToSelectList(classId);
        }

        public ActionResult Add(int? selectedClassId = null)
        {
            var model = new AddStudentViewModel
            {
                Student = new Data.Models.Student(),
                SelectedClassId = selectedClassId,
                Classes = GetClasses(selectedClassId).ToList()
            };
            ViewBag.Title = "Add Student";
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AddStudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Classes = GetClasses(model.SelectedClassId).ToList();
                return View(model);
            }

            DataRepository.CreateStudent(model.Student);

            return RedirectToAction("Index", new { selectedClassId = model.Student.ClassId });
        }

        // GET: Students
        public ActionResult Index(int? selectedClassId = null)
        {
            ViewBag.Title = "My Students";
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = string.Empty, Value = "0" });
            items.AddRange(DataRepository.GetTeachersClasses(SessionItems.TeacherId.Value).
                    Where(x => x.Active).
                    Select(s => new SelectListItem { Text = s.ClassName, Value = s.Id.ToString(), Selected = (selectedClassId.HasValue && s.Id == selectedClassId.Value) }).OrderBy(x => x.Text));
            var model = new StudentIndexViewModel
            {
                SelectedClassId = selectedClassId,
                Classes = items.ToList()
            };
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var student = DataRepository.GetStudent(id);
            if (!DataRepository.IsChild<Data.Models.Student>(SessionItems.TeacherId.Value, student))
            {
                throw new AccessViolationException($"User does not have access to {id.ToString()}");
            }

            return View(new EditStudentViewModel { Student = student, Classes = GetClasses(student.ClassId).ToList(), SelectedClassId = student.ClassId });
        }

        [HttpPost]
        public ActionResult Edit(EditStudentViewModel model)
        {

            if (!DataRepository.IsChild<Data.Models.Student>(SessionItems.TeacherId.Value, model.Student))
            {
                throw new AccessViolationException($"User does not have access to {model.Student.Id.ToString()}");
            }
            if (!ModelState.IsValid)
            {
                model.Classes = GetClasses(model.Student.ClassId).ToList();
                return View(model);
            }

            DataRepository.UpdateStudent(model.Student);
            return RedirectToAction("Index", new { selectedClassId = model.Student.ClassId });

        }

        [HttpPost]
        public ActionResult List([DataSourceRequest] DataSourceRequest request, int id)
        {

            return Json(DataRepository.GetStudents(id).OrderByDescending(x => x.Active).ThenBy(x => x.ClassName).ToDataSourceResult(request));
        }
    }
}