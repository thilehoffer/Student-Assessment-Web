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
        public static readonly string SendGridApiKey = ConfigurationManager.AppSettings["wc:sendGridApiKey"];
        public static readonly string AdminUserEmail = ConfigurationManager.AppSettings["wc:adminUserEmail"];
        public static readonly string AdminUserPassword = ConfigurationManager.AppSettings["wc:adminUserPassword"];
        public static readonly string AdminUserPhoneNumber = ConfigurationManager.AppSettings["wc:adminUserPhoneNumber"];
    }
}