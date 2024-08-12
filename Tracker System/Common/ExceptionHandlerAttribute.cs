using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ZipERP.Classes;
using ZipERP.Models;

public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    public void OnException(ExceptionContext filterContext)
    {
        LogActionAttribute obj = new LogActionAttribute();
        RouteData rd = new RouteData();
        rd.Values["action"] = filterContext.RouteData.Values["controller"] + "-" + filterContext.RouteData.Values["action"];
        rd.Values["controller"] = "ERROR";
        obj.Log("OnException", rd, 0, "", filterContext.Exception.Message, filterContext.Exception.StackTrace);
        if (!filterContext.ExceptionHandled)
        {
            if (filterContext.Exception != null && filterContext.Exception.Message != null && filterContext.Exception.Message != "Cannot redirect after HTTP headers have been sent.") // Ignore Redirect Method Errors, Occurs from permission check
            {
                clsSysException ObjclsSysException = new clsSysException();
                ObjclsSysException.SetException(filterContext.Exception, "Common");
            }
            filterContext.ExceptionHandled = true;
        }
    }

}
