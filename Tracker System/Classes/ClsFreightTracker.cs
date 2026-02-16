using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using Tracker_System.Models;

namespace Tracker_System.Classes
{
    public class ClsFreightTracker  
    {
        #region Variable Declaration

        private SQLHelper ObjSQLHelper;
        private SqlCommand cmd;
        private DataSet ds;
        public string SheetName { get; set; }
        public DataTable DTExcel { get; set; }
        public string FileName { get; set; }
        public string ColNames { get; set; }
        public string HeaderNames { get; set; }
        public string ColAlign { get; set; }
        public string ColWidth { get; set; }

        #endregion
        private SqlConnection con;
        private SqlConnection Com_Storege;
        public int employeeRowId = 0;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        private void connectionForStorageManager()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings_StoreManager"].ToString();
            Com_Storege = new SqlConnection(constr);
        }
        public List<FreightTracker> GetAllFyear()
        {
            connection();
            List<FreightTracker> lstFyear = new List<FreightTracker>();

            SqlCommand com = new SqlCommand("GetAllList", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "Fyear");
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            // Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                /*  FreightTracker ObjFT = new FreightTracker();
                  ObjFT.FyearId = Convert.ToString(dr["SYear"]);
                  ObjFT.EYear = Convert.ToString(dr["FYear"]);
                  lstFyear.Add(ObjFT);*/
                FreightTracker ObjFT = new FreightTracker();
                string sYearStr = dr["SYear"].ToString();

                if (int.TryParse(sYearStr, out int sYear))
                {
                    int fYear = sYear + 1;
                    ObjFT.FyearId = sYear.ToString();              // "2025"
                    ObjFT.EYear = $"{sYear}-{fYear}";              // "2025-2026"
                    lstFyear.Add(ObjFT);
                }
            }

            return lstFyear;
        }
        public List<FreightTracker> GetAllListForMosaic(int PageSize)
        {
            connection();
            List<FreightTracker> AllList = new List<FreightTracker>();
            SqlCommand com = new SqlCommand("TS_FT_FreightTrackingSystem_For_Mosaic", con);
            com.CommandType = CommandType.StoredProcedure;
            if(PageSize == 0)
            {
                com.Parameters.AddWithValue("@PageSize", 50);
            }
            else
            {
                com.Parameters.AddWithValue("@PageSize", PageSize);
            }
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            // Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                FreightTracker ObjFT = new FreightTracker();
                //ObjFT.SrNo = Convert.ToString(dr["Id"]);
                //ObjFT.Doc_Date = Convert.ToDateTime(dr["Doc_Date"]);
                ObjFT.Doc_Date = Convert.ToString(dr["Doc_Date"]);
                ObjFT.Doc_No = Convert.ToString(dr["Doc_No"]);
                ObjFT.CustCode = Convert.ToString(dr["CustCode"]);
                ObjFT.Cust_Name = Convert.ToString(dr["Cust_Name"]);
                ObjFT.Cust_Type = Convert.ToString(dr["Cust_Type"]);
                ObjFT.Cust_Category_Code = Convert.ToString(dr["Cust_Category_Code"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                AllList.Add(ObjFT);
            }

            return AllList;
        }
        public List<FreightTracker> GetAllListForTile(int PageSize)
        {
            connection();
            List<FreightTracker> AllList = new List<FreightTracker>();
            SqlCommand com = new SqlCommand("TS_FT_FreightTrackingSystem_ForTile", con);
            com.CommandType = CommandType.StoredProcedure;
            if(PageSize == 0)
            {
                com.Parameters.AddWithValue("@PageSize", 50);
            }
            else
            {
                com.Parameters.AddWithValue("@PageSize", PageSize);
            }
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            // Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                FreightTracker ObjFT = new FreightTracker();
                //ObjFT.SrNo = Convert.ToString(dr["Id"]);
                //ObjFT.Doc_Date = Convert.ToDateTime(dr["Doc_Date"]);
                ObjFT.Doc_Date = Convert.ToString(dr["Doc_Date"]);
                ObjFT.Doc_No = Convert.ToString(dr["Doc_No"]);
                ObjFT.CustCode = Convert.ToString(dr["CustCode"]);
                ObjFT.Cust_Name = Convert.ToString(dr["Cust_Name"]);
                ObjFT.Cust_Type = Convert.ToString(dr["Cust_Type"]);
                ObjFT.Cust_Category_Code = Convert.ToString(dr["Cust_Category_Code"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                //ObjFT.InvoiceDate = Convert.ToString(dr["Doc_No"]);
                AllList.Add(ObjFT);
            }

            return AllList;
        }

        #region General Methods
        public DataSet GetList()
        {
            ds = new DataSet();
            cmd = new SqlCommand();
            ObjSQLHelper = new SQLHelper();

            try
            {
                // cmd.Parameters.AddWithValue("@Action", "TS_FT_FreightTrackingSystem_prachi_Ajay");
                //cmd.Parameters.AddWithValue("@TenantId", TenantId);
                //if (!string.IsNullOrEmpty(fltLeadBaseTypeId))
                //    cmd.Parameters.AddWithValue("@BaseTypeCode", fltLeadBaseTypeId);
                //if (!string.IsNullOrEmpty(fltUserId))
                //    cmd.Parameters.AddWithValue("@AssignToId", fltUserId);
                //cmd.Parameters.AddWithValue("@StartIndex", jtStartIndex);
                //cmd.Parameters.AddWithValue("@PageSize", jtPageSize);
                //cmd.Parameters.AddWithValue("@Sorting", jtSorting);
                //cmd.Parameters.AddWithValue("@isAdmin", isAdmin);
                //cmd.Parameters.AddWithValue("@MyAllLeads", MyAllLeads);
                //cmd.Parameters.AddWithValue("@LeadNo", LeadNo);
                //cmd.Parameters.AddWithValue("@LeadTitle", LeadTitle);
                //cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                //cmd.Parameters.AddWithValue("@ContactName", ContactName);
                //cmd.Parameters.AddWithValue("@Phone", Phone);
                //cmd.Parameters.AddWithValue("@Mobile", Mobile);
                //cmd.Parameters.AddWithValue("@Email", Email);
                //cmd.Parameters.AddWithValue("@StageName", LeadStageName);
                //cmd.Parameters.AddWithValue("@LeadStatusName", LeadStatusName);
                ////cmd.Parameters.AddWithValue("@SourceName", LeadSourceName);
                ////cmd.Parameters.AddWithValue("@CreatedDateSys", CreatedDateSys);
                //cmd.Parameters.AddWithValue("@AssignToName", AssigntoName);
                //if (CampaignId == "")
                //    cmd.Parameters.AddWithValue("@CampaignId", null);
                //else
                //    cmd.Parameters.AddWithValue("@CampaignId", CampaignId);

                //if (!string.IsNullOrEmpty(LeadDate))
                //    cmd.Parameters.AddWithValue("@LeadDate", Common.Get_SQLDate_From_DateString(LeadDate));
                ds = ObjSQLHelper.SelectProcDataDS("TS_FT_FreightTrackingSystem_prachi_Ajay", cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ObjSQLHelper.ClearObjects();
            }
            return ds;
        }
        #endregion
        public List<FreightTracker> ConvertDataSetToLeadsList(DataSet ds)
        {
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0)
                return new List<FreightTracker>();

            List<FreightTracker> lstList = new List<FreightTracker>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                FreightTracker S = new FreightTracker();
                if (dr.Table.Columns.Contains("RecordCount"))
                    S.RecordCount = Common.ConvertDBnullToInt32(dr["RecordCount"]);
                if (dr.Table.Columns.Contains("SrNo"))
                    S.SrNo = Common.ConvertDBnullToString(dr["SrNo"]);
                if (dr.Table.Columns.Contains("Doc_Date"))
                    S.Doc_Date = Common.ConvertDBnullToString(dr["Doc_Date"]);
                if (dr.Table.Columns.Contains("Doc_No"))
                    S.Doc_No = Common.ConvertDBnullToString(dr["Doc_No"]);
                if (dr.Table.Columns.Contains("Cust Code"))
                    S.CustCode = Common.ConvertDBnullToString(dr["Cust Code"]);
                if (dr.Table.Columns.Contains("Cust Name"))
                    S.Cust_Name = Common.ConvertDBnullToString(dr["Cust_Name"]);
                if (dr.Table.Columns.Contains("Cust Type"))
                    S.Cust_Type = Common.ConvertDBnullToString(dr["Cust Type"]);
                if (dr.Table.Columns.Contains("Cust_Category_Code"))
                    S.Cust_Category_Code = Common.ConvertDBnullToString(dr["Cust_Category_Code"]);
                if (dr.Table.Columns.Contains("Salesperson_Name"))
                    S.Salesperson_Name = Common.ConvertDBnullToString(dr["Salesperson_Name"]);
                if (dr.Table.Columns.Contains("Brand"))
                    S.Brand = Common.ConvertDBnullToString(dr["Brand"]);
                if (dr.Table.Columns.Contains("Promotional_Sample_Sale"))
                    S.Promotional_Sample_Sale = Common.ConvertDBnullToString(dr["Promotional_Sample_Sale"]);
                if (dr.Table.Columns.Contains("Freight_Basis"))
                    S.Freight_Basis = Common.ConvertDBnullToString(dr["Freight_Basis"]);
                if (dr.Table.Columns.Contains("Freight_Paid_By"))
                    S.Freight_Paid_By = Common.ConvertDBnullToString(dr["Freight_Paid_By"]);
                if (dr.Table.Columns.Contains("Qnty_SQM"))
                    S.Qnty_SQM = Common.ConvertDBnullToString(dr["Qnty_SQM"]);
                if (dr.Table.Columns.Contains("Invoice_Amt"))
                    S.Invoice_Amt = Common.ConvertDBnullToString(dr["Invoice_Amt"]);
                if (dr.Table.Columns.Contains("Net Rlsn Per SQM"))
                    S.Net_Rlsn_Per_SQM = Common.ConvertDBnullToString(dr["Net Rlsn Per SQM"]);
                if (dr.Table.Columns.Contains("LR RR Date"))
                    S.LR_RR_Date = Common.ConvertDBnullToNullString(dr["LR RR Date"]);
                if (dr.Table.Columns.Contains("LR RR No"))
                    S.LR_RR_No = Common.ConvertDBnullToString(dr["LR RR No"]);
                if (dr.Table.Columns.Contains("Transporter Name"))
                    S.Transporter_Name = Common.ConvertDBnullToString(dr["Transporter Name"]);
                if (dr.Table.Columns.Contains("Freight Bill Recd. ON"))
                    S.FreightBillRecdON = Common.ConvertDBnullToString(dr["Freight Bill Recd. ON"]);
                if (dr.Table.Columns.Contains("Freight Bill No"))
                    S.FreightBillNo = Common.ConvertDBnullToString(dr["Freight Bill No"]);
                if (dr.Table.Columns.Contains("Bill Date"))
                    S.BillDate = Common.ConvertDBnullToNullString(dr["Bill Date"]);
                if (dr.Table.Columns.Contains("ActualFreightAmt"))
                    S.ActualFreightAmt = Common.ConvertDBnullToString(dr["ActualFreightAmt"]);
                if (dr.Table.Columns.Contains("Freight Bill Forwarded"))
                    S.FreightBillForwarded = Common.ConvertDBnullToNullString(dr["Freight Bill Forwarded"]);
                if (dr.Table.Columns.Contains("Shown Sep ON Invoice"))
                    S.ISShownSepONInvoice = Common.ConvertDBnullToString(dr["ISShownSepONInvoice"]);
                if (dr.Table.Columns.Contains("Debit Note To Be Raised"))
                    S.DebitNoteToBeRaised = Common.ConvertDBnullToString(dr["Debit Note To Be Raised"]);
                if (dr.Table.Columns.Contains("Debit Advise No"))
                    S.DebitAdviseNo = Common.ConvertDBnullToString(dr["Debit Advise No"]);
                if (dr.Table.Columns.Contains("Sent For HOD Approval"))
                    S.SentForHODApprovalDate = Common.ConvertDBnullToString(dr["Sent For HOD Approval"]);
                if (dr.Table.Columns.Contains("Approval Forwarded ON"))
                    S.ApprovalForwardedON = Common.ConvertDBnullToString(dr["Approval Forwarded ON"]);
                if (dr.Table.Columns.Contains("Remarks"))
                    S.Remarks = Common.ConvertDBnullToString(dr["Remarks"]);
                if (dr.Table.Columns.Contains("Payment Checque Recd ON"))
                    S.PaymentChecqueRecdONDate = Common.ConvertDBNullToDateTime(dr["Payment Checque Recd ON"]);
                if (dr.Table.Columns.Contains("Payment Advise Sent ON"))
                    S.PaymentAdviseSentONDate = Common.ConvertDBNullToDateTime(dr["Payment Advise Sent ON"]);
                if (dr.Table.Columns.Contains("Total Weight"))
                    S.TotalWeight = Common.ConvertDBnullToString(dr["Total Weight"]);
                if (dr.Table.Columns.Contains("Estimated Freight"))
                    S.EstimatedFreightAmt = Common.ConvertDBnullToString(dr["Estimated Freight"]);
                if (dr.Table.Columns.Contains("No Of Boxes"))
                    S.NoOfBoxes = Common.ConvertDBnullToString(dr["No Of Boxes"]);
                if (dr.Table.Columns.Contains("ModeOfTransport"))
                    S.ModeOfTransport = Common.ConvertDBnullToString(dr["ModeOfTransport"]);
                lstList.Add(S);
            }
            return lstList;
        }
        #region Factory And HO Tab Upadate Data
        public bool GetDataUpadateFactoryHO(string InvoiceNo, string FreightBillRecdON, string FreightBillNo, string BillDate, string ActualFreightAmt, string FreightBillForwarded, string Remarks)
        {
            connection();
            SqlCommand com = new SqlCommand("FTFactoryData", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "UpadateFactoryHO");
            com.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            com.Parameters.AddWithValue("@FreightBillRecdON", FreightBillRecdON);
            com.Parameters.AddWithValue("@FreightBillNo", FreightBillNo);
            com.Parameters.AddWithValue("@BillDate", BillDate);
            com.Parameters.AddWithValue("@ActualFreightAmt", ActualFreightAmt);
            com.Parameters.AddWithValue("@FreightBillForwarded", FreightBillForwarded);
            com.Parameters.AddWithValue("@Remarks", Remarks);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
            {
                employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
            }
            com.Parameters.AddWithValue("@Login_ID", employeeRowId);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Details Tab Upadate Data
        public bool GetDataDetailsUpadteInvoice(string InvoiceNo, string FreightBillRecdONDate, string FreightBillNo, string BillDate, string ShownSepONInvoice, string DebitNoteToBeRaised, string DebitAdviseNo, string FreightBillForwardedDate, string SentForHODApprovalDate, string ForwardedONDate, string Remarks, string PaymentChecqueRecdONDate, string PaymentSentONDate)
        {
            connection();
            SqlCommand com = new SqlCommand("FTDetailsUpadteInvoice", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "UpadteInvoice");
            com.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            com.Parameters.AddWithValue("@FreightBillRecdONDate", FreightBillRecdONDate);
            com.Parameters.AddWithValue("@FreightBillNo", FreightBillNo);
            com.Parameters.AddWithValue("@BillDate", BillDate);
            com.Parameters.AddWithValue("@ShownSepONInvoice", ShownSepONInvoice);
            com.Parameters.AddWithValue("@DebitNoteToBeRaised", DebitNoteToBeRaised);
            com.Parameters.AddWithValue("@DebitAdviseNo", DebitAdviseNo);
            com.Parameters.AddWithValue("@FreightBillForwardedDate", FreightBillForwardedDate);
            com.Parameters.AddWithValue("@SentForHODApprovalDate", SentForHODApprovalDate);
            com.Parameters.AddWithValue("@ForwardedONDate", ForwardedONDate);
            com.Parameters.AddWithValue("@Remarks", Remarks);
            com.Parameters.AddWithValue("@PaymentChecqueRecdONDate", PaymentChecqueRecdONDate);
            com.Parameters.AddWithValue("@PaymentSentONDate", PaymentSentONDate);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
            {
                employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
            }
            com.Parameters.AddWithValue("@Login_ID", employeeRowId);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Details Tab Delete Invoice 
        public bool GetDataDetailsDeleteInvoice(string InvoiceNo)
        {
            connection();
            SqlCommand com = new SqlCommand("FTDetailsUpadteInvoice", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "DeleteInvoice");
            com.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Commercial Tab Upadate Data
        public bool GetDataUpadateCommercial(string InvoiceNo, string SentForHODApproval,string Remarks)
        {
            connection();
            SqlCommand com = new SqlCommand("FTFactoryData", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "UpadateCommercial");
            com.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            com.Parameters.AddWithValue("@SentForHODApproval", SentForHODApproval);           
            com.Parameters.AddWithValue("@Remarks", Remarks);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
            {
                employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
            }
            com.Parameters.AddWithValue("@Login_ID", employeeRowId);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Commercial Tab Sent Mail
        public bool GetDataCommercialSendMail(string InvoiceNo)
        {
            connection();
            SqlCommand com = new SqlCommand("FTDetailsUpadteInvoice", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "UpadteInvoice");
            com.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
            {
                employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
            }
            com.Parameters.AddWithValue("@Login_ID", employeeRowId);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }  
        public DataSet GetDataApprovalSendMailDT(string InvoiceNo)
        {
            connection();
            DataSet DS = new DataSet();
            try
            {
                SqlCommand com = new SqlCommand("TS_FT_FreightTrackingSystem_SendMail_Ajay", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@InvoiveNo", InvoiceNo);
                SqlDataAdapter da = new SqlDataAdapter(com);
                con.Open();
                da.Fill(DS);
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return DS;
        }
        public DataTable GetDataApprovalSendMailDTT(string salespersoncode)
        {
            connection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand com = new SqlCommand("AL_AllocationReportingToChain_New_Ajay", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ReqUserRowID", salespersoncode);
                SqlDataAdapter da = new SqlDataAdapter(com);
                con.Open();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dt;
        }
        public bool AL_SaveSendforApproval(int TaskID, string InvoiceNo, string UserFromID, string UserToID, int RefRowID)
        {
            connectionForStorageManager();
            //SqlCommand com = new SqlCommand("AL_FT_SaveSendforApproval_new_Ajay", con);
            SqlCommand com = new SqlCommand("AL_FT_SaveSendforApproval_new", Com_Storege);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@TaskID", TaskID);
            com.Parameters.AddWithValue("@InvoicNo", InvoiceNo);
            com.Parameters.AddWithValue("@UserFromID", UserFromID);
            com.Parameters.AddWithValue("@UserToID", UserToID);
            com.Parameters.AddWithValue("@RefRowID", RefRowID);
            Com_Storege.Open();
            int i = com.ExecuteNonQuery();
            Com_Storege.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AL_SaveMailLog(string SFARowID, string EmailFrom, string EmailTo, string EmailCC, string EmailBCC, string Status,string ErrorDescription)
        {
            connectionForStorageManager();
            SqlCommand com = new SqlCommand("AL_FT_MailLog_new", Com_Storege);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SFARowID", SFARowID);
            com.Parameters.AddWithValue("@EmailFrom", EmailFrom);
            com.Parameters.AddWithValue("@EmailTo", EmailTo);
            com.Parameters.AddWithValue("@EmailCC", EmailCC);
            com.Parameters.AddWithValue("@EmailBCC", EmailBCC);
            com.Parameters.AddWithValue("@Status", Status);
            com.Parameters.AddWithValue("@ErrorDescription", ErrorDescription);
            Com_Storege.Open();
            int i = com.ExecuteNonQuery();
            Com_Storege.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region HOApproval Tab Upadate Data
        public bool GetDataUpadateHOApproval(string InvoiceNo, string ApprovalForwardedON, string Remarks, bool IsActive = false)
        {
            connection();
            SqlCommand com = new SqlCommand("FTFactoryData", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "UpadateHOApproval");
            com.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            com.Parameters.AddWithValue("@ApprovalForwardedON", ApprovalForwardedON);
            com.Parameters.AddWithValue("@Remarks", Remarks);
            com.Parameters.AddWithValue("@IsActive", IsActive);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
            {
                employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
            }
            com.Parameters.AddWithValue("@Login_ID", employeeRowId);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region HOApproval Tab Upadate ERP table Data
        public bool GetDataUpadateHOApprovalERP(string InvoiceNo)
        {
            connection();
            SqlCommand com = new SqlCommand("TS_FT_ERP_AcutalfreightAmt", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@invoiceNo", InvoiceNo);     
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region HOApprovalExcel  Data
        public DataSet GetDataHOApprovalExcelDT(string InvoiceNo)
        {
            connection();
            DataSet DS = new DataSet();
            try
            {
                SqlCommand com = new SqlCommand("FTFactoryData", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "HOApprovalExcelsSheet");
                com.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                SqlDataAdapter da = new SqlDataAdapter(com);
                con.Open();
                da.Fill(DS);
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return DS;
        }
        #endregion

        #region   Excel fromat
        public static byte[] DataTable(string filename, string para, string strWidth, string strAlign, string ReportHeader, DataTable dt, string strHeaderName, string splitExcelrowColIndex = "", string DisplayDateFormat = "")
        {
            DataTable dtnew = dt.Copy();
            string[] ReportColumnsList = strHeaderName.Split('|');
            strHeaderName = "";        
            for (int i = 0; i < ReportColumnsList.Length; i++)
            {
                string ReportColumnName = ReportColumnsList[i].ToString();                
                strHeaderName = (strHeaderName == "" ? "" : (strHeaderName + "|")) + (ReportColumnName.ToString() == " " ? ReportColumnName.ToString() : ReportColumnName.Trim().ToString());
            }
            for (int i = 0; i < dtnew.Columns.Count; i++)
            {
                IEnumerable<DataRow> rows = dtnew.Select().Cast<DataRow>().Where(row => row[dtnew.Columns[i].ColumnName].ToString().Contains("<br>") || row[dtnew.Columns[i].ColumnName].ToString().Contains("<b>") || row[dtnew.Columns[i].ColumnName].ToString().Contains("</b>"));
                rows.ToList().ForEach(r => { r[dtnew.Columns[i].ColumnName] = r[dtnew.Columns[i].ColumnName].ToString().Replace("<br>", "\n").Replace("<b>", "").Replace("</b>", ""); });
                dtnew.AcceptChanges();
            }
            string[] ColAlignList = strAlign.Split('|');
            string[] ColHeaderList = para.Split('|');
            string NumericColName = "";
            string DateColName = "";
            DataTable dtNumeric = new DataTable();
            dtNumeric = dtnew.Clone();
            if (ColAlignList.Length == ColHeaderList.Length)
            {
                dtNumeric = dtnew.Clone();
                System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex((@"^[0-9]+|^[0-9]+.[0-9]+$"));
                System.Text.RegularExpressions.Regex rx1 = new System.Text.RegularExpressions.Regex(@"^-[0-9]+|^-[0-9]+.[0-9]+$");
                for (int j = 0; j < ColAlignList.Length; j++)
                {
                    try
                    {
                        if (ColAlignList[j] == ">")
                        {
                            IEnumerable<DataRow> rows = dtnew.Select().Cast<DataRow>().Where(row => (rx.IsMatch(Common.ConvertDBnullToString(row[ColHeaderList[j]]).Replace("CR", "").Replace("DR", "").Trim()) == true || rx1.IsMatch(Common.ConvertDBnullToString(row[ColHeaderList[j]]).Replace("CR", "").Replace("DR", "").Trim()) == true || string.IsNullOrEmpty(Common.ConvertDBnullToString(row[ColHeaderList[j]])))); // && !Common.ConvertDBnullToString(row[ColHeaderList[j]]).Contains(" ")
                            if (rows.ToList().Count == dtnew.Rows.Count)
                            {                            
                                NumericColName = NumericColName + ColHeaderList[j] + ",";
                            }
                        }
                        else if (ColAlignList[j] == "<")
                        {
                            DateTime tempdate;
                            IEnumerable<DataRow> rows = dtnew.Select().Cast<DataRow>().Where(row => DateTime.TryParseExact(Common.ConvertDBnullToString(row[ColHeaderList[j]]), new string[] { "dd/MM/yyyy", "dd MMM yyyy" }, null, DateTimeStyles.None, out tempdate) == true || string.IsNullOrEmpty(Common.ConvertDBnullToString(row[ColHeaderList[j]]))); // && !Common.ConvertDBnullToString(row[ColHeaderList[j]]).Contains(" ")
                            if (rows.ToList().Count == dtnew.Rows.Count)
                            {
                                DateColName = DateColName + ColHeaderList[j] + ",";
                            }
                        }
                    }
                    catch (Exception ex)
                    { }
                }
                if (DateColName != "")
                {
                    try
                    {
                        for (int i = 0; i < dtnew.Columns.Count; i++)
                        {
                            if (DateColName.Contains(dtnew.Columns[i].ColumnName.ToString() + ",") && dtnew.Columns[i].DataType != typeof(DateTime))
                            {
                                dtNumeric.Columns.RemoveAt(i);
                                dtNumeric.Columns.Add(dtnew.Columns[i].ColumnName.ToString(), typeof(DateTime));
                                dtNumeric.Columns[dtnew.Columns[i].ColumnName.ToString()].SetOrdinal(i);
                            }
                            else
                            {
                                DateColName = DateColName.Replace(dtnew.Columns[i].ColumnName.ToString() + ",", "");
                            }
                        }
                        DateColName = DateColName.Replace(",", "").Trim();
                    }
                    catch (Exception ex)
                    { }
                }
            }
            string CompanyAddress = "";
            try
            {
                CompanyAddress = Common.ConvertDBnullToString(HttpContext.Current.Session["TenantAddress"]);
            }
            catch
            { }
            ReportHeader = ReportHeader.Replace("<br>", "\n");
            ReportHeader = ReportHeader.Replace("<b>", "");
            ReportHeader = ReportHeader.Replace("</b>", "");
            ClsFreightTracker.ReNameColumnNames(ref dtnew, para, strHeaderName, ref dtNumeric, true);
            if (NumericColName == "" && DateColName == "")
                dtNumeric = null;
            byte[] filecontent = ClsFreightTracker.ExportExcel(dtnew, ReportHeader, ClsFreightTracker.GetColumnList(strHeaderName), true, splitExcelrowColIndex, dtNumeric, DisplayDateFormat);
            return filecontent;
        }
        public static byte[] ExportExcel(DataTable dataTable, string heading = "", List<string> columnsToTake = null, bool IsHeader = true, string splitExcelrowColIndex = "", DataTable NumericdataTable = null, string DisplayDateFormat = "")
        {
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet;
                if (Common.ConvertDBnullToString(heading) != "")
                    workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                else
                    workSheet = package.Workbook.Worksheets.Add("Data");
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;
                if (NumericdataTable != null)
                {
                    workSheet.Cells["A" + startRowFrom].LoadFromDataTable(NumericdataTable, true);
                }
                else
                {
                    workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                    // autofit width of cells with small content  
                    int columnIndex = 1;
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        workSheet.Column(columnIndex).AutoFit();
                        workSheet.Column(columnIndex).Style.WrapText = true;
                        columnIndex++;
                    }
                }

                if (IsHeader)
                {
                    // format header - bold, yellow on black  
                    using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                    {
                        r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        r.Style.Font.Bold = true;
                        r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#62a3c8"));
                    }
                }
                else
                {
                    // Hide Header
                    using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                    {
                        r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#ffffff"));
                    }
                }

                // format cells - add borders  
                if (dataTable.Rows.Count > 0)
                {
                    using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                    {
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                    }
                }

                //Freeze Header row in excel
                for (int f = 1; f <= startRowFrom + 1; f++)
                {
                    workSheet.View.FreezePanes(f, 1);
                }

                //if any column datatype converted to Decimal then one by one row bind and format that cell
                if (NumericdataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            try
                            {
                                if ((NumericdataTable.Columns[j].DataType == typeof(decimal) || NumericdataTable.Columns[j].DataType == typeof(double) || NumericdataTable.Columns[j].DataType == typeof(int)))
                                {
                                    workSheet.Cells[startRowFrom + i + 1, j + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                    if (!string.IsNullOrEmpty(Common.ConvertDBnullToString(dataTable.Rows[i][j])) && !Common.ConvertDBnullToString(dataTable.Rows[i][j]).Contains(" "))
                                    {
                                        if (Common.ConvertDBnullToDecimal(dataTable.Rows[i][j]) < 0)
                                            workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Numberformat.Format = "_-#,##0.00_-;-#,##0.00_-;_-* \"-\"??_-;_-@_-";
                                        else
                                            workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Numberformat.Format = "_-* #,##0.00_-;-* #,##0.00_-;_-* \"-\"??_-;_-@_-";
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Value = Common.ConvertDBnullToDecimal(dataTable.Rows[i][j]);
                                    }
                                    else
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Value = dataTable.Rows[i][j];
                                }
                                else if (NumericdataTable.Columns[j].DataType == typeof(DateTime))
                                {
                                    if (!string.IsNullOrEmpty(Common.ConvertDBnullToString(dataTable.Rows[i][j])))
                                    {
                                        if (!string.IsNullOrEmpty(DisplayDateFormat))
                                            workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Numberformat.Format = DisplayDateFormat;
                                        else
                                            workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Numberformat.Format = "dd/MM/yyyy";
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Value = Common.ConvertDBNullToDateTime(dataTable.Rows[i][j]);
                                    }
                                    else
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Value = dataTable.Rows[i][j];
                                }
                                else
                                {
                                    DateTime tempdate;
                                    if (!string.IsNullOrEmpty(Common.ConvertDBnullToString(dataTable.Rows[i][j])) && DateTime.TryParseExact(Common.ConvertDBnullToString(dataTable.Rows[i][j]), new string[] { "dd/MM/yyyy", "dd MMM yyyy" }, null, DateTimeStyles.None, out tempdate) == true)
                                    {
                                        if (!string.IsNullOrEmpty(DisplayDateFormat))
                                            workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Numberformat.Format = DisplayDateFormat;
                                        else
                                            workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Numberformat.Format = "dd/MM/yyyy";
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Value = Common.ConvertDBNullToDateTime(dataTable.Rows[i][j]);
                                    }
                                    else
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Value = dataTable.Rows[i][j];
                                }

                                if (dataTable.Columns.Contains("_Style"))
                                {
                                    if (Common.ConvertDBnullToString(dataTable.Rows[i]["_Style"]) == "B")
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Font.Bold = true;
                                    else if (Common.ConvertDBnullToString(dataTable.Rows[i]["_Style"]) == "I")
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Font.Italic = true;
                                    else if (Common.ConvertDBnullToString(dataTable.Rows[i]["_Style"]) == "BI")
                                    {
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Font.Bold = true;
                                        workSheet.Cells[startRowFrom + i + 1, j + 1].Style.Font.Italic = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                workSheet.Cells[startRowFrom + i + 1, j + 1].Value = dataTable.Rows[i][j];
                            }
                        }
                    }
                    // autofit width of cells with small content  
                    int columnIndex = 1;
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        try
                        {
                            workSheet.Column(columnIndex).AutoFit();
                        }
                        catch (Exception)
                        {
                            //Common.LogActivity("Data Excel Length Excceded");
                        }
                        //workSheet.Cells[startRowFrom +1,1,startRowFrom+1,2].AutoFitColumns();
                        workSheet.Column(columnIndex).Style.WrapText = true;
                        columnIndex++;
                    }
                } //if any column datatype converted to Decimal then one by one row bind and format that cell
                else
                {
                    if (dataTable.Columns.Contains("_Style"))
                    {
                        try
                        {
                            DataTable dttemp = new DataTable();
                            dttemp = dataTable.Clone();
                            DataColumn column = new DataColumn();
                            column.DataType = System.Type.GetType("System.Int32");
                            column.AutoIncrement = true;
                            column.AutoIncrementSeed = 1;
                            column.AutoIncrementStep = 1;
                            column.ColumnName = "IdentityColRowNo";
                            column.AllowDBNull = false;
                            dttemp.Columns.Add(column);
                            dttemp.AcceptChanges();
                            dttemp.Merge(dataTable);
                            if (dataTable.Select("_Style = 'B'").Length > 0)
                            {
                                foreach (var dr in dttemp.Select("_Style = 'B'"))
                                {
                                    workSheet.Cells[startRowFrom + Convert.ToInt32(dr["IdentityColRowNo"]), 1, startRowFrom + Convert.ToInt32(dr["IdentityColRowNo"]), dataTable.Columns.Count].Style.Font.Bold = true;
                                }
                            }
                            if (dataTable.Select("_Style = 'I'").Length > 0)
                            {
                                foreach (var dr in dttemp.Select("_Style = 'I'"))
                                {
                                    workSheet.Cells[startRowFrom + Convert.ToInt32(dr["IdentityColRowNo"]), 1, startRowFrom + Convert.ToInt32(dr["IdentityColRowNo"]), dataTable.Columns.Count].Style.Font.Italic = true;
                                }
                            }
                            if (dataTable.Select("_Style = 'BI'").Length > 0)
                            {
                                foreach (var dr in dttemp.Select("_Style = 'BI'"))
                                {
                                    workSheet.Cells[startRowFrom + Convert.ToInt32(dr["IdentityColRowNo"]), 1, startRowFrom + Convert.ToInt32(dr["IdentityColRowNo"]), dataTable.Columns.Count].Style.Font.Italic = true;
                                    workSheet.Cells[startRowFrom + Convert.ToInt32(dr["IdentityColRowNo"]), 1, startRowFrom + Convert.ToInt32(dr["IdentityColRowNo"]), dataTable.Columns.Count].Style.Font.Bold = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                int TotalExcelColumnCnt = dataTable.Columns.Count;

                //splitExcelrowColIndex is identify the index that column should be split compare to other data [Index strat from: 1] [Use "|" to concat multiple index]
                //merge account detail and split Credit debit amount row against Account (use in Voucher register report)
                if (splitExcelrowColIndex != "")
                {
                    if (dataTable.Columns.Contains("MergeRowCount"))
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(Common.ConvertDBnullToString(dataTable.Rows[i]["MergeRowCount"])))
                            {
                                for (int j = 0; j < dataTable.Columns.Count; j++)
                                {
                                    if (!(splitExcelrowColIndex + "|").Contains((j + 1).ToString() + "|"))
                                    {
                                        workSheet.Cells[startRowFrom + 1 + i, j + 1, startRowFrom + 1 + i + Common.ConvertDBnullToInt32(Common.ConvertDBnullToString(dataTable.Rows[i]["MergeRowCount"])), (j + 1)].Merge = true;
                                    }
                                }
                                i = i + Common.ConvertDBnullToInt32(Common.ConvertDBnullToString(dataTable.Rows[i]["MergeRowCount"]));
                            }
                        }
                        if (columnsToTake.IndexOf("MergeRowCount") != -1)
                            columnsToTake.RemoveAt(columnsToTake.IndexOf("MergeRowCount"));
                    }
                }
                //merge account detail and split Credit debit amount row against Account (use in Voucher register report)

                // removed ignored columns  
                for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                {
                    if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName == " " ? dataTable.Columns[i].ColumnName : dataTable.Columns[i].ColumnName.Trim()))
                    {
                        workSheet.DeleteColumn(i + 1); //
                        TotalExcelColumnCnt = TotalExcelColumnCnt - 1;
                    }
                }

                if (!String.IsNullOrEmpty(heading))
                {

                    workSheet.Cells["A2"].Value = heading;
                    workSheet.Cells["A2"].Style.Font.Size = 12;
                    workSheet.Cells["A2"].Style.WrapText = false;
                    workSheet.Cells["A2"].Style.Font.Bold = true;
                    workSheet.Cells[2, 1, 2, TotalExcelColumnCnt].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[2, 1, 2, TotalExcelColumnCnt].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    workSheet.Cells[2, 1, 2, TotalExcelColumnCnt].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#293885"));

                }

                result = package.GetAsByteArray();
            }

            return result;
        }
        public static List<string> GetColumnList(string ReportHeader)
        {
            List<string> columns = new List<string>();
            string[] columnslist = ReportHeader.Split('|');
            for (int i = 0; i < columnslist.Length; i++)
                columns.Add(columnslist[i]);
            return columns;
        }
        public static void ReNameColumnNames(ref DataTable dataTable, string OriginalNames, string ReportNames, ref DataTable NumericdataTable, bool ReOrder = false)
        {
            string[] OriginalColumnsList = OriginalNames.Split('|');
            string[] ReportColumnsList = ReportNames.Split('|');

            if (ReOrder)
            {
                for (int i = 0; i < OriginalColumnsList.Length; i++)
                {
                    try
                    {
                        dataTable.Columns[(OriginalColumnsList[i] == " " ? OriginalColumnsList[i] : OriginalColumnsList[i].Trim())].SetOrdinal(i);
                        NumericdataTable.Columns[(OriginalColumnsList[i] == " " ? OriginalColumnsList[i] : OriginalColumnsList[i].Trim())].SetOrdinal(i);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            dataTable.PrimaryKey = null;
            NumericdataTable.PrimaryKey = null;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                string ColName = dataTable.Columns[i].ColumnName == " " ? dataTable.Columns[i].ColumnName : dataTable.Columns[i].ColumnName.Trim();
                if (!Array.Exists(OriginalColumnsList, x => x.ToUpper() == ColName.ToUpper()) && dataTable.Columns[i].ColumnName != "cmdLine" && dataTable.Columns[i].ColumnName != "_Style")
                {
                    dataTable.Columns.Remove(dataTable.Columns[i].ColumnName);
                    dataTable.AcceptChanges();

                    NumericdataTable.Columns.Remove(NumericdataTable.Columns[i].ColumnName);
                    NumericdataTable.AcceptChanges();
                    i = i - 1;
                }
            }

            // rename column
            for (int i = 0; i < OriginalColumnsList.Length; i++)
            {
                try
                {
                    dataTable.Columns[(OriginalColumnsList[i] == " " ? OriginalColumnsList[i] : OriginalColumnsList[i].Trim())].ColumnName = (ReportColumnsList[i] == " " ? ReportColumnsList[i] : ReportColumnsList[i].Trim());
                    NumericdataTable.Columns[(OriginalColumnsList[i] == " " ? OriginalColumnsList[i] : OriginalColumnsList[i].Trim())].ColumnName = (ReportColumnsList[i] == " " ? ReportColumnsList[i] : ReportColumnsList[i].Trim());
                }
                catch (Exception ex)
                {
                }
            }

        }
        #endregion
    }
}