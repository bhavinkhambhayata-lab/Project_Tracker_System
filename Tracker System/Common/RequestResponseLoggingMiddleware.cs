using System;
using System.Diagnostics;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Reflection;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Linq;
using ZipERP.Models;
using System.Net.Http;

public class LogActionAttribute : System.Web.Mvc.ActionFilterAttribute
{
    //public override void OnActionExecuting(ActionExecutingContext filterContext)
    //{

    //    var controller = filterContext.RequestContext.RouteData.Values["Controller"];
    //    var action = filterContext.RequestContext.RouteData.Values["Action"];

    //    //
    //    // Perform logging here                                                        
    //    //

    //    base.OnActionExecuting(filterContext);               
    //}
    private static Logger logger = LogManager.GetCurrentClassLogger()   ;                                                            
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        base.OnActionExecuted(filterContext);
        var controllerActionDescriptor = filterContext.ActionDescriptor as ActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            if (controllerActionDescriptor.GetCustomAttributes(typeof(MaintainLogAttribute), true).Any())
            {
                bool isSuccess = false;
                if (filterContext.Result.GetType() == typeof(RedirectToRouteResult))
                {
                    isSuccess = true;
                }
                else if (filterContext.Result.GetType() == typeof(JsonResult))
                {
                    isSuccess = true;
                }
                if (isSuccess)
                {
                    RouteData rd = new RouteData();
                    rd.Values["action"] = filterContext.RouteData.Values["controller"] + "-" + filterContext.RouteData.Values["action"];
                    rd.Values["controller"] = filterContext.RouteData.Values["controller"];
                    DateTime startTime = (DateTime)filterContext.HttpContext.Items["StartTime"];
                    Log("OnActionExecuted", rd, (DateTime.UtcNow - startTime).TotalMilliseconds, "JSON:" + filterContext.HttpContext.Items["ActionParameters"].ToString());
                }
            }
            else if (controllerActionDescriptor.ControllerDescriptor.ControllerName.ToLower().StartsWith("rpt") && HttpContext.Current.Request.HttpMethod == HttpMethod.Post.ToString())
            {
                RouteData rd = new RouteData();
                rd.Values["action"] = filterContext.RouteData.Values["controller"] + "-" + filterContext.RouteData.Values["action"];
                rd.Values["controller"] = "Report";

                DateTime startTime = (DateTime)filterContext.HttpContext.Items["StartTime"];
                Log("OnActionExecuted", rd, (DateTime.UtcNow - startTime).TotalMilliseconds, "JSON:" + filterContext.HttpContext.Items["ActionParameters"].ToString());
            }
        }
    }
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {

        var controllerActionDescriptor = filterContext.ActionDescriptor as ActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            if (controllerActionDescriptor.GetCustomAttributes(typeof(MaintainLogAttribute), true).Any() || (controllerActionDescriptor.ControllerDescriptor.ControllerName.ToLower().StartsWith("rpt") && HttpContext.Current.Request.HttpMethod == HttpMethod.Post.ToString()))
            {
                filterContext.HttpContext.Items["StartTime"] = DateTime.UtcNow;

                string prop = "";
                if (filterContext.ActionParameters != null)
                {
                    foreach (KeyValuePair<string, object> kvp in filterContext.ActionParameters)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(kvp.Value)))
                        {
                            if (kvp.Value.GetType().IsClass)
                            {
                                prop = prop + (prop == "" ? "" : "|") + Newtonsoft.Json.JsonConvert.SerializeObject(kvp.Value);
                            }
                            else
                            {
                                prop = prop + (prop == "" ? "" : "|") + String.Format(" Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                            }
                        }
                    }
                }
                filterContext.HttpContext.Items["ActionParameters"] = prop;
            }
        }
        base.OnActionExecuting(filterContext);
        //Log("OnActionExecuting", filterContext.RouteData, 0, filterContext.HttpContext.Request.InputStream.Length, prop);
    }

    public override void OnResultExecuted(ResultExecutedContext filterContext)
    {
        //base.OnResultExecuted(filterContext);
        //DateTime startTime = (DateTime)filterContext.HttpContext.Items["StartTime"];
        // System.Diagnostics.Debug.WriteLine(filterContext.HttpContext.Items["ActionName"].ToString() + "- (R)" + (DateTime.UtcNow - startTime).TotalMilliseconds);
        // Log("OnResultExecuted", filterContext.RouteData, (DateTime.UtcNow - startTime).TotalMilliseconds, 0, null);

    }

    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {

        //Log("OnResultExecuting ", filterContext.RouteData, 0, filterContext.HttpContext.Request.InputStream.Length, null);
    }

    public void Log(string methodName, RouteData routeData, double Duration = 0, string ActionParam = "", string Exception = "", string StackTrace = "")
    {
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];

        var message = String.Format("{0}", methodName);

        try
        {
            string Version = typeof(LogActionAttribute).Assembly.GetName().Version.ToString();

            //logger.Info(Version);
            logger.WithProperty("StackTrace", StackTrace).WithProperty("Exception", Exception).WithProperty("Duration", "Response Time:" + Duration).WithProperty("Version", Version).WithProperty("EnterpriseId", Convert.ToString(HttpContext.Current.Session["EnterpriseId"])).WithProperty("tenantid", Convert.ToString(HttpContext.Current.Session["TenantId"])).WithProperty("userid", Convert.ToString(HttpContext.Current.Session["UserId"])).WithProperty("Requestquerystring", HttpContext.Current.Request.QueryString).WithProperty("RequestURL", HttpContext.Current.Request.Url.AbsoluteUri).WithProperty("HostName", System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName()).WithProperty("MVCaction", actionName).WithProperty("MVCcontroller", controllerName).WithProperty("Property1", ActionParam).WithProperty("Logger", Convert.ToString(HttpContext.Current.Session["UserId"])).Info(message);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Something bad happened");
        }

    }

}