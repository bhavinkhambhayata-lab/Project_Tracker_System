using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Tracker_System.App_Start.DatePickerHelperBundleConfig), "RegisterBundles")]

namespace Tracker_System.App_Start
{
	public class DatePickerHelperBundleConfig
	{
		public static void RegisterBundles()
		{
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
            "~/Scripts/bootstrap-datepicker.js"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/datepicker").Include(
            "~/Content/bootstrap-datepicker.css"));
		}
	}
}