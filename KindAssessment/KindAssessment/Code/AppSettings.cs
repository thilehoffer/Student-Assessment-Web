using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace AssessmentApp.WebClient.Code
{
    public static class AppSettings
    {
        public static readonly string Version = ConfigurationManager.AppSettings["wc:version"];
        public static readonly string SendGridApiKey = string.IsNullOrEmpty(ConfigurationManager.AppSettings["sg:SendGridApiKey"]) ? "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" : ConfigurationManager.AppSettings["sg:SendGridApiKey"];
    }
}