using System.Web;
using System.Web.Mvc;
using Tracker_System.Filters;

namespace Tracker_System
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SessionCheckAttribute());
        }
    }
}
