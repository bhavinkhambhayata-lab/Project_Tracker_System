using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker_System.Classes;
using Tracker_System.Models;

namespace Tracker_System.Controllers
{
    public class ImpressController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Impress Tab -> List Tab
        public System.Web.Mvc.ActionResult ImpressListTab()
        {
            ClsImpress clsImpress = new ClsImpress();
            var filterData = clsImpress.GetImpressFilterList();
            return PartialView("_ImpressListTab", filterData);
        }
        public JsonResult GetImpressList(int divisionId, int companyId, string empName, int monthId, int yearId)
        {
            ClsImpress clsImpress = new ClsImpress();
            var list = clsImpress.GetImpressList(divisionId, companyId, empName, monthId, yearId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Impress Tab -> Other Tab -> Export Donwload

        public ActionResult ExportImpressExcel(int divisionId, int companyId, string empName, int monthId, int yearId)
        {

            string fileName = "Imprest_Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

            ClsImpress clsImpressExcel = new ClsImpress();
            var impressData = clsImpressExcel.GetImpressReportData(divisionId, companyId, empName, monthId, yearId);
            var byteData = clsImpressExcel.GenerateImprestExcel(impressData);

            return File(byteData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public System.Web.Mvc.ActionResult GetImpressOtherReportTab()
        {
            ClsImpress clsImpress = new ClsImpress();
            var data = clsImpress.GetImpressOtherTabFilterData();
            data.CompanyId = 6;
            data.ReportType = 1;

            return PartialView("_GetImpressOtherReportTab", data);
        }

        public ActionResult ExportImpressOtherExcel(int reportType, int divisionId, int companyId, int fYearId)
        {
            try
            {
                ClsImpress obj = new ClsImpress();

                byte[] fileBytes = null;
                var fileName = "";

                if (reportType == 1)
                {
                    fileBytes = obj.SubVSPaymentRPT(divisionId, companyId, fYearId);
                    fileName = "Imprest_SubVsPayment_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                }
                else if (reportType == 2)
                {
                    fileName = "Imprest_Submission_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                    fileBytes = obj.SubmissionRPT(fYearId, divisionId, companyId);
                }
                else if (reportType == 3)
                {
                    fileName = "Imprest_Payment_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                    fileBytes = obj.PaymentRPT(fYearId, divisionId, companyId);
                }
                else if (reportType == 4)
                {
                    fileName = "Imprest_PendingPayment_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                    fileBytes = obj.PaymentPendingAllRecordRPT();
                }
                else if (reportType == 5)
                {
                    fileName = "Imprest_SubmmitedPendingPayment_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                    fileBytes = obj.SubmittedInaccountButPendingforPayment_EPPlus();
                }

                if (fileBytes == null || fileBytes.Length == 0)
                    return Content("No Record Found");

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return Content("Error : " + ex.Message);
            }
        }

        #endregion

        #region Impress Tab -> Detail Tab

        #region Get Impress Add Detail Tab
        public ActionResult GetImpressAddDetailsTab()
        {
            ClsImpress clsImpress = new ClsImpress();
            var data = clsImpress.GetImpressAddEditDropDownData();

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            int setMonth;
            int setYear;

            if (currentMonth > 1)
            {
                setMonth = currentMonth - 1;
                setYear = currentYear;
            }
            else
            {
                setMonth = 12;
                setYear = currentYear - 1;
            }


            GetImpressAddModel model = new GetImpressAddModel()
            {
                DivisionList = data.DivisionList,
                CompanyList = data.CompanyList,
                YearList = data.YearList,
                MonthList = data.MonthList,
                No = data.MaxNo,
                CompanyRowID = 6,
                IMonth = setMonth,
                IYear = setYear
            };

            return PartialView("_GetImpressAddDetailsTab", model);
        }

        #endregion

        #region Filter Impress Details

        [HttpGet]
        public JsonResult SearchEmployeeName(int divisionRowId, int companyRowId, string name)
        {
            ClsImpress clsImpress = new ClsImpress();
            var empList = clsImpress.GetAllEmployeeList(divisionRowId, companyRowId, name);

            return Json(empList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEmployeeDetailById(int empRowId)
        {
            ClsImpress clsImpress = new ClsImpress();
            var empDetail = clsImpress.GetEmployeeDetail(empRowId);

            return Json(empDetail, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get PaymentCycle List By Date (dd/MMM/yyyy)
        public ActionResult GetPaymentCycle(string receiptDate)
        {
            ClsImpress clsImpress = new ClsImpress();
            var data = clsImpress.PaymentCycleList(receiptDate);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Save Add and Edit Both Impress Detail
        [HttpPost]
        public JsonResult SaveImpressDetail(SaveImpressModel model)
        {
            try
            {
                // ================= BASIC VALIDATION =================
                if (model == null)
                    return Json(new { success = false, message = "Invalid data" });

                if (model.DivisionRowId <= 0)
                    return Json(new { success = false, message = "Please select Division" });

                if (model.CompanyRowID <= 0)
                    return Json(new { success = false, message = "Please select Company" });

                if (model.EmpRowId <= 0)
                    return Json(new { success = false, message = "Please select Employee" });

                if (model.IMonth <= 0)
                    return Json(new { success = false, message = "Please select Month" });

                if (model.IYear <= 0)
                    return Json(new { success = false, message = "Please select Year" });

                if (model.ClaimedAmount <= 0)
                    return Json(new { success = false, message = "Claimed amount must be greater than zero" });

                // ================= DATE FLOW VALIDATION =================
                if (model.TourFrom.HasValue && model.TourTo.HasValue &&
                    model.TourFrom > model.TourTo)
                {
                    return Json(new { success = false, message = "Tour From date cannot be greater than Tour To date" });
                }

                if (model.IsCommercial && !model.CommercialDate.HasValue)
                    return Json(new { success = false, message = "Please enter Commercial receipt date" });

                if (model.IsCrossCheck && !model.CrossCheckingDate.HasValue)
                    return Json(new { success = false, message = "Please enter Cross Checking date" });

                if (model.IsHeadApproval && !model.HeadForApprovalDate.HasValue)
                    return Json(new { success = false, message = "Please enter Head approval date" });

                if (model.IsDirectorApproval && !model.DirectorForApprovalDate.HasValue)
                    return Json(new { success = false, message = "Please enter Director approval date" });

                if (model.IsSubmitted && !model.SubmittedInAccountsDate.HasValue)
                    return Json(new { success = false, message = "Please enter Submitted date" });

                //if (model.IsConfmSend && !model.ConfirmationSendDate.HasValue)
                //    return Json(new { success = false, message = "Please enter Confirmation send date" });

                if (model.IsPayment && !model.PaymentDate.HasValue)
                    return Json(new { success = false, message = "Please enter Payment date" });

                if (model.IsPayment && model.PaidAmount <= 0)
                    return Json(new { success = false, message = "Please enter Paid Amount" });

                // ================= SAVE =================
                ClsImpress clsImpress = new ClsImpress();
                bool result = clsImpress.SaveImprest(model);

                if (result)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Impress details saved successfully"
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Failed to save Impress details"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region Edit Impress Details Tab
        public System.Web.Mvc.ActionResult GetImpressEditDetailsTab(int rowID)
        {
            ClsImpress clsImpress = new ClsImpress();

            var getEditImpressData = new GetImpressEditModel();
            getEditImpressData = clsImpress.GetEditImpressModel(rowID);

            if (getEditImpressData != null)
            {
                var empDetail = clsImpress.GetEmployeeDetail(getEditImpressData.EmpRowId);
                if (empDetail != null)
                {
                    getEditImpressData.EmployeeName = empDetail.EmpName;
                    getEditImpressData.Designation = empDetail.Designation;
                    getEditImpressData.Zone = empDetail.Zone;
                    getEditImpressData.Region = empDetail.Region;
                }

                if (getEditImpressData.CommercialDate != null)
                {
                    getEditImpressData.PaymentCycleList = clsImpress.PaymentCycleList(getEditImpressData.CommercialDate.Value.ToString("dd/MMM/yyyy"));
                }
            }

            var dropDownList = clsImpress.GetImpressAddEditDropDownData();
            getEditImpressData.DivisionList = dropDownList.DivisionList;
            getEditImpressData.CompanyList = dropDownList.CompanyList;
            getEditImpressData.MonthList = dropDownList.MonthList;
            getEditImpressData.YearList = dropDownList.YearList;



            return PartialView("_GetImpressEditDetailsTab", getEditImpressData);
        }
        #endregion

        #region Delete Impress Detail
        [System.Web.Mvc.HttpPost]
        public JsonResult DeleteImpress(int rowId)
        {
            if (rowId <= 0)
                return Json(new { success = false, message = "Invalid record selected" });

            ClsImpress obj = new ClsImpress();
            bool deleted = obj.DeleteImpress(rowId);

            if (deleted)
            {
                return Json(new
                {
                    success = true,
                    message = "Record deleted successfully"
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "Record not found"
                });
            }
        }
        #endregion

        #endregion

        #region Impress Tab -> Commecial Tab List
        public System.Web.Mvc.ActionResult GetImpressPendingCommercialTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetImpressPendingCommercialTabList");
        }

        #endregion


        #region Impress Tab -> HOD Tab List
        public System.Web.Mvc.ActionResult GetImpressPendingHODApprovalTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetImpressPendingHODApprovalTabList");
        }

        #endregion

        #region Impress Tab -> MKT Head Tab List
        public System.Web.Mvc.ActionResult GetImpressPendingMKTHeadTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetImpressPendingMKTHeadTabList");
        }
        #endregion

        #region Impress Tab -> Directors Approval Tab List
        public System.Web.Mvc.ActionResult GetImpressPendingDirectorsApprovalTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetImpressPendingDirectorsApprovalTabList");
        }

        #endregion

        #region Impess Tab -> Submmited Account Tab List
        public System.Web.Mvc.ActionResult GetImpressPendingSubmittedAccountTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetImpressPendingSubmittedAccountTabList");
        }
        #endregion

        #region Pending Impress Tabs (Commecial Tab,HOD Tab,MKT Tab,Directors Approval Tab,Submmited Account,Confirm Tab)
        public JsonResult GetImpressPendingList(int groupId)
        {
            ClsImpress obj = new ClsImpress();
            var data = obj.GetImpressPendingList_Model(groupId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveImpressRowWise(SaveImpressRequest request)
        {
            try
            {
                if (request == null || request.Rows == null || request.Rows.Count == 0)
                {
                    return Json(new { success = false });
                }

                ClsImpress obj = new ClsImpress();

                obj.SaveImpressRowWise(request.GroupId, request.Rows);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

    }
}