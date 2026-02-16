using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Tracker_System.Models;

namespace Tracker_System.Classes
{
    public class ClsTSD
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

        #region SQL Connections
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        #endregion

        #region TSD List
        public List<TSDDetailsModel> GetAllTSDList(string CompanyName, int BrandRowID, string Name)
        {
            connection();

            List<TSDDetailsModel> tsdList = new List<TSDDetailsModel>();

            SqlCommand com = new SqlCommand("TS_GetTSDDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Company", CompanyName);
            com.Parameters.AddWithValue("@Brand", BrandRowID.ToString());
            com.Parameters.AddWithValue("@Name", Name);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                TSDDetailsModel obj = new TSDDetailsModel();

                obj.RowID = Convert.ToInt32(dr["RowID"]);
                obj.SrNo = Convert.ToInt32(dr["SrNo"]);
                obj.Company = Convert.ToString(dr["Company"]);
                obj.Brand = Convert.ToString(dr["Brand"]);
                obj.Name = Convert.ToString(dr["Name"]);
                obj.City = Convert.ToString(dr["City"]);

                obj.TSDAmount = dr["TSDAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TSDAmount"]);

                obj.CommercialReceiptDate = dr["CommercialReceiptDate"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(dr["CommercialReceiptDate"]);
                obj.CommercialNoOfDays = dr["CommercialNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CommercialNoOfDays"]);

                obj.SendToCrossCheckingDate = dr["SendToCrossCheckingDate"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(dr["SendToCrossCheckingDate"]);
                obj.SendToCrossCheckingNoOfDays = dr["SendToCrossCheckingNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SendToCrossCheckingNoOfDays"]);

                obj.SendToHeadForApprovalDate = dr["SendToHeadForApprovalDate"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(dr["SendToHeadForApprovalDate"]);
                obj.SendToHeadForApprovalNoOfDays = dr["SendToHeadForApprovalNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SendToHeadForApprovalNoOfDays"]);

                obj.SendToDirectorForApprovalDate = dr["SendToDirectorForApprovalDate"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(dr["SendToDirectorForApprovalDate"]);
                obj.SendToDirectorForApprovalNoOfDays = dr["SendToDirectorForApprovalNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SendToDirectorForApprovalNoOfDays"]);

                obj.SubmittedInAccountsDate = dr["SubmittedInAccountsDate"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(dr["SubmittedInAccountsDate"]);
                obj.SubmittedInAccountsNoOfDays = dr["SubmittedInAccountsNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SubmittedInAccountsNoOfDays"]);

                obj.TotalDays = dr["TotalDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalDays"]);

                obj.PaymentDate = dr["PaymentDate"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(dr["PaymentDate"]);
                obj.ChequeNumber = Convert.ToString(dr["ChequeNumber"]);
                obj.PaymentAmount = dr["PaymentAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PaymentAmount"]);

                obj.CurieredOnSentDate = dr["CurieredOnSentDate"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(dr["CurieredOnSentDate"]);
                obj.CurieredOnNoOfDays = dr["CurieredOnNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CurieredOnNoOfDays"]);
                obj.CurieredOnConfirmationDate = dr["CurieredOnConfirmationDate"] == DBNull.Value
                    ? (DateTime?)null
                    : Convert.ToDateTime(dr["CurieredOnConfirmationDate"]);

                obj.Remarks = Convert.ToString(dr["Remarks"]);

                tsdList.Add(obj);
            }

            return tsdList;
        }
        #endregion

        #region TSD Details
        public DataTable GetTSDDetails(string company, int brandId, string empName)
        {
            DataTable dt = new DataTable();
            connection();

            using (SqlCommand cmd = new SqlCommand("TS_GetTSDDetails", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Company", company ?? "");
                cmd.Parameters.AddWithValue("@Brand", brandId.ToString());
                cmd.Parameters.AddWithValue("@Name", empName ?? "");
                cmd.Parameters.AddWithValue("@Type", "Report");

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return dt;
        }

        #endregion

        #region TSD Pending DataTable List
        public DataTable GetTSDPendingList(int groupId)
        {
            connection();

            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("TS_GetTSD_PendingList", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupId", groupId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }
        #endregion

        #region TSD Pending Model Convert List
        public List<TSDPendingModel> GetTSDPendingList_Model(int groupId)
        {
            List<TSDPendingModel> list = new List<TSDPendingModel>();
            DataTable dt = GetTSDPendingList(groupId);

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new TSDPendingModel
                {
                    RowID = Convert.ToInt32(dr["RowID"]),
                    SrNo = Convert.ToInt32(dr["SrNo"]),
                    Company = dr["Company"].ToString(),
                    Name = dr["Name"].ToString(),
                    TSDAmount = Convert.ToDecimal(dr["TSDAmount"]),
                    Remarks = dr["Remarks"].ToString(),

                    CommercialReceiptDate = dt.Columns.Contains("CommercialReceiptDate") && dr["CommercialReceiptDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["CommercialReceiptDate"])
                                            : (DateTime?)null,

                    SendToCrossCheckingDate = dt.Columns.Contains("SendToCrossCheckingDate") && dr["SendToCrossCheckingDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["SendToCrossCheckingDate"])
                                            : (DateTime?)null,

                    SendToHeadForApprovalDate = dt.Columns.Contains("SendToHeadForApprovalDate") && dr["SendToHeadForApprovalDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["SendToHeadForApprovalDate"])
                                            : (DateTime?)null,

                    SendToDirectorForApprovalDate = dt.Columns.Contains("SendToDirectorForApprovalDate") && dr["SendToDirectorForApprovalDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["SendToDirectorForApprovalDate"])
                                            : (DateTime?)null,

                    SubmittedInAccountsDate = dt.Columns.Contains("SubmittedInAccountsDate") && dr["SubmittedInAccountsDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["SubmittedInAccountsDate"])
                                            : (DateTime?)null,

                    PaymentDate = dt.Columns.Contains("PaymentDate") && dr["PaymentDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["PaymentDate"])
                                            : (DateTime?)null,

                    CurieredOnSentDate = dt.Columns.Contains("CurieredOnSentDate") && dr["CurieredOnSentDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["CurieredOnSentDate"])
                                            : (DateTime?)null,

                    CurieredOnConfirmationDate = dt.Columns.Contains("CurieredOnConfirmationDate") && dr["CurieredOnConfirmationDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["CurieredOnConfirmationDate"])
                                            : (DateTime?)null
                });
            }

            return list;
        }
        #endregion

        #region TSD Save Row Wise (Using SP)
        public void SaveTSDRowWise(int groupId, List<TSDSaveModel> list)
        {
            connection();

            using (SqlConnection con = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString()))
            {
                con.Open();

                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item.Date1))
                    {
                        using (SqlCommand cmd = new SqlCommand("TS_TSD_SaveRowWise", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@GroupId", SqlDbType.Int).Value = groupId;
                            cmd.Parameters.Add("@RowId", SqlDbType.Int).Value = item.RowID;

                            DateTime dt =
                                DateTime.ParseExact(item.Date1, "dd/MM/yy",
                                System.Globalization.CultureInfo.InvariantCulture);

                            cmd.Parameters.Add("@D1", SqlDbType.DateTime).Value = dt;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        #endregion

        #region Get Register TSD Report
        public List<RegisterTSDReportModel> GetRegisterTSDReport(string company, string brand, string name, bool isPending)
        {
            List<RegisterTSDReportModel> list = new List<RegisterTSDReportModel>();
            connection();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = isPending
                    ? "TS_GetTSDDetails_Pending"
                    : "TS_GetTSDDetails";

                cmd.Parameters.AddWithValue("@Company", company ?? "");
                cmd.Parameters.AddWithValue("@Brand", brand ?? "");
                cmd.Parameters.AddWithValue("@Name", name ?? "");
                cmd.Parameters.AddWithValue("@Type", "Report");

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new RegisterTSDReportModel
                        {
                            // Basic
                            Company = dr["Company"].ToString(),
                            BrandName = dr["BrandName"].ToString(),
                            Region = dr["Region"].ToString(),
                            Name = dr["Name"].ToString(),
                            City = dr["City"].ToString(),
                            Regarding = dr["Regarding"].ToString(),

                            // Amount
                            TSDAmount = dr["TSDAmount"] == DBNull.Value
                                ? 0
                                : Convert.ToDecimal(dr["TSDAmount"]),

                            // Commercial
                            CommercialReceiptDate = dr["CommercialReceiptDate"] as DateTime?,
                            CommercialNoOfDays = dr["CommercialNoOfDays"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["CommercialNoOfDays"]),

                            // Cross Checking
                            SendToCrossCheckingDate = dr["SendToCrossCheckingDate"] as DateTime?,
                            SendToCrossCheckingNoOfDays = dr["SendToCrossCheckingNoOfDays"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["SendToCrossCheckingNoOfDays"]),

                            // HOD Approval
                            SendToHeadForApprovalDate = dr["SendToHeadForApprovalDate"] as DateTime?,
                            SendToHeadForApprovalNoOfDays = dr["SendToHeadForApprovalNoOfDays"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["SendToHeadForApprovalNoOfDays"]),

                            // Director Approval
                            SendToDirectorForApprovalDate = dr["SendToDirectorForApprovalDate"] as DateTime?,
                            SendToDirectorForApprovalNoOfDays = dr["SendToDirectorForApprovalNoOfDays"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["SendToDirectorForApprovalNoOfDays"]),

                            // Refund / Accounts
                            TotalRefundAmount = dr["TotalRefundAmount"] == DBNull.Value
                                ? 0
                                : Convert.ToDecimal(dr["TotalRefundAmount"]),

                            SubmittedInAccountsDate = dr["SubmittedInAccountsDate"] as DateTime?,
                            SubmittedInAccountsNoOfDays = dr["SubmittedInAccountsNoOfDays"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["SubmittedInAccountsNoOfDays"]),

                            // Payment
                            PaymentDate = dr["PaymentDate"] as DateTime?,
                            ChequeNumber = dr["ChequeNumber"].ToString(),
                            PaymentAmount = dr["PaymentAmount"] == DBNull.Value
                                ? 0
                                : Convert.ToDecimal(dr["PaymentAmount"]),

                            // Total & Courier
                            TotalDays = dr["TotalDays"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["TotalDays"]),

                            CurieredOnSentDate = dr["CurieredOnSentDate"] as DateTime?,
                            CurieredOnNoOfDays = dr["CurieredOnNoOfDays"] == DBNull.Value
                                ? 0
                                : Convert.ToInt32(dr["CurieredOnNoOfDays"]),

                            CurieredOnConfirmationDate = dr["CurieredOnConfirmationDate"] as DateTime?,

                            // Remarks
                            Remarks = dr["Remarks"].ToString()
                        });
                    }
                }
                con.Close();
            }

            return list;
        }


        #endregion

        #region Other Register List
        public byte[] GenerateRegisterTSDExcel(List<RegisterTSDReportModel> list, bool isPending)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Register TSD Report");

                // ================= TITLE =================
                ws.Cells[1, 1].Value = "Italia Group";
                ws.Cells[2, 1].Value = isPending
                    ? "CUSTOMER / DEALER TSD REFUND PENDING STATUS"
                    : "CUSTOMER / DEALER TSD REFUND STATUS";

                ws.Cells[1, 1, 1, 24].Merge = true;
                ws.Cells[2, 1, 2, 24].Merge = true;
                ws.Cells[1, 1, 2, 24].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.Font.Size = 14;

                // ================= HEADER =================
                int hRow = 4;

                ws.Cells[hRow, 1].Value = "Company";
                ws.Cells[hRow, 2].Value = "Brand";
                ws.Cells[hRow, 3].Value = "Region";
                ws.Cells[hRow, 4].Value = "Name";
                ws.Cells[hRow, 5].Value = "City";
                ws.Cells[hRow, 6].Value = "Regarding";
                ws.Cells[hRow, 7].Value = "TSDAmount";

                ws.Cells[hRow, 8, hRow, 9].Merge = true;
                ws.Cells[hRow, 8].Value = "Commercial";
                ws.Cells[hRow + 1, 8].Value = "Receipt Date";
                ws.Cells[hRow + 1, 9].Value = "No. of Days";

                ws.Cells[hRow, 10, hRow, 11].Merge = true;
                ws.Cells[hRow, 10].Value = "Sent for Mkt Head's Approval";
                ws.Cells[hRow + 1, 10].Value = "Date";
                ws.Cells[hRow + 1, 11].Value = "No. of Days";

                ws.Cells[hRow, 12, hRow, 13].Merge = true;
                ws.Cells[hRow, 12].Value = "Sent for Director's Approval";
                ws.Cells[hRow + 1, 12].Value = "Date";
                ws.Cells[hRow + 1, 13].Value = "No. of Days";

                ws.Cells[hRow, 14].Value = "Refund Amount";

                ws.Cells[hRow, 15, hRow, 16].Merge = true;
                ws.Cells[hRow, 15].Value = "Submitted In Accounts";
                ws.Cells[hRow + 1, 15].Value = "Date";
                ws.Cells[hRow + 1, 16].Value = "No. of Days";

                ws.Cells[hRow, 17].Value = "Payment Date";
                ws.Cells[hRow, 18].Value = "Cheque Number";
                ws.Cells[hRow, 19].Value = "Payment Amount";
                ws.Cells[hRow, 20].Value = "TOTAL DAYS";

                ws.Cells[hRow, 21, hRow, 22].Merge = true;
                ws.Cells[hRow, 21].Value = "Curiered On";
                ws.Cells[hRow + 1, 21].Value = "Date";
                ws.Cells[hRow + 1, 22].Value = "No. of Days";

                ws.Cells[hRow, 23].Value = "Formalities Completed On";
                ws.Cells[hRow, 24].Value = "Remarks";

                ws.Cells[hRow, 1, hRow + 1, 24].Style.Font.Bold = true;
                ws.Cells[hRow, 1, hRow + 1, 24].Style.WrapText = true;

                // ================= DATA =================
                int row = 6;

                foreach (var x in list)
                {
                    ws.Cells[row, 1].Value = x.Company;
                    ws.Cells[row, 2].Value = x.BrandName;
                    ws.Cells[row, 3].Value = x.Region;
                    ws.Cells[row, 4].Value = x.Name;
                    ws.Cells[row, 5].Value = x.City;
                    ws.Cells[row, 6].Value = x.Regarding;
                    ws.Cells[row, 7].Value = x.TSDAmount;

                    ws.Cells[row, 8].Value = x.CommercialReceiptDate?.ToString("dd/MM/yy");
                    ws.Cells[row, 9].Value = x.CommercialNoOfDays;

                    ws.Cells[row, 10].Value = x.SendToHeadForApprovalDate?.ToString("dd/MM/yy");
                    ws.Cells[row, 11].Value = x.SendToHeadForApprovalNoOfDays;

                    ws.Cells[row, 12].Value = x.SendToDirectorForApprovalDate?.ToString("dd/MM/yy");
                    ws.Cells[row, 13].Value = x.SendToDirectorForApprovalNoOfDays;

                    ws.Cells[row, 14].Value = x.TotalRefundAmount;

                    ws.Cells[row, 15].Value = x.SubmittedInAccountsDate?.ToString("dd/MM/yy");
                    ws.Cells[row, 16].Value = x.SubmittedInAccountsNoOfDays;

                    ws.Cells[row, 17].Value = x.PaymentDate?.ToString("dd/MM/yy");
                    ws.Cells[row, 18].Value = x.ChequeNumber;
                    ws.Cells[row, 19].Value = x.PaymentAmount;

                    ws.Cells[row, 20].Value = x.TotalDays;

                    ws.Cells[row, 21].Value = x.CurieredOnSentDate?.ToString("dd/MM/yy");
                    ws.Cells[row, 22].Value = x.CurieredOnNoOfDays;
                    ws.Cells[row, 23].Value = x.CurieredOnConfirmationDate?.ToString("dd/MM/yy");

                    ws.Cells[row, 24].Value = x.Remarks;

                    row++;
                }

                // ================= TOTAL =================
                ws.Cells[row, 7].Formula = $"SUM(G6:G{row - 1})";
                ws.Cells[row, 14].Formula = $"SUM(N6:N{row - 1})"; // Refund Amount

                ws.Cells[row, 1, row, 24].Style.Font.Bold = true;
                ws.Cells[6, 14, row, 14].Style.Numberformat.Format = "0";
                ws.Column(14).Width = 18;

                // ================= BORDER =================
                ws.Cells[4, 1, row, 24].Style.Border.Top.Style =
                ws.Cells[4, 1, row, 24].Style.Border.Bottom.Style =
                ws.Cells[4, 1, row, 24].Style.Border.Left.Style =
                ws.Cells[4, 1, row, 24].Style.Border.Right.Style =
                    OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                // ================= FREEZE =================
                ws.View.FreezePanes(6, 7);

                return package.GetAsByteArray();
            }
        }

        #endregion

        #region Other TSD Pending Submitted or Not Submiited
        public List<TSDSubmittedAccountPendingModel> GetTSDSubmmitedAccountPendingList(string company, string brand, string name, int submittedFlag)
        {
            List<TSDSubmittedAccountPendingModel> list = new List<TSDSubmittedAccountPendingModel>();

            connection();

            using (SqlCommand cmd = new SqlCommand("TS_GetTSDDetails_SubmittedInAccount_Pending", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Company", company);
                cmd.Parameters.AddWithValue("@Brand", brand);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@SubmittedInAccount", submittedFlag);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new TSDSubmittedAccountPendingModel
                    {
                        Company = dr["Company"].ToString(),
                        BrandName = dr["BrandName"].ToString(),
                        Region = dr["Region"].ToString(),
                        Name = dr["Name"].ToString(),
                        City = dr["City"].ToString(),
                        Regarding = dr["Regarding"].ToString(),

                        TSDAmount = Convert.ToDecimal(dr["TSDAmount"]),
                        CommercialReceiptDate = dr["CommercialReceiptDate"] as DateTime?,
                        CommercialNoOfDays = Convert.ToInt32(dr["CommercialNoOfDays"]),

                        SendToCrossCheckingDate = dr["SendToCrossCheckingDate"] as DateTime?,
                        SendToCrossCheckingNoOfDays = Convert.ToInt32(dr["SendToCrossCheckingNoOfDays"]),

                        SendToHeadForApprovalDate = dr["SendToHeadForApprovalDate"] as DateTime?,
                        SendToHeadForApprovalNoOfDays = Convert.ToInt32(dr["SendToHeadForApprovalNoOfDays"]),

                        SendToDirectorForApprovalDate = dr["SendToDirectorForApprovalDate"] as DateTime?,
                        SendToDirectorForApprovalNoOfDays = Convert.ToInt32(dr["SendToDirectorForApprovalNoOfDays"]),

                        TotalRefundAmount = Convert.ToDecimal(dr["TotalRefundAmount"]),

                        SubmittedInAccountsDate = dr["SubmittedInAccountsDate"] as DateTime?,
                        SubmittedInAccountsNoOfDays = Convert.ToInt32(dr["SubmittedInAccountsNoOfDays"]),

                        PaymentDate = dr["PaymentDate"] as DateTime?,
                        ChequeNumber = dr["ChequeNumber"].ToString(),
                        PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"]),

                        TotalDays = Convert.ToInt32(dr["TotalDays"]),
                        CurieredOnSentDate = dr["CurieredOnSentDate"] as DateTime?,
                        CurieredOnNoOfDays = Convert.ToInt32(dr["CurieredOnNoOfDays"]),
                        CurieredOnConfirmationDate = dr["CurieredOnConfirmationDate"] as DateTime?,

                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }
            return list;
        }


        public byte[] ExportTSDExcel(string company, string brand, string name)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var submitted = GetTSDSubmmitedAccountPendingList(company, brand, name, 0);
            var notSubmitted = GetTSDSubmmitedAccountPendingList(company, brand, name, 1);

            if (submitted.Count == 0 && notSubmitted.Count == 0)
                return null;

            using (var pkg = new ExcelPackage())
            {
                var ws = pkg.Workbook.Worksheets.Add("TSD Pending");
                int row = 1;

                ws.Cells[row, 1].Value = "Italia Group";
                ws.Cells[row, 1, row, 10].Merge = true;
                ws.Cells[row, 1].Style.Font.Bold = true;
                ws.Cells[row, 1].Style.Font.Size = 14;

                row += 3;

                row = WriteSection(ws, row,
                    "Pending TSD REFUND STATUS Submitted in A/c's",
                    submitted);

                row += 3;

                row = WriteSection(ws, row,
                    "Pending TSD REFUND STATUS Not Submitted in A/c's",
                    notSubmitted);

                ws.Cells.AutoFitColumns();
                ws.View.FreezePanes(6, 1);

                return pkg.GetAsByteArray();
            }
        }

        private int WriteSection(ExcelWorksheet ws, int startRow, string title, List<TSDSubmittedAccountPendingModel> list)
        {
            if (list == null || list.Count == 0)
                return startRow;

            int row = startRow;
            int col = 1;

            // ===== Section Title =====
            ws.Cells[row, 1].Value = title;
            ws.Cells[row, 1].Style.Font.Bold = true;
            row += 2;

            int h1 = row;
            int h2 = row + 1;

            // ===== Headers =====
            ws.Cells[h1, col].Value = "Company"; ws.Cells[h1, col, h2, col].Merge = true; col++;
            ws.Cells[h1, col].Value = "Brand"; ws.Cells[h1, col, h2, col].Merge = true; col++;
            ws.Cells[h1, col].Value = "Region"; ws.Cells[h1, col, h2, col].Merge = true; col++;
            ws.Cells[h1, col].Value = "Name"; ws.Cells[h1, col, h2, col].Merge = true; col++;
            ws.Cells[h1, col].Value = "City"; ws.Cells[h1, col, h2, col].Merge = true; col++;
            ws.Cells[h1, col].Value = "Regarding"; ws.Cells[h1, col, h2, col].Merge = true; col++;
            ws.Cells[h1, col].Value = "TSDAmount"; ws.Cells[h1, col, h2, col].Merge = true; col++;

            ws.Cells[h1, col].Value = "Commercial";
            ws.Cells[h1, col, h1, col + 1].Merge = true;
            ws.Cells[h2, col].Value = "Receipt Date";
            ws.Cells[h2, col + 1].Value = "No. of Days";
            col += 2;

            ws.Cells[h1, col].Value = "Sent for Mkt Head's Approval";
            ws.Cells[h1, col, h1, col + 1].Merge = true;
            ws.Cells[h2, col].Value = "Date";
            ws.Cells[h2, col + 1].Value = "No. of Days";
            col += 2;

            ws.Cells[h1, col].Value = "Sent for Director's Approval";
            ws.Cells[h1, col, h1, col + 1].Merge = true;
            ws.Cells[h2, col].Value = "Date";
            ws.Cells[h2, col + 1].Value = "No. of Days";
            col += 2;

            ws.Cells[h1, col].Value = "Refund Amount";
            ws.Cells[h1, col, h2, col].Merge = true;
            col++;

            ws.Cells[h1, col].Value = "Submitted In Account";
            ws.Cells[h1, col, h1, col + 1].Merge = true;
            ws.Cells[h2, col].Value = "Date";
            ws.Cells[h2, col + 1].Value = "No. of Days";
            col += 2;

            ws.Cells[h1, col].Value = "Payment Date"; ws.Cells[h1, col, h2, col].Merge = true; col++;
            ws.Cells[h1, col].Value = "Cheque Number"; ws.Cells[h1, col, h2, col].Merge = true; col++;
            ws.Cells[h1, col].Value = "Payment Amount"; ws.Cells[h1, col, h2, col].Merge = true; col++;
            ws.Cells[h1, col].Value = "TOTAL DAYS"; ws.Cells[h1, col, h2, col].Merge = true; col++;

            ws.Cells[h1, col].Value = "Curiered On";
            ws.Cells[h1, col, h1, col + 1].Merge = true;
            ws.Cells[h2, col].Value = "Date";
            ws.Cells[h2, col + 1].Value = "No. of Days";
            col += 2;

            ws.Cells[h1, col].Value = "Formalities Completed On";
            ws.Cells[h1, col, h2, col].Merge = true; col++;

            ws.Cells[h1, col].Value = "Remarks";
            ws.Cells[h1, col, h2, col].Merge = true;

            int lastCol = col;

            ws.Cells[h1, 1, h2, lastCol].Style.Font.Bold = true;
            ws.Cells[h1, 1, h2, lastCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[h1, 1, h2, lastCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells[h1, 1, h2, lastCol].Style.WrapText = true;

            // ===== Data Rows =====
            row += 2;
            int dataStartRow = row;

            foreach (var x in list)
            {
                col = 1;
                ws.Cells[row, col++].Value = x.Company;
                ws.Cells[row, col++].Value = x.BrandName;
                ws.Cells[row, col++].Value = x.Region;
                ws.Cells[row, col++].Value = x.Name;
                ws.Cells[row, col++].Value = x.City;
                ws.Cells[row, col++].Value = x.Regarding;
                ws.Cells[row, col++].Value = x.TSDAmount;

                ws.Cells[row, col++].Value = x.CommercialReceiptDate?.ToString("dd/MM/yy");
                ws.Cells[row, col++].Value = x.CommercialNoOfDays;

                ws.Cells[row, col++].Value = x.SendToHeadForApprovalDate?.ToString("dd/MM/yy");
                ws.Cells[row, col++].Value = x.SendToHeadForApprovalNoOfDays;

                ws.Cells[row, col++].Value = x.SendToDirectorForApprovalDate?.ToString("dd/MM/yy");
                ws.Cells[row, col++].Value = x.SendToDirectorForApprovalNoOfDays;

                ws.Cells[row, col++].Value = x.TotalRefundAmount;

                ws.Cells[row, col++].Value = x.SubmittedInAccountsDate?.ToString("dd/MM/yy");
                ws.Cells[row, col++].Value = x.SubmittedInAccountsNoOfDays;

                ws.Cells[row, col++].Value = x.PaymentDate?.ToString("dd/MM/yy");
                ws.Cells[row, col++].Value = x.ChequeNumber;
                ws.Cells[row, col++].Value = x.PaymentAmount;
                ws.Cells[row, col++].Value = x.TotalDays;

                ws.Cells[row, col++].Value = x.CurieredOnSentDate?.ToString("dd/MM/yy");
                ws.Cells[row, col++].Value = x.CurieredOnNoOfDays;
                ws.Cells[row, col++].Value = x.CurieredOnConfirmationDate?.ToString("dd/MM/yy");

                ws.Cells[row, col++].Value = x.Remarks;

                row++;
            }

            // ===== Totals =====
            ws.Cells[row, 7].Formula = $"SUM(G{dataStartRow}:G{row - 1})";   // TSDAmount
            ws.Cells[row, 14].Formula = $"SUM(N{dataStartRow}:N{row - 1})"; // Refund Amount
            ws.Cells[row, 1, row, lastCol].Style.Font.Bold = true;

            // ===== Borders =====
            ws.Cells[h1, 1, row, lastCol].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[h1, 1, row, lastCol].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[h1, 1, row, lastCol].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.Cells[h1, 1, row, lastCol].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            return row;
        }

        #endregion

        #region TSD Get Max No
        public int GetMaxNumber()
        {
            int maxNumber = 1;

            connection();

            SqlCommand cmd = new SqlCommand(
                "SELECT ISNULL(MAX(No),0) FROM TS_TSD", con);

            con.Open();
            maxNumber = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            con.Close();

            return maxNumber;
        }
        #endregion

        #region Search Employee
        public List<TSDNameSearchModel> SearchName(int CompanyAndBrandRowId, string Name)
        {
            List<TSDNameSearchModel> list = new List<TSDNameSearchModel>();
            DataTable dt = new DataTable();

            connection();

            SqlCommand cmd = new SqlCommand("TS_TSDSearchName", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CompanyAndBrandRowId", CompanyAndBrandRowId);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@ListTpe", "List");
            cmd.Parameters.AddWithValue("@VTableName", "");
            cmd.Parameters.AddWithValue("@VMasterCode", "");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TSDNameSearchModel
                {
                    MasterCode = row["MasterCode"].ToString(),
                    Name = row["Name"].ToString()
                });
            }

            return list;
        }
        #endregion

        #region Get Name Details
        public TSDNameDetailsModel GetNameDetails(int CompanyAndBrandRowId, string Name, string TableName, string MasterCode)
        {
            DataTable dt = new DataTable();

            connection();

            SqlCommand cmd = new SqlCommand("TS_TSDSearchName", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CompanyAndBrandRowId", CompanyAndBrandRowId);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@ListTpe", "Details");
            cmd.Parameters.AddWithValue("@VTableName", TableName);
            cmd.Parameters.AddWithValue("@VMasterCode", MasterCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return null;

            return new TSDNameDetailsModel
            {
                MasterCode = dt.Rows[0]["MasterCode"].ToString(),
                Name = dt.Rows[0]["Name"].ToString(),
                ShortName = dt.Rows[0]["ShortName"].ToString(),
                City = dt.Rows[0]["City"].ToString(),
                Region = dt.Rows[0]["Region"].ToString()
            };
        }
        #endregion

        #region TSD Add and Edit Data
        public bool SaveTSD(SaveTSDDetailModel m)
        {
            try
            {
                connection();

                SqlCommand cmd;
                SqlDataAdapter da;
                SqlCommandBuilder cb;
                DataSet ds = new DataSet();
                DataTable dt;
                DataRow dr;

                // ================= SELECT =================
                if (m.RowID <= 0)
                    cmd = new SqlCommand("SELECT * FROM TS_TSD WHERE 1<>1", con);
                else
                    cmd = new SqlCommand("SELECT * FROM TS_TSD WHERE RowID=" + m.RowID, con);

                da = new SqlDataAdapter(cmd);
                cb = new SqlCommandBuilder(da);
                da.Fill(ds, "TS_TSD");
                dt = ds.Tables[0];

                // ================= NEW / EXISTING =================
                if (m.RowID <= 0)
                {
                    dr = dt.NewRow();
                }
                else
                {
                    dr = dt.Rows[0];
                }

                dr["No"] = m.No;

                dr["Company"] = m.CompanyName ?? "";
                dr["Brand"] = m.BrandID.ToString() ?? "";
                dr["CompanyAndBrandRowID"] = m.BrandID ?? 0;

                dr["Name"] = m.Name ?? "";
                dr["MasterCode"] = m.MasterCode ?? "";
                dr["TableName"] = m.TableName ?? "";
                dr["City"] = m.City ?? "";
                dr["Region"] = m.Region ?? "";
                dr["Regarding"] = m.Regarding ?? "";
                dr["TSDAmount"] = m.TSDAmount;

                // ===== Commercial =====
                if (m.IsCommercial)
                {
                    dr["CommercialReceiptDate"] =
                        m.CommercialReceiptDate.HasValue ? (object)m.CommercialReceiptDate : DBNull.Value;
                    dr["CommercialNoOfDays"] = m.CommercialNoOfDays;

                    if (m.CommercialReceiptDate.HasValue)
                        dr["CommercialReceiptDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["CommercialReceiptDate"] = DBNull.Value;
                    dr["CommercialNoOfDays"] = 0;
                    dr["CommercialReceiptDateLogDate"] = DBNull.Value;
                }

                //// ===== HOD =====
                //if (m.IsHOD)
                //{
                //    dr["SendToCrossCheckingDate"] =
                //        m.HODReceiptDate.HasValue ? (object)m.HODReceiptDate : DBNull.Value;
                //    dr["SendToCrossCheckingNoOfDays"] = m.HODNoOfDays;

                //    if (m.HODReceiptDate.HasValue)
                //        dr["SendToCrossCheckingDateLogDate"] = DateTime.Now;
                //}

                // ===== MKT =====
                if (m.IsMkt)
                {
                    dr["SendToHeadForApprovalDate"] =
                        m.MktReceiptDate.HasValue ? (object)m.MktReceiptDate : DBNull.Value;
                    dr["SendToHeadForApprovalNoOfDays"] = m.MktNoOfDays;

                    if (m.MktReceiptDate.HasValue)
                        dr["SendToHeadForApprovalDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["SendToHeadForApprovalDate"] = DBNull.Value;
                    dr["SendToHeadForApprovalNoOfDays"] = 0;
                    dr["SendToHeadForApprovalDateLogDate"] = DBNull.Value;

                }

                // ===== Director =====
                if (m.IsDirector)
                {
                    dr["SendToDirectorForApprovalDate"] =
                        m.DirectorReceiptDate.HasValue ? (object)m.DirectorReceiptDate : DBNull.Value;
                    dr["SendToDirectorForApprovalNoOfDays"] = m.DirectorNoOfDays;
                    dr["TotalRefundAmount"] = m.TotalRefundAmount;

                    if (m.DirectorReceiptDate.HasValue)
                        dr["SendToDirectorForApprovalDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["SendToDirectorForApprovalDate"] = DBNull.Value;
                    dr["SendToDirectorForApprovalNoOfDays"] = 0;
                    dr["TotalRefundAmount"] = 0;
                    dr["SendToDirectorForApprovalDateLogDate"] = DBNull.Value;

                }

                // ===== Submitted =====
                if (m.IsSubmitted)
                {
                    dr["SubmittedInAccountsDate"] =
                        m.SubmittedDate.HasValue ? (object)m.SubmittedDate : DBNull.Value;
                    dr["SubmittedInAccountsNoOfDays"] = m.SubmittedNoOfDays;

                    if (m.SubmittedDate.HasValue)
                        dr["SubmittedInAccountsDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["SubmittedInAccountsDate"] = DBNull.Value;
                    dr["SubmittedInAccountsNoOfDays"] = 0;
                    dr["SubmittedInAccountsDateLogDate"] = DBNull.Value;
                }

                // ===== Payment =====
                if (m.IsPayment)
                {
                    dr["ChequeNumber"] = m.ChequeNumber ?? "";
                    dr["PaymentAmount"] = m.PaymentAmount;
                    dr["PaymentDate"] =
                        m.PaymentDate.HasValue ? (object)m.PaymentDate : DBNull.Value;
                }
                else
                {
                    dr["ChequeNumber"] = DBNull.Value;
                    dr["PaymentAmount"] = 0;
                    dr["PaymentDate"] = DBNull.Value;
                }

                // ===== Courier =====
                if (m.IsCourier)
                {
                    dr["CurieredOnSentDate"] =
                        m.CourierSentDate.HasValue ? (object)m.CourierSentDate : DBNull.Value;
                    dr["CurieredOnNoOfDays"] = m.CourierNoOfDays;

                    if (m.CourierSentDate.HasValue)
                        dr["CurieredOnSentDateLogDate"] = DateTime.Now;

                    dr["CurieredOnConfirmationDate"] =
                        m.ConfirmationReceiptDate.HasValue ? (object)m.ConfirmationReceiptDate : DBNull.Value;
                }
                else
                {
                    dr["CurieredOnSentDate"] = DBNull.Value;
                    dr["CurieredOnNoOfDays"] = 0;
                    dr["CurieredOnSentDateLogDate"] = DBNull.Value;
                    dr["CurieredOnConfirmationDate"] = DBNull.Value;
                }

                // ===== Total Days =====
                dr["TotalDays"] = m.TotalDays;

                // ===== Remarks =====
                if (dt.Columns.Contains("Remarks") &&
                    dr["Remarks"].ToString() != (m.Remarks ?? ""))
                {
                    dr["Remarks"] = m.Remarks ?? "";
                    dr["RemarksLogDate"] = DateTime.Now;
                }

                // ================= ADD ROW =================
                if (m.RowID <= 0)
                    dt.Rows.Add(dr);

                da.Update(ds, "TS_TSD");

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region TSD Delete Details
        public bool DeleteTSD(int rowId)
        {
            try
            {
                connection();

                SqlCommand cmd = new SqlCommand("DELETE FROM TS_TSD WHERE RowID = @RowID", con);

                cmd.Parameters.AddWithValue("@RowID", rowId);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();

                return rows > 0;
            }
            catch
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();

                return false;
            }
        }
        #endregion

        #region TSD Edit Details
        public GetEditTSDDetailsViewModel GetEditTSDDetails(int rowID)
        {
            GetEditTSDDetailsViewModel model = new GetEditTSDDetailsViewModel();

            try
            {
                connection();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @" SELECT T.*, C.name,C.RowID AS CompanyID
                                         FROM TS_TSD T
                                         LEFT JOIN Master_CompanyMaster C ON C.ShortName = T.Company
                                         WHERE T.RowID = @RowID";

                    cmd.Parameters.AddWithValue("@RowID", rowID);

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    if (dt.Rows.Count == 0)
                        return model;

                    DataRow dr = dt.Rows[0];

                    /* ========= BASIC ========= */
                    model.RowID = Convert.ToInt32(dr["RowID"]);
                    model.CompanyID = dr["CompanyID"] != DBNull.Value ? Convert.ToInt32(dr["CompanyID"]) : 0;
                    model.CompanyName = dr["Company"].ToString();
                    model.BrandID = dr["Brand"] != DBNull.Value ? Convert.ToInt32(dr["Brand"]) : 0;
                    model.No = dr["No"] != DBNull.Value ? Convert.ToInt32(dr["No"]) : 0;

                    //model.EmployeeID = dr["EmployeeID"] != DBNull.Value ? Convert.ToInt32(dr["EmployeeID"]) : 0;
                    model.EmployeeName = dr["Name"].ToString();

                    model.TableName = dr["TableName"].ToString();
                    model.MasterCode = dr["MasterCode"].ToString();
                    model.City = dr["City"].ToString();
                    model.Region = dr["Region"].ToString();
                    model.Regarding = dr["Regarding"].ToString();
                    model.TSDAmount = dr["TSDAmount"] != DBNull.Value ? Convert.ToDecimal(dr["TSDAmount"]) : 0;

                    /* ========= COMMERCIAL ========= */
                    model.CommercialReceiptDate = dr["CommercialReceiptDate"] != DBNull.Value
                        ? Convert.ToDateTime(dr["CommercialReceiptDate"])
                        : (DateTime?)null;

                    model.CommercialNoOfDays = dr["CommercialNoOfDays"] != DBNull.Value
                        ? Convert.ToInt32(dr["CommercialNoOfDays"])
                        : (int?)null;

                    model.IsCommercial = model.CommercialReceiptDate.HasValue;

                    /* ========= HOD ========= */
                    model.HODReceiptDate = dr["SendToCrossCheckingDate"] != DBNull.Value
                        ? Convert.ToDateTime(dr["SendToCrossCheckingDate"])
                        : (DateTime?)null;

                    model.HODNoOfDays = dr["SendToCrossCheckingNoOfDays"] != DBNull.Value
                        ? Convert.ToInt32(dr["SendToCrossCheckingNoOfDays"])
                        : (int?)null;

                    model.IsHOD = model.HODReceiptDate.HasValue;

                    /* ========= MKT ========= */
                    model.MktReceiptDate = dr["SendToHeadForApprovalDate"] != DBNull.Value
                        ? Convert.ToDateTime(dr["SendToHeadForApprovalDate"])
                        : (DateTime?)null;

                    model.MktNoOfDays = dr["SendToHeadForApprovalNoOfDays"] != DBNull.Value
                        ? Convert.ToInt32(dr["SendToHeadForApprovalNoOfDays"])
                        : (int?)null;

                    model.IsMKT = model.MktReceiptDate.HasValue;

                    /* ========= DIRECTOR ========= */
                    model.DirectorReceiptDate = dr["SendToDirectorForApprovalDate"] != DBNull.Value
                        ? Convert.ToDateTime(dr["SendToDirectorForApprovalDate"])
                        : (DateTime?)null;

                    model.DirectorNoOfDays = dr["SendToDirectorForApprovalNoOfDays"] != DBNull.Value
                        ? Convert.ToInt32(dr["SendToDirectorForApprovalNoOfDays"])
                        : (int?)null;

                    model.TotalRefundAmount = dr["TotalRefundAmount"] != DBNull.Value
                        ? Convert.ToDecimal(dr["TotalRefundAmount"])
                        : (decimal?)null;

                    model.IsDirector = model.DirectorReceiptDate.HasValue;

                    /* ========= SUBMITTED ========= */
                    model.SubmittedDate = dr["SubmittedInAccountsDate"] != DBNull.Value
                        ? Convert.ToDateTime(dr["SubmittedInAccountsDate"])
                        : (DateTime?)null;

                    model.SubmittedNoOfDays = dr["SubmittedInAccountsNoOfDays"] != DBNull.Value
                        ? Convert.ToInt32(dr["SubmittedInAccountsNoOfDays"])
                        : (int?)null;

                    model.TotalDays = dr["TotalDays"] != DBNull.Value
                        ? Convert.ToInt32(dr["TotalDays"])
                        : (int?)null;

                    model.IsSubmitted = model.SubmittedDate.HasValue;

                    /* ========= PAYMENT ========= */
                    model.PaymentDate = dr["PaymentDate"] != DBNull.Value
                        ? Convert.ToDateTime(dr["PaymentDate"])
                        : (DateTime?)null;

                    model.ChequeNumber = dr["ChequeNumber"].ToString();
                    model.PaymentAmount = dr["PaymentAmount"] != DBNull.Value
                        ? Convert.ToDecimal(dr["PaymentAmount"])
                        : (decimal?)null;

                    model.IsPayment = model.PaymentDate.HasValue;

                    /* ========= COURIER ========= */
                    model.CourierSentDate = dr["CurieredOnSentDate"] != DBNull.Value
                        ? Convert.ToDateTime(dr["CurieredOnSentDate"])
                        : (DateTime?)null;

                    model.CourierNoOfDays = dr["CurieredOnNoOfDays"] != DBNull.Value
                        ? Convert.ToInt32(dr["CurieredOnNoOfDays"])
                        : (int?)null;

                    model.IsCourier = model.CourierSentDate.HasValue;

                    /* ========= CONFIRMATION ========= */
                    model.ConfirmationReceiptDate = dr["CurieredOnConfirmationDate"] != DBNull.Value
                        ? Convert.ToDateTime(dr["CurieredOnConfirmationDate"])
                        : (DateTime?)null;

                    model.IsConfm = model.ConfirmationReceiptDate.HasValue;

                    model.Remarks = dr["Remarks"].ToString();
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return model;
        }
        #endregion

        #region Search Employee Only Customer

        #region Search Customer Name
        public List<TSDNameSearchOnlyCustomerModel> SearchCustomerName(int CompanyAndBrandRowId, string Name)
        {
            List<TSDNameSearchOnlyCustomerModel> list = new List<TSDNameSearchOnlyCustomerModel>();
            DataTable dt = new DataTable();

            connection();

            SqlCommand cmd = new SqlCommand("TS_TSDSearchCustomerOnly", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CompanyAndBrandRowId", CompanyAndBrandRowId);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@ListTpe", "List");
            cmd.Parameters.AddWithValue("@MasterCode", "");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TSDNameSearchOnlyCustomerModel
                {
                    MasterCode = row["MasterCode"].ToString(),
                    Name = row["Name"].ToString(),
                    TableName = "Customer"
                });
            }

            return list;
        }

        #region Get Customer Details
        public TSDNameDetailsOnlyCustomerModel GetCustomerDetails(int CompanyAndBrandRowId, string Name, string MasterCode)
        {
            DataTable dt = new DataTable();

            connection();

            SqlCommand cmd = new SqlCommand("TS_TSDSearchCustomerOnly", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CompanyAndBrandRowId", CompanyAndBrandRowId);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@ListTpe", "Details");
            cmd.Parameters.AddWithValue("@MasterCode", MasterCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return null;

            return new TSDNameDetailsOnlyCustomerModel
            {
                MasterCode = dt.Rows[0]["MasterCode"].ToString(),
                Name = dt.Rows[0]["Name"].ToString(),
                ShortName = dt.Rows[0]["ShortName"].ToString(),
                City = dt.Rows[0]["City"].ToString(),
                Region = dt.Rows[0]["Region"].ToString(),
                TableName = "Customer",
                TSDAmount = Convert.ToDecimal(dt.Rows[0]["TSDAmount"])
            };
        }
        #endregion


        #endregion


        #endregion


        public List<CompanyListModel> GetCompanyList(int isAllCompnayFlag)
        {
            connection();
            List<CompanyListModel> lstCompany = new List<CompanyListModel>();

            SqlCommand com = new SqlCommand("GetAllList", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "GetCompanyList");
            com.Parameters.AddWithValue("@AllCompany", isAllCompnayFlag);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            // Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                CompanyListModel OBjCM = new CompanyListModel();
                OBjCM.ID = Convert.ToString(dr["RowID"]);
                OBjCM.Name = Convert.ToString(dr["Name"]);
                OBjCM.ShortName = Convert.ToString(dr["ShortName"]);
                lstCompany.Add(OBjCM);
            }

            return lstCompany;
        }


        public List<BrandModel> GetBrandListByShortName(string ShortName)
        {
            connection();
            List<BrandModel> lstCompany = new List<BrandModel>();

            SqlCommand com = new SqlCommand("GetAllList", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "GetBrandListByShortName");
            com.Parameters.AddWithValue("@ShortName", ShortName);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                BrandModel OBjCM = new BrandModel();
                OBjCM.ID = Convert.ToString(dr["RowID"]);
                OBjCM.Name = Convert.ToString(dr["Name"]);
                lstCompany.Add(OBjCM);
            }

            return lstCompany;
        }
    }
}