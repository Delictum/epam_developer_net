using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace ManagerCloud.MVC.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var path = ConfigurationManager.AppSettings["FileLog"];

            using (var sw = File.AppendText(path))
            {
                sw.WriteLine(string.Join(";", "***\n", filterContext.Exception.Message, filterContext.Exception.StackTrace,
                    filterContext.RouteData.Values["controller"].ToString(),
                    filterContext.RouteData.Values["action"].ToString(), DateTime.Now));
            }

            filterContext.ExceptionHandled = true;
        }
    }
}