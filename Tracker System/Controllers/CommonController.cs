using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls.Expressions;
using Tracker_System.Classes;
using Tracker_System.Models;
using FileResult = Microsoft.AspNetCore.Mvc.FileResult;

namespace Tracker_System.Controllers
{
    public class CommonController : Controller
    {


        public JsonResult GetGridListFrieghtForMosaic(string ObjectType = "", string TypeOfSales = "", int Month = 0, int Year = 2024, bool ShowAllFT = false, String SearchType = " ",bool ExportSales = false)
        {
            string sortDefault = "";
            int PageSize = 859;
            sortDefault = "Doc_Date DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ALL", ObjectType);
            cmd.Parameters.AddWithValue("@PageNumber", 1);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@ByInvoiceDate", "");
            cmd.Parameters.AddWithValue("@InvoiceNo", "");
            cmd.Parameters.AddWithValue("@FreightBasis", "");
            cmd.Parameters.AddWithValue("@BySaleType", "");
            cmd.Parameters.AddWithValue("@ByTransporter", "");
            cmd.Parameters.AddWithValue("@TypeOfSales", TypeOfSales);
            cmd.Parameters.AddWithValue("@SearchType", SearchType);
            cmd.Parameters.AddWithValue("@ShowAllFT", ShowAllFT);
            cmd.Parameters.AddWithValue("@ExportSales", ExportSales);
            DataSet ds = new DataSet();
            int Total = 0;
            //ds = ObjSQLHelper.SelectProcDataDS("TS_FT_FreightTrackingSystem_prachi_Ajay_11122024", cmd);
            ds = ObjSQLHelper.SelectProcDataDS("TS_FT_FreightTrackingSystem_For_Mosaic", cmd);

            List<FreightTracker> dynamicDtFreight = new List<FreightTracker>();
            if (!(ds == null || ds.Tables == null || ds.Tables.Count == 0))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Total = (int)dt.Rows[0]["RecordCount"];
                }
                foreach (DataRow row in dt.Rows)
                {
                    FreightTracker FT = new FreightTracker();
                    {
                        FT.Total = row["RecordCount"].ToString();
                        FT.SrNo = row["SrNo"].ToString();
                        FT.Doc_No = row["Doc_No"].ToString();
                        FT.Doc_Date = row["Doc_Date"].ToString();
                        FT.CustCode = (row["CustCode"]).ToString();
                        FT.Cust_Name = row["Cust_Name"].ToString();
                        FT.Cust_Type = row["Cust_Type"].ToString();
                        FT.Cust_Category_Code = (row["Cust_Category_Code"]).ToString();
                        FT.Salesperson_Name = row["Salesperson_Name"].ToString();
                        FT.Brand = row["Brand"].ToString();
                        FT.Promotional_Sample_Sale = (row["Promotional_Sample_Sale"]).ToString();
                        FT.Freight_Basis = row["Freight_Basis"].ToString();
                        FT.Freight_Paid_By = row["Freight_Paid_By"].ToString();
                        FT.Qnty_SQM = (row["Qnty_SQM"]).ToString();
                        FT.Invoice_Amt = row["Invoice_Amt"].ToString();
                        FT.Net_Rlsn_Per_SQM = row["Net_Rlsn_Per_SQM"].ToString();
                        FT.LR_RR_Date = (row["LR_RR_Date"]).ToString();
                        FT.LR_RR_No = row["LR_RR_No"].ToString();
                        FT.Transporter_Name = row["Transporter_Name"].ToString();
                        FT.Freight_Amt = (row["Freight_Amt"]).ToString();
                        FT.FreightBillRecdON = row["FreightBillRecdON"].ToString();
                        FT.FreightBillNo = row["FreightBillNo"].ToString();
                        FT.BillDate = (row["BillDate"]).ToString();
                        FT.ActualFreightAmt = row["ActualFreightAmt"].ToString();
                        FT.FreightBillForwarded = row["FreightBillForwarded"].ToString();
                        FT.ShownSepONInvoice = (row["ShownSepONInvoice"]).ToString();
                        FT.DebitNoteToBeRaised = (row["DebitNoteToBeRaised"]).ToString();
                        FT.DebitAdviseNo = row["DebitAdviseNo"].ToString();
                        FT.SentForHODApproval = row["SentForHODApproval"].ToString();
                        FT.ApprovalForwardedON = row["ApprovalForwardedON"].ToString();
                        FT.Remarks = (row["Remarks"]).ToString();
                        FT.PaymentChecqueRecdON = row["PaymentChecqueRecdON"].ToString();
                        FT.PaymentAdviseSentON = row["PaymentAdviseSentON"].ToString();
                        FT.TotalWeight = (row["TotalWeight"]).ToString();
                        FT.EstimatedFreightAmt = row["EstimatedFreightAmt"].ToString();
                        FT.NoOfBoxes = row["NoOfBoxes"].ToString();
                        FT.ModeOfTransport = (row["ModeOfTransport"]).ToString();
                        FT.Region = (row["Region"]).ToString();
                        FT.checkApproval = row["checkApproval"].ToString();
                        FT.PendingName = (row["PendingName"]).ToString();
                        FT.VPendingdate = (row["VPendingdate"]).ToString();
                    }
                    ;

                    dynamicDtFreight.Add(FT);
                }
            }

            ObjSQLHelper.ClearObjects();
            return Json(dynamicDtFreight, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGridListFrieghtForTile(string ObjectType = "", string TypeOfSales = "", int Month = 0, int Year = 2024, bool ShowAllFT = false, String SearchType = " ", bool ExportSales = false)
        {
            string sortDefault = "";
            int PageSize = 859;
            sortDefault = "Doc_Date DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ALL", ObjectType);
            cmd.Parameters.AddWithValue("@PageNumber", 1);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@ByInvoiceDate", "");
            cmd.Parameters.AddWithValue("@InvoiceNo", "");
            cmd.Parameters.AddWithValue("@FreightBasis", "");
            cmd.Parameters.AddWithValue("@BySaleType", "");
            cmd.Parameters.AddWithValue("@ByTransporter", "");
            cmd.Parameters.AddWithValue("@TypeOfSales", TypeOfSales);
            cmd.Parameters.AddWithValue("@ShowAllFT", ShowAllFT);
            cmd.Parameters.AddWithValue("@SearchType", SearchType);
            cmd.Parameters.AddWithValue("@ExportSales", ExportSales);
            DataSet ds = new DataSet();
            int Total = 0;
            //ds = ObjSQLHelper.SelectProcDataDS("TS_FT_FreightTrackingSystem_prachi_Ajay_11122024", cmd);
            ds = ObjSQLHelper.SelectProcDataDS("TS_FT_FreightTrackingSystem_ForTile", cmd);

            List<FreightTracker> dynamicDtFreight = new List<FreightTracker>();
            if (!(ds == null || ds.Tables == null || ds.Tables.Count == 0))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Total = (int)dt.Rows[0]["RecordCount"];
                }
                foreach (DataRow row in dt.Rows)
                {
                    FreightTracker FT = new FreightTracker();
                    {
                        FT.Total = row["RecordCount"].ToString();
                        FT.SrNo = row["SrNo"].ToString();
                        FT.Doc_No = row["Doc_No"].ToString();
                        FT.Doc_Date = row["Doc_Date"].ToString();
                        FT.CustCode = (row["CustCode"]).ToString();
                        FT.Cust_Name = row["Cust_Name"].ToString();
                        FT.Cust_Type = row["Cust_Type"].ToString();
                        FT.Cust_Category_Code = (row["Cust_Category_Code"]).ToString();
                        FT.Salesperson_Name = row["Salesperson_Name"].ToString();
                        FT.Brand = row["Brand"].ToString();
                        FT.Promotional_Sample_Sale = (row["Promotional_Sample_Sale"]).ToString();
                        FT.Freight_Basis = row["Freight_Basis"].ToString();
                        FT.Freight_Paid_By = row["Freight_Paid_By"].ToString();
                        FT.Qnty_SQM = (row["Qnty_SQM"]).ToString();
                        FT.Invoice_Amt = row["Invoice_Amt"].ToString();
                        FT.Net_Rlsn_Per_SQM = row["Net_Rlsn_Per_SQM"].ToString();
                        FT.LR_RR_Date = (row["LR_RR_Date"]).ToString();
                        FT.LR_RR_No = row["LR_RR_No"].ToString();
                        FT.Transporter_Name = row["Transporter_Name"].ToString();
                        FT.Freight_Amt = (row["Freight_Amt"]).ToString();
                        FT.FreightBillRecdON = row["FreightBillRecdON"].ToString();
                        FT.FreightBillNo = row["FreightBillNo"].ToString();
                        FT.BillDate = (row["BillDate"]).ToString();
                        FT.ActualFreightAmt = row["ActualFreightAmt"].ToString();
                        FT.FreightBillForwarded = row["FreightBillForwarded"].ToString();
                        FT.ShownSepONInvoice = (row["ShownSepONInvoice"]).ToString();
                        FT.DebitNoteToBeRaised = (row["DebitNoteToBeRaised"]).ToString();
                        FT.DebitAdviseNo = row["DebitAdviseNo"].ToString();
                        FT.SentForHODApproval = row["SentForHODApproval"].ToString();
                        FT.ApprovalForwardedON = row["ApprovalForwardedON"].ToString();
                        FT.Remarks = (row["Remarks"]).ToString();
                        FT.PaymentChecqueRecdON = row["PaymentChecqueRecdON"].ToString();
                        FT.PaymentAdviseSentON = row["PaymentAdviseSentON"].ToString();
                        FT.TotalWeight = (row["TotalWeight"]).ToString();
                        FT.EstimatedFreightAmt = row["EstimatedFreightAmt"].ToString();
                        FT.NoOfBoxes = row["NoOfBoxes"].ToString();
                        FT.ModeOfTransport = (row["ModeOfTransport"]).ToString();
                        FT.Region = (row["Region"]).ToString();
                        FT.checkApproval = row["checkApproval"].ToString();
                        FT.PendingName = (row["PendingName"]).ToString();
                        FT.VPendingdate = (row["VPendingdate"]).ToString();
                    }
                    ;

                    dynamicDtFreight.Add(FT);
                }
            }

            ObjSQLHelper.ClearObjects();
            return Json(dynamicDtFreight, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGridCommissionListMosaic(String SearchType = " ",string ObjectType = "", string invoiceNo = "", string invoiceDate = "")
        {
            string sortDefault = "";
            int PageSize = 859;
            sortDefault = "Doc_Date DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ALL", ObjectType);
            cmd.Parameters.AddWithValue("@SearchType", SearchType);
            DataSet ds = new DataSet();
            ds = ObjSQLHelper.SelectProcDataDS("TS_CommisstionTracker_ForMosaic", cmd);

            List<Commissiontracker> dynamicDtList = new List<Commissiontracker>();
            if (!(ds == null || ds.Tables == null || ds.Tables.Count == 0))
            {
                dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    string currentInvoiceNo = row["InvoiceNo"]?.ToString() ?? string.Empty;
                    string currentInvoiceDate = row["InvDate"].ToString() ?? string.Empty;

                    if (ObjectType == "PreMarketing")
                    {
                        if (!string.IsNullOrEmpty(invoiceNo) && !currentInvoiceNo.Contains(invoiceNo))
                            continue;// Skipped
                        if (!string.IsNullOrEmpty(invoiceDate) && currentInvoiceDate != invoiceDate)
                            continue;
                    }
                    if (!string.IsNullOrEmpty(invoiceNo) && !currentInvoiceNo.Contains(invoiceNo))
                    {
                        continue;
                    }
                    Commissiontracker CSList = new Commissiontracker();

                    CSList.SrNo = row["SrNo"] != DBNull.Value ? row["SrNo"].ToString() : string.Empty;
                    CSList.PI_No = row["PINo"] != DBNull.Value ? row["PINo"].ToString() : string.Empty;
                    CSList.OCF_No = row["OCF_No"] != DBNull.Value ? row["OCF_No"].ToString() : string.Empty;
                    CSList.Invoice_No = row["InvoiceNo"] != DBNull.Value ? row["InvoiceNo"].ToString() : string.Empty;
                    CSList.INV_Dates = row["InvDate"] != DBNull.Value ? row["InvDate"].ToString() : string.Empty;
                    CSList.CustomerName = row["CustomerName"] != DBNull.Value ? row["CustomerName"].ToString() : string.Empty;
                    CSList.Commission_Vendor_Name = row["VendorName"] != DBNull.Value ? row["VendorName"].ToString() : string.Empty;
                    CSList.CommissionType = row["CommissionType"] != DBNull.Value ? row["CommissionType"].ToString() : string.Empty;
                    CSList.TentativeCommBasic_Amt = row["TentativeAmount"] != DBNull.Value ? Convert.ToString(row["TentativeAmount"]) : string.Empty;
                    CSList.CommissionRate = row["CommissionRate"] != DBNull.Value ? decimal.Parse(row["CommissionRate"].ToString()) : 0m;
                    CSList.SalesPerson_Name = row["SalesPersonName"] != DBNull.Value ? row["SalesPersonName"].ToString() : string.Empty;
                    CSList.TaggedOnPI = row["TaggedONPI_Flag"] != DBNull.Value ? Convert.ToBoolean(row["TaggedONPI_Flag"]) : false;
                    CSList.TaggedOnOCF = row["TaggedOnERP_Flag"] != DBNull.Value ? Convert.ToBoolean(row["TaggedOnERP_Flag"]) : false;
                    CSList.TaggedAfterInvoice = row["TaggedAfterInVoice_Flag"] != DBNull.Value ? Convert.ToBoolean(row["TaggedAfterInVoice_Flag"]) : false;
                    CSList.Claimform_Received = row["ClaimFormReceived"] != DBNull.Value ? row["ClaimFormReceived"].ToString() : string.Empty;// Or handle invalid date formats as needed
                    CSList.CommissionworkingsenttoVendor_Employee = row["CommissionWorkingSentToVendor"] != DBNull.Value ? row["CommissionWorkingSentToVendor"].ToString() : string.Empty;
                    CSList.Credit_Note_Prepared = row["CreditNotePrepared"] != DBNull.Value ? row["CreditNotePrepared"].ToString() : string.Empty;
                    CSList.Credit_Advice_Prepared = row["CreditAdvicePrepared"] != DBNull.Value ? row["CreditAdvicePrepared"].ToString() : string.Empty;
                    CSList.CommissionInvoice_Received = row["CommissionInvoiceReceived"] != DBNull.Value ? row["CommissionInvoiceReceived"].ToString() : string.Empty;
                    CSList.Vendor_Code = row["VenderNo"] != DBNull.Value ? row["VenderNo"].ToString() : string.Empty;
                    CSList.Credit_Advice_Details = row["CreditAdviceDetails"] != DBNull.Value ? Convert.ToString(row["CreditAdviceDetails"]) : string.Empty;
                    CSList.Remarks = row["Remark"] != DBNull.Value ? Convert.ToString(row["Remark"]) : string.Empty;
                    CSList.FreightAmt = row["FreightAmt"] != DBNull.Value ? Convert.ToString(row["FreightAmt"]) : string.Empty;
                    CSList.FreightBillNo = row["freightBillNo"] != DBNull.Value ? Convert.ToString(row["freightBillNo"]) : string.Empty;
                    CSList.FromNo = row["FromNo"] != DBNull.Value ? Convert.ToString(row["FromNo"]) : string.Empty;
                    CSList.FreightBillDate = row["FreightBillDate"] != DBNull.Value ? row["FreightBillDate"].ToString() : string.Empty;
                    CSList.ActualCommission = row["ActualCommission"] != DBNull.Value ? Convert.ToString(row["ActualCommission"]) : string.Empty;
                    CSList.CreditNotePreparedNo = row["CreditNotePreparedNo"] != DBNull.Value ? Convert.ToString(row["CreditNotePreparedNo"]) : string.Empty;
                    dynamicDtList.Add(CSList);
                }
            }
           ObjSQLHelper.ClearObjects();
            return Json(dynamicDtList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGridCommissionListTile(String SearchType = " ", string ObjectType = "", string invoiceNo = "", string invoiceDate = "")
        {
            string sortDefault = "";
            int PageSize = 859;
            sortDefault = "Doc_Date DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ALL", ObjectType);
            cmd.Parameters.AddWithValue("@SearchType", SearchType);
            DataSet ds = new DataSet();
            ds = ObjSQLHelper.SelectProcDataDS("TS_CommisstionTracker_ForTile", cmd);

            List<Commissiontracker> dynamicDtList = new List<Commissiontracker>();
            if (!(ds == null || ds.Tables == null || ds.Tables.Count == 0))
            {
                dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    string currentInvoiceNo = row["InvoiceNo"]?.ToString() ?? string.Empty;
                    string currentInvoiceDate = row["InvDate"]?.ToString() ?? string.Empty;

                    if (ObjectType == "PreMarketing")
                    {
                        if (!string.IsNullOrEmpty(invoiceNo) && !currentInvoiceNo.Contains(invoiceNo))
                            continue;// Skipped
                        if (!string.IsNullOrEmpty(invoiceDate) && currentInvoiceDate != invoiceDate)
                            continue;
                    }
                    if (!string.IsNullOrEmpty(invoiceNo) && !currentInvoiceNo.Contains(invoiceNo))
                    {
                        continue;
                    }
                    Commissiontracker CSList = new Commissiontracker();
                    CSList.SrNo = row["SrNo"] != DBNull.Value ? row["SrNo"].ToString() : string.Empty;
                    CSList.PI_No = row["PINo"] != DBNull.Value ? row["PINo"].ToString() : string.Empty;
                    CSList.OCF_No = row["OCF_No"] != DBNull.Value ? row["OCF_No"].ToString() : string.Empty;
                    CSList.Invoice_No = row["InvoiceNo"] != DBNull.Value ? row["InvoiceNo"].ToString() : string.Empty;
                    CSList.INV_Dates = row["InvDate"] != DBNull.Value ? row["InvDate"].ToString() : string.Empty;
                    CSList.CustomerName = row["CustomerName"] != DBNull.Value ? row["CustomerName"].ToString() : string.Empty;
                    CSList.Commission_Vendor_Name = row["VendorName"] != DBNull.Value ? row["VendorName"].ToString() : string.Empty;
                    CSList.CommissionType = row["CommissionType"] != DBNull.Value ? row["CommissionType"].ToString() : string.Empty;
                    CSList.TentativeCommBasic_Amt = row["TentativeAmount"] != DBNull.Value ? Convert.ToString(row["TentativeAmount"]) : string.Empty;
                    CSList.CommissionRate = row["CommissionRate"] != DBNull.Value ? decimal.Parse(row["CommissionRate"].ToString()) : 0m;
                    CSList.SalesPerson_Name = row["SalesPersonName"] != DBNull.Value ? row["SalesPersonName"].ToString() : string.Empty;
                    CSList.TaggedOnPI = row["TaggedONPI_Flag"] != DBNull.Value ? Convert.ToBoolean(row["TaggedONPI_Flag"]) : false;
                    CSList.TaggedOnOCF = row["TaggedOnERP_Flag"] != DBNull.Value ? Convert.ToBoolean(row["TaggedOnERP_Flag"]) : false;
                    CSList.TaggedAfterInvoice = row["TaggedAfterInVoice_Flag"] != DBNull.Value ? Convert.ToBoolean(row["TaggedAfterInVoice_Flag"]) : false;
                    CSList.Claimform_Received = row["ClaimFormReceived"] != DBNull.Value ? row["ClaimFormReceived"].ToString() : string.Empty;// Or handle invalid date formats as needed
                    CSList.CommissionworkingsenttoVendor_Employee = row["CommissionWorkingSentToVendor"] != DBNull.Value ? row["CommissionWorkingSentToVendor"].ToString() : string.Empty;
                    CSList.Credit_Note_Prepared = row["CreditNotePrepared"] != DBNull.Value ? row["CreditNotePrepared"].ToString() : string.Empty;
                    CSList.Credit_Advice_Prepared = row["CreditAdvicePrepared"] != DBNull.Value ? row["CreditAdvicePrepared"].ToString() : string.Empty;
                    CSList.CommissionInvoice_Received = row["CommissionInvoiceReceived"] != DBNull.Value ? row["CommissionInvoiceReceived"].ToString() : string.Empty;
                    CSList.Vendor_Code = row["VenderNo"] != DBNull.Value ? row["VenderNo"].ToString() : string.Empty;
                    CSList.Credit_Advice_Details = row["CreditAdviceDetails"] != DBNull.Value ? Convert.ToString(row["CreditAdviceDetails"]) : string.Empty;
                    CSList.Remarks = row["Remark"] != DBNull.Value ? Convert.ToString(row["Remark"]) : string.Empty;
                    CSList.FreightAmt = row["FreightAmt"] != DBNull.Value ? Convert.ToString(row["FreightAmt"]) : string.Empty;
                    CSList.FreightBillNo = row["freightBillNo"] != DBNull.Value ? Convert.ToString(row["freightBillNo"]) : string.Empty;
                    CSList.FromNo = row["FromNo"] != DBNull.Value ? Convert.ToString(row["FromNo"]) : string.Empty;
                    CSList.FreightBillDate = row["FreightBillDate"] != DBNull.Value ? row["FreightBillDate"].ToString() : string.Empty;
                    CSList.ActualCommission = row["ActualCommission"] != DBNull.Value ? Convert.ToString(row["ActualCommission"]) : string.Empty;
                    CSList.CreditNotePreparedNo = row["CreditNotePreparedNo"] != DBNull.Value ? Convert.ToString(row["CreditNotePreparedNo"]) : string.Empty;
                    dynamicDtList.Add(CSList);
                }
            }
            ObjSQLHelper.ClearObjects();
            return Json(dynamicDtList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGridExportCommissionList(String SearchType = " ",string ObjectType = "", string invoiceNo = "", string invoiceDate = "")
        {
            string sortDefault = "";
            int PageSize = 859;
            sortDefault = "Doc_Date DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ALL", ObjectType);
            cmd.Parameters.AddWithValue("@SearchType", SearchType);
            DataSet ds = new DataSet();

            ds = ObjSQLHelper.SelectProcDataDS("TS_ExportCommisstionTracker", cmd);
            List<ExportCommissiontracker> dynamicDtList = new List<ExportCommissiontracker>();
            if (!(ds == null || ds.Tables == null || ds.Tables.Count == 0))
            {
                dt = ds.Tables[0];
             
                foreach (DataRow row in dt.Rows)
                {
                    string currentInvoiceNo = row["InvoiceNo"]?.ToString() ?? string.Empty;
                    string currentInvoiceDate = row["InvDate"].ToString() ?? string.Empty;

                    if (ObjectType == "PreMarketing")
                    {
                        if (!string.IsNullOrEmpty(invoiceNo) && !currentInvoiceNo.Contains(invoiceNo))
                            continue;// Skipped
                        if (!string.IsNullOrEmpty(invoiceDate) && currentInvoiceDate != invoiceDate)
                            continue;
                    }
                    if (!string.IsNullOrEmpty(invoiceNo) && !currentInvoiceNo.Contains(invoiceNo))
                    {
                        continue;
                    }
                    ExportCommissiontracker CSList = new ExportCommissiontracker();
                    { 
                    CSList.SrNo = row["SrNo"] != DBNull.Value ? row["SrNo"].ToString() : string.Empty;
                    CSList.PI_No = row["PINo"] != DBNull.Value ? row["PINo"].ToString() : string.Empty;
                    CSList.OCF_No = row["OCF_No"] != DBNull.Value ? row["OCF_No"].ToString() : string.Empty;
                    CSList.Division = row["Division"] != DBNull.Value ? row["Division"].ToString() : string.Empty;
                    CSList.Invoice_No = row["InvoiceNo"] != DBNull.Value ? row["InvoiceNo"].ToString() : string.Empty;
                    CSList.INV_Dates = row["InvDate"] != DBNull.Value ? row["InvDate"].ToString() : string.Empty;
                    CSList.CustomerName = row["CustomerName"] != DBNull.Value ? row["CustomerName"].ToString() : string.Empty;
                    CSList.Commission_Vendor_Name = row["VendorName"] != DBNull.Value ? row["VendorName"].ToString() : string.Empty;
                    CSList.CommissionType = row["CommissionType"] != DBNull.Value ? row["CommissionType"].ToString() : string.Empty;
                    CSList.TentativeCommBasic_Amt = row["TentativeAmount"] != DBNull.Value ? Convert.ToString(row["TentativeAmount"]) : string.Empty;
                    CSList.CommissionRate = row["CommissionRate"] != DBNull.Value ? decimal.Parse(row["CommissionRate"].ToString()) : 0m;
                    CSList.SalesPerson_Name = row["SalesPersonName"] != DBNull.Value ? row["SalesPersonName"].ToString() : string.Empty;
                    CSList.TaggedOnPI = row["TaggedONPI_Flag"] != DBNull.Value ? Convert.ToBoolean(row["TaggedONPI_Flag"]) : false;
                    CSList.TaggedOnOCF = row["TaggedOnERP_Flag"] != DBNull.Value ? Convert.ToBoolean(row["TaggedOnERP_Flag"]) : false;
                    CSList.TaggedAfterInvoice = row["TaggedAfterInVoice_Flag"] != DBNull.Value ? Convert.ToBoolean(row["TaggedAfterInVoice_Flag"]) : false;
                    CSList.Claimform_Received = row["ClaimFormReceived"] != DBNull.Value ? row["ClaimFormReceived"].ToString() : string.Empty;// Or handle invalid date formats as needed
                    CSList.CommissionworkingsenttoVendor_Employee = row["CommissionWorkingSentToVendor"] != DBNull.Value ? row["CommissionWorkingSentToVendor"].ToString() : string.Empty;
                    CSList.Credit_Note_Prepared = row["CreditNotePrepared"] != DBNull.Value ? row["CreditNotePrepared"].ToString() : string.Empty;
                    CSList.Credit_Advice_Prepared = row["CreditAdvicePrepared"] != DBNull.Value ? row["CreditAdvicePrepared"].ToString() : string.Empty;
                    CSList.CommissionInvoice_Received = row["CommissionInvoiceReceived"] != DBNull.Value ? row["CommissionInvoiceReceived"].ToString() : string.Empty;
                    CSList.Vendor_Code = row["VenderNo"] != DBNull.Value ? row["VenderNo"].ToString() : string.Empty;
                    CSList.Credit_Advice_Details = row["CreditAdviceDetails"] != DBNull.Value ? Convert.ToString(row["CreditAdviceDetails"]) : string.Empty;
                    CSList.Remarks = row["Remark"] != DBNull.Value ? Convert.ToString(row["Remark"]) : string.Empty;
                    CSList.FreightAmt = row["FreightAmt"] != DBNull.Value ? Convert.ToString(row["FreightAmt"]) : string.Empty;
                    CSList.FreightBillNo = row["freightBillNo"] != DBNull.Value ? Convert.ToString(row["freightBillNo"]) : string.Empty;
                    CSList.FromNo = row["FromNo"] != DBNull.Value ? Convert.ToString(row["FromNo"]) : string.Empty;
                    CSList.FreightBillDate = row["FreightBillDate"] != DBNull.Value ? row["FreightBillDate"].ToString() : string.Empty;
                    CSList.ActualCommission = row["ActualCommission"] != DBNull.Value ? Convert.ToString(row["ActualCommission"]) : string.Empty;
                        CSList.CreditNotePreparedNo = row["CreditNotePreparedNo"] != DBNull.Value ? Convert.ToString(row["CreditNotePreparedNo"]) : string.Empty;
                        SqlCommand cmdLog = new SqlCommand();
                        cmdLog.Parameters.AddWithValue("@InvNo", CSList.Invoice_No);
                        cmdLog.Parameters.AddWithValue("@Mode", "GetLogDetails");

                        DataSet dslog = ObjSQLHelper.SelectProcDataDS("Export_LogDetails", cmdLog);
                        DataTable dtlog = dslog?.Tables.Count > 0 ? dslog.Tables[0] : new DataTable();

                        CSList.ExportCommissionLogDetails = dtlog.AsEnumerable()
                            .Select(r => new ExportCommissionInvoiceLogDetail
                            {
                                SectionName = r["SectionName"].ToString(),
                                LogDate = r["LogDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["LogDate"]),
                                EmpId = r["EmpId"] == DBNull.Value ? 0 : Convert.ToInt32(r["EmpId"]),
                                EmployeeName = r["EmployeeName"].ToString()
                            }).ToList();
                        CSList.ExportCommissionLogDetails = dtlog.AsEnumerable()
                                                       .Select(r => new ExportCommissionInvoiceLogDetail
                                                       {
                                                           SectionName = r["SectionName"].ToString(),
                                                           LogDate = r["LogDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["LogDate"]),
                                                           EmpId = r["EmpId"] == DBNull.Value ? 0 : Convert.ToInt32(r["EmpId"]),
                                                           EmployeeName = r["EmployeeName"].ToString()
                                                       }).ToList();
                        dynamicDtList.Add(CSList);
                    }
                }
            }
            ObjSQLHelper.ClearObjects();
            return Json(dynamicDtList, JsonRequestBehavior.AllowGet);
        }
        private string GetFormattedDate(DataRow row, string columnName)
        {
            if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
            {
                var value = row[columnName].ToString();
                if (DateTime.TryParse(value, out DateTime parsedDate))
                {
                    return parsedDate.ToString("dd/MM/yyyy");
                }
            }
            return string.Empty;
        }
        public JsonResult GetExportGridList(String SearchType = " ")
        {
            string sortDefault = "";
            int PageSize = 859;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdMaster = new SqlCommand();
            cmdMaster.Parameters.AddWithValue("@Mode", "GET");


            try
            {
                DataSet dsMaster = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdMaster);

                SqlCommand cmdOther = new SqlCommand();
                cmdOther.Parameters.AddWithValue("@Mode", "OtherGet");
                DataSet dsOther = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdOther);
                SqlCommand cmdcfsOther = new SqlCommand();
                cmdcfsOther.Parameters.AddWithValue("@Mode", "CFS Charges get");
                DataSet dscfsOther = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdcfsOther);
                SqlCommand cmdLog = new SqlCommand();
                cmdLog.Parameters.AddWithValue("@Mode", "GetLogDetails");

                DataSet dslog = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdLog);

                List<Exporttracker> exportList = new List<Exporttracker>();
                if (!(dsMaster == null || dsMaster.Tables == null || dsMaster.Tables.Count == 0))
                {
                    DataTable dtMaster = dsMaster.Tables[0];
                    DataTable dtOther = dsOther?.Tables.Count > 0 ? dsOther.Tables[0] : new DataTable();
                    DataTable dtcfsOther = dscfsOther?.Tables.Count > 0 ? dscfsOther.Tables[0] : new DataTable();
                    DataTable dtlog = dslog?.Tables.Count > 0 ? dslog.Tables[0] : new DataTable();

                    foreach (DataRow row in dtMaster.Rows)
                    {



                        Exporttracker export = new Exporttracker
                        {
                            SrNo = GetValueOrDefault(row, "SrNo"),

                            rowid = row["RowId"] != DBNull.Value ? Convert.ToInt32(row["RowId"]) : 0,
                            Invoice_No = GetValueOrDefault(row, "InvNo"),
                            InvDate = GetValueOrDefault(row, "InvDate"),
                            Division = GetValueOrDefault(row, "Division"),
                            Mfr_Out = GetValueOrDefault(row, "MFROut"),
                            Bill_to_Name = GetValueOrDefault(row, "Bill-to Name"),
                            Ship_to_Name = GetValueOrDefault(row, "Ship-to Name"),
                            Shipping_Bill_No = GetValueOrDefault(row, "Shipping Bill No_"),
                            Shipping_Bill_Date = GetValueOrDefault(row, "Shipment Date"),
                            Currency = string.IsNullOrWhiteSpace(GetValueOrDefault(row, "Currency Code"))
                                                   ? "INR"
                                                   : GetValueOrDefault(row, "Currency Code"),
                            //Currency = GetValueOrDefault(row, "Currency Code"),
                            Basic_Amt_Charges = GetValueOrDefault(row, "FOB Value(In Foreign Currency)"),
                            Sea_Freight_Air_Freight = GetValueOrDefault(row, "Freight Amount"),
                            Insurarnce_charged = GetValueOrDefault(row, "Insurarnce_charged"),
                            Other_Charges = GetValueOrDefault(row, "Other_Charges"),
                            Invoice_Value = GetValueOrDefault(row, "Invoice_Value"),
                            Mode = GetValueOrDefault(row, "Mode_of_Transport"),
                            Country = GetValueOrDefault(row, "Country"),
                            Port_of_Discharge = GetValueOrDefault(row, "Port_of_Discharge"),
                            Payment_Terms = GetValueOrDefault(row, "Payment Terms Code"),
                            EPCG_Licence_No = GetValueOrDefault(row, "EPCG_Licence_No"),
                            AirWay_BillNo_Bill_of_LodingNo = GetValueOrDefault(row, "AirWay_BillNo_Bill_of_LodingNo"),
                            AirWay_BillDate_Bill_of_LodingDate = GetValueOrDefault(row, "AirWay_BillDate_Bill_of_LodingDate"),
                            Clearing_Point = GetValueOrDefault(row, "Clearing_Point"),
                            Inco_Terms = GetValueOrDefault(row, "Inco_Terms"),
                            Port_of_Loading = GetValueOrDefault(row, "Port_of_Loading"),
                            Remarks = GetValueOrDefault(row, "Remarks"),
                            CCClearance_Agent_Name = GetValueOrDefault(row, "CCClearance_Agent"),
                            CCInvoice_No = GetValueOrDefault(row, "CCInvNo"),
                            CCInvDate = GetValueOrDefault(row, "CCInvDate"),
                            CCClearance_agency_charges = GetValueOrDefault(row, "CCClearance_Agency_charges"),
                            CCClearance_Agency_chargesGST = GetValueOrDefault(row, "CCClearance_Agency_chargesGST"),
                            CCClearance_Agency_chargesGSTAmount = GetValueOrDefault(row, "CCClearance_Agency_chargesGSTAmount"),
                            CCClearance_Agency_charges_Total = GetValueOrDefault(row, "CCClearance_Agency_charges_Total"),

                            CCEDI_Xeam_charges = GetValueOrDefault(row, "CCEdi_Xeam_Charges"),
                            CCEdi_Xeam_ChargesGST = GetValueOrDefault(row, "CCEdi_Xeam_ChargesGST"),
                            CCEdi_Xeam_ChargesGSTAmount = GetValueOrDefault(row, "CCEdi_Xeam_ChargesGSTAmount"),
                            CCEdi_Xeam_Charges_Total = GetValueOrDefault(row, "CCEdi_Xeam_Charges_Total"),

                            CCVGM = GetValueOrDefault(row, "CCVGM"),
                            CCVGMGST = GetValueOrDefault(row, "CCVGMGST"),
                            CCVGMGSTAmount = GetValueOrDefault(row, "CCVGMGSTAmount"),
                            CCVGM_Total = GetValueOrDefault(row, "CCVGM_Total"),

                            CCGSEC = GetValueOrDefault(row, "CCGSEC"),
                            CCGSECGST = GetValueOrDefault(row, "CCGSECGST"),
                            CCGSECGSTAmount = GetValueOrDefault(row, "CCGSECGSTAmount"),
                            CCGSEC_Total = GetValueOrDefault(row, "CCGSEC_Total"),

                            CCCOO = GetValueOrDefault(row, "CCCOO"),
                            CCCOOGST = GetValueOrDefault(row, "CCCOOGST"),
                            CCCOOGSTAmount = GetValueOrDefault(row, "CCCOOGSTAmount"),
                            CCCOO_Total = GetValueOrDefault(row, "CCCOO_Total"),

                            CCExamination_CFS = GetValueOrDefault(row, "CCExamination_CFS"),
                            CCExamination_CFSGST = GetValueOrDefault(row, "CCExamination_CFSGST"),
                            CCExamination_CFSGSTAmount = GetValueOrDefault(row, "CCExamination_CFSGSTAmount"),
                            CCExamination_CFS_Total = GetValueOrDefault(row, "CCExamination_CFS_Total"),

                            CCLift_on_Lift_off = GetValueOrDefault(row, "CCLift_on_Lift_off"),
                            CCLift_on_Lift_offGST = GetValueOrDefault(row, "CCLift_on_Lift_offGST"),
                            CCLift_on_Lift_offGSTAmount = GetValueOrDefault(row, "CCLift_on_Lift_offGSTAmount"),
                            CCLift_on_Lift_off_Total = GetValueOrDefault(row, "CCLift_on_Lift_off_Total"),

                            CCCFS = GetValueOrDefault(row, "CCCFS"),
                            CCCFSGST = GetValueOrDefault(row, "CCCFSGST"),
                            CCCFSGSTAmount = GetValueOrDefault(row, "CCCFSGSTAmount"),
                            CCCFSTotal = GetValueOrDefault(row, "CCCFSTotal"),

                            CCOthers = GetValueOrDefault(row, "CCOthers"),
                            CCOthersGST = GetValueOrDefault(row, "CCOthersGST"),
                            CCOthersGSTAmount = GetValueOrDefault(row, "CCOthersGSTAmount"),
                            CCOthers_Total = GetValueOrDefault(row, "CCOthers_Total"),
                            CCAmt_Before_GST = GetValueOrDefault(row, "CCAmt_Before_GST"),
                            CCGST = GetValueOrDefault(row, "CCGST"),
                            CC_total = GetValueOrDefault(row, "CCTotal"),

                            FCForwarder = GetValueOrDefault(row, "FCForwarder"),
                            FCInvoice_No = GetValueOrDefault(row, "FCInvNo"),
                            FCInvDate = GetValueOrDefault(row, "FCInvDate"),

                            FCSFreight = GetValueOrDefault(row, "FCSFreight"),
                            FCSFreight_GST = GetValueOrDefault(row, "FCSFreightGST"),
                            FCSFreight_GSTAmount = GetValueOrDefault(row, "FCSFreightGSTAmount"),
                            FCSFreight_Total = GetValueOrDefault(row, "FCSFreight_Total"),

                            FCSTHC = GetValueOrDefault(row, "FCSTHC"),
                            FCSTHC_GST = GetValueOrDefault(row, "FCSTHCGST"),
                            FCSTHC_GSTAmount = GetValueOrDefault(row, "FCSTHCGSTAmount"),
                            FCSTHC_Total = GetValueOrDefault(row, "FCSTHC_Total"),

                            FCSBL = GetValueOrDefault(row, "FCSBL"),
                            FCSBL_GST = GetValueOrDefault(row, "FCSBLGST"),
                            FCSBL_GSTAmount = GetValueOrDefault(row, "FCSBLGSTAmount"),
                            FCSBL_Total = GetValueOrDefault(row, "FCSBL_Total"),

                            FCSSeal = GetValueOrDefault(row, "FCSSEAL"),
                            FCSSEAL_GST = GetValueOrDefault(row, "FCSSEALGST"),
                            FCSSEAL_GSTAmount = GetValueOrDefault(row, "FCSSEALGSTAmount"),
                            FCSSEAL_Total = GetValueOrDefault(row, "FCSSEAL_Total"),

                            FCSVGM = GetValueOrDefault(row, "FCSVGM"),
                            FCSVGM_GST = GetValueOrDefault(row, "FCSVGMGST"),
                            FCSVGM_GSTAmount = GetValueOrDefault(row, "FCSVGMGSTAmount"),
                            FCSVGM_Total = GetValueOrDefault(row, "FCSVGM_Total"),

                            FCSMUC = GetValueOrDefault(row, "FCSMUC"),
                            FCSMUC_GST = GetValueOrDefault(row, "FCSMUCGST"),
                            FCSMUC_GSTAmount = GetValueOrDefault(row, "FCSMUCGSTAmount"),
                            FCSMUC_Total = GetValueOrDefault(row, "FCSMUC_Total"),

                            FCSITHC = GetValueOrDefault(row, "FCSITHC"),
                            FCSITHC_GST = GetValueOrDefault(row, "FCSITHCGST"),
                            FCSITHC_GSTAmount = GetValueOrDefault(row, "FCSITHCGSTAmount"),
                            FCSITHC_Total = GetValueOrDefault(row, "FCSITHC_Total"),

                            FCSDry_Port_charges = GetValueOrDefault(row, "FCSDry_Port_charges"),
                            FCSDry_Port_charges_GST = GetValueOrDefault(row, "FCSDry_Port_chargesGST"),
                            FCSDry_Port_charges_GSTAmount = GetValueOrDefault(row, "FCSDry_Port_chargesGSTAmount"),
                            FCSDry_Port_charges_Total = GetValueOrDefault(row, "FCSDry_Port_charges_Total"),

                            FCSAdministrative_charges = GetValueOrDefault(row, "FCSAdministrative_charges"),
                            FCSAdministrative_charges_GST = GetValueOrDefault(row, "FCSAdministrative_chargesGST"),
                            FCSAdministrative_charges_GSTAmount = GetValueOrDefault(row, "FCSAdministrative_chargesGSTAmount"),
                            FCSAdministrative_charges_Total = GetValueOrDefault(row, "FCSAdministrative_charges_Total"),

                            FCSSecurity_filling_fees = GetValueOrDefault(row, "FCSSecurity_filling_fees"),
                            FCSSecurity_filling_fees_GST = GetValueOrDefault(row, "FCSSecurity_filling_feesGST"),
                            FCSSecurity_filling_fees_GSTAmount = GetValueOrDefault(row, "FCSSecurity_filling_feesGSTAmount"),
                            FCSSecurity_filling_fees_Total = GetValueOrDefault(row, "FCSSecurity_filling_fees_Total"),

                            FCSOther = GetValueOrDefault(row, "FCSOther"),
                            FCSOther_GST = GetValueOrDefault(row, "FCSOtherGST"),
                            FCSOther_GSTAmount = GetValueOrDefault(row, "FCSOtherGSTAmount"),
                            FCSOther_Total = GetValueOrDefault(row, "FCSOther_Total"),

                            FCSAmt_Before_GST = GetValueOrDefault(row, "FCSAmt_before_GST"),
                            FCSGST = GetValueOrDefault(row, "FCSGST"),
                            FCSTotal = GetValueOrDefault(row, "FCSTOTAL"),

                            FCAAir_Freight = GetValueOrDefault(row, "FCAAir_Freight"),
                            FCAAir_Freight_GST = GetValueOrDefault(row, "FCAAir_FreightGST"),
                            FCAAir_Freight_GSTAmount = GetValueOrDefault(row, "FCAAir_FreightGSTAmount"),
                            FCAAir_Freight_Total = GetValueOrDefault(row, "FCAAir_Freight_Total"),

                            FCAMCC = GetValueOrDefault(row, "FCAMCC"),
                            FCAMCC_GST = GetValueOrDefault(row, "FCAMCCGST"),
                            FCAMCC_GSTAmount = GetValueOrDefault(row, "FCAMCCGSTAmount"),
                            FCAMCC_Total = GetValueOrDefault(row, "FCAMCC_Total"),

                            FCAX_Ray = GetValueOrDefault(row, "FCAX_Ray"),
                            FCAX_Ray_GST = GetValueOrDefault(row, "FCAX_RayGST"),
                            FCAX_Ray_GSTAmount = GetValueOrDefault(row, "FCAX_RayGSTAmount"),
                            FCAX_Ray_Total = GetValueOrDefault(row, "FCAX_Ray_Total"),

                            FCAMYC_Fuel = GetValueOrDefault(row, "FCAMYC_Fuel"),
                            FCAMYC_Fuel_GST = GetValueOrDefault(row, "FCAMYC_FuelGST"),
                            FCAMYC_Fuel_GSTAmount = GetValueOrDefault(row, "FCAMYC_FuelGSTAmount"),
                            FCAMYC_Fuel_Total = GetValueOrDefault(row, "FCAMYC_FuelTotal"),

                            FCAAMS = GetValueOrDefault(row, "FCAAMS"),
                            FCAAMS_GST = GetValueOrDefault(row, "FCAAMSGST"),
                            FCAAMS_GSTAmount = GetValueOrDefault(row, "FCAAMSGSTAmount"),
                            FCAAMS_Total = GetValueOrDefault(row, "FCAAMS_Total"),

                            FCAAWB = GetValueOrDefault(row, "FCAAWB"),
                            FCAAWB_GST = GetValueOrDefault(row, "FCAAWBGST"),
                            FCAAWB_GSTAmount = GetValueOrDefault(row, "FCAAWBGSTAmount"),
                            FCAAWB_Total = GetValueOrDefault(row, "FCAAWB_Total"),

                            FCAPCA = GetValueOrDefault(row, "FCAPCA"),
                            FCAPCA_GST = GetValueOrDefault(row, "FCAPCAGST"),
                            FCAPCA_GSTAmount = GetValueOrDefault(row, "FCAPCAGSTAmount"),
                            FCAPCA_Total = GetValueOrDefault(row, "FCAPCA_Total"),

                            FCAOthers = GetValueOrDefault(row, "FCAOthers"),
                            FCAOthers_GST = GetValueOrDefault(row, "FCAOthersGST"),
                            FCAOthers_GSTAmount = GetValueOrDefault(row, "FCAOthersGSTAmount"),
                            FCAOthers_Total = GetValueOrDefault(row, "FCAOthers_Total"),

                            FCAAMT_before_GST = GetValueOrDefault(row, "FCAAmt_before_GST"),
                            FCAGST = GetValueOrDefault(row, "FCAGST"),
                            FCATotal = GetValueOrDefault(row, "FCATOTAL"),
                            CFS_Vendor = GetValueOrDefault(row, "CFSVendor"),
                            CFS_Invoice_No = GetValueOrDefault(row, "CFSInvNo"),
                            CFS_InvDate = GetValueOrDefault(row, "CFSInvDate"),
                            CFS_Particulars = GetValueOrDefault(row, "CFSParticulars"),
                            CFS_Amt_Before_GST = GetValueOrDefault(row, "CFSAmt_before_GST"),
                            CFS_GST = GetValueOrDefault(row, "CFSGST"),
                            CFS_Total = GetValueOrDefault(row, "CFSTotal"),
                            TC_Transporter = GetValueOrDefault(row, "TCTransporter"),
                            TC_Invoice_No = GetValueOrDefault(row, "TCInvNo"),
                            TC_InvDate = GetValueOrDefault(row, "TCInvDate"),
                            TC_Charges = GetValueOrDefault(row, "TCCharges"),
                            TCChargesGST = GetValueOrDefault(row, "TCChargesGST"),
                            TCChargesGSTAmount = GetValueOrDefault(row, "TCChargesGSTAmount"),
                            TCCharges_Total = GetValueOrDefault(row, "TCCharges_Total"),

                            TC_VGM = GetValueOrDefault(row, "TCVGM"),
                            TCVGMGST = GetValueOrDefault(row, "TCVGMGST"),
                            TCVGMGSTAmount = GetValueOrDefault(row, "TCVGMGSTAmount"),
                            TCVGM_Total = GetValueOrDefault(row, "TCVGM_Total"),

                            TC_Other = GetValueOrDefault(row, "TCOther"),
                            TCOtherGST = GetValueOrDefault(row, "TCOtherGST"),
                            TCOtherGSTAmount = GetValueOrDefault(row, "TCOtherGSTAmount"),
                            TCOther_Total = GetValueOrDefault(row, "TCOther_Total"),

                            TC_AMT_Before_GST = GetValueOrDefault(row, "TCAmt_before_GST"),
                            TC_GST = GetValueOrDefault(row, "TCGST"),
                            TC_Total = GetValueOrDefault(row, "TCTotal"),
                            AddTC_Transporter = GetValueOrDefault(row, "AddTCTransporter"),
                            AddTC_Invoice_No = GetValueOrDefault(row, "AddTCInvNo"),
                            AddTC_InvDate = GetValueOrDefault(row, "AddTCInvDate"),
                            AddTCAdvPaymentOn = GetValueOrDefault(row, "AddTCAdvPaymentOn"),
                            AddTCPaymentDate = GetValueOrDefault(row, "AddTCPaymentDate"),
                            AddTC_Charges = GetValueOrDefault(row, "AddTCCharges"),
                            AddTCChargesGST = GetValueOrDefault(row, "AddTCChargesGST"),
                            AddTCChargesGSTAmount = GetValueOrDefault(row, "AddTCChargesGSTAmount"),
                            AddTCCharges_Total = GetValueOrDefault(row, "AddTCCharges_Total"),

                            AddTC_VGM = GetValueOrDefault(row, "AddTCVGM"),
                            AddTCVGMGST = GetValueOrDefault(row, "AddTCVGMGST"),
                            AddTCVGMGSTAmount = GetValueOrDefault(row, "AddTCVGMGSTAmount"),
                            AddTCVGM_Total = GetValueOrDefault(row, "AddTCVGM_Total"),

                            AddTC_Other = GetValueOrDefault(row, "AddTCOther"),
                            AddTCOtherGST = GetValueOrDefault(row, "AddTCOtherGST"),
                            AddTCOtherGSTAmount = GetValueOrDefault(row, "AddTCOtherGSTAmount"),
                            AddTCOther_Total = GetValueOrDefault(row, "AddTCOther_Total"),
                            AddTC_AMT_Before_GST = GetValueOrDefault(row, "AddTCAmt_before_GST"),
                            AddTC_GST = GetValueOrDefault(row, "AddTCGST"),
                            AddTC_Total = GetValueOrDefault(row, "AddTCTotal"),
                            COO_Vendor = GetValueOrDefault(row, "COOVendor"),
                            COO_Invoice_No = GetValueOrDefault(row, "COOInvNo"),
                            COO_InvDate = GetValueOrDefault(row, "COOInvDate"),
                            COO_Particulars = GetValueOrDefault(row, "COOParticulars"),
                            COO_Amt_Before_GST = GetValueOrDefault(row, "COOAmt_before_GST"),
                            COOGSTPercentage = GetValueOrDefault(row, "COOGSTPercentage"),
                            COO_GST = GetValueOrDefault(row, "COOGST"),
                            COO_Total = GetValueOrDefault(row, "COOTotal"),
                            EIA_Vendor = GetValueOrDefault(row, "EIAVendor"),
                            EIA_Invoice_No = GetValueOrDefault(row, "EIAInvNo"),
                            EIA_InvDate = GetFormattedDate(row, "EIAInvDate"),
                            EIA_Particulars = GetValueOrDefault(row, "EIAParticulars"),
                            EIA_Amt_Before_GST = GetValueOrDefault(row, "EIAAmt_before_GST"),
                            EIAGSTPercentage = GetValueOrDefault(row, "EIAGSTPercentage"),
                            EIA_GST = GetValueOrDefault(row, "EIAGST"),
                            EIA_Total = GetValueOrDefault(row, "EIATotal"),
                            Doc_ForwardedNo = GetValueOrDefault(row, "DocForwardedNo"),
                            Doc_Date = GetValueOrDefault(row, "DocDate"),
                            Doc_Sent_Through = GetValueOrDefault(row, "DocSentThrough"),
                            Doc_Submitted_Account_On = GetValueOrDefault(row, "DocSubmittedAccountOn"),
                            finalsave = row["FinalSave"] != DBNull.Value && (bool)row["FinalSave"],
                            runfor = Convert.ToInt32(row["RunFor"].ToString()),
                            Weight = GetValueOrDefault(row, "Weight"),
                            Quntity_SQM = GetValueOrDefault(row, "Quntity_SQM"),
                            Type = GetValueOrDefault(row, "Type"),
                            No_of_FCL = GetValueOrDefault(row, "No_of_FCL"),
                            AvgCostPerFCL = GetValueOrDefault(row, "AvgCostPerFCL"),
                            AvgCostPerSQM = GetValueOrDefault(row, "AvgCostPerSQM"),
                            FinalTotal=GetValueOrDefault(row, "FinalTotal"),
                            AdvPaymenton=GetValueOrDefault(row, "AdvPaymenton"),
                            PaymentDate=GetValueOrDefault(row, "PaymentDate")
                        };
                        export.ExporttrackerLogDetails = dtlog.AsEnumerable()
                               .Select(r => new InvoiceLogDetail
                               {
                                   SectionName = r["SectionName"].ToString(),
                                   LogDate = r["LogDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["LogDate"]),
                                   EmpId = r["EmpId"] == DBNull.Value ? 0 : Convert.ToInt32(r["EmpId"]),
                                   EmployeeName = r["EmployeeName"].ToString()
                               }).ToList();
                      
                        export.OtherCharges = dtOther.AsEnumerable()
                            .Where(r => Convert.ToInt32(r["ETMasterID"]) == export.rowid)
                            .Select(r => new Exporttracker_OtherCharges
                            {
                                OC_RowId = r["RowId"] != DBNull.Value ? Convert.ToInt32(r["RowId"]) : 0,
                                OC_ETMasterId = r["ETMasterId"] != DBNull.Value ? Convert.ToInt32(r["ETMasterId"]) : 0,
                                OC_Vendor = GetValueOrDefault(r, "OCVendor"),
                                OC_Invoice_No = GetValueOrDefault(r, "OCInvNo"),
                                OC_InvDate = GetValueOrDefault(r, "OCInvDate"),
                                OCAdvPaymenton = GetValueOrDefault(r, "OCAdvPaymenton"),
                                OCPaymentDate = GetValueOrDefault(r, "OCPaymentDate"),
                                OC_BLAdvPaymenton = GetValueOrDefault(r, "OC_BLAdvPaymenton"),
                                OC_BLPaymentDate = GetValueOrDefault(r, "OC_BLPaymentDate"),
                                OC_Particulars = GetValueOrDefault(r, "OCParticulars"),
                                OC_Amt_Before_GST = r["OCAmt_Before_GST"] != DBNull.Value ? Convert.ToDecimal(r["OCAmt_Before_GST"]).ToString("F2") : "0.00",
                                OC_GSTPercentage = r["OCGSTPercentage"] != DBNull.Value ? Convert.ToDecimal(r["OCGSTPercentage"]).ToString("F2") : "0.00",
                                OC_GSTAmount = r["OCGST"] != DBNull.Value ? Convert.ToDecimal(r["OCGST"]).ToString("F2") : "0.00",
                                OC_Total = r["OCTotal"] != DBNull.Value ? Convert.ToDecimal(r["OCTotal"]).ToString("F2") : "0.00"
                            }).ToList();
                        export.CFSOtherCharges = dtcfsOther.AsEnumerable()
                                .Where(r => Convert.ToInt32(r["MasterId"]) == export.rowid)
                                .Select(r => new ExportCFSChargeDetails
                                {
                                    Cfs_RowId = r["RowID"] != DBNull.Value ? Convert.ToInt32(r["RowID"]) : 0,
                                    Cfs_MasterId = r["MasterId"] != DBNull.Value ? Convert.ToInt32(r["MasterId"]) : 0,
                                    Nature_of_Service = GetValueOrDefault(r, "Nature_of_Service"),
                                    AmtBeforeGST = GetValueOrDefault(r, "AmtBeforeGST"),
                                    GstPercentage = r["GstPercentage"] != DBNull.Value ? Convert.ToDecimal(r["GstPercentage"]).ToString("F2") : "0.00",
                                    GstAmount = r["GstAmount"] != DBNull.Value ? Convert.ToDecimal(r["GstAmount"]).ToString("F2") : "0.00",
                                    Total = r["Total"] != DBNull.Value ? Convert.ToDecimal(r["Total"]).ToString("F2") : "0.00"
                                }).ToList();
                        exportList.Add(export);
                    }
                    if (SearchType == "Pending")
                    {
                        exportList = exportList.Where(x => x.runfor == 1 || x.runfor == 2).ToList();
                    }
                    else if (SearchType == "Completed")
                    {
                        exportList = exportList.Where(x => x.runfor == 3).ToList();
                    }
                    else if (SearchType == "All")
                    {
                        exportList = exportList.Where(x => x.runfor == 1 || x.runfor == 2|| x.runfor == 3).ToList();
                    }
                    else
                    {
                        Console.WriteLine("No Data found");
                    }
                    // If SearchType is "Pending", filter the list for runfor = 1 or 2

                }

                ObjSQLHelper.ClearObjects();
                //return Json(exportList, JsonRequestBehavior.AllowGet);
                return new JsonResult()
                {
                    Data = exportList,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };
            }
            catch (Exception e)
            {
                return Json(new { error = "Error: " + e.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult GetShipmentGridList()
        {
            string sortDefault = "";
            int PageSize = 859;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdMaster = new SqlCommand();
            cmdMaster.Parameters.AddWithValue("@Mode", "Get Shipment Data");


            try
            {
                DataSet dsMaster = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdMaster);

                List<Shipmenttracker> exportList = new List<Shipmenttracker>();
                if (!(dsMaster == null || dsMaster.Tables == null || dsMaster.Tables.Count == 0))
                {
                    DataTable dtMaster = dsMaster.Tables[0];
                    

                    foreach (DataRow row in dtMaster.Rows)
                    {



                        Shipmenttracker export = new Shipmenttracker
                        {
                            SrNo = GetValueOrDefault(row, "SrNo"),

                            
                            Invoice_No = GetValueOrDefault(row, "InvNo"),
                            InvDate = GetValueOrDefault(row, "InvDate"),
                            No_of_FCL = GetValueOrDefault(row, "No_of_FCL"),
                            Bill_to_Name = GetValueOrDefault(row, "Bill-to Name"),
                            Ship_to_Name = GetValueOrDefault(row, "Ship-to Name"),
                            Mode = GetValueOrDefault(row, "Mode_of_Transport"),
                            Port_of_Loading = GetValueOrDefault(row, "Port_of_Loading"),
                            Port_of_Discharge = GetValueOrDefault(row, "Port_of_Discharge"),
                            Vessel_Airlines = GetValueOrDefault(row, "Vessel_Airlines"),
                            ETD = GetValueOrDefault(row, "ETD"),
                            ETA = GetValueOrDefault(row, "ETA"),
                            Transit_Time = GetValueOrDefault(row, "Transit_Time"),
                            BL_Express = GetValueOrDefault(row, "BL_Express"),
                            Doc_Date = GetValueOrDefault(row, "DocDate"),
                            Doc_Submitted_Account_On = GetValueOrDefault(row, "DocSubmittedAccountOn"),
                        };
                       
                        exportList.Add(export);
                    }
                    
                    // If SearchType is "Pending", filter the list for runfor = 1 or 2

                }

                ObjSQLHelper.ClearObjects();
                return Json(exportList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = "Error: " + e.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult GetAdvanceReportGridList()
        {
            string sortDefault = "";
            int PageSize = 859;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdMaster = new SqlCommand();
            cmdMaster.Parameters.AddWithValue("@Mode", "Get AdvancePayment Data");


            try
            {
                DataSet dsMaster = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdMaster);

                List<AdvanceTracker> exportList = new List<AdvanceTracker>();
                if (!(dsMaster == null || dsMaster.Tables == null || dsMaster.Tables.Count == 0))
                {
                    DataTable dtMaster = dsMaster.Tables[0];
                    

                    foreach (DataRow row in dtMaster.Rows)
                    {



                        AdvanceTracker export = new AdvanceTracker
                        {
                            SrNo = GetValueOrDefault(row, "SrNo"),
                            Division= GetValueOrDefault(row, "Division"),
                            Invoice_No = GetValueOrDefault(row, "InvNo"),
                            FCForwarder = GetValueOrDefault(row, "FCForwarder"),
                            FCInvoiceNo = GetValueOrDefault(row, "FCInvNo"),
                            FCInvDate = GetValueOrDefault(row, "FCInvDate"),
                            FCSTOTAL = GetValueOrDefault(row, "FCSTOTAL"),
                            FCATOTAL = GetValueOrDefault(row, "FCATOTAL"),
                            Inco_Terms = GetValueOrDefault(row, "Inco_Terms"),
                            AdvPaymenton = GetValueOrDefault(row, "AdvPaymenton"),
                            PaymentDate = GetValueOrDefault(row, "PaymentDate"),
                            SourceType = GetValueOrDefault(row, "SourceType"),
                        };
                        exportList.Add(export);
                    }
                }
                ObjSQLHelper.ClearObjects();
                return Json(exportList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = "Error: " + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetFCLByMode(string mode)
        {
            ClsExportTracker helper = new ClsExportTracker();
            var list = helper.GetAllListFCL(mode); // filtered by mode
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public static string GetValueOrDefault(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? row[columnName].ToString() : string.Empty;
        }

        public JsonResult GetGridListComForPIStatus()
        {
            string sortDefault = "";
            int PageSize = 859;
            sortDefault = "Doc_Date DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd2 = new SqlCommand();
            DataSet ds2 = new DataSet();
            ds2 = ObjSQLHelper.SelectProcDataDS("TS_CommisstionTracker_PI_status_New", cmd2);

            List<Commissiontracker> dynamicDtCommision = new List<Commissiontracker>();
            if (!(ds2 == null || ds2.Tables == null || ds2.Tables.Count == 0))
            {
                DataTable dt2 = ds2.Tables[0];
                foreach (DataRow row in dt2.Rows)
                {
                    Commissiontracker com = new Commissiontracker();


                    com.SrNo = row["SrNo"].ToString();
                    com.PI_No = row["PINo"].ToString();
                    com.OCF_No = row["OCF_No"].ToString();
                    com.Invoice_No = row["InvoiceNo"].ToString();
                    com.PI_Date = ((DateTime?)row["PIDate"])?.ToString("dd/MM/yyyy");
                    com.CustomerName = row["CustomerName"].ToString();
                    com.Commission_Vendor_Name = row["VendorName"].ToString();
                    com.CommissionType = row["CommissionType"].ToString();
                    com.CommissionRate = decimal.Parse(row["CommissionRate"].ToString());
                    com.SalesPerson_Name = row["SalesPersonName"].ToString();
                    com.TaggedOnPI = Convert.ToBoolean(row["TaggedONPI_Flag"]);
                    com.TaggedOnOCF = Convert.ToBoolean(row["TaggedOnERP_Flag"]);
                    com.TaggedOnINV = Convert.ToBoolean(row["TaggedONINV_Flag"]);

                    dynamicDtCommision.Add(com);  // Add the populated CommissionTracker object to the list
                }
            }

            //var result = new
            //{
            //    FreightTrackerData = dynamicDt,
            //    CommissionTrackerData = dynamicDt1
            //};


            //ObjSQLHelper.ClearObjects();


            //return Json(result, JsonRequestBehavior.AllowGet);

            ObjSQLHelper.ClearObjects();
            return Json(dynamicDtCommision, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetpanelExportForList(string invoiceNo = "")
        {

            string sortDefault = "";
            int PageSize = 859;
            sortDefault = "Doc_Date DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@InvoiceNo", invoiceNo);

            DataSet ds = new DataSet();
            //int Total = 0;

            ds = ObjSQLHelper.SelectProcDataDS("Export_GetInvoiceDetails", cmd);
            SqlCommand cmdOther = new SqlCommand();
            cmdOther.Parameters.AddWithValue("@Mode", "OtherGet");
            DataSet dsOther = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdOther);
            SqlCommand cmdcfsOther = new SqlCommand();
            cmdcfsOther.Parameters.AddWithValue("@Mode", "CFS Charges get");
            DataSet dscfsOther = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdcfsOther);
            SqlCommand cmdLog = new SqlCommand();
            cmdLog.Parameters.AddWithValue("@Mode", "GetLogDetails");
            cmdLog.Parameters.AddWithValue("@InvNo", invoiceNo);
            DataSet dslog = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdLog);

            List<Exporttracker> dynamicDtList = new List<Exporttracker>();
            if (!(ds == null || ds.Tables == null || ds.Tables.Count == 0))
            {
                dt = ds.Tables[0];
                DataTable dtOther = dsOther?.Tables.Count > 0 ? dsOther.Tables[0] : new DataTable();
                DataTable dtcfsOther = dscfsOther?.Tables.Count > 0 ? dscfsOther.Tables[0] : new DataTable();

                DataTable dtlog = dslog?.Tables.Count > 0 ? dslog.Tables[0] : new DataTable();
                foreach (DataRow row in dt.Rows)
                {
                    Exporttracker CSList = new Exporttracker()
                    {
                        SrNo = GetValueOrDefault(row, "SrNo"),
                        rowid = row["RowId"] != DBNull.Value ? Convert.ToInt32(row["RowId"]) : 0,
                        Invoice_No = GetValueOrDefault(row, "InvNo"),
                        InvDate = GetValueOrDefault(row, "InvDate"),
                        Division = GetValueOrDefault(row, "Division"),
                        Mfr_Out = GetValueOrDefault(row, "MFROut"),
                        Bill_to_Name = GetValueOrDefault(row, "Bill-to Name"),
                        Ship_to_Name = GetValueOrDefault(row, "Ship-to Name"),
                        Shipping_Bill_No = GetValueOrDefault(row, "Shipping Bill No_"),
                        Shipping_Bill_Date = GetValueOrDefault(row, "Shipment Date"),
                        Currency = string.IsNullOrWhiteSpace(GetValueOrDefault(row, "Currency Code"))
                                                   ? "INR"
                                                   : GetValueOrDefault(row, "Currency Code"),
                        // Currency = GetValueOrDefault(row, "Currency Code"),
                        Basic_Amt_Charges = GetValueOrDefault(row, "FOB Value(In Foreign Currency)"),
                        Sea_Freight_Air_Freight = GetValueOrDefault(row, "Freight Amount"),
                        Insurarnce_charged = GetValueOrDefault(row, "Insurarnce_charged"),
                        Other_Charges = GetValueOrDefault(row, "Other_Charges"),
                        Invoice_Value = GetValueOrDefault(row, "Invoice_Value"),
                        Mode = GetValueOrDefault(row, "Mode_of_Transport"),
                        Country = GetValueOrDefault(row, "Country"),
                        Port_of_Discharge = GetValueOrDefault(row, "Port_of_Discharge"),
                        Payment_Terms = GetValueOrDefault(row, "Payment Terms Code"),
                        Due_Date = row["Due Date"] != DBNull.Value ? row["Due Date"].ToString() : "",
                        EPCG_Licence_No = GetValueOrDefault(row, "EPCG_Licence_No"),
                        AirWay_BillNo_Bill_of_LodingNo = GetValueOrDefault(row, "AirWay_BillNo_Bill_of_LodingNo"),
                        AirWay_BillDate_Bill_of_LodingDate = GetValueOrDefault(row, "AirWay_BillDate_Bill_of_LodingDate"),
                        Clearing_Point = GetValueOrDefault(row, "Clearing_Point"),
                        Inco_Terms = GetValueOrDefault(row, "Inco_Terms"),
                        Port_of_Loading = GetValueOrDefault(row, "Port_of_Loading"),
                        Remarks = GetValueOrDefault(row, "Remarks"),
                        CCClearance_Agent_Name = GetValueOrDefault(row, "CCClearance_Agent"),
                        CCInvoice_No = GetValueOrDefault(row, "CCInvNo"),
                        CCInvDate = GetValueOrDefault(row, "CCInvDate"),
                        CCClearance_agency_charges = GetValueOrDefault(row, "CCClearance_Agency_charges"),
                        CCClearance_Agency_chargesGST = GetValueOrDefault(row, "CCClearance_Agency_chargesGST"),
                        CCClearance_Agency_chargesGSTAmount = GetValueOrDefault(row, "CCClearance_Agency_chargesGSTAmount"),
                        CCClearance_Agency_charges_Total = GetValueOrDefault(row, "CCClearance_Agency_charges_Total"),

                        CCEDI_Xeam_charges = GetValueOrDefault(row, "CCEdi_Xeam_Charges"),
                        CCEdi_Xeam_ChargesGST = GetValueOrDefault(row, "CCEdi_Xeam_ChargesGST"),
                        CCEdi_Xeam_ChargesGSTAmount = GetValueOrDefault(row, "CCEdi_Xeam_ChargesGSTAmount"),
                        CCEdi_Xeam_Charges_Total = GetValueOrDefault(row, "CCEdi_Xeam_Charges_Total"),

                        CCVGM = GetValueOrDefault(row, "CCVGM"),
                        CCVGMGST = GetValueOrDefault(row, "CCVGMGST"),
                        CCVGMGSTAmount = GetValueOrDefault(row, "CCVGMGSTAmount"),
                        CCVGM_Total = GetValueOrDefault(row, "CCVGM_Total"),

                        CCGSEC = GetValueOrDefault(row, "CCGSEC"),
                        CCGSECGST = GetValueOrDefault(row, "CCGSECGST"),
                        CCGSECGSTAmount = GetValueOrDefault(row, "CCGSECGSTAmount"),
                        CCGSEC_Total = GetValueOrDefault(row, "CCGSEC_Total"),

                        CCCOO = GetValueOrDefault(row, "CCCOO"),
                        CCCOOGST = GetValueOrDefault(row, "CCCOOGST"),
                        CCCOOGSTAmount = GetValueOrDefault(row, "CCCOOGSTAmount"),
                        CCCOO_Total = GetValueOrDefault(row, "CCCOO_Total"),

                        CCExamination_CFS = GetValueOrDefault(row, "CCExamination_CFS"),
                        CCExamination_CFSGST = GetValueOrDefault(row, "CCExamination_CFSGST"),
                        CCExamination_CFSGSTAmount = GetValueOrDefault(row, "CCExamination_CFSGSTAmount"),
                        CCExamination_CFS_Total = GetValueOrDefault(row, "CCExamination_CFS_Total"),

                        CCLift_on_Lift_off = GetValueOrDefault(row, "CCLift_on_Lift_off"),
                        CCLift_on_Lift_offGST = GetValueOrDefault(row, "CCLift_on_Lift_offGST"),
                        CCLift_on_Lift_offGSTAmount = GetValueOrDefault(row, "CCLift_on_Lift_offGSTAmount"),
                        CCLift_on_Lift_off_Total = GetValueOrDefault(row, "CCLift_on_Lift_off_Total"),

                        CCCFS = GetValueOrDefault(row, "CCCFS"),
                        CCCFSGST = GetValueOrDefault(row, "CCCFSGST"),
                        CCCFSGSTAmount = GetValueOrDefault(row, "CCCFSGSTAmount"),
                        CCCFSTotal = GetValueOrDefault(row, "CCCFSTotal"),

                        CCOthers = GetValueOrDefault(row, "CCOthers"),
                        CCOthersGST = GetValueOrDefault(row, "CCOthersGST"),
                        CCOthersGSTAmount = GetValueOrDefault(row, "CCOthersGSTAmount"),
                        CCOthers_Total = GetValueOrDefault(row, "CCOthers_Total"),
                        CCAmt_Before_GST = GetValueOrDefault(row, "CCAmt_Before_GST"),
                        CCGST = GetValueOrDefault(row, "CCGST"),
                        CC_total = GetValueOrDefault(row, "CCTotal"),

                        FCForwarder = GetValueOrDefault(row, "FCForwarder"),
                        FCInvoice_No = GetValueOrDefault(row, "FCInvNo"),
                        FCInvDate = GetValueOrDefault(row, "FCInvDate"),

                        FCSFreight = GetValueOrDefault(row, "FCSFreight"),
                        FCSFreight_GST = GetValueOrDefault(row, "FCSFreightGST"),
                        FCSFreight_GSTAmount = GetValueOrDefault(row, "FCSFreightGSTAmount"),
                        FCSFreight_Total = GetValueOrDefault(row, "FCSFreight_Total"),

                        FCSTHC = GetValueOrDefault(row, "FCSTHC"),
                        FCSTHC_GST = GetValueOrDefault(row, "FCSTHCGST"),
                        FCSTHC_GSTAmount = GetValueOrDefault(row, "FCSTHCGSTAmount"),
                        FCSTHC_Total = GetValueOrDefault(row, "FCSTHC_Total"),

                        FCSBL = GetValueOrDefault(row, "FCSBL"),
                        FCSBL_GST = GetValueOrDefault(row, "FCSBLGST"),
                        FCSBL_GSTAmount = GetValueOrDefault(row, "FCSBLGSTAmount"),
                        FCSBL_Total = GetValueOrDefault(row, "FCSBL_Total"),

                        FCSSeal = GetValueOrDefault(row, "FCSSEAL"),
                        FCSSEAL_GST = GetValueOrDefault(row, "FCSSEALGST"),
                        FCSSEAL_GSTAmount = GetValueOrDefault(row, "FCSSEALGSTAmount"),
                        FCSSEAL_Total = GetValueOrDefault(row, "FCSSEAL_Total"),

                        FCSVGM = GetValueOrDefault(row, "FCSVGM"),
                        FCSVGM_GST = GetValueOrDefault(row, "FCSVGMGST"),
                        FCSVGM_GSTAmount = GetValueOrDefault(row, "FCSVGMGSTAmount"),
                        FCSVGM_Total = GetValueOrDefault(row, "FCSVGM_Total"),

                        FCSMUC = GetValueOrDefault(row, "FCSMUC"),
                        FCSMUC_GST = GetValueOrDefault(row, "FCSMUCGST"),
                        FCSMUC_GSTAmount = GetValueOrDefault(row, "FCSMUCGSTAmount"),
                        FCSMUC_Total = GetValueOrDefault(row, "FCSMUC_Total"),

                        FCSITHC = GetValueOrDefault(row, "FCSITHC"),
                        FCSITHC_GST = GetValueOrDefault(row, "FCSITHCGST"),
                        FCSITHC_GSTAmount = GetValueOrDefault(row, "FCSITHCGSTAmount"),
                        FCSITHC_Total = GetValueOrDefault(row, "FCSITHC_Total"),

                        FCSDry_Port_charges = GetValueOrDefault(row, "FCSDry_Port_charges"),
                        FCSDry_Port_charges_GST = GetValueOrDefault(row, "FCSDry_Port_chargesGST"),
                        FCSDry_Port_charges_GSTAmount = GetValueOrDefault(row, "FCSDry_Port_chargesGSTAmount"),
                        FCSDry_Port_charges_Total = GetValueOrDefault(row, "FCSDry_Port_charges_Total"),

                        FCSAdministrative_charges = GetValueOrDefault(row, "FCSAdministrative_charges"),
                        FCSAdministrative_charges_GST = GetValueOrDefault(row, "FCSAdministrative_chargesGST"),
                        FCSAdministrative_charges_GSTAmount = GetValueOrDefault(row, "FCSAdministrative_chargesGSTAmount"),
                        FCSAdministrative_charges_Total = GetValueOrDefault(row, "FCSAdministrative_charges_Total"),

                        FCSSecurity_filling_fees = GetValueOrDefault(row, "FCSSecurity_filling_fees"),
                        FCSSecurity_filling_fees_GST = GetValueOrDefault(row, "FCSSecurity_filling_feesGST"),
                        FCSSecurity_filling_fees_GSTAmount = GetValueOrDefault(row, "FCSSecurity_filling_feesGSTAmount"),
                        FCSSecurity_filling_fees_Total = GetValueOrDefault(row, "FCSSecurity_filling_fees_Total"),

                        FCSOther = GetValueOrDefault(row, "FCSOther"),
                        FCSOther_GST = GetValueOrDefault(row, "FCSOtherGST"),
                        FCSOther_GSTAmount = GetValueOrDefault(row, "FCSOtherGSTAmount"),
                        FCSOther_Total = GetValueOrDefault(row, "FCSOther_Total"),

                        FCSAmt_Before_GST = GetValueOrDefault(row, "FCSAmt_before_GST"),
                        FCSGST = GetValueOrDefault(row, "FCSGST"),
                        FCSTotal = GetValueOrDefault(row, "FCSTOTAL"),

                        FCAAir_Freight = GetValueOrDefault(row, "FCAAir_Freight"),
                        FCAAir_Freight_GST = GetValueOrDefault(row, "FCAAir_FreightGST"),
                        FCAAir_Freight_GSTAmount = GetValueOrDefault(row, "FCAAir_FreightGSTAmount"),
                        FCAAir_Freight_Total = GetValueOrDefault(row, "FCAAir_Freight_Total"),

                        FCAMCC = GetValueOrDefault(row, "FCAMCC"),
                        FCAMCC_GST = GetValueOrDefault(row, "FCAMCCGST"),
                        FCAMCC_GSTAmount = GetValueOrDefault(row, "FCAMCCGSTAmount"),
                        FCAMCC_Total = GetValueOrDefault(row, "FCAMCC_Total"),

                        FCAX_Ray = GetValueOrDefault(row, "FCAX_Ray"),
                        FCAX_Ray_GST = GetValueOrDefault(row, "FCAX_RayGST"),
                        FCAX_Ray_GSTAmount = GetValueOrDefault(row, "FCAX_RayGSTAmount"),
                        FCAX_Ray_Total = GetValueOrDefault(row, "FCAX_Ray_Total"),

                        FCAMYC_Fuel = GetValueOrDefault(row, "FCAMYC_Fuel"),
                        FCAMYC_Fuel_GST = GetValueOrDefault(row, "FCAMYC_FuelGST"),
                        FCAMYC_Fuel_GSTAmount = GetValueOrDefault(row, "FCAMYC_FuelGSTAmount"),
                        FCAMYC_Fuel_Total = GetValueOrDefault(row, "FCAMYC_FuelTotal"),

                        FCAAMS = GetValueOrDefault(row, "FCAAMS"),
                        FCAAMS_GST = GetValueOrDefault(row, "FCAAMSGST"),
                        FCAAMS_GSTAmount = GetValueOrDefault(row, "FCAAMSGSTAmount"),
                        FCAAMS_Total = GetValueOrDefault(row, "FCAAMS_Total"),

                        FCAAWB = GetValueOrDefault(row, "FCAAWB"),
                        FCAAWB_GST = GetValueOrDefault(row, "FCAAWBGST"),
                        FCAAWB_GSTAmount = GetValueOrDefault(row, "FCAAWBGSTAmount"),
                        FCAAWB_Total = GetValueOrDefault(row, "FCAAWB_Total"),

                        FCAPCA = GetValueOrDefault(row, "FCAPCA"),
                        FCAPCA_GST = GetValueOrDefault(row, "FCAPCAGST"),
                        FCAPCA_GSTAmount = GetValueOrDefault(row, "FCAPCAGSTAmount"),
                        FCAPCA_Total = GetValueOrDefault(row, "FCAPCA_Total"),

                        FCAOthers = GetValueOrDefault(row, "FCAOthers"),
                        FCAOthers_GST = GetValueOrDefault(row, "FCAOthersGST"),
                        FCAOthers_GSTAmount = GetValueOrDefault(row, "FCAOthersGSTAmount"),
                        FCAOthers_Total = GetValueOrDefault(row, "FCAOthers_Total"),

                        FCAAMT_before_GST = GetValueOrDefault(row, "FCAAmt_before_GST"),
                        FCAGST = GetValueOrDefault(row, "FCAGST"),
                        FCATotal = GetValueOrDefault(row, "FCATOTAL"),
                        CFS_Vendor = GetValueOrDefault(row, "CFSVendor"),
                        CFS_Invoice_No = GetValueOrDefault(row, "CFSInvNo"),
                        CFS_InvDate = GetValueOrDefault(row, "CFSInvDate"),
                        CFS_Particulars = GetValueOrDefault(row, "CFSParticulars"),
                        CFS_Amt_Before_GST = GetValueOrDefault(row, "CFSAmt_before_GST"),
                        CFS_GST = GetValueOrDefault(row, "CFSGST"),
                        CFS_Total = GetValueOrDefault(row, "CFSTotal"),
                        TC_Transporter = GetValueOrDefault(row, "TCTransporter"),
                        TC_Invoice_No = GetValueOrDefault(row, "TCInvNo"),
                        TC_InvDate = GetValueOrDefault(row, "TCInvDate"),
                        TC_Charges = GetValueOrDefault(row, "TCCharges"),
                        TCChargesGST = GetValueOrDefault(row, "TCChargesGST"),
                        TCChargesGSTAmount = GetValueOrDefault(row, "TCChargesGSTAmount"),
                        TCCharges_Total = GetValueOrDefault(row, "TCCharges_Total"),

                        TC_VGM = GetValueOrDefault(row, "TCVGM"),
                        TCVGMGST = GetValueOrDefault(row, "TCVGMGST"),
                        TCVGMGSTAmount = GetValueOrDefault(row, "TCVGMGSTAmount"),
                        TCVGM_Total = GetValueOrDefault(row, "TCVGM_Total"),

                        TC_Other = GetValueOrDefault(row, "TCOther"),
                        TCOtherGST = GetValueOrDefault(row, "TCOtherGST"),
                        TCOtherGSTAmount = GetValueOrDefault(row, "TCOtherGSTAmount"),
                        TCOther_Total = GetValueOrDefault(row, "TCOther_Total"),

                        TC_AMT_Before_GST = GetValueOrDefault(row, "TCAmt_before_GST"),
                        TC_GST = GetValueOrDefault(row, "TCGST"),
                        TC_Total = GetValueOrDefault(row, "TCTotal"),
                        AddTC_Transporter = GetValueOrDefault(row, "AddTCTransporter"),
                        AddTC_Invoice_No = GetValueOrDefault(row, "AddTCInvNo"),
                        AddTC_InvDate = GetValueOrDefault(row, "AddTCInvDate"),
                        AddTCAdvPaymentOn = GetValueOrDefault(row, "AddTCAdvPaymentOn"),
                        AddTCPaymentDate = GetValueOrDefault(row, "AddTCPaymentDate"),
                        AddTC_Charges = GetValueOrDefault(row, "AddTCCharges"),
                        AddTCChargesGST = GetValueOrDefault(row, "AddTCChargesGST"),
                        AddTCChargesGSTAmount = GetValueOrDefault(row, "AddTCChargesGSTAmount"),
                        AddTCCharges_Total = GetValueOrDefault(row, "AddTCCharges_Total"),

                        AddTC_VGM = GetValueOrDefault(row, "AddTCVGM"),
                        AddTCVGMGST = GetValueOrDefault(row, "AddTCVGMGST"),
                        AddTCVGMGSTAmount = GetValueOrDefault(row, "AddTCVGMGSTAmount"),
                        AddTCVGM_Total = GetValueOrDefault(row, "AddTCVGM_Total"),

                        AddTC_Other = GetValueOrDefault(row, "AddTCOther"),
                        AddTCOtherGST = GetValueOrDefault(row, "AddTCOtherGST"),
                        AddTCOtherGSTAmount = GetValueOrDefault(row, "AddTCOtherGSTAmount"),
                        AddTCOther_Total = GetValueOrDefault(row, "AddTCOther_Total"),
                        AddTC_AMT_Before_GST = GetValueOrDefault(row, "AddTCAmt_before_GST"),
                        AddTC_GST = GetValueOrDefault(row, "AddTCGST"),
                        AddTC_Total = GetValueOrDefault(row, "AddTCTotal"),
                        COO_Vendor = GetValueOrDefault(row, "COOVendor"),
                        COO_Invoice_No = GetValueOrDefault(row, "COOInvNo"),
                        COO_InvDate = GetValueOrDefault(row, "COOInvDate"),
                        COO_Particulars = GetValueOrDefault(row, "COOParticulars"),
                        COO_Amt_Before_GST = GetValueOrDefault(row, "COOAmt_before_GST"),
                        COOGSTPercentage = GetValueOrDefault(row, "COOGSTPercentage"),
                        COO_GST = GetValueOrDefault(row, "COOGST"),
                        COO_Total = GetValueOrDefault(row, "COOTotal"),
                        EIA_Vendor = GetValueOrDefault(row, "EIAVendor"),
                        EIA_Invoice_No = GetValueOrDefault(row, "EIAInvNo"),
                        EIA_InvDate = GetFormattedDate(row, "EIAInvDate"),
                        EIA_Particulars = GetValueOrDefault(row, "EIAParticulars"),
                        EIA_Amt_Before_GST = GetValueOrDefault(row, "EIAAmt_before_GST"),
                        EIAGSTPercentage = GetValueOrDefault(row, "EIAGSTPercentage"),
                        EIA_GST = GetValueOrDefault(row, "EIAGST"),
                        EIA_Total = GetValueOrDefault(row, "EIATotal"),
                        Doc_ForwardedNo = GetValueOrDefault(row, "DocForwardedNo"),
                        Doc_Date = GetValueOrDefault(row, "DocDate"),
                        Doc_Sent_Through = GetValueOrDefault(row, "DocSentThrough"),
                        Doc_Submitted_Account_On = GetValueOrDefault(row, "DocSubmittedAccountOn"),
                        finalsave = row["FinalSave"] != DBNull.Value && (bool)row["FinalSave"],
                        runfor = Convert.ToInt32(row["RunFor"].ToString()),
                        Weight = GetValueOrDefault(row, "Weight"),
                        Quntity_SQM = GetValueOrDefault(row, "Quntity_SQM"),
                        Type = GetValueOrDefault(row, "Type"),
                        No_of_FCL = GetValueOrDefault(row, "No_of_FCL"),
                        AvgCostPerFCL = GetValueOrDefault(row, "AvgCostPerFCL"),
                        AvgCostPerSQM = GetValueOrDefault(row, "AvgCostPerSQM"),
                        AdvPaymenton = GetValueOrDefault(row, "AdvPaymenton"),
                        PaymentDate = GetValueOrDefault(row, "PaymentDate")
                    };
                    CSList.ExporttrackerLogDetails = dtlog.AsEnumerable()
                          .Select(r => new InvoiceLogDetail
                          {
                              SectionName = r["SectionName"].ToString(),
                              LogDate = r["LogDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["LogDate"]),
                              EmpId = r["EmpId"] == DBNull.Value ? 0 : Convert.ToInt32(r["EmpId"]),
                              EmployeeName = r["EmployeeName"].ToString()
                          }).ToList();
                  

                    CSList.OtherCharges = dtOther.AsEnumerable()
                            .Where(r => Convert.ToInt32(r["ETMasterID"]) == CSList.rowid)
                            .Select(r => new Exporttracker_OtherCharges
                            {
                                OC_RowId = r["RowId"] != DBNull.Value ? Convert.ToInt32(r["RowId"]) : 0,
                                OC_ETMasterId = r["ETMasterId"] != DBNull.Value ? Convert.ToInt32(r["ETMasterId"]) : 0,
                                OC_Vendor = GetValueOrDefault(r, "OCVendor"),
                                OC_Invoice_No = GetValueOrDefault(r, "OCInvNo"),
                                OC_InvDate = GetValueOrDefault(r, "OCInvDate"),
                                OCAdvPaymenton = GetValueOrDefault(r, "OCAdvPaymenton"),
                                OC_BLAdvPaymenton = GetValueOrDefault(r, "OC_BLAdvPaymenton"),
                                OC_BLPaymentDate = GetValueOrDefault(r, "OC_BLPaymentDate"),
                                OCPaymentDate = GetValueOrDefault(r, "OCPaymentDate"),
                                OC_Particulars = GetValueOrDefault(r, "OCParticulars"),

                                OC_Amt_Before_GST = r["OCAmt_Before_GST"] != DBNull.Value ? Convert.ToDecimal(r["OCAmt_Before_GST"]).ToString("F2") : "0.00",
                                OC_GSTPercentage = r["OCGSTPercentage"] != DBNull.Value ? Convert.ToDecimal(r["OCGSTPercentage"]).ToString("F2") : "0.00",
                                OC_GSTAmount = r["OCGST"] != DBNull.Value ? Convert.ToDecimal(r["OCGST"]).ToString("F2") : "0.00",
                                OC_Total = r["OCTotal"] != DBNull.Value ? Convert.ToDecimal(r["OCTotal"]).ToString("F2") : "0.00"
                            }).ToList();

                    CSList.CFSOtherCharges = dtcfsOther.AsEnumerable()
                        .Where(r => Convert.ToInt32(r["MasterId"]) == CSList.rowid)
                        .Select(r => new ExportCFSChargeDetails
                        {
                            Cfs_RowId = r["RowID"] != DBNull.Value ? Convert.ToInt32(r["RowID"]) : 0,
                            Cfs_MasterId = r["MasterId"] != DBNull.Value ? Convert.ToInt32(r["MasterId"]) : 0,
                            Nature_of_Service = GetValueOrDefault(r, "Nature_of_Service"),
                            AmtBeforeGST = GetValueOrDefault(r, "AmtBeforeGST"),
                            GstPercentage = r["GstPercentage"] != DBNull.Value ? Convert.ToDecimal(r["GstPercentage"]).ToString("F2") : "0.00",
                            GstAmount = r["GstAmount"] != DBNull.Value ? Convert.ToDecimal(r["GstAmount"]).ToString("F2") : "0.00",
                            Total = r["Total"] != DBNull.Value ? Convert.ToDecimal(r["Total"]).ToString("F2") : "0.00"
                        }).ToList();






                    dynamicDtList.Add(CSList);
                }
            }
            ObjSQLHelper.ClearObjects();
            return Json(dynamicDtList, JsonRequestBehavior.AllowGet);
        }
        private object FormatDateString(string dateStr)
        {
            if (DateTime.TryParse(dateStr, out DateTime parsedDate))
            {
                return parsedDate.ToString("yyyy-MM-dd");  // ISO format string
            }
            else
            {
                return DBNull.Value;
            }
        }
        //private List<ImprestDetailsModel> GetFilteredImprestDetails(int ObjectType, int Month, int Year, string EmpName = "", int? DivisionId = null, int? CompanyId = null)
        //{
        //    SQLHelper ObjSQLHelper = new SQLHelper();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.AddWithValue("@Type", "Report");
        //    cmd.Parameters.AddWithValue("@DivisionRowId", DivisionId);
        //    cmd.Parameters.AddWithValue("@CompanyRowId", CompanyId);
        //    cmd.Parameters.AddWithValue("@EmpName", EmpName);
        //    cmd.Parameters.AddWithValue("@Month", Month);
        //    cmd.Parameters.AddWithValue("@Year", Year);

        //    DataSet ds = ObjSQLHelper.SelectProcDataDS("TS_GetImprestDetails", cmd);
        //    List<ImprestDetailsModel> result = new List<ImprestDetailsModel>();

        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            string rawMonth = row["Month"]?.ToString()?.Trim();
        //            int rowMonth;

        //            if (!int.TryParse(rawMonth, out rowMonth))
        //            {
        //                rowMonth = DateTime.ParseExact(rawMonth, "MMM", CultureInfo.InvariantCulture).Month;
        //            }

        //            string rawYear = row["IYear"]?.ToString()?.Trim();
        //            if (!int.TryParse(rawYear, out int rowYear)) continue;

        //            if (rowMonth != Month || rowYear != Year) continue;

        //            string emp = row["EmpName"]?.ToString() ?? string.Empty;
        //            int division = row["DivisionRowID"] != DBNull.Value ? Convert.ToInt32(row["DivisionRowID"]) : 0;
        //            int company = row["CompanyRowID"] != DBNull.Value ? Convert.ToInt32(row["CompanyRowID"]) : 0;

        //            bool empMatch = string.IsNullOrWhiteSpace(EmpName) || emp.IndexOf(EmpName, StringComparison.OrdinalIgnoreCase) >= 0;
        //            bool divisionMatch = !DivisionId.HasValue || division == DivisionId.Value;
        //            bool companyMatch = !CompanyId.HasValue || company == CompanyId.Value;

        //            if (!(empMatch && divisionMatch && companyMatch)) continue;

        //            result.Add(new ImprestDetailsModel
        //            {
        //                RowID = row["RowID"]?.ToString() ?? string.Empty,
        //                SrNo = row["SrNo"]?.ToString() ?? string.Empty,
        //                Company = row["Company"]?.ToString() ?? string.Empty,
        //                CompanyID = row["CompanyRowID"]?.ToString() ?? string.Empty,
        //                EmployeeName = emp,
        //                Month = rawMonth,
        //                IYear =  rawYear,
        //                ClaimedAmount = row["ClaimedAmount"]?.ToString() ?? string.Empty,
        //                CommercialReceiptDate = GetFormattedDate(row, "CommercialReceiptDate"),
        //                CommercialNoOfDays = row["CommercialNoOfDays"]?.ToString() ?? string.Empty,
        //                DivisionID = row["DivisionRowID"]?.ToString() ?? string.Empty,
        //                DivisionName = row["Division"]?.ToString() ?? string.Empty,
        //                SendToCrossCheckingDate = GetFormattedDate(row, "SendToCrossCheckingDate"),
        //                SendToCrossCheckingNoOfDays = row["SendToCrossCheckingNoOfDays"]?.ToString() ?? string.Empty,
        //                SendToHeadForApprovalDate = GetFormattedDate(row, "SendToHeadForApprovalDate"),
        //                SendToHeadForApprovalNoOfDays = row["SendToHeadForApprovalNoOfDays"]?.ToString() ?? string.Empty,
        //                SendToDirectorForApprovalDate = GetFormattedDate(row, "SendToDirectorForApprovalDate"),
        //                SendToDirectorForApprovalNoOfDays = row["SendToDirectorForApprovalNoOfDays"]?.ToString() ?? string.Empty,
        //                SubmittedInAccountsDate = GetFormattedDate(row, "SubmittedInAccountsDate"),
        //                SubmittedInAccountsNoOfDays = row["SubmittedInAccountsNoOfDays"]?.ToString() ?? string.Empty,
        //                PaymentDate = GetFormattedDate(row, "PaymentDate"),
        //                TotalDays = row["TotalDays"]?.ToString() ?? string.Empty,
        //                Remarks = row["Remarks"]?.ToString() ?? string.Empty,
        //            });
        //        }
        //    }

        //    ObjSQLHelper.ClearObjects();
        //    return result;
        //}
    }

}