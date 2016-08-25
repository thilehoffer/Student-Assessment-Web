using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentApp.WebClient.Code
{
    public static class Services
    {
        public static AssessmentApp.Data.IRepository DataRepository() => new Data.Repository();
    }
}