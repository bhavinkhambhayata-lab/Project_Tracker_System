using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Tracker_System.Classes;
using Tracker_System.Models;

namespace Tracker_System.Controllers
{
    public class FreightTrackerController : Controller
    {private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        // GET: FreightTracker
        [HttpGet]
        public ActionResult CreateFreightTracker(string TypeOfSales = "", int Month = 0, int Year = 0, bool ShowAllFT = false, string selectedValue = "")
        {
            if (Common.ConvertDBnullToString(Session["EmployeeRowId"]) == "")
                return RedirectToAction("Login", "TrackerSystem");
            FreightTracker ObjFT = new FreightTracker();
            ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
            ObjFT.lstFyear = ObjClsFreightTracker.GetAllFyear();
            DateTime currentDate = DateTime.Now;
            int currentFY = currentDate.Month >= 4 ? currentDate.Year : currentDate.Year - 1;
            ObjFT.Year = Year > 0 ? Year.ToString() : currentFY.ToString();

            if (!string.IsNullOrEmpty(TypeOfSales))
                ViewBag.TypeOfSales = TypeOfSales;
            else
                ViewBag.TypeOfSales = "";
            if (Month > 0)
                ViewBag.Month = Month;
            else
                ViewBag.Month = currentDate.Month;
            if (Year > 0)
                ViewBag.Year = ObjFT.Year; 
            else
                ViewBag.Year = ObjFT.Year; 
            if (ShowAllFT)
                ViewBag.ShowAllFT = ShowAllFT;
            else
                ViewBag.ShowAllFT = false;
            Session["FreightValue"] = selectedValue;
            var searchTypes = new List<SelectListItem>
                    {


                        new SelectListItem { Text = "Pending", Value = "Pending", Selected = true },
                      new SelectListItem { Text = "Completed", Value = "Completed" },
                      new SelectListItem { Text = "All", Value = "All" },
                                           
                    };
            ViewBag.SearchTypes = searchTypes;
            return View(ObjFT);

        }

        public ActionResult CreateTDFreightTracker(string TypeOfSales = "", int Month = 0, int Year = 0, bool ShowAllFT = false, string selectedValue = "")
        {
            if (Common.ConvertDBnullToString(Session["EmployeeRowId"]) == "")
                return RedirectToAction("Login", "TrackerSystem");
            FreightTracker ObjFT = new FreightTracker();
            ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
            ObjFT.lstFyear = ObjClsFreightTracker.GetAllFyear();
            DateTime currentDate = DateTime.Now;
            int currentFY = currentDate.Month >= 4 ? currentDate.Year : currentDate.Year - 1;
            ObjFT.Year = Year > 0 ? Year.ToString() : currentFY.ToString();

            if (!string.IsNullOrEmpty(TypeOfSales))
                ViewBag.TypeOfSales = TypeOfSales;
            else
                ViewBag.TypeOfSales = "";
            if (Month > 0)
                ViewBag.Month = Month;
            else
                ViewBag.Month = currentDate.Month;
            if (Year > 0)
                ViewBag.Year = ObjFT.Year;
            else
                ViewBag.Year = ObjFT.Year;
            if (ShowAllFT)
                ViewBag.ShowAllFT = ShowAllFT;
            else
                ViewBag.ShowAllFT = false;
            Session["FreightValue"] = selectedValue;
            var searchTypes = new List<SelectListItem>
                    {


                        new SelectListItem { Text = "Pending", Value = "Pending", Selected = true },
                      new SelectListItem { Text = "Completed", Value = "Completed" },
                         new SelectListItem { Text = "All", Value = "All" },

                    };
            ViewBag.SearchTypes = searchTypes;
            return View(ObjFT);

        }

       
        public ActionResult GetAllListFM(int PageSize = 0)
        {
            ClsFreightTracker ObjFTT = new ClsFreightTracker();
            ModelState.Clear();
          return View(ObjFTT.GetAllListForMosaic(PageSize));
        }
        public ActionResult GetAllListFT(int PageSize = 0)
        {
            ClsFreightTracker ObjFTT = new ClsFreightTracker();
            ModelState.Clear();
            return View(ObjFTT.GetAllListForTile(PageSize));
        }

        #region Details Tab Upadte Invoice
        // [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public JsonResult DetailsUpadteInvoice(string InvoiceNo, string FreightBillRecdONDate, string FreightBillNo, string BillDate, string ShownSepONInvoice, string DebitNoteToBeRaised, string DebitAdviseNo, string FreightBillForwardedDate, string SentForHODApprovalDate, string ForwardedONDate, string Remarks, string PaymentChecqueRecdONDate, string PaymentSentONDate)
        {
            bool Status = false;
            try
            {
                ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
                if (!string.IsNullOrEmpty(FreightBillRecdONDate) || !string.IsNullOrEmpty(FreightBillNo) || !string.IsNullOrEmpty(BillDate) || !string.IsNullOrEmpty(FreightBillForwardedDate) || !string.IsNullOrEmpty(ShownSepONInvoice) || !string.IsNullOrEmpty(DebitNoteToBeRaised) || !string.IsNullOrEmpty(DebitAdviseNo) || !string.IsNullOrEmpty(SentForHODApprovalDate) || !string.IsNullOrEmpty(ForwardedONDate) || !string.IsNullOrEmpty(Remarks) || !string.IsNullOrEmpty(PaymentChecqueRecdONDate) || !string.IsNullOrEmpty(PaymentSentONDate))
                {
                    Status = ObjClsFreightTracker.GetDataDetailsUpadteInvoice(InvoiceNo, FreightBillRecdONDate, FreightBillNo, BillDate, ShownSepONInvoice, DebitNoteToBeRaised, DebitAdviseNo, FreightBillForwardedDate, SentForHODApprovalDate, ForwardedONDate, Remarks, PaymentChecqueRecdONDate, PaymentSentONDate);                    
                }
                if (Status == true)
                {
                    return Json(new { Status = Status, MSG = "Upadte Recode" });
                }
                else
                {
                    return Json(new { Status = Status, MSG = " Not Upadte Recode" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = 0, MSG = "" });
            }
        }
        #endregion
        #region Factory Tab Data Updated
        //[AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public JsonResult GetDataUpadateFactory(List<FreightTracker> rowsData)
        {
            bool Status = false;
            if (rowsData != null && rowsData.Count > 0)
            {
                try
                {
                    ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
                    for (int i = 0; i < rowsData.Count; i++)
                    {
                        var row = rowsData[i];
                        var Doc_No = row.Doc_No;
                        var FreightBillRecdON = row.FreightBillRecdON;
                        var FreightBillNo = row.FreightBillNo;
                        var BillDate = row.BillDate;
                        var ActualFreightAmt = row.ActualFreightAmt;
                        var Remarks = row.Remarks;
                        var FreightBillForwarded = row.FreightBillForwarded;
                        Status = ObjClsFreightTracker.GetDataUpadateFactoryHO(Doc_No, FreightBillRecdON, FreightBillNo, BillDate, ActualFreightAmt, FreightBillForwarded, Remarks);
                    }
                    if (Status == true)
                    {
                        return Json(new { Status = Status, MSG = "Records updated successfully" });
                    }
                    else
                    {
                        return Json(new { Status = false, MSG = "Error updating records!" });
                    }                   
                }
                catch (Exception ex)
                {
                    return Json(new { returnVal = false, MSG = $"Error updating records: {ex.Message}" });
                }
            }
            else
            {
                return Json(new { returnVal = false, MSG = "No data received" });
            }
        }
        #endregion
        #region HO Tab Data Updated
        //[AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public JsonResult GetDataUpadateHO(List<FreightTracker> rowsData)
        {
            bool returnVal = false;
            ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
            if (rowsData != null && rowsData.Count > 0)
            {
                try
                {
                    for (int i = 0; i < rowsData.Count; i++)
                    {
                        var row = rowsData[i];
                        var Doc_No = row.Doc_No;
                        var FreightBillRecdON = row.FreightBillRecdON;
                        var FreightBillNo = row.FreightBillNo;
                        var BillDate = row.BillDate;
                        var ActualFreightAmt = row.ActualFreightAmt;
                        var Remarks = row.Remarks;
                        var FreightBillForwarded = row.FreightBillForwarded;
                        returnVal = ObjClsFreightTracker.GetDataUpadateFactoryHO(Doc_No, FreightBillRecdON, FreightBillNo, BillDate, ActualFreightAmt, FreightBillForwarded, Remarks);
                    }
                    if (returnVal == true)
                    {
                        return Json(new { returnVal = returnVal, MSG = "Records updated successfully!", Status = true});
                    }
                    else
                    {
                        return Json(new { returnVal = false, MSG = "Error updating records!", Status = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { returnVal = false, MSG = $"Error updating records: {ex.Message}" });
                }
            }
            else
            {
                return Json(new { returnVal = false, MSG = "No data received" });
            }
        }
        #endregion
        #region Commercial Tab Data Updated
        [HttpPost]
        public JsonResult GetDataUpadateCommercial(List<FreightTracker> rowsDataComm)
        {
            bool returnVal = false;
            ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
            if (rowsDataComm != null && rowsDataComm.Count > 0)
            {
                try
                {
                    foreach (var row in rowsDataComm)
                    {
                        var Doc_No = row.Doc_No;
                        var SentForHODApproval = row.SentForHODApproval;
                        var Remarks = row.Remarks;
                        returnVal = ObjClsFreightTracker.GetDataUpadateCommercial(Doc_No, SentForHODApproval, Remarks);
                    }
                    if (returnVal == true)
                    {
                        return Json(new { returnVal = returnVal, MSG = "Records updated successfully!" });
                    }
                    else
                    {
                        return Json(new { returnVal = false, MSG = "Error updating records!" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { returnVal = false, MSG = $"Error updating records: {ex.Message}" });
                }
            }
            else
            {
                return Json(new { returnVal = false, MSG = "No data received" });
            }
        }
        #endregion
        #region HOApproval Tab Update Data
        [HttpPost]
        public JsonResult GetDataUpadateHOApproval(List<FreightTracker> rowsDataHOApp)
        {
            bool returnVal = false;
            bool UpadteAmount = false;
            ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
            if (rowsDataHOApp != null && rowsDataHOApp.Count > 0)
            {
                try
                {
                    foreach (var row in rowsDataHOApp)
                    {
                        var Doc_No = row.Doc_No;
                        var ApprovalForwardedON = row.ApprovalForwardedON;
                        var Remarks = row.Remarks;
                        var IsActive = row.IsActive;
                        returnVal = ObjClsFreightTracker.GetDataUpadateHOApproval(Doc_No, ApprovalForwardedON, Remarks, IsActive);
                        //if (returnVal == true)
                        //{
                        //   UpadteAmount = ObjClsFreightTracker.GetDataUpadateHOApprovalERP(Doc_No);
                        //}                   
                    }
                    if (returnVal == true)
                    {
                        return Json(new { returnVal = returnVal, MSG = "Records updated successfully!" });
                    }
                    else
                    {
                        return Json(new { returnVal = false, MSG = "Error updating records!" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { returnVal = false, MSG = $"Error updating records: {ex.Message}" });
                }
            }
            else
            {
                return Json(new { returnVal = false, MSG = "No data received" });
            }
        }
        #endregion

        #region Details Tab Delete Invoice
        [HttpPost]
        public JsonResult DetailsDeleteInvoice(string InvoiceNo)
        {
            bool returnVal = false;
            try
            {
                ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
                if (InvoiceNo != null)
                {
                    returnVal = ObjClsFreightTracker.GetDataDetailsDeleteInvoice(InvoiceNo);
                    if (returnVal == true)
                    {
                        return Json(new { returnVal = returnVal, MSG = "Delete Recode" });
                    }
                    else
                    {
                        return Json(new { returnVal = 0, MSG = " Not Delete Recode" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = 0, MSG = "" });
            }
            return Json("");
        }
        #endregion
        #region Commercial Tab Sent Mail
        [HttpPost]
        public JsonResult GetDataCommercialSendMail(List<FreightTracker> rowsDataSentmail)
        {
            string InvoiceNo = "";
            string checkApproval = "";
            bool returnVal = false;
            DataTable DT = new DataTable();
            try
            {
                ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
                foreach (var row in rowsDataSentmail)
                {
                    InvoiceNo = row.Doc_No;
                    var SentForHODApproval = row.SentForHODApproval;
                    var Remarks = row.Remarks;
                    string DATA = Common.ExecuteScalarByQuery("SELECT InvoiceNo  from StorageManagerNew.dbo.AL_FT_SendforApproval WHERE InvoiceNo = '" + InvoiceNo + "' group by InvoiceNo");
                    if (!string.IsNullOrEmpty(DATA))
                    {
                        if (InvoiceNo != null)
                        {
                            sendMail_Approval(InvoiceNo, 123);
                            return Json(new { returnVal = true, MSG = "Already Send Mail . InvoiceNo:" + InvoiceNo });
                            //returnVal = ObjClsFreightTracker.GetDataCommercialSendMail(InvoiceNo);
                        }
                        else
                        {
                            if (checkApproval == "Yes")
                            {
                                sendMail_Approval(InvoiceNo, 123);
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        if (checkApproval == "Yes")
                        {
                            sendMail_Approval(InvoiceNo, 123);
                        }
                        else
                        {
                            sendMail_Approval(InvoiceNo, 123);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { returnVal = 0, MSG = "" });
            }
            return Json("");
        }
        public ActionResult sendMail_Approval(string InvoiceNo = "", int Userid = 0)
        {
            string Too;
            string EToo;
            var CC = default(string);
            var BCC = default(string);
            var ReportTo = default(string);
            var DDataStr = default(string);
            var FROM = default(string);
            var Body = default(string);
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            DataTable DTT = new DataTable();
            DataTable DDTT = new DataTable();
            DataTable SFA = new DataTable();
            string EmpRowId = Common.ConvertDBnullToString(Session["EmpRowId"]);
            string UserId = Common.ExecuteScalarByQuery("SELECT RowID  from HRMS.dbo.Master_EmployeeMaster WHERE RowId = '" + EmpRowId + "' group by InvoiceNo");
            string Cust_Name, inv_date, Sales_personName, salespersoncode, brand, Region, typeofsale, freightbasis, Invoice_Amt, freightBronby, qtySQM, lRRRdate, IRRRno, Transporter_Name, Net_Rlsn_Per_SQM, freightAMT, freightbillno, BillDate, freightbillrecodeON, actualfreightAMT, FreightBillForwarded, ShownSepONInvoice, DebitNoteToBeRaised, DebitAdviseNo, SentForHODApproval, ApprovalForwardedON, Remarks, PaymentChecqueRecdON, PaymentAdviseSentON, TotalWeight, EstimatedFreightAmt, NoOfBoxes, WeightKgsPerSQM, QuantitySQM, ModeOfTransport, PI_NO, ApproxfreightRate, ActualFreightRate, ApproxEffectiveDiscount, ActualEffectiveDiscount, Diff_FreightRate, Diff_FreightAmt, Diff_EffectiveDiscount, UserFromId, UserToId, SFARowid, PerDiffFreightRate_1, PerDiffFreightAmt_1, PerDiffEffDisc_1, PerDiffFreightRate_2, PerDiffFreightAmt_2, PerDiffEffDisc_2, sendtoName, sendtosurname, RateRsPerKg, DeliveryCharges;
            ClsFreightTracker ObjClsFT = new ClsFreightTracker();
            DS = ObjClsFT.GetDataApprovalSendMailDT(InvoiceNo);
            DT = DS.Tables[0];
            if (DT.Rows.Count > 0)
            {
                {
                    var withBlock = DT.Rows[0];
                    inv_date = withBlock["Doc_Date"].ToString();
                    Cust_Name = withBlock["Cust_Name"].ToString();
                    Sales_personName = withBlock["Salesperson_Name"].ToString();
                    salespersoncode = withBlock["SalesPersionCode"].ToString();
                    brand = withBlock["Brand"].ToString();
                    typeofsale = withBlock["Promotional_Sample_Sale"].ToString();
                    freightbasis = withBlock["Freight_Basis"].ToString();
                    freightBronby = withBlock["Freight_Paid_By"].ToString();
                    qtySQM = withBlock["Qnty_SQM"].ToString();
                    Region = withBlock["Region"].ToString();
                    Transporter_Name = withBlock["Transporter_Name"].ToString();
                    Net_Rlsn_Per_SQM = withBlock["Net_Rlsn_Per_SQM"].ToString();
                    Invoice_Amt = withBlock["Invoice_Amt"].ToString();
                    lRRRdate = withBlock["LR_RR_Date"].ToString();
                    IRRRno = withBlock["LR_RR_No"].ToString();
                    freightAMT = withBlock["Freight_Amt"].ToString();
                    freightbillno = withBlock["FreightBillNo"].ToString();
                    BillDate = withBlock["BillDate"].ToString();
                    freightbillrecodeON = withBlock["FreightBillRecdON"].ToString();
                    FreightBillForwarded = withBlock["FreightBillForwarded"].ToString();
                    actualfreightAMT = withBlock["ActualFreightAmt"].ToString();
                    ShownSepONInvoice = withBlock["ShownSepONInvoice"].ToString();
                    DebitNoteToBeRaised = withBlock["DebitNoteToBeRaised"].ToString();
                    DebitAdviseNo = withBlock["DebitAdviseNo"].ToString();
                    SentForHODApproval = withBlock["SentForHODApproval"].ToString();
                    ApprovalForwardedON = withBlock["ApprovalForwardedON"].ToString();
                    Remarks = withBlock["Remarks"].ToString();
                    PaymentChecqueRecdON = withBlock["PaymentChecqueRecdON"].ToString();
                    PaymentAdviseSentON = withBlock["PaymentAdviseSentON"].ToString();
                    TotalWeight = withBlock["TotalWeight"].ToString();
                    EstimatedFreightAmt = withBlock["EstimatedFreightAmt"].ToString();
                    NoOfBoxes = withBlock["NoOfBoxes"].ToString();
                    WeightKgsPerSQM = withBlock["WeightKgsPerSQM"].ToString();
                    QuantitySQM = withBlock["Qnty_SQM"].ToString();
                    ModeOfTransport = withBlock["ModeOfTransport"].ToString();
                    PI_NO = withBlock["PI_NO"].ToString();
                    RateRsPerKg = withBlock["RateRsPerKg"].ToString();
                    DeliveryCharges = withBlock["DeliveryCharges"].ToString();
                    if (string.IsNullOrEmpty(PI_NO))
                    {
                        ApproxfreightRate = "N.A.";
                        ActualFreightRate = "N.A.";
                        ApproxEffectiveDiscount = "N.A.";
                        ActualEffectiveDiscount = "N.A.";
                        Diff_FreightRate = "N.A.";
                        Diff_FreightAmt = "N.A.";
                        Diff_EffectiveDiscount = "N.A.";

                        PerDiffFreightRate_1 = "N.A.";
                        PerDiffFreightAmt_1 = "N.A.";
                        PerDiffEffDisc_1 = "N.A.";
                        PerDiffFreightRate_2 = "N.A.";
                        PerDiffFreightAmt_2 = "N.A.";
                        PerDiffEffDisc_2 = "N.A.";
                    }
                    else
                    {
                        ApproxfreightRate = withBlock["ApproxfreightRate"].ToString();
                        ActualFreightRate = withBlock["ActualFreightRate"].ToString();
                        ApproxEffectiveDiscount = withBlock["ApproxEffectiveDiscount"].ToString();
                        ActualEffectiveDiscount = withBlock["ActualEffectiveDiscount"].ToString();
                        Diff_FreightRate = withBlock["Diff_FreightRate"].ToString();
                        Diff_FreightAmt = withBlock["Diff_FreightAmt"].ToString();
                        Diff_EffectiveDiscount = withBlock["Diff_EffectiveDiscount"].ToString();

                        PerDiffFreightRate_1 = withBlock["PerDiffFreightRate_1"].ToString();
                        PerDiffFreightAmt_1 = withBlock["PerDiffFreightAmt_1"].ToString();
                        PerDiffEffDisc_1 = withBlock["PerDiffEffDisc_1"].ToString();
                        PerDiffFreightRate_2 = withBlock["PerDiffFreightRate_2"].ToString();
                        PerDiffFreightAmt_2 = withBlock["PerDiffFreightAmt_2"].ToString();
                        PerDiffEffDisc_2 = withBlock["PerDiffEffDisc_2"].ToString();
                    }
                    DTT = ObjClsFT.GetDataApprovalSendMailDTT(salespersoncode);
                    if (DTT.Rows.Count > 0)
                    {
                        {

                            var withBlock1 = DTT.Rows[0];
                            ReportTo = withBlock1["ReportingtoNewID"].ToString();
                            string query = "SELECT TOP 1 MailID_Office, EmpFirstName, EmpSurname FROM HRMS.dbo.Master_EmployeeMaster WHERE newID = @SalespersonCode";

                            DataTable MailDetail = Common.ExecuteDataTableByQuery(query, salespersoncode);

                            if (MailDetail.Rows.Count > 0)
                            {
                                {
                                    var withBlock2 = MailDetail.Rows[0];
                                    EToo = withBlock2["MailID_Office"].ToString();
                                    sendtoName = withBlock2["EmpFirstName"].ToString();
                                    sendtosurname = withBlock2["EmpSurname"].ToString();
                                    // Try
                                    string html = string.Empty;
                                    var Smtp_Server = new SmtpClient();
                                    var e_mail = new MailMessage();
                                    /*Smtp_Server.UseDefaultCredentials = false;
                                    Smtp_Server.Credentials = new System.Net.NetworkCredential("auto.mail@italiagroup.in", "jifv gupy kcuw wyde");
                                    Smtp_Server.Port = 587;
                                    Smtp_Server.EnableSsl = true;
                                    Smtp_Server.Host = "smtp.gmail.com";*/
                                    //Too = ("ajay.makwana@italiagroup.in");
                                    FROM = "auto.mail@italiagroup.in";
                                    CC = "";
                                    BCC = "";
                                    e_mail = new MailMessage();
                                    e_mail.From = new MailAddress(FROM.ToString());

                                    // e_mail.To.Add(Too);
                                    e_mail.To.Add(EToo);

                                    e_mail.Subject = "POST DISPATCH - ACTUAL FREIGHT BILL APPROVAL  ";
                                    e_mail.IsBodyHtml = true;

                                    Body = "";

                                    Body = Body + "Dear Sir / Madam,";
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";

                                    Body = Body + "Please find hereunder the details of one of the  dispatch already  done  where  actual  freight is considerably  higher than the tentative freight considered at the time of  P.I.  . ";
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";

                                    // ''''''''''''''

                                    Body = Body + "<table cellspacing='0' border='1' width='700px' style='margin-top:9px;'>";
                                    Body = Body + "<tr><td colspan='4'><font FACE='Calibri' SIZE=3 ><b>  </b></font></td></tr>";
                                    Body = Body + "<tr bgcolor='lightgrey' align='center' style='font-family:Calibri;font-size:12px;font-weight:Bold'><td>Particulars</td><td>Approx Amt  <br/> (considered on PI) </td><td>Actual Amt </td><td>Difference</td><td>% Difference</td>  <td bgcolor='lightgrey' ></td> </tr>";


                                    Body = Body + " <tr align='center' style='font-family:Calibri;font-size:12px'><td>Freight Rate(Rs. Per UOM) </td><td>  " + ApproxfreightRate + "</td><td>  " + ActualFreightRate + "</td><td align='center'>" + Diff_FreightRate + "</td><td>" + PerDiffFreightRate_1 + " " + PerDiffFreightRate_2 + "   </td> <td> (As per P.I. " + RateRsPerKg + " /kg + DD. Chrgs-Rs." + DeliveryCharges + " ) </td></tr>";
                                    Body = Body + "<tr align='center' style='font-family:Calibri;font-size:12px'><td>Freight Amt(Rs.)         </td><td>  " + freightAMT + "      </td><td>  " + actualfreightAMT + "</td><td align='center'>" + Diff_FreightAmt + "</td><td>" + PerDiffFreightAmt_1 + "  " + PerDiffFreightAmt_2 + " </td><td> " + " " + "</td> </tr>";
                                    Body = Body + "<tr align='center' style='font-family:Calibri;font-size:12px'><td>Effective Discount(%) </td><td>" + ApproxEffectiveDiscount + "</td><td>" + ActualEffectiveDiscount + "</td><td align='center'>" + Diff_EffectiveDiscount + "</td><td> " + PerDiffEffDisc_1 + " " + PerDiffEffDisc_2 + "  </td>  <td> " + " " + " </td> </tr>";

                                    Body = Body + "</table>";
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";

                                    // '''''''''''''''''

                                    Body = Body + "<b><u>Invoice Details:- </u></b> ";
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";

                                    Body = Body + "Invoice No." + ":- " + InvoiceNo;
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";
                                    Body = Body + "PI No." + ":- " + PI_NO;
                                    Body = Body + "<Br>";
                                    Body = Body + "Customer Name" + "    :- " + Cust_Name;
                                    Body = Body + "<Br>";
                                    Body = Body + "Sales Person Name " + ":- " + Sales_personName;
                                    Body = Body + "<Br>";
                                    Body = Body + "Brand" + " :- " + brand;

                                    Body = Body + "<Br>";
                                    Body = Body + "Region " + " :- " + Region;
                                    Body = Body + "<Br>";
                                    Body = Body + "Type Of Sale " + " :- " + typeofsale;
                                    Body = Body + "<Br>";
                                    Body = Body + "Qnty SQM " + " :- " + qtySQM;
                                    Body = Body + "<Br>";
                                    Body = Body + "Invoice Amt " + " :- " + Invoice_Amt;
                                    Body = Body + "<Br>";
                                    Body = Body + "NBRR " + ":-  Rs. " + Net_Rlsn_Per_SQM + "  Per UOM ";
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";

                                    Body = Body + "<b><u>All Freight Details :- </u></b>";
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";

                                    Body = Body + "Mode Of Transport " + " :- " + ModeOfTransport;
                                    Body = Body + "<Br>";
                                    Body = Body + "Total Weight " + " :- " + TotalWeight;
                                    Body = Body + "<Br>";
                                    Body = Body + "No Of Boxes       " + " :- " + NoOfBoxes;

                                    Body = Body + "<table>";
                                    Body = Body + "<tr>";
                                    Body = Body + "<td colspan='3' style='background-color:lightblue;'>Freight Basis " + " :- " + freightbasis + "</td>";
                                    Body = Body + "</tr>";
                                    Body = Body + "</table>";

                                    Body = Body + "<table>";
                                    Body = Body + "<tr>";
                                    Body = Body + "<td colspan='3' style='background-color:lightblue;'>Freight Borne By " + " :- " + freightBronby + "</td>";
                                    Body = Body + "</tr>";
                                    Body = Body + "</table>";

                                    Body = Body + "<table>";
                                    Body = Body + "<tr>";
                                    Body = Body + "<td colspan='3' style='background-color:lightblue;'>Shown Separately on Inv" + " :- " + ShownSepONInvoice + "</td>";
                                    Body = Body + "</tr>";
                                    Body = Body + "</table>";

                                    Body = Body + "Freight Debit Note To Be Raised" + " :- " + DebitNoteToBeRaised;
                                    Body = Body + "<Br>";
                                    Body = Body + "Freight Debit Advise No " + " :- " + DebitAdviseNo;
                                    Body = Body + "<Br>";

                                    Body = Body + "<Br>";

                                    Body = Body + "Transporter Name " + " :- " + Transporter_Name;
                                    Body = Body + "<Br>";
                                    Body = Body + "LR RR No." + " :- " + IRRRno;
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";
                                    Body = Body + "<table>";
                                    Body = Body + "<tr>";
                                    Body = Body + "<td colspan='3' style='background-color:lightblue;'> Approx Freight Amt. :- " + freightAMT + "  ( Considered  at  the  time  of  PI  approval. ) " + "</td>";
                                    Body = Body + "</tr>";
                                    Body = Body + "</table>";
                                    Body = Body + "Freight Bill Recd on " + " :- " + freightbillrecodeON;
                                    Body = Body + "<Br>";
                                    Body = Body + "Freight Bill No " + " :- " + freightbillno;
                                    Body = Body + "<Br>";
                                    Body = Body + " Freight Bill Date " + " :- " + BillDate;
                                    Body = Body + "<table>";
                                    Body = Body + "<tr>";
                                    Body = Body + "<td colspan='3' style='background-color:lightblue;'>Actual Freight Amt  :- " + actualfreightAMT + "</td>";
                                    Body = Body + "</tr>";
                                    Body = Body + "</table>";
                                    Body = Body + "Freight Bill Forwarded To HO On" + " :- " + FreightBillForwarded;
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";

                                    Body = Body + "<b><u>Other Details :- </u></b>";
                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";
                                    Body = Body + "Remark from Freight Tracker " + " :- " + Remarks;
                                    Body = Body + "<Br>";

                                    Body = Body + "<Br>";
                                    Body = Body + "<Br>";
                                    Body = Body + "Thanking You";
                                    Body = Body + "<Br>";
                                    Body = Body + "Commercial Team";

                                    Body = Body + "<Br>";

                                    Body = Body + "</span>";

                                    Body = Convert.ToString(Body);

                                    int TaskID;
                                    int RefRowID;
                                    RefRowID = 1;
                                    TaskID = 1;

                                    ObjClsFT.AL_SaveSendforApproval(1, InvoiceNo, salespersoncode, salespersoncode, 1);
                                    string ErrorDescription;

                                    UserFromId = salespersoncode;
                                    UserToId = ReportTo;
                                    DateTime logdatetime;
                                    logdatetime = DateTime.Now;
                                    string SFAquery = "Select * From StorageManagerNew.dbo.AL_FT_SendforApproval where InvoiceNo = @InvoiceNo";
                                    SFA = Common.ExecuteDataTableByQueryInvoiceNoGet(SFAquery, InvoiceNo);
                                    if (SFA.Rows.Count > 0)
                                    {
                                        {
                                            var withBlock3 = SFA.Rows[0];

                                            SFARowid = withBlock3["Rowid"].ToString();

                                            //  Body = Body + "  <a href=http://27.109.4.178:8081/Onlineportal/FreightMailApproval.aspx?Str=" + InvoiceNo + "_" + UserFromId + "_" + UserToId + "_" + SFARowid + " >Click Here To Approve</a>";
                                            Body = Body + "  <a href=https://app.italiagroup.in/bella/FreightMailApproval.aspx?Str=" + InvoiceNo + "_" + UserFromId + "_" + UserToId + "_" + SFARowid + " >Click Here To Approve</a>";
                                            Body = Body + "<Br>";

                                            if (DS.Tables.Count > 1)
                                            {
                                                if (DS.Tables[1].Rows.Count > 0)
                                                {
                                                    int j = 0;
                                                    while (j < DS.Tables[1].Rows.Count)
                                                    {
                                                        byte[] bytes = (byte[])DS.Tables[1].Rows[j]["AttachedData"];
                                                        string FileName = DS.Tables[1].Rows[j]["Filename"].ToString();
                                                        using (var ms = new MemoryStream(bytes))
                                                        {
                                                            var ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Plain);
                                                            var attach = new System.Net.Mail.Attachment(ms, ct);
                                                            if (DS.Tables[1].Rows[j]["Category"].ToString() == "6")
                                                            {
                                                                FileName = FileName.Replace("/", "-");
                                                            }

                                                            attach.ContentDisposition.FileName = FileName;
                                                            e_mail.Attachments.Add(attach);
                                                        }
                                                        j++;
                                                    }
                                                }
                                            }
                                            e_mail.Body = Body;
                                            e_mail.IsBodyHtml = true;
                                            e_mail.Body = Body;

                                          
                                            try
                                            {
                                                using (var smtpClient = ConfigureSmtpClient())
                                                using (var email = CreateMailMessage("auto.mail@italiagroup.in", e_mail.To, e_mail.Subject, e_mail.Body, e_mail.Attachments.Cast<Attachment>().ToList()))
                                                {
                                                    smtpClient.Send(email);
                                                }

                                                ErrorDescription = string.Empty;
                                                ObjClsFT.AL_SaveMailLog(SFARowid, salespersoncode, EToo, CC, BCC, "Done", ErrorDescription);
                                                TempData["SuccessMessage"] = "Mail sent to " + sendtoName + " " + sendtosurname;

                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorDescription = ex.Message;
                                                ObjClsFT.AL_SaveMailLog(SFARowid, salespersoncode, EToo, CC, BCC, "Error", ErrorDescription);
                                            }
                                            //MessageBox.Show("Mail sent");
                                            //MessageBox.Show("Mail sent to " + sendtoName + " " + sendtosurname);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return View();
        }
        public MailMessage CreateMailMessage(string from, MailAddressCollection toList, string subject, string body, List<Attachment> attachments = null)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true // Set to false if you're sending plain text
            };

            foreach (var address in toList)
            {
                mailMessage.To.Add(address);
            }
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    mailMessage.Attachments.Add(attachment);
                }
            }
            return mailMessage;
        }
        public SmtpClient ConfigureSmtpClient()
        {
            var smtpClient = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("auto.mail@italiagroup.in", "jifv gupy kcuw wyde"),
                Port = 587,
                EnableSsl = true,
                Host = "smtp.gmail.com"
            };

            return smtpClient;
        }
        #endregion
        #region ExportToExcel     
        [HttpPost]
        public ActionResult ExportToExcel(string InvoiceNo = "")
        {
            try
            {
               ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                ClsFreightTracker ObjClsFreightTracker = new ClsFreightTracker();
                DataSet DSExcel = ObjClsFreightTracker.GetDataHOApprovalExcelDT(InvoiceNo);
                DataTable dt = DSExcel.Tables[0];           
                string ReportHeader = "", headerName = "", strWidth = "", stralign = "", Header, FileName, filename;
                foreach (DataColumn item in dt.Columns)
                {
                    ReportHeader += ReportHeader == "" ? item.ColumnName : "|" + item.ColumnName;
                    headerName += headerName == "" ? item.ColumnName : "|" + item.ColumnName;
                    strWidth += strWidth == "" ? "10" : "|10";
                    stralign += stralign == "" ? "<" : "|<";
                }
                Header = "HODApproval Excel";
                FileName = "HODApprovalExcel";
                filename = FileName + ".xls";
                byte[] filecontent = ClsFreightTracker.DataTable(filename, headerName, strWidth, stralign, Header, dt, ReportHeader, "", "");
                System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                mStream.Write(filecontent, 0, filecontent.Length);
                mStream.Position = 0;
                TempData["ObjHOAppExcel"] = mStream;
                return new JsonResult() { Data = new { IsSuceess = true, filename = filename }, MaxJsonLength = Int32.MaxValue };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult Download(string filetype, string file)
        {
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            try
            {
                mStream = (MemoryStream)TempData["ObjHOAppExcel"];
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            if (filetype.ToUpper() == "PDF")
                return File(mStream, "application/pdf", file);
            else
                return File(mStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", file);
        }
        #endregion
    }
}