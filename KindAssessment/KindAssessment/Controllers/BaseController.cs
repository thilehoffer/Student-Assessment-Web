using AssessmentApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentApp.WebClient.Controllers
{
    public class BaseController : Controller
    {
        internal readonly IRepository DataRepository = Code.Services.DataRepository();
    }
}