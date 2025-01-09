using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc.Filters;
using System.Web.Mvc;

namespace TradeSphereECommerceApp.Areas.ManagerPanel.Filters
{
    public class ManagerAuthorizationFilterAttribute : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.Filters.IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Session["manager"] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/ManagerPanel/Login/Index");
            }
        }
    }
}