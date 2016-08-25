using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AssessmentApp.WebClient.Startup))]
namespace AssessmentApp.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
