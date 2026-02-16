using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker_System.Classes;
using Tracker_System.Models;

namespace Tracker_System.Controllers
{
    public class TSDController : Controller
    {
        // GET: TSD
        public System.Web.Mvc.ActionResult Index()
        {
            return View();
        }

        #region Get Compnay List Only ICL(0)
        public JsonResult GetCompanyList()
        {
            ClsTSD ObjClsTSD = new ClsTSD();
            return Json(ObjClsTSD.GetCompanyList(0), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get Brand List By Short Name
        public JsonResult GetBrandListByShortName(string ShortName)
        {
            ClsTSD clsTSD = new ClsTSD();
            return Json(clsTSD.GetBrandListByShortName(ShortName), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TSD Tab -> List Tab
        public System.Web.Mvc.ActionResult TSDListTab()
        {
            return PartialView("_TSDListTab");
        }
        public JsonResult GetTSDList(string CompanyName, int BrandRowID, string Name)
        {
            ClsTSD clsTSD = new ClsTSD();
            var list = clsTSD.GetAllTSDList(CompanyName, BrandRowID, Name);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TSD -> List Tab -> Action -> Export To Excel Data

        public System.Web.Mvc.ActionResult ExportTSDExcel(string CompanyName, int BrandRowID, string Name)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            ClsTSD clsTSD = new ClsTSD();
            DataTable TB = clsTSD.GetTSDDetails(CompanyName, BrandRowID, Name);

            using (ExcelPackage package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("TSD Report");

                int Rw = 4;
                int Cl = 1;
                int DataStartRw = 6;

                // ================= TITLE =================
                ws.Cells[1, 1].Value = "Italia Group";
                ws.Cells[2, 1].Value = "CUSTOMER / DEALER TSD REFUND STATUS";

                ws.Cells[1, 1, 1, 24].Merge = true;
                ws.Cells[2, 1, 2, 24].Merge = true;

                ws.Cells[1, 1, 3, 24].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.Font.Size = 14;

                // ================= HEADER =================
                void Merge(int r1, int c1, int r2, int c2)
                {
                    ws.Cells[r1, c1, r2, c2].Merge = true;
                }

                ws.Cells[Rw, Cl].Value = "Company"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Brand"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Region"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Name"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "City"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Regarding"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "TSDAmount"; Merge(Rw, Cl, Rw + 1, Cl++);

                ws.Cells[Rw, Cl].Value = "Commercial"; Merge(Rw, Cl, Rw, Cl + 1);
                ws.Cells[Rw + 1, Cl++].Value = "Receipt Date";
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Sent for Mkt Head's Approval"; Merge(Rw, Cl, Rw, Cl + 1);
                ws.Cells[Rw + 1, Cl++].Value = "Date";
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Sent for Director's Approval"; Merge(Rw, Cl, Rw, Cl + 1);
                ws.Cells[Rw + 1, Cl++].Value = "Date";
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Refund Amount"; Merge(Rw, Cl, Rw + 1, Cl++);

                ws.Cells[Rw, Cl].Value = "Submitted In Account"; Merge(Rw, Cl, Rw, Cl + 1);
                ws.Cells[Rw + 1, Cl++].Value = "Date";
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Payment"; ws.Cells[Rw + 1, Cl++].Value = "Date";
                ws.Cells[Rw, Cl].Value = "Cheque"; ws.Cells[Rw + 1, Cl++].Value = "Number";
                ws.Cells[Rw, Cl].Value = "Payment"; ws.Cells[Rw + 1, Cl++].Value = "Amount";

                ws.Cells[Rw, Cl].Value = "TOTAL DAYS"; Merge(Rw, Cl, Rw + 1, Cl++);

                ws.Cells[Rw, Cl].Value = "CurieredOn"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Formalities Completed On"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Remarks"; Merge(Rw, Cl, Rw + 1, Cl);

                // ================= HEADER STYLING =================
                ws.Cells[4, 1, 5, 24].Style.Font.Bold = true;
                ws.Cells[4, 1, 5, 24].Style.Font.Size = 10;
                ws.Cells[4, 1, 5, 24].Style.WrapText = true;
                ws.Row(4).Height = 42;
                ws.Row(5).Height = 29.25;

                // ================= DATA =================
                foreach (DataRow dr in TB.Rows)
                {
                    int c = 1;

                    ws.Cells[DataStartRw, c++].Value = dr["Company"];
                    ws.Cells[DataStartRw, c++].Value = dr["BrandName"];
                    ws.Cells[DataStartRw, c++].Value = dr["Region"];
                    ws.Cells[DataStartRw, c++].Value = dr["Name"];
                    ws.Cells[DataStartRw, c++].Value = dr["City"];
                    ws.Cells[DataStartRw, c++].Value = dr["Regarding"];
                    ws.Cells[DataStartRw, c++].Value = dr["TSDAmount"];

                    ws.Cells[DataStartRw, c++].Value = dr["CommercialReceiptDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["CommercialReceiptDate"]).ToString("dd/MM/yy");
                    ws.Cells[DataStartRw, c++].Value = dr["CommercialNoOfDays"];

                    ws.Cells[DataStartRw, c++].Value = dr["SendToHeadForApprovalDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["SendToHeadForApprovalDate"]).ToString("dd/MM/yy");
                    ws.Cells[DataStartRw, c++].Value = dr["SendToHeadForApprovalNoOfDays"];

                    ws.Cells[DataStartRw, c++].Value = dr["SendToDirectorForApprovalDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["SendToDirectorForApprovalDate"]).ToString("dd/MM/yy");
                    ws.Cells[DataStartRw, c++].Value = dr["SendToDirectorForApprovalNoOfDays"];

                    ws.Cells[DataStartRw, c++].Value = dr["TotalRefundAmount"];

                    ws.Cells[DataStartRw, c++].Value = dr["SubmittedInAccountsDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["SubmittedInAccountsDate"]).ToString("dd/MM/yy");
                    ws.Cells[DataStartRw, c++].Value = dr["SubmittedInAccountsNoOfDays"];

                    ws.Cells[DataStartRw, c++].Value = dr["PaymentDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["PaymentDate"]).ToString("dd/MM/yy");
                    ws.Cells[DataStartRw, c++].Value = dr["ChequeNumber"];
                    ws.Cells[DataStartRw, c++].Value = dr["PaymentAmount"];
                    ws.Cells[DataStartRw, c++].Value = dr["TotalDays"];

                    ws.Cells[DataStartRw, c++].Value = dr["CurieredOnSentDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["CurieredOnSentDate"]).ToString("dd/MM/yy");
                    ws.Cells[DataStartRw, c++].Value = dr["CurieredOnNoOfDays"];

                    ws.Cells[DataStartRw, c++].Value = dr["CurieredOnConfirmationDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["CurieredOnConfirmationDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, c].Value = dr["Remarks"];

                    DataStartRw++;
                }

                // ================= TOTALS =================
                ws.Cells[DataStartRw, 7].Formula = $"SUM(G6:G{DataStartRw - 1})";
                ws.Cells[DataStartRw, 14].Formula = $"SUM(N6:N{DataStartRw - 1})";
                ws.Cells[DataStartRw, 1, DataStartRw, 24].Style.Font.Bold = true;

                // ================= BORDERS =================
                ws.Cells[4, 1, DataStartRw, 24].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[4, 1, DataStartRw, 24].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[4, 1, DataStartRw, 24].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[4, 1, DataStartRw, 24].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // ================= FREEZE & PAGE =================
                ws.View.FreezePanes(6, 7);

                ws.PrinterSettings.Orientation = eOrientation.Landscape;
                ws.PrinterSettings.TopMargin = 0.05M;
                ws.PrinterSettings.BottomMargin = 0.05M;
                ws.PrinterSettings.LeftMargin = 0.05M;
                ws.PrinterSettings.RightMargin = 0.05M;
                ws.PrinterSettings.RepeatRows = ws.Cells["1:3"];

                ws.Cells.AutoFitColumns();

                byte[] fileBytes = package.GetAsByteArray();
                string fileName = "TSD_Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                return File(
                    fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
        }



        #region Comment Export to Excel Code
        /*
        public System.Web.Mvc.ActionResult ExportTSDExcel(string CompanyName, int BrandRowID, string Name)
        {
            ClsTSD clsTSD = new ClsTSD();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            DataTable TB = clsTSD.GetTSDDetails(CompanyName, BrandRowID, Name);

            using (ExcelPackage package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("TSD Report");

                int Rw = 4;
                int Cl = 1;
                int DataStartRw = 6;

                // 🔹 Titles
                ws.Cells[1, 1].Value = "Italia Group";
                ws.Cells[2, 1].Value = "CUSTOMER / DEALER TSD REFUND STATUS";
                ws.Cells[1, 1, 1, 26].Merge = true;
                ws.Cells[2, 1, 2, 26].Merge = true;
                ws.Cells["A1:A2"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Size = 14;
                ws.Cells["A1:A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // 🔹 Header (Row 4 & 5) – SAME AS VB
                void Merge(int r1, int c1, int r2, int c2) => ws.Cells[r1, c1, r2, c2].Merge = true;

                ws.Cells[Rw, Cl].Value = "Company"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Brand"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Region"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Name"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "City"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Regarding"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "TSDAmount"; Merge(Rw, Cl, Rw + 1, Cl++);

                ws.Cells[Rw, Cl].Value = "Commercial"; Merge(Rw, Cl, Rw, Cl + 1);
                ws.Cells[Rw + 1, Cl++].Value = "Receipt Date";
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Sent to HOD for crosscheck"; Merge(Rw, Cl, Rw, Cl + 1);
                ws.Cells[Rw + 1, Cl++].Value = "Date";
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Sent for Mkt Head's Approval"; Merge(Rw, Cl, Rw, Cl + 1);
                ws.Cells[Rw + 1, Cl++].Value = "Date";
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Sent for Director's Approval"; Merge(Rw, Cl, Rw, Cl + 1);
                ws.Cells[Rw + 1, Cl++].Value = "Date";
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Refund Amount"; Merge(Rw, Cl, Rw + 1, Cl++);

                ws.Cells[Rw, Cl].Value = "Submitted In Account"; Merge(Rw, Cl, Rw, Cl + 1);
                ws.Cells[Rw + 1, Cl++].Value = "Date";
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";

                ws.Cells[Rw, Cl].Value = "Payment Date";
                ws.Cells[Rw + 1, Cl++].Value = "Date";

                ws.Cells[Rw, Cl].Value = "Cheque Number";
                ws.Cells[Rw + 1, Cl++].Value = "Number";

                ws.Cells[Rw, Cl].Value = "Payment Amount";
                ws.Cells[Rw + 1, Cl++].Value = "Amount";

                ws.Cells[Rw, Cl].Value = "TOTAL DAYS"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "CurieredOn"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw + 1, Cl++].Value = "No. of Days";
                ws.Cells[Rw, Cl].Value = "Formalities Completed On"; Merge(Rw, Cl, Rw + 1, Cl++);
                ws.Cells[Rw, Cl].Value = "Remarks"; Merge(Rw, Cl, Rw + 1, Cl);

                // 🔹 Data Binding (Row 6)
                foreach (DataRow dr in TB.Rows)
                {
                    int c = 1;
                    ws.Cells[DataStartRw, c++].Value = dr["Company"];
                    ws.Cells[DataStartRw, c++].Value = dr["BrandName"];
                    ws.Cells[DataStartRw, c++].Value = dr["Region"];
                    ws.Cells[DataStartRw, c++].Value = dr["Name"];
                    ws.Cells[DataStartRw, c++].Value = dr["City"];
                    ws.Cells[DataStartRw, c++].Value = dr["Regarding"];
                    ws.Cells[DataStartRw, c++].Value = dr["TSDAmount"];

                    ws.Cells[DataStartRw, c++].Value = dr["CommercialReceiptDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["CommercialReceiptDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, c++].Value = dr["CommercialNoOfDays"];
                    ws.Cells[DataStartRw, c++].Value = dr["SendToCrossCheckingDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["SendToCrossCheckingDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, c++].Value = dr["SendToCrossCheckingNoOfDays"];
                    ws.Cells[DataStartRw, c++].Value = dr["SendToHeadForApprovalDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["SendToHeadForApprovalDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, c++].Value = dr["SendToHeadForApprovalNoOfDays"];
                    ws.Cells[DataStartRw, c++].Value = dr["SendToDirectorForApprovalDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["SendToDirectorForApprovalDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, c++].Value = dr["SendToDirectorForApprovalNoOfDays"];
                    ws.Cells[DataStartRw, c++].Value = dr["TotalRefundAmount"];
                    ws.Cells[DataStartRw, c++].Value = dr["SubmittedInAccountsDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["SubmittedInAccountsDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, c++].Value = dr["SubmittedInAccountsNoOfDays"];
                    ws.Cells[DataStartRw, c++].Value = dr["PaymentDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["PaymentDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, c++].Value = dr["ChequeNumber"];
                    ws.Cells[DataStartRw, c++].Value = dr["PaymentAmount"];
                    ws.Cells[DataStartRw, c++].Value = dr["TotalDays"];
                    ws.Cells[DataStartRw, c++].Value = dr["CurieredOnSentDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["CurieredOnSentDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, c++].Value = dr["CurieredOnNoOfDays"];
                    ws.Cells[DataStartRw, c++].Value = dr["CurieredOnConfirmationDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["CurieredOnConfirmationDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, c].Value = dr["Remarks"];

                    DataStartRw++;
                }

                // 🔹 Totals
                ws.Cells[DataStartRw, 7].Formula = $"SUM(G6:G{DataStartRw - 1})";
                ws.Cells[DataStartRw, 16].Formula = $"SUM(P6:P{DataStartRw - 1})";
                ws.Cells[DataStartRw, 1, DataStartRw, 26].Style.Font.Bold = true;

                ws.Cells[4, 1, DataStartRw, 26].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                ws.Cells.AutoFitColumns();
                ws.View.FreezePanes(6, 7);

                byte[] fileBytes = package.GetAsByteArray();


                return File(fileBytes,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","TSD_Report.xlsx"
                );
            }
        }
        */
        #endregion

        #endregion

        #region TSD -> Other Report Tab

        public System.Web.Mvc.ActionResult GetTSDOtherReportTab()
        {
            ClsTSD ObjClsTSD = new ClsTSD();
            var companyList = ObjClsTSD.GetCompanyList(0);

            GetTSDOtherReportModel getTSDOtherReportModel = new GetTSDOtherReportModel();
            getTSDOtherReportModel.GetCompanyList = companyList;

            getTSDOtherReportModel.GetBrandList = ObjClsTSD.GetBrandListByShortName(companyList.FirstOrDefault().ShortName);
            return PartialView("_GetTSDOtherReportTab", getTSDOtherReportModel);
        }

        [System.Web.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult ExportTSDOtherReport(string Company, string Brand, string Name, int ReportType)
        {
            if (ReportType == 1)
            {
                ClsTSD clsTSD = new ClsTSD();

                var tsdList = clsTSD.GetRegisterTSDReport(Company, Brand, Name, false);

                if (tsdList == null || tsdList.Count == 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "No record found"
                    }, JsonRequestBehavior.AllowGet);
                }

                byte[] excelBytes = clsTSD.GenerateRegisterTSDExcel(tsdList, false);

                string fileName = "Register_TSD_Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                return Json(new
                {
                    status = true,
                    fileName = fileName,
                    fileData = Convert.ToBase64String(excelBytes)
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ClsTSD clsTSD = new ClsTSD();

                byte[] excelBytes = clsTSD.ExportTSDExcel(
                    Company, Brand, Name);

                if (excelBytes == null || excelBytes.Length == 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "No record found"
                    }, JsonRequestBehavior.AllowGet);
                }

                string fileName = "Register_TSD_Pending_Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                return Json(new
                {
                    status = true,
                    fileName = fileName,
                    fileData = Convert.ToBase64String(excelBytes)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region TSD Tab -> Commecial Tab List
        public System.Web.Mvc.ActionResult GetTSDPendingCommercialTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetTSDPendingCommercialTabList");
        }

        #endregion

        #region TSD Tab -> HOD Tab List
        public System.Web.Mvc.ActionResult GetTSDPendingHODApprovalTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetTSDPendingHODApprovalTabList");
        }

        #endregion

        #region TSD Tab -> MKT Head Tab List
        public System.Web.Mvc.ActionResult GetTSDPendingMKTHeadTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetTSDPendingMKTHeadTabList");
        }
        #endregion

        #region TSD Tab -> Directors Approval Tab List
        public System.Web.Mvc.ActionResult GetTSDPendingDirectorsApprovalTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetTSDPendingDirectorsApprovalTabList");
        }

        #endregion

        #region TSD Tab -> Submmited Account Tab List
        public System.Web.Mvc.ActionResult GetTSDPendingSubmittedAccountTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetTSDPendingSubmittedAccountTabList");
        }
        #endregion

        #region TSD Tab -> Payment Tab
        public System.Web.Mvc.ActionResult GetTSDPendingPaymentTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetTSDPendingPaymentTabList");
        }
        #endregion

        #region TSD Tab -> Couriered Tab List
        public System.Web.Mvc.ActionResult GetTSDPendingCourieredTabList(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            return PartialView("_GetTSDPendingCourieredTabList");
        }
        #endregion

        #region Pending Row Wise Save Data in TSD (Commecial Tab,HOD Tab,MKT Tab,Directors Approval Tab,Submmited Account,Payment Tab,Courired Tab)
        public JsonResult GetTSDPendingList(int groupId)
        {
            ClsTSD obj = new ClsTSD();
            var data = obj.GetTSDPendingList_Model(groupId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveTSDRowWise(SaveTSDRequest request)
        {
            try
            {
                if (request == null || request.Rows == null || request.Rows.Count == 0)
                {
                    return Json(new { success = false });
                }

                ClsTSD obj = new ClsTSD();

                obj.SaveTSDRowWise(request.GroupId, request.Rows);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // optional: log error
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region TSD -> Details Tab

        public System.Web.Mvc.ActionResult GetTSDDetailsTab()
        {
            ClsTSD clsTSD = new ClsTSD();

            var companyList = clsTSD.GetCompanyList(0);


            var data = new TSDDetailsViewModel();
            data.CompanyList = companyList;
            data.BrandList = clsTSD.GetBrandListByShortName(companyList.FirstOrDefault().ShortName);
            data.No = clsTSD.GetMaxNumber();
            return PartialView("_GetTSDDetailsTab", data);
        }

        #region Search Name Only Customer and Get Details
        public JsonResult SearchNameOnlyCustomer(int companyAndBrandRowId, string name)
        {
            ClsTSD obj = new ClsTSD();

            var list = obj.SearchCustomerName(companyAndBrandRowId, name);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNameDetailsOnlyCustomer(int companyAndBrandRowId, string name, string masterCode)
        {
            ClsTSD obj = new ClsTSD();

            var model = obj.GetCustomerDetails(companyAndBrandRowId, name, masterCode);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Save TSD Detail Add and Edit
        [System.Web.Mvc.HttpPost]
        public JsonResult SaveTSDDetail(SaveTSDDetailModel model)
        {
            // ================= NULL CHECK =================
            if (model == null)
                return Json(new { success = false, message = "Invalid request data" });

            // ================= BASIC VALIDATIONS =================
            if (model.CompanyID == null || model.CompanyID == 0)
                return Json(new { success = false, message = "Please select Company" });

            if (model.BrandID == null || model.BrandID == 0)
                return Json(new { success = false, message = "Please select Brand" });

            if (string.IsNullOrWhiteSpace(model.Name))
                return Json(new { success = false, message = "Please enter Name" });

            if (model.TSDAmount <= 0)
                return Json(new { success = false, message = "TSD Amount must be greater than zero" });

            // ================= COMMERCIAL =================
            if (model.IsCommercial)
            {
                if (model.CommercialReceiptDate == null)
                    return Json(new { success = false, message = "Please select Commercial Receipt Date" });
            }

            // ================= MKT =================
            if (model.IsMkt)
            {
                if (model.MktReceiptDate == null)
                    return Json(new { success = false, message = "Please select MKT Receipt Date" });

                if (model.MktReceiptDate < model.CommercialReceiptDate)
                    return Json(new { success = false, message = "MKT date cannot be before Commercial date" });
            }

            // ================= DIRECTOR =================
            if (model.IsDirector)
            {
                if (model.DirectorReceiptDate == null)
                    return Json(new { success = false, message = "Please select CMO Receipt Date" });

                if (model.DirectorReceiptDate < model.MktReceiptDate)
                    return Json(new { success = false, message = "CMO date cannot be before MKT date" });
            }

            // ================= SUBMITTED =================
            if (model.IsSubmitted)
            {
                if (model.SubmittedDate == null)
                    return Json(new { success = false, message = "Please select Submitted Date" });

                if (model.SubmittedDate < model.DirectorReceiptDate)
                    return Json(new { success = false, message = "Submitted date cannot be before Director date" });

                // Refund Amount mandatory at Submitted stage
                if (model.TotalRefundAmount <= 0)
                    return Json(new { success = false, message = "Please enter valid Refund Amount" });
            }

            // ================= PAYMENT =================
            if (model.IsPayment)
            {
                if (model.PaymentDate == null)
                    return Json(new { success = false, message = "Please select Payment Date" });

                if (model.PaymentDate < model.SubmittedDate)
                    return Json(new { success = false, message = "Payment date cannot be before Submitted date" });

                if (string.IsNullOrWhiteSpace(model.ChequeNumber))
                    return Json(new { success = false, message = "Please enter Cheque Number" });

                if (model.PaymentAmount <= 0)
                    return Json(new { success = false, message = "Please enter valid Payment Amount" });
            }

            // ================= COURIER =================
            if (model.IsCourier)
            {
                if (model.CourierSentDate == null)
                    return Json(new { success = false, message = "Please select Courier Sent Date" });

                if (model.CourierSentDate < model.PaymentDate)
                    return Json(new { success = false, message = "Courier date cannot be before Payment date" });
            }

            // ================= CONFIRMATION =================
            if (model.IsConfirmation)
            {
                if (model.ConfirmationReceiptDate == null)
                    return Json(new { success = false, message = "Please select Confirmation Receipt Date" });

                if (model.ConfirmationReceiptDate < model.CourierSentDate)
                    return Json(new { success = false, message = "Confirmation date cannot be before Courier date" });
            }

            try
            {
                ClsTSD clsTSD = new ClsTSD();

                // ================= SAVE / UPDATE =================
                clsTSD.SaveTSD(model);

                return Json(new
                {
                    success = true,
                    message = "TSD details saved successfully"
                });
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

        #region Delete TSD Detail
        [System.Web.Mvc.HttpPost]
        public JsonResult DeleteTSD(int rowId)
        {
            if (rowId <= 0)
                return Json(new { success = false, message = "Invalid record selected" });

            ClsTSD obj = new ClsTSD();
            bool deleted = obj.DeleteTSD(rowId);

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

        #region Edit TSD Details
        public System.Web.Mvc.ActionResult GetEditTSDDetailsTab(int rowID)
        {
            ClsTSD clsTSD = new ClsTSD();

            var companyList = clsTSD.GetCompanyList(1);

            var data = clsTSD.GetEditTSDDetails(rowID);

            data.CompanyList = companyList;
            data.BrandList = clsTSD.GetBrandListByShortName(data.CompanyName);

            return PartialView("_GetEditTSDDetailsTab", data);
        }
        #endregion

        #endregion
    }
}