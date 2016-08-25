using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace AssessmentApp.WebClient.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            Debug.Assert(SessionItems.TeacherId != null, "SessionItems.TeacherId != null");
            if ( !DataRepository.GetTeachersClasses(SessionItems.TeacherId.Value).Any())
            {
               return RedirectToAction("Add", "Classes");
            }
            else {
                ViewBag.Title = "Assessment App";
                var classes = DataRepository.GetTeachersClasses(SessionItems.TeacherId.Value).Where(x => x.Active).OrderBy(x => x.ClassName);
                var model = new Models.HomeIndexViewModel { Classes = classes.Select(s =>
                {
                    Debug.Assert(s.Id != null, "s.Id != null");
                    return new SelectListItem {Value = s.Id.Value.ToString(), Text = s.ClassName};
                }).ToList() };

                return View(model);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       
    }
}