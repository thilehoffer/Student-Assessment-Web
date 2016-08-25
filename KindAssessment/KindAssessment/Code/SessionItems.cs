using System.Web;
namespace AssessmentApp.WebClient 
{
    public  class SessionItems {
        private static readonly Data.IRepository Repository = Code.Services.DataRepository();

        public static int? TeacherId
        {
            get
            {
                if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                    return null;

                var o = HttpContext.Current.Session["TeacherId"];
                if (o == null)
                {
                    HttpContext.Current.Session["TeacherId"] = Repository.GetTeacher(HttpContext.Current.User.Identity.Name).Id;
                    o = HttpContext.Current.Session["TeacherId"];
                }
                return (int)o;
            }
            set {
                HttpContext.Current.Session["TeacherId"] = value;
            }
        }
    }
}