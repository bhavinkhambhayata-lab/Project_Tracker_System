using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker_System.Classes;
using Tracker_System.Models;

namespace Tracker_System.Controllers
{
    public class ImprestDetailsController : Controller
    {
        // GET: ImprestDetails
        public ActionResult ImprestDetails()
        {
            ImprestDetailsModel ObjImprestDetailsModel = new ImprestDetailsModel();
            clsDivision ObjclsDivision = new clsDivision();
            ClsTrackerSystem ObjClsTrackerSystem =  new ClsTrackerSystem();
             
            ObjImprestDetailsModel.lstDivision = ObjclsDivision.GetAllList();
            ObjImprestDetailsModel.lstCompany = ObjClsTrackerSystem.GetAllList();
            ObjImprestDetailsModel.ImprestDetailsList = ObjClsTrackerSystem.GetAllListNo();
          //  ObjImprestDetailsModel.EmployeeList = ObjClsTrackerSystem.GetAllListEmployee();
            return View(ObjImprestDetailsModel);
        }
        public ActionResult ImprestDetailsTest()
        {
            return View();


        }
        #region Autocomplete
        public JsonResult EmployeeAutoComplete(string EmployeeName)
        {
            DataSet ds = new DataSet();           
            ds = new ClsTrackerSystem().GetAllListEmployee(EmployeeName);

            List<string> lstresult = new List<string>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                lstresult.Add(Common.ConvertDBnullToString(item[0]));
            }
            return Json(lstresult, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
