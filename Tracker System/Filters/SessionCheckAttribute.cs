using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tracker_System.Filters
{
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string action = filterContext.ActionDescriptor.ActionName;

            if (controller == "TrackerSystem" && action == "Login")
                return;

            var session = HttpContext.Current.Session;

            if (session == null || session["EmplRowId"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult
                    {
                        Content = "SessionExpired",
                        ContentType = "text/plain"
                    };
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            controller = "TrackerSystem",
                            action = "Login"
                        })
                    );
                    return;
                }

            }

            base.OnActionExecuting(filterContext);
        }
    }
}