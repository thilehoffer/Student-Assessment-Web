using AssessmentApp.WebClient.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentApp.WebClient.Controllers
{
    [Authorize]
    public class ClassesController : BaseController
    {
        

        // GET: Classes
        public ActionResult Add()
        {
            var model = new AddClassViewModel { TeacherClass = new Data.Models.Class { TeacherId = SessionItems.TeacherId.Value },  FirstTime = !DataRepository.GetTeachersClasses(SessionItems.TeacherId.Value).Any() };

            ViewBag.Title = model.FirstTime ? "Let's Get Started... " : string.Empty + "Add New Class";
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AddClassViewModel model) {
            Debug.Assert(SessionItems.TeacherId != null, "SessionItems.TeacherId != null");
            if (DataRepository.CheckIfClassAlreadyExists(SessionItems.TeacherId.Value, model.TeacherClass.ClassName)) {
                ModelState.AddModelError("alreadyExists", $"{model.TeacherClass.ClassName} already exists");
            }
            if (!ModelState.IsValid)
            { 
                return View(model);
            }

            DataRepository.CreateClass(model.TeacherClass);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult List([DataSourceRequest] DataSourceRequest request) {
            return  Json(DataRepository.GetTeachersClasses(SessionItems.TeacherId.Value).OrderByDescending(x => x.Active).ThenBy(x => x.ClassName).ToDataSourceResult(request));
        }


        public ActionResult Index()
        {
            
           //var model = new ClassIndexViewModel { TableItems = DataRepository.GetTeachersClasses(SessionItems.TeacherId.Value).OrderByDescending(x => x.Active).ThenBy(x => x.ClassName).ToList()};
            return View();
        }

        public ActionResult Edit(int id) {
            //*todo add security check 
            var model = DataRepository.GetClass(id);
            if (!DataRepository.IsChild<Data.Models.Class>(SessionItems.TeacherId.Value, model)){
                throw new AccessViolationException($"User does not have access to {id.ToString()}");
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(Data.Models.Class model) {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DataRepository.UpdateClass(model);
            return RedirectToAction("Index");
        }




        //[HttpPost]
        //public ActionResult AddNewClass(string className) {
        //    return Json(new PostResultModel { Success = true });
        //}
    }
}