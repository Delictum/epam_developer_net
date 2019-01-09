using System.Web;
using System.Web.Mvc;

namespace ManagerCloud.MVC.Filters
{
    public class LocalAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Request.IsLocal || base.AuthorizeCore(httpContext);
        }
    }
}