using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using Tracker_System.Models;

namespace Tracker_System.Classes
{
    public class ClsImpress
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

        #region Connection HRMS
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        #endregion

        #region Get Impress Filter List
        public ImpressFilterModel GetImpressFilterList()
        {
            connection();

            ImpressFilterModel model = new ImpressFilterModel();

            using (SqlCommand com = new SqlCommand("GetAllList", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetImpressFilerData");

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //Divison List
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            model.GetDivisionFilter.Add(new DivisionFilterModel
                            {
                                RowId = Convert.ToInt32(dr["RowId"]),
                                DivisionName = dr["DivisionName"].ToString()
                            });
                        }
                    }

                    //Company List
                    if (ds.Tables.Count > 1)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            model.GetCompanyFilter.Add(new CompanyFilterModel
                            {
                                RowID = Convert.ToInt32(dr["RowID"]),
                                Name = dr["Name"].ToString()
                            });
                        }
                    }

                    //Month List
                    if (ds.Tables.Count > 2)
                    {
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            model.GetMonthFilter.Add(new MonthFilterModel
                            {
                                RowID = Convert.ToInt32(dr["RowID"]),
                                ShortName = dr["ShortName"].ToString()
                            });
                        }
                    }

                    // Year List
                    if (ds.Tables.Count > 3)
                    {
                        foreach (DataRow dr in ds.Tables[3].Rows)
                        {
                            model.GetYearFilter.Add(new YearFilterModel
                            {
                                EYear = Convert.ToInt32(dr["EYear"])
                            });
                        }
                    }
                }
            }

            return model;
        }
        #endregion

        #region Get Impress List
        public List<ImpressListModel> GetImpressList(int divisionId, int companyId, string empName, int monthId, int yearId)
        {
            connection();

            List<ImpressListModel> list = new List<ImpressListModel>();

            using (SqlCommand com = new SqlCommand("TS_GetImprestDetails", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                // 🔹 SP parameters (must match exactly)
                com.Parameters.AddWithValue("@DivisionRowId", divisionId);
                com.Parameters.AddWithValue("@CompanyRowId", companyId);
                com.Parameters.AddWithValue("@EmpName", empName);
                com.Parameters.AddWithValue("@Month", monthId);
                com.Parameters.AddWithValue("@Year", yearId);
                com.Parameters.AddWithValue("@Type", "");   // last param '' = Report

                con.Open();

                using (SqlDataReader dr = com.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ImpressListModel m = new ImpressListModel();

                        m.RowID = Convert.ToInt32(dr["RowID"]);
                        m.SrNo = Convert.ToInt32(dr["SrNo"]);
                        m.Company = dr["Company"].ToString();
                        m.EmpName = dr["EmpName"].ToString();
                        m.Month = dr["Month"].ToString();
                        m.IYear = Convert.ToInt32(dr["IYear"]);
                        m.ClaimedAmount = Convert.ToDecimal(dr["ClaimedAmount"]);

                        m.CommercialReceiptDate = dr["CommercialReceiptDate"] == DBNull.Value
                                                    ? (DateTime?)null
                                                    : Convert.ToDateTime(dr["CommercialReceiptDate"]);
                        m.CommercialNoOfDays = Convert.ToInt32(dr["CommercialNoOfDays"]);

                        m.SendToCrossCheckingDate = dr["SendToCrossCheckingDate"] == DBNull.Value
                                                    ? (DateTime?)null
                                                    : Convert.ToDateTime(dr["SendToCrossCheckingDate"]);
                        m.SendToCrossCheckingNoOfDays = Convert.ToInt32(dr["SendToCrossCheckingNoOfDays"]);

                        m.SendToHeadForApprovalDate = dr["SendToHeadForApprovalDate"] == DBNull.Value
                                                    ? (DateTime?)null
                                                    : Convert.ToDateTime(dr["SendToHeadForApprovalDate"]);
                        m.SendToHeadForApprovalNoOfDays = Convert.ToInt32(dr["SendToHeadForApprovalNoOfDays"]);

                        m.SendToDirectorForApprovalDate = dr["SendToDirectorForApprovalDate"] == DBNull.Value
                                                    ? (DateTime?)null
                                                    : Convert.ToDateTime(dr["SendToDirectorForApprovalDate"]);
                        m.SendToDirectorForApprovalNoOfDays = Convert.ToInt32(dr["SendToDirectorForApprovalNoOfDays"]);

                        m.SubmittedInAccountsDate = dr["SubmittedInAccountsDate"] == DBNull.Value
                                                    ? (DateTime?)null
                                                    : Convert.ToDateTime(dr["SubmittedInAccountsDate"]);
                        m.SubmittedInAccountsNoOfDays = Convert.ToInt32(dr["SubmittedInAccountsNoOfDays"]);

                        m.PaymentDate = dr["PaymentDate"] == DBNull.Value
                                                    ? (DateTime?)null
                                                    : Convert.ToDateTime(dr["PaymentDate"]);
                        m.TotalDays = dr["TotalDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalDays"]);

                        list.Add(m);
                    }
                }
                con.Close();
            }

            return list;
        }
        #endregion

        #region Get Impress Report Data - Using Excel Sheet
        public DataTable GetImpressReportData(int divisionId, int companyId, string empName, int monthId, int yearId)
        {
            DataTable dt = new DataTable();

            connection();

            using (SqlCommand cmd = new SqlCommand("TS_GetImprestDetails", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DivisionRowId", divisionId);
                cmd.Parameters.AddWithValue("@CompanyRowId", companyId);
                cmd.Parameters.AddWithValue("@EmpName", empName);
                cmd.Parameters.AddWithValue("@Month", monthId);
                cmd.Parameters.AddWithValue("@Year", yearId);
                cmd.Parameters.AddWithValue("@Type", "Report");

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }
        #endregion

        #region Export Impress Data
        public byte[] GenerateImprestExcel(DataTable TB)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Imprest");

                string ReportTitle = "Italia Group";
                string ReportTitle1 = "Employee Imprest Status";

                int Rw = 4;
                int Cl = 4;
                int DataStartRw = 6;
                int DataStartCl = 1;

                // ================= Title =================
                ws.Cells[1, 1].Value = ReportTitle;
                ws.Cells[2, 1].Value = ReportTitle1;

                ws.Cells[1, 1].Style.Font.Size = 14;
                ws.Cells[1, 1, 1, 5].Merge = true;
                ws.Cells[2, 1, 2, 5].Merge = true;
                ws.Cells[1, 1, 3, 15].Style.Font.Bold = true;

                ws.Cells[1, 6].Value = "PAYMENT PROCESS";
                ws.Cells[2, 6].Value = "1 to 15  Reimbursed will be done between 16 to 30";
                ws.Cells[3, 6].Value = "16 to 30  Reimbursed will be done between 1 to 15 of next Month";
                ws.Cells[1, 6, 3, 6].Style.Font.Size = 10;

                // ================= HEADER =================
                Cl = 1;
                ws.Cells[Rw, Cl].Value = "Company";
                ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true; Cl++;

                ws.Cells[Rw, Cl].Value = "Region";
                ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true; Cl++;

                ws.Cells[Rw, Cl].Value = "Zone";
                ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true; Cl++;

                ws.Cells[Rw, Cl].Value = "Employee Name";
                ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true; Cl++;

                ws.Cells[Rw, Cl].Value = "Month/ Tour Period";
                ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true; Cl++;

                ws.Cells[Rw, Cl].Value = "Claimed Amount";
                ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true; Cl++;

                ws.Cells[Rw, Cl].Value = "Commercial";
                ws.Cells[Rw, Cl, Rw, Cl + 1].Merge = true;
                ws.Cells[Rw + 1, Cl].Value = "Receipt Date";
                ws.Cells[Rw + 1, Cl + 1].Value = "No. of Days";
                Cl += 2;

                ws.Cells[Rw, Cl].Value = "Sent to HOD for crosscheck";
                ws.Cells[Rw, Cl, Rw, Cl + 1].Merge = true;
                ws.Cells[Rw + 1, Cl].Value = "Date";
                ws.Cells[Rw + 1, Cl + 1].Value = "No. of Days";
                Cl += 2;

                ws.Cells[Rw, Cl].Value = "Sent for Mkt Head's Approval";
                ws.Cells[Rw, Cl, Rw, Cl + 1].Merge = true;
                ws.Cells[Rw + 1, Cl].Value = "Date";
                ws.Cells[Rw + 1, Cl + 1].Value = "No. of Days";
                Cl += 2;

                ws.Cells[Rw, Cl].Value = "Sent for Director's Approval";
                ws.Cells[Rw, Cl, Rw, Cl + 1].Merge = true;
                ws.Cells[Rw + 1, Cl].Value = "Date";
                ws.Cells[Rw + 1, Cl + 1].Value = "No. of Days";
                Cl += 2;

                ws.Cells[Rw, Cl].Value = "PASSED Amount";
                ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true; Cl++;

                ws.Cells[Rw, Cl].Value = "Submitted In Account";
                ws.Cells[Rw, Cl, Rw, Cl + 1].Merge = true;
                ws.Cells[Rw + 1, Cl].Value = "Date";
                ws.Cells[Rw + 1, Cl + 1].Value = "No. of Days";
                Cl += 2;

                ws.Cells[Rw, Cl].Value = "Payment";
                ws.Cells[Rw, Cl, Rw, Cl + 1].Merge = true;
                ws.Cells[Rw + 1, Cl].Value = "Date";
                ws.Cells[Rw + 1, Cl + 1].Value = "Amount";
                Cl += 2;

                ws.Cells[Rw, Cl].Value = "TOTAL DAYS";
                ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true; Cl++;

                ws.Cells[Rw, Cl].Value = "Remarks";
                ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true; Cl++;

                ws.Cells[Rw, Cl].Value = "Imprest Balance Confirmation";
                ws.Cells[Rw, Cl, Rw, Cl + 2].Merge = true;
                ws.Cells[Rw + 1, Cl].Value = "Sent Date";
                ws.Cells[Rw + 1, Cl + 1].Value = "No. of Days";
                ws.Cells[Rw + 1, Cl + 2].Value = "Conf. Recd. Date";

                Rw += 2;

                // ================= DATA =================
                foreach (DataRow dr in TB.Rows)
                {
                    DataStartCl = 1;

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Company"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Region"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Zone"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["EmpName"];
                    ws.Cells[DataStartRw, DataStartCl++].Value =
                        dr["IMonthName"] + "-" + dr["IYear"];

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["ClaimedAmount"];

                    ws.Cells[DataStartRw, DataStartCl++].Value =
                        dr["CommercialReceiptDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["CommercialReceiptDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["CommercialNoOfDays"];

                    ws.Cells[DataStartRw, DataStartCl++].Value =
                        dr["SendToCrossCheckingDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["SendToCrossCheckingDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["SendToCrossCheckingNoOfDays"];

                    ws.Cells[DataStartRw, DataStartCl++].Value =
                        dr["SendToHeadForApprovalDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["SendToHeadForApprovalDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["SendToHeadForApprovalNoOfDays"];

                    ws.Cells[DataStartRw, DataStartCl++].Value =
                        dr["SendToDirectorForApprovalDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["SendToDirectorForApprovalDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["SendToDirectorForApprovalNoOfDays"];

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["PassedAmount"];

                    ws.Cells[DataStartRw, DataStartCl++].Value =
                        dr["SubmittedInAccountsDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["SubmittedInAccountsDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["SubmittedInAccountsNoOfDays"];

                    ws.Cells[DataStartRw, DataStartCl++].Value =
                        dr["PaymentDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["PaymentDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["PaidAmount"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["TotalDays"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Remarks"];

                    ws.Cells[DataStartRw, DataStartCl++].Value =
                        dr["Imp_Bal_Con_SendDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["Imp_Bal_Con_SendDate"]).ToString("dd/MM/yy");

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Imp_Bal_Con_SendNoOfDays"];

                    ws.Cells[DataStartRw, DataStartCl++].Value =
                        dr["Imp_Bal_Con_ReceivedDate"] == DBNull.Value ? "" :
                        Convert.ToDateTime(dr["Imp_Bal_Con_ReceivedDate"]).ToString("dd/MM/yy");

                    if (dr["TourFrom"] != DBNull.Value && dr["TourTo"] != DBNull.Value)
                    {
                        ws.Cells[DataStartRw, DataStartCl].Value =
                            "Tour : " +
                            Convert.ToDateTime(dr["TourFrom"]).ToString("dd/MM/yy") +
                            " to " +
                            Convert.ToDateTime(dr["TourTo"]).ToString("dd/MM/yy");
                    }

                    DataStartRw++;
                }

                // ================= FORMULA =================
                int lastRow = DataStartRw;
                ws.Cells[lastRow, 6].Formula = $"SUM(F6:F{lastRow - 1})";
                ws.Cells[lastRow, 15].Formula = $"SUM(O6:O{lastRow - 1})";

                // ================= FORMAT =================
                ws.Cells[4, 1, lastRow, Cl].AutoFitColumns();
                ws.Cells[4, 1, 5, Cl].Style.Font.Bold = true;
                ws.Cells[4, 1, 5, Cl].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[4, 1, 5, Cl].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.View.FreezePanes(6, 7);

                // ================= RETURN =================
                return package.GetAsByteArray();
            }
        }
        #endregion

        #region Get Impress Filter List
        public GetImpressOtherTabModel GetImpressOtherTabFilterData()
        {
            connection();

            GetImpressOtherTabModel model = new GetImpressOtherTabModel();

            using (SqlCommand com = new SqlCommand("GetAllList", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetImpressOtherTabFilterData");

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //Divison List
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            model.GetDivisionFilter.Add(new DivisionFilterModel
                            {
                                RowId = Convert.ToInt32(dr["RowId"]),
                                DivisionName = dr["DivisionName"].ToString()
                            });
                        }
                    }

                    //Company List
                    if (ds.Tables.Count > 1)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            model.GetCompanyFilter.Add(new CompanyFilterModel
                            {
                                RowID = Convert.ToInt32(dr["RowID"]),
                                Name = dr["Name"].ToString()
                            });
                        }
                    }

                    // Year List
                    if (ds.Tables.Count > 2)
                    {
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            model.GetFYearFilter.Add(new FYearsFilterModel
                            {
                                RowID = Convert.ToInt32(dr["RowID"]),
                                FYear = dr["FYear"].ToString(),
                            });
                        }
                    }
                }
            }

            return model;
        }
        #endregion

        #region Impress - Other Tab - Export Download File

        #region 1. Submission vs Payment

        public DataTable GetImprestSubmissionAndPaidDate(int divisionId, int companyId, int yearId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("TS_ImprestSubmissionAndPaidDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DivisionRowId", divisionId);
                    cmd.Parameters.AddWithValue("@CompanyRowId", companyId);
                    cmd.Parameters.AddWithValue("@FYearRowID", yearId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public byte[] SubVSPaymentRPT(int divisionId, int companyId, int yearId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            DataTable TB = GetImprestSubmissionAndPaidDate(divisionId, companyId, yearId);

            if (TB == null || TB.Rows.Count == 0)
                return null;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Imprest Report");

                int Rw = 3;
                int Cl = 1;

                string ReportTitle = "Italia Group";
                string ReportTitle1 = "Date of Imprest Actual Submission and payment for "
                                    + TB.Rows[0]["SYear"] + "-" + TB.Rows[0]["EYear"];

                int DataStartRw = 4;
                int DataStartCl = 1;
                int AlterColor = 1;

                #region ===== HEADER =====
                ws.Cells[1, 1].Value = ReportTitle;
                ws.Cells[2, 1].Value = ReportTitle1;

                ws.Cells[1, 1].Style.Font.Size = 14;
                ws.Cells[1, 1, 1, 5].Merge = true;
                ws.Cells[2, 1, 2, 11].Merge = true;
                ws.Cells[1, 1, 3, 15].Style.Font.Bold = true;
                #endregion

                #region ===== COLUMN HEADERS =====
                string[] headers =
                {
            "Sr. No.","Zone","Region","Employee ID","Employee Name","Designation",
            "Company","For",
            "Apr-"+TB.Rows[0]["SYear2"],"May-"+TB.Rows[0]["SYear2"],"Jun-"+TB.Rows[0]["SYear2"],
            "Jul-"+TB.Rows[0]["SYear2"],"Aug-"+TB.Rows[0]["SYear2"],"Sep-"+TB.Rows[0]["SYear2"],
            "Oct-"+TB.Rows[0]["SYear2"],"Nov-"+TB.Rows[0]["SYear2"],"Dec-"+TB.Rows[0]["SYear2"],
            "Jan-"+TB.Rows[0]["EYear2"],"Feb-"+TB.Rows[0]["EYear2"],"Mar-"+TB.Rows[0]["EYear2"],
            "Status"
        };

                for (int i = 0; i < headers.Length; i++)
                    ws.Cells[Rw, i + 1].Value = headers[i];

                Rw += 2;
                #endregion

                #region ===== DATA LOOP =====
                for (int C = 0; C < TB.Rows.Count; C++)
                {
                    DataRow dr = TB.Rows[C];
                    DataStartCl = 1;

                    // ---------- Submission Row ----------
                    ws.Cells[DataStartRw, DataStartCl++].Value = C + 1;
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Zone"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Region"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["NewId"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["EmpName"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Designation"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Company"];
                    ws.Cells[DataStartRw, DataStartCl++].Value = "Submission";

                    // Sub4 → Sub12
                    for (int m = 4; m <= 12; m++)
                        ImpressSubmissionVsPaymentWriteDate(ws, DataStartRw, ref DataStartCl, dr["Sub" + m]);

                    // Sub1 → Sub3
                    for (int m = 1; m <= 3; m++)
                        ImpressSubmissionVsPaymentWriteDate(ws, DataStartRw, ref DataStartCl, dr["Sub" + m]);

                    DataStartRw++;

                    // ---------- Payment Row ----------
                    DataStartCl = 8;
                    ws.Cells[DataStartRw, DataStartCl++].Value = "Payment";

                    // Paid4 → Paid12
                    for (int m = 4; m <= 12; m++)
                        ImpressSubmissionVsPaymentWriteDate(ws, DataStartRw, ref DataStartCl, dr["Paid" + m]);

                    // Paid1 → Paid3
                    for (int m = 1; m <= 3; m++)
                        ImpressSubmissionVsPaymentWriteDate(ws, DataStartRw, ref DataStartCl, dr["Paid" + m]);

                    ws.Cells[DataStartRw, DataStartCl++].Value = dr["Status"];

                    // ---------- Alternate Color ----------
                    if (AlterColor == 1 && DataStartRw % 2 == 1)
                    {
                        using (var r = ws.Cells[DataStartRw - 1, 1, DataStartRw, DataStartCl])
                        {
                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            r.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                        AlterColor = 0;
                    }
                    else
                    {
                        AlterColor = 1;
                    }

                    DataStartRw++;
                }
                #endregion

                int LastRow = DataStartRw - 1;

                #region ===== FORMATTING =====
                ws.Cells[1, 1, 3, DataStartCl].Style.Font.Bold = true;

                using (var r = ws.Cells[3, 1, LastRow, DataStartCl])
                {
                    r.Style.Border.Top.Style =
                    r.Style.Border.Left.Style =
                    r.Style.Border.Right.Style =
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                ws.Cells[3, 1, LastRow, DataStartCl].AutoFitColumns();

                using (var r = ws.Cells[3, 1, 3, DataStartCl])
                {
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.Black);
                    r.Style.Font.Color.SetColor(Color.White);
                }
                #endregion

                return package.GetAsByteArray();
            }
        }

        private void ImpressSubmissionVsPaymentWriteDate(ExcelWorksheet ws, int row, ref int col, object val)
        {
            if (val != null && DateTime.TryParse(val.ToString(), out DateTime dt))
                ws.Cells[row, col].Value = " " + dt.ToString("dd/MM/yy");
            else
                ws.Cells[row, col].Value = "";

            col++;
        }

        #endregion

        #region 2. Submission Monthwise
        private Color GetImpressSubmissionColorFromIndex(int index)
        {
            Color[] palette = new Color[]
            {
        Color.Empty, Color.Black, Color.White, Color.Red, Color.Lime, Color.Blue,
        Color.Yellow, Color.Magenta, Color.Cyan, Color.Maroon, Color.Green, Color.Navy,
        Color.Olive, Color.Purple, Color.Teal, Color.Silver, Color.Gray,
        Color.LightGreen, Color.LightYellow, Color.LightBlue, Color.LightPink,
        Color.LightCyan, Color.DarkRed, Color.DarkGreen, Color.DarkBlue,
        Color.Goldenrod, Color.DarkMagenta, Color.DarkCyan, Color.DarkGray
            };

            if (index > 0 && index < palette.Length)
                return palette[index];

            return Color.White;
        }

        private void ApplyImpressSubmissionBorder(ExcelRange range)
        {
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        }

        private void WriteImpressSubmissionMonth(ExcelWorksheet ws, int row, int col,
                        DataRow dr, string valueCol, string colorCol)
        {
            ws.Cells[row, col].Value = Convert.ToDecimal(dr[valueCol]);
            ws.Cells[row, col].Style.Numberformat.Format = "0";   // 6666666 style

            if (dr[colorCol] != DBNull.Value && dr[colorCol].ToString() != "")
            {
                int colorIndex = Convert.ToInt32(dr[colorCol]);
                ws.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, col].Style.Fill.BackgroundColor
                    .SetColor(GetImpressSubmissionColorFromIndex(colorIndex));
            }
        }

        public byte[] SubmissionRPT(int yearId, int divisionId, int companyId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            DataTable TB = new DataTable();
            DataTable TBLeft = new DataTable();

            // ===== CURRENT DATA =====
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString()))
            using (SqlCommand cmd = new SqlCommand("TS_Imprest_Paymentstage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FyearRowId", yearId);
                cmd.Parameters.AddWithValue("@DivisionRowId", divisionId);
                cmd.Parameters.AddWithValue("@CompanyRowId", companyId);
                cmd.Parameters.AddWithValue("@EmpStatus", "Current");

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(TB);
            }

            if (TB.Rows.Count == 0)
                return null;

            // ===== LEFT DATA =====
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString()))
            using (SqlCommand cmd = new SqlCommand("TS_Imprest_Paymentstage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FyearRowId", yearId);
                cmd.Parameters.AddWithValue("@DivisionRowId", divisionId);
                cmd.Parameters.AddWithValue("@CompanyRowId", companyId);
                cmd.Parameters.AddWithValue("@EmpStatus", "Left");

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(TBLeft);
            }

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Report");

                string sYear = TB.Rows[0]["SYear"].ToString();
                string eYear = TB.Rows[0]["EYear"].ToString();

                int row = 1;

                // ===== TITLE =====
                ws.Cells[row, 1].Value = "Italia Group";
                ws.Cells[row, 1, row, 21].Merge = true;
                ws.Cells[row, 1].Style.Font.Bold = true;
                ws.Cells[row, 1].Style.Font.Size = 14;
                ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                row++;
                ws.Cells[row, 1].Value = $"Imprest Submission Stage for {sYear}-{eYear}";
                ws.Cells[row, 1, row, 21].Merge = true;
                ws.Cells[row, 1].Style.Font.Bold = true;
                ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                row += 2;

                // ===== HEADERS =====
                string[] headers =
                {
            "Sr. No.","Zone","Region","Company","EmpID","Employee Name",
            "Grade","Designation",
            $"Apr-{sYear}",$"May-{sYear}",$"Jun-{sYear}",$"Jul-{sYear}",
            $"Aug-{sYear}",$"Sep-{sYear}",$"Oct-{sYear}",$"Nov-{sYear}",
            $"Dec-{sYear}",$"Jan-{eYear}",$"Feb-{eYear}",$"Mar-{eYear}",
            "Total"
        };

                void WriteHeaderRow(int r)
                {
                    for (int c = 0; c < headers.Length; c++)
                    {
                        ws.Cells[r, c + 1].Value = headers[c];
                        ws.Cells[r, c + 1].Style.Font.Bold = true;
                        ws.Cells[r, c + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[r, c + 1].Style.Fill.BackgroundColor.SetColor(Color.Black);
                        ws.Cells[r, c + 1].Style.Font.Color.SetColor(Color.White);
                        ws.Cells[r, c + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    ApplyImpressSubmissionBorder(ws.Cells[r, 1, r, 21]);
                }

                WriteHeaderRow(row);
                int headerRow = row;
                row++;

                int dataStartRow = row;

                // ===== CURRENT DATA =====
                for (int i = 0; i < TB.Rows.Count; i++)
                {
                    int col = 1;
                    DataRow dr = TB.Rows[i];

                    ws.Cells[row, col++].Value = i + 1;
                    ws.Cells[row, col++].Value = dr["Zone"];
                    ws.Cells[row, col++].Value = dr["Region"];
                    ws.Cells[row, col++].Value = dr["Company"];
                    ws.Cells[row, col++].Value = dr["NewId"];
                    ws.Cells[row, col++].Value = dr["EmpName"];
                    ws.Cells[row, col++].Value = dr["GradeGroup"];
                    ws.Cells[row, col++].Value = dr["Designation"];

                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimApr", "AprColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimMay", "MayColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimJun", "JunColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimJul", "JulColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimAug", "AugColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimSep", "SepColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimOct", "OctColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimNov", "NovColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimDec", "DecColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimJan", "JanColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimFeb", "FebColor");
                    WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimMar", "MarColor");

                    ws.Cells[row, col].Value = Convert.ToDecimal(dr["TotalClaim"]);
                    ws.Cells[row, col].Style.Numberformat.Format = "0";
                    col++;

                    ApplyImpressSubmissionBorder(ws.Cells[row, 1, row, 21]);
                    row++;
                }

                // ===== TOTAL ROW (CURRENT) =====
                ws.Cells[row, 9].Formula = $"SUM(I{dataStartRow}:I{row - 1})";
                ws.Cells[row, 10].Formula = $"SUM(J{dataStartRow}:J{row - 1})";
                ws.Cells[row, 11].Formula = $"SUM(K{dataStartRow}:K{row - 1})";
                ws.Cells[row, 12].Formula = $"SUM(L{dataStartRow}:L{row - 1})";
                ws.Cells[row, 13].Formula = $"SUM(M{dataStartRow}:M{row - 1})";
                ws.Cells[row, 14].Formula = $"SUM(N{dataStartRow}:N{row - 1})";
                ws.Cells[row, 15].Formula = $"SUM(O{dataStartRow}:O{row - 1})";
                ws.Cells[row, 16].Formula = $"SUM(P{dataStartRow}:P{row - 1})";
                ws.Cells[row, 17].Formula = $"SUM(Q{dataStartRow}:Q{row - 1})";
                ws.Cells[row, 18].Formula = $"SUM(R{dataStartRow}:R{row - 1})";
                ws.Cells[row, 19].Formula = $"SUM(S{dataStartRow}:S{row - 1})";
                ws.Cells[row, 20].Formula = $"SUM(T{dataStartRow}:T{row - 1})";
                ws.Cells[row, 21].Formula = $"SUM(U{dataStartRow}:U{row - 1})";

                ws.Cells[row, 9, row, 21].Style.Numberformat.Format = "0";
                ws.Cells[row, 1, row, 21].Style.Font.Bold = true;
                ApplyImpressSubmissionBorder(ws.Cells[row, 1, row, 21]);

                int endCurrentRow = row;

                // ===== LEFT SECTION =====
                if (TBLeft.Rows.Count > 0)
                {
                    row += 3;

                    ws.Cells[row, 1].Value =
                        $"Imprest Submission Stage for {TBLeft.Rows[0]["SYear"]}-{TBLeft.Rows[0]["EYear"]} (Resign / Left)";
                    ws.Cells[row, 1, row, 21].Merge = true;
                    ws.Cells[row, 1].Style.Font.Bold = true;
                    ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    row += 2;
                    WriteHeaderRow(row);
                    int leftHeaderRow = row;
                    row++;

                    int leftStartRow = row;

                    for (int i = 0; i < TBLeft.Rows.Count; i++)
                    {
                        int col = 1;
                        DataRow dr = TBLeft.Rows[i];

                        ws.Cells[row, col++].Value = i + 1;
                        ws.Cells[row, col++].Value = dr["Zone"];
                        ws.Cells[row, col++].Value = dr["Region"];
                        ws.Cells[row, col++].Value = dr["Company"];
                        ws.Cells[row, col++].Value = dr["NewId"];
                        ws.Cells[row, col++].Value = dr["EmpName"];
                        ws.Cells[row, col++].Value = dr["GradeGroup"];
                        ws.Cells[row, col++].Value = dr["Designation"];

                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimApr", "AprColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimMay", "MayColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimJun", "JunColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimJul", "JulColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimAug", "AugColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimSep", "SepColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimOct", "OctColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimNov", "NovColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimDec", "DecColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimJan", "JanColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimFeb", "FebColor");
                        WriteImpressSubmissionMonth(ws, row, col++, dr, "ClaimMar", "MarColor");

                        ws.Cells[row, col].Value = Convert.ToDecimal(dr["TotalClaim"]);
                        ws.Cells[row, col].Style.Numberformat.Format = "0";
                        col++;

                        if (dr["Left"].ToString() == "Y")
                            ws.Cells[row, 1, row, col].Style.Font.Color.SetColor(Color.Red);

                        ApplyImpressSubmissionBorder(ws.Cells[row, 1, row, 21]);
                        row++;
                    }

                    // ---- TOTAL (LEFT) ----
                    ws.Cells[row, 9].Formula = $"SUM(I{leftStartRow}:I{row - 1})";
                    ws.Cells[row, 10].Formula = $"SUM(J{leftStartRow}:J{row - 1})";
                    ws.Cells[row, 11].Formula = $"SUM(K{leftStartRow}:K{row - 1})";
                    ws.Cells[row, 12].Formula = $"SUM(L{leftStartRow}:L{row - 1})";
                    ws.Cells[row, 13].Formula = $"SUM(M{leftStartRow}:M{row - 1})";
                    ws.Cells[row, 14].Formula = $"SUM(N{leftStartRow}:N{row - 1})";
                    ws.Cells[row, 15].Formula = $"SUM(O{leftStartRow}:O{row - 1})";
                    ws.Cells[row, 16].Formula = $"SUM(P{leftStartRow}:P{row - 1})";
                    ws.Cells[row, 17].Formula = $"SUM(Q{leftStartRow}:Q{row - 1})";
                    ws.Cells[row, 18].Formula = $"SUM(R{leftStartRow}:R{row - 1})";
                    ws.Cells[row, 19].Formula = $"SUM(S{leftStartRow}:S{row - 1})";
                    ws.Cells[row, 20].Formula = $"SUM(T{leftStartRow}:T{row - 1})";
                    ws.Cells[row, 21].Formula = $"SUM(U{leftStartRow}:U{row - 1})";

                    ws.Cells[row, 9, row, 21].Style.Numberformat.Format = "0";
                    ws.Cells[row, 1, row, 21].Style.Font.Bold = true;
                    ApplyImpressSubmissionBorder(ws.Cells[row, 1, row, 21]);
                }

                // ===== FINAL FORMATTING =====
                ws.Cells.AutoFitColumns();

                // Outer border for main current table
                ws.Cells[headerRow, 1, endCurrentRow, 21]
                    .Style.Border.BorderAround(ExcelBorderStyle.Medium);

                return package.GetAsByteArray();
            }
        }

        #endregion

        #region 3. Payment Monthwise

        private void ApplyPaymentBorder(ExcelRange range)
        {
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        }

        private void ApplyPaymentAlternateColor(ExcelWorksheet ws, int row, int lastCol)
        {
            if (row % 2 == 1)
            {
                ws.Cells[row, 1, row, lastCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[row, 1, row, lastCol].Style.Fill.BackgroundColor
                    .SetColor(Color.LightGray);
            }
        }

        public byte[] PaymentRPT(int yearId, int divisionId, int companyId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            DataTable TB = new DataTable();

            // ===== GET DATA =====
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString()))
            using (SqlCommand cmd = new SqlCommand("TS_Imprest_Paymentstage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FyearRowId", yearId);
                cmd.Parameters.AddWithValue("@DivisionRowId", divisionId);
                cmd.Parameters.AddWithValue("@CompanyRowId", companyId);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(TB);
            }

            if (TB.Rows.Count == 0)
                return null;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Payment Report");

                string sYear = TB.Rows[0]["SYear"].ToString();
                string eYear = TB.Rows[0]["EYear"].ToString();

                int row = 3;
                int col = 1;

                // ===== TITLES =====
                ws.Cells[1, 1].Value = "Italia Group";
                ws.Cells[2, 1].Value = $"Imprest Payment Stage for {sYear}-{eYear}";
                ws.Cells[1, 1].Style.Font.Size = 14;
                ws.Cells[1, 1, 3, 15].Style.Font.Bold = true;

                ws.Cells[1, 1, 1, 5].Merge = true;
                ws.Cells[2, 1, 2, 11].Merge = true;

                // ===== HEADERS =====
                string[] headers =
                {
            "Sr. No.","Zone","Region","Company","EmpID","Employee Name",
            "Grade","Designation",
            $"Apr-{sYear}",$"May-{sYear}",$"Jun-{sYear}",$"Jul-{sYear}",
            $"Aug-{sYear}",$"Sep-{sYear}",$"Oct-{sYear}",$"Nov-{sYear}",
            $"Dec-{sYear}",$"Jan-{eYear}",$"Feb-{eYear}",$"Mar-{eYear}",
            "Total"
        };

                for (int c = 0; c < headers.Length; c++)
                {
                    ws.Cells[row, c + 1].Value = headers[c];
                    ws.Cells[row, c + 1].Style.Font.Bold = true;
                    ws.Cells[row, c + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[row, c + 1].Style.Fill.BackgroundColor.SetColor(Color.Black);
                    ws.Cells[row, c + 1].Style.Font.Color.SetColor(Color.White);
                }

                int headerRow = row;
                row += 1;

                int dataStartRow = row;

                // ===== DATA =====
                for (int i = 0; i < TB.Rows.Count; i++)
                {
                    col = 1;
                    DataRow dr = TB.Rows[i];

                    ws.Cells[row, col++].Value = i + 1;
                    ws.Cells[row, col++].Value = dr["Zone"];
                    ws.Cells[row, col++].Value = dr["Region"];
                    ws.Cells[row, col++].Value = dr["Company"];
                    ws.Cells[row, col++].Value = dr["NewId"];
                    ws.Cells[row, col++].Value = dr["EmpName"];
                    ws.Cells[row, col++].Value = dr["GradeGroup"];
                    ws.Cells[row, col++].Value = dr["Designation"];

                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentApr"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentMay"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentJun"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentJul"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentAug"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentSep"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentOct"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentNov"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentDec"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentJan"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentFeb"]);
                    ws.Cells[row, col++].Value = Convert.ToDecimal(dr["PaymentMar"]);

                    ws.Cells[row, col].Value = Convert.ToDecimal(dr["TotalPayment"]);
                    col++;

                    // ---- number format: no scientific, no comma ----
                    ws.Cells[row, 9, row, col - 1].Style.Numberformat.Format = "0";

                    // ---- alternate row color ----
                    ApplyPaymentAlternateColor(ws, row, col - 1);

                    row++;
                }

                int lastDataRow = row - 1;

                // ===== TOTAL ROW =====
                ws.Cells[row, 9].Formula = $"SUM(I{dataStartRow}:I{lastDataRow})";
                ws.Cells[row, 10].Formula = $"SUM(J{dataStartRow}:J{lastDataRow})";
                ws.Cells[row, 11].Formula = $"SUM(K{dataStartRow}:K{lastDataRow})";
                ws.Cells[row, 12].Formula = $"SUM(L{dataStartRow}:L{lastDataRow})";
                ws.Cells[row, 13].Formula = $"SUM(M{dataStartRow}:M{lastDataRow})";
                ws.Cells[row, 14].Formula = $"SUM(N{dataStartRow}:N{lastDataRow})";
                ws.Cells[row, 15].Formula = $"SUM(O{dataStartRow}:O{lastDataRow})";
                ws.Cells[row, 16].Formula = $"SUM(P{dataStartRow}:P{lastDataRow})";
                ws.Cells[row, 17].Formula = $"SUM(Q{dataStartRow}:Q{lastDataRow})";
                ws.Cells[row, 18].Formula = $"SUM(R{dataStartRow}:R{lastDataRow})";
                ws.Cells[row, 19].Formula = $"SUM(S{dataStartRow}:S{lastDataRow})";
                ws.Cells[row, 20].Formula = $"SUM(T{dataStartRow}:T{lastDataRow})";
                ws.Cells[row, 21].Formula = $"SUM(U{dataStartRow}:U{lastDataRow})";

                ws.Cells[row, 1, row, 21].Style.Font.Bold = true;
                ws.Cells[row, 9, row, 21].Style.Numberformat.Format = "0";

                int totalRow = row;

                // ===== BORDERS =====
                ApplyPaymentBorder(ws.Cells[headerRow, 1, totalRow, 21]);

                // ===== AUTOFIT =====
                ws.Cells[headerRow, 1, totalRow, 21].AutoFitColumns();

                // ===== PAGE SETUP (same as VB) =====
                ws.PrinterSettings.PaperSize = ePaperSize.Letter;
                ws.PrinterSettings.Orientation = eOrientation.Landscape;
                ws.PrinterSettings.BottomMargin = 0.05M;
                ws.PrinterSettings.TopMargin = 0.05M;
                ws.PrinterSettings.LeftMargin = 0.05M;
                ws.PrinterSettings.RightMargin = 0.05M;
                ws.PrinterSettings.RepeatRows = new ExcelAddress("$1:$3");

                return package.GetAsByteArray();
            }
        }


        #endregion

        #region 4. Pending Payment Monthwise

        private DataTable GetPendingPayment(string flag, int type)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con =
                new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString()))
            {
                using (SqlCommand cmd =
                    new SqlCommand("TS_Imprest_PaymentPendingMonthwise_Test", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClaimedOrSubmitted", flag);   // 'S' or 'C'
                    cmd.Parameters.AddWithValue("@OnlyTitleField", type);   // 0 or 1

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public byte[] PaymentPendingAllRecordRPT()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // ===== SAME AS VB =====
            DataTable TB_S = GetPendingPayment("S", 0); // main data
            DataTable TBTitle = GetPendingPayment("S", 1); // dynamic headers
            DataTable TB_C = GetPendingPayment("C", 0); // second section

            using (var pkg = new ExcelPackage())
            {
                var ws = pkg.Workbook.Worksheets.Add("Report");
                int row = 1;

                // ===== TITLES =====
                ws.Cells[row, 1].Value = "Italia Group";
                ws.Cells[row, 1, row, 15].Merge = true;
                ws.Cells[row, 1].Style.Font.Bold = true;
                ws.Cells[row, 1].Style.Font.Size = 14;

                row++;
                ws.Cells[row, 1].Value = "Pending Imprest Payment - Submitted in A/c's";
                ws.Cells[row, 1, row, 25].Merge = true;
                ws.Cells[row, 1].Style.Font.Bold = true;

                row += 2;

                // ===== SECTION : SUBMITTED =====
                row = WritePendingPaymentSection(ws, row, TB_S, TBTitle, "Payment");

                // ===== SECTION : CLAIMED =====
                if (TB_C.Rows.Count > 0)
                {
                    row += 3;
                    ws.Cells[row, 1].Value =
                      "Pending Imprest Payment - Not Submitted in A/c's";
                    ws.Cells[row, 1].Style.Font.Bold = true;

                    row += 2;
                    row = WritePendingPaymentSection(ws, row, TB_C, TBTitle, "Claim");
                }

                ws.Cells.AutoFitColumns();
                return pkg.GetAsByteArray();   // ✔ byte[]
            }
        }

        private string GetPendingPaymentCol(int index)
        {
            int div = index;
            string col = "";
            while (div > 0)
            {
                int mod = (div - 1) % 26;
                col = (char)(65 + mod) + col;
                div = (div - mod) / 26;
            }
            return col;
        }

        private int WritePendingPaymentSection(ExcelWorksheet ws, int startRow,
                         DataTable TB, DataTable TBTitle,
                         string prefix)
        {
            int row = startRow;
            int col;

            // ===== HEADER (VB: Cells(Rw,Cl)="Sr.No"... ) =====
            string[] fixedCols =
            { "Sr.No.","Zone","Region","Company",
      "EmpID","Employee Name","Grade","Designation" };

            col = 1;
            foreach (var h in fixedCols)
                ws.Cells[row, col++].Value = h;

            foreach (DataRow r in TBTitle.Rows)
                ws.Cells[row, col++].Value = r["FieldName"].ToString();

            ws.Cells[row, col++].Value = "Total";

            // HEADER STYLE
            using (var rng = ws.Cells[row, 1, row, col - 1])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.Black);
                rng.Style.Font.Color.SetColor(Color.White);
            }

            row++;

            // ===== DATA ROWS (VB For C=0 To TB.Rows.Count-1) =====
            int sr = 1;
            int dataStart = row;

            foreach (DataRow dr in TB.Rows)
            {
                col = 1;
                ws.Cells[row, col++].Value = sr++;
                ws.Cells[row, col++].Value = dr["Zone"];
                ws.Cells[row, col++].Value = dr["Region"];
                ws.Cells[row, col++].Value = dr["Company"];
                ws.Cells[row, col++].Value = dr["NewId"];
                ws.Cells[row, col++].Value = dr["EmpName"];
                ws.Cells[row, col++].Value = dr["GradeGroup"];
                ws.Cells[row, col++].Value = dr["Designation"];

                // ===== DYNAMIC FIELDS (Payment1..N / Claim1..N) =====
                for (int i = 0; i < TBTitle.Rows.Count; i++)
                {
                    var cell = ws.Cells[row, col++];
                    cell.Value = Convert.ToDecimal(dr[$"{prefix}{i + 1}"]);
                    cell.Style.Numberformat.Format = "0"; // no 3E+05
                }

                var tcell = ws.Cells[row, col++];
                tcell.Value = Convert.ToDecimal(dr[$"Total{prefix}"]);
                tcell.Style.Numberformat.Format = "0";

                // ===== ALTERNATE COLOR (VB AlterColor) =====
                if (row % 2 == 0)
                {
                    ws.Cells[row, 1, row, col - 1]
                      .Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[row, 1, row, col - 1]
                      .Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                row++;
            }

            // ===== TOTAL FORMULA (VB: =SUM(I4:I...)) =====
            int totalRow = row;
            ws.Cells[totalRow, 1].Value = "Total";
            ws.Cells[totalRow, 1].Style.Font.Bold = true;

            int firstDataCol = 9; // I column
            int totalCols = TBTitle.Rows.Count + 1;

            for (int c = 0; c < totalCols; c++)
            {
                int excelCol = firstDataCol + c;
                ws.Cells[totalRow, excelCol].Formula =
                  $"SUM({GetPendingPaymentCol(excelCol)}{dataStart}:{GetPendingPaymentCol(excelCol)}{row - 1})";
                ws.Cells[totalRow, excelCol].Style.Font.Bold = true;
            }

            // ===== BORDERS (VB: .Columns.Borders.LineStyle=1) =====
            using (var rng = ws.Cells[startRow, 1,
                                      totalRow, firstDataCol + totalCols - 1])
            {
                rng.Style.Border.Top.Style =
                rng.Style.Border.Bottom.Style =
                rng.Style.Border.Left.Style =
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }

            return totalRow + 1;
        }
        #endregion

        #region 5. Submmited Account By Payment Pending
        private DataTable GetSubmittedInAccountButPending()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con =
                new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString()))
            {
                using (SqlCommand cmd =
                    new SqlCommand("TS_SubmittedInAccountButPayemntIsPending", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DivisionRowId", "");
                    cmd.Parameters.AddWithValue("@CompanyRowId", "");
                    cmd.Parameters.AddWithValue("@EmpName", "");
                    cmd.Parameters.AddWithValue("@Month", "");
                    cmd.Parameters.AddWithValue("@Year", "");
                    cmd.Parameters.AddWithValue("@Type", "Report");

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public byte[] SubmittedInaccountButPendingforPayment_EPPlus()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            DataTable TB = GetSubmittedInAccountButPending();

            if (TB == null || TB.Rows.Count == 0)
                return null;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Pending Payment");

                int Rw = 4;
                int Cl = 1;
                int DataStartRw = 6;

                // ---------------- TITLES ----------------
                ws.Cells[1, 1].Value = "Italia Group";
                ws.Cells[2, 1].Value = "Pending Imprest Payment";

                ws.Cells[1, 1, 1, 5].Merge = true;
                ws.Cells[2, 1, 2, 5].Merge = true;

                ws.Cells[1, 1, 3, 15].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.Font.Size = 14;

                ws.Cells[1, 6].Value = "PAYMENT PROCESS";
                ws.Cells[2, 6].Value = "1 to 15  Reimbursed will be done between 16 to 30";
                ws.Cells[3, 6].Value = "16 to 30  Reimbursed will be done between 1 to 15 of next Month";
                ws.Cells[1, 6, 3, 6].Style.Font.Size = 10;

                // ---------------- HEADERS ----------------
                void MergeCol(string txt)
                {
                    ws.Cells[Rw, Cl].Value = txt;
                    ws.Cells[Rw, Cl, Rw + 1, Cl].Merge = true;
                    Cl++;
                }

                void MergeTwo(string title, string c1, string c2)
                {
                    ws.Cells[Rw, Cl].Value = title;
                    ws.Cells[Rw, Cl, Rw, Cl + 1].Merge = true;
                    ws.Cells[Rw + 1, Cl].Value = c1;
                    ws.Cells[Rw + 1, Cl + 1].Value = c2;
                    Cl += 2;
                }

                MergeCol("Company");
                MergeCol("Region");
                MergeCol("Zone");
                MergeCol("Employee Name");
                MergeCol("Month/ Tour Period");
                MergeCol("Claimed Amount");

                MergeTwo("Commercial", "Receipt Date", "No. of Days");
                MergeTwo("Sent to HOD for crosscheck", "Date", "No. of Days");
                MergeTwo("Sent for Mkt Head's Approval", "Date", "No. of Days");
                MergeTwo("Sent for Director's Approval", "Date", "No. of Days");

                MergeCol("PASSED Amount");

                MergeTwo("Submitted In Account", "Date", "No. of Days");

                MergeCol("Payment cycle");
                MergeTwo("Payment", "Date", "Amount");

                MergeCol("TOTAL DAYS");
                MergeCol("Remarks");

                ws.Cells[Rw, Cl].Value = "Imprest Balance Confirmation";
                ws.Cells[Rw, Cl, Rw, Cl + 2].Merge = true;
                ws.Cells[Rw + 1, Cl].Value = "Sent Date";
                ws.Cells[Rw + 1, Cl + 1].Value = "No. of Days";
                ws.Cells[Rw + 1, Cl + 2].Value = "Conf. Recd. Date";
                Cl += 3;

                Rw += 2;

                // ---------------- DATA ----------------
                foreach (DataRow dr in TB.Rows)
                {
                    int c = 1;

                    ws.Cells[DataStartRw, c++].Value = dr["Company"];
                    ws.Cells[DataStartRw, c++].Value = dr["Region"];
                    ws.Cells[DataStartRw, c++].Value = dr["Zone"];
                    ws.Cells[DataStartRw, c++].Value = dr["EmpName"];
                    ws.Cells[DataStartRw, c++].Value = dr["IMonthName"] + "-" + dr["IYear"];

                    WriteSubmmitedInaccountButPendingForPaymentAmount(ws, DataStartRw, c++, dr["ClaimedAmount"]);

                    WriteSubmmitedInaccountButPendingForPaymentDate(ws, DataStartRw, ref c, dr["CommercialReceiptDate"]);
                    ws.Cells[DataStartRw, c++].Value = dr["CommercialNoOfDays"];

                    WriteSubmmitedInaccountButPendingForPaymentDate(ws, DataStartRw, ref c, dr["SendToCrossCheckingDate"]);
                    ws.Cells[DataStartRw, c++].Value = dr["SendToCrossCheckingNoOfDays"];

                    WriteSubmmitedInaccountButPendingForPaymentDate(ws, DataStartRw, ref c, dr["SendToHeadForApprovalDate"]);
                    ws.Cells[DataStartRw, c++].Value = dr["SendToHeadForApprovalNoOfDays"];

                    WriteSubmmitedInaccountButPendingForPaymentDate(ws, DataStartRw, ref c, dr["SendToDirectorForApprovalDate"]);
                    ws.Cells[DataStartRw, c++].Value = dr["SendToDirectorForApprovalNoOfDays"];

                    WriteSubmmitedInaccountButPendingForPaymentAmount(ws, DataStartRw, c++, dr["PassedAmount"]);

                    WriteSubmmitedInaccountButPendingForPaymentDate(ws, DataStartRw, ref c, dr["SubmittedInAccountsDate"]);
                    ws.Cells[DataStartRw, c++].Value = dr["SubmittedInAccountsNoOfDays"];

                    ws.Cells[DataStartRw, c++].Value = dr["PaymentCycle"];

                    WriteSubmmitedInaccountButPendingForPaymentDate(ws, DataStartRw, ref c, dr["PaymentDate"]);
                    WriteSubmmitedInaccountButPendingForPaymentAmount(ws, DataStartRw, c++, dr["PaidAmount"]);

                    ws.Cells[DataStartRw, c++].Value = dr["TotalDays"];
                    ws.Cells[DataStartRw, c++].Value = dr["Remarks"];

                    WriteSubmmitedInaccountButPendingForPaymentDate(ws, DataStartRw, ref c, dr["Imp_Bal_Con_SendDate"]);
                    ws.Cells[DataStartRw, c++].Value = dr["Imp_Bal_Con_SendNoOfDays"];
                    WriteSubmmitedInaccountButPendingForPaymentDate(ws, DataStartRw, ref c, dr["Imp_Bal_Con_ReceivedDate"]);

                    DataStartRw++;
                }

                int LastRow = DataStartRw;

                // ---------------- TOTALS ----------------
                ws.Cells[LastRow, 6].Formula = $"SUM(F6:F{LastRow - 1})";
                ws.Cells[LastRow, 15].Formula = $"SUM(O6:O{LastRow - 1})";
                ws.Cells[LastRow, 1, LastRow, Cl].Style.Font.Bold = true;

                // ---------------- BORDERS ----------------
                var rng = ws.Cells[4, 1, LastRow, Cl];
                rng.Style.Border.Top.Style =
                rng.Style.Border.Bottom.Style =
                rng.Style.Border.Left.Style =
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                // ---------------- FORMAT ----------------
                ws.Cells[4, 1, 5, Cl].Style.Font.Bold = true;
                ws.Cells[4, 1, 5, Cl].Style.WrapText = true;
                ws.Cells[4, 1, LastRow, Cl].Style.Font.Size = 10;

                ws.Cells[4, 1, 5, Cl].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[4, 1, 5, Cl].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Row(4).Height = 29.25;
                ws.Row(5).Height = 29.25;

                ws.Cells.AutoFitColumns();

                // ---------------- FREEZE ----------------
                ws.View.FreezePanes(6, 7);

                // ---------------- PAGE SETUP ----------------
                ws.PrinterSettings.Orientation = eOrientation.Landscape;
                ws.PrinterSettings.PaperSize = ePaperSize.Letter;
                ws.PrinterSettings.TopMargin = 0.05M;
                ws.PrinterSettings.BottomMargin = 0.05M;
                ws.PrinterSettings.LeftMargin = 0.05M;
                ws.PrinterSettings.RightMargin = 0.05M;

                return package.GetAsByteArray();
            }
        }
        private void WriteSubmmitedInaccountButPendingForPaymentDate(ExcelWorksheet ws, int row, ref int col, object val)
        {
            if (val != DBNull.Value && DateTime.TryParse(val.ToString(), out DateTime d))
            {
                ws.Cells[row, col].Value = d;
                ws.Cells[row, col].Style.Numberformat.Format = "dd/MM/yy";
            }
            col++;
        }
        private void WriteSubmmitedInaccountButPendingForPaymentAmount(ExcelWorksheet ws, int row, int col, object val)
        {
            if (val != DBNull.Value)
            {
                ws.Cells[row, col].Value = Convert.ToDecimal(val);
                ws.Cells[row, col].Style.Numberformat.Format = "#,##0.00";
            }
        }

        #endregion

        #endregion

        #region Detail Tab

        #region Get Impress Filter List
        public AddEditImpressDropDownListModel GetImpressAddEditDropDownData()
        {
            connection();

            AddEditImpressDropDownListModel model = new AddEditImpressDropDownListModel();

            using (SqlCommand com = new SqlCommand("GetAllList", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetImpressDetailTabData");

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //Divison List
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            model.DivisionList.Add(new AddEditImpressDivisionFilterModel
                            {
                                RowId = Convert.ToInt32(dr["RowId"]),
                                DivisionName = dr["DivisionName"].ToString()
                            });
                        }
                    }

                    //Company List
                    if (ds.Tables.Count > 1)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            model.CompanyList.Add(new AddEditImpressCompanyFilterModel
                            {
                                RowID = Convert.ToInt32(dr["RowID"]),
                                Name = dr["Name"].ToString()
                            });
                        }
                    }

                    // Month List
                    if (ds.Tables.Count > 2)
                    {
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            model.MonthList.Add(new AddEditImpressMonthFilterModel
                            {
                                RowID = Convert.ToInt32(dr["RowID"]),
                                ShortName = dr["ShortName"].ToString(),
                            });
                        }
                    }

                    // Year List
                    if (ds.Tables.Count > 3)
                    {
                        foreach (DataRow dr in ds.Tables[3].Rows)
                        {
                            model.YearList.Add(new AddEditImpressYearsFilterModel
                            {
                                EYear = dr["EYear"].ToString(),
                            });
                        }
                    }

                    // Max No
                    if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
                    {
                        model.MaxNo = Convert.ToInt32(ds.Tables[4].Rows[0]["MaxNo"]);
                    }
                }
            }

            return model;
        }
        #endregion

        #region Get Employee List By Divison Id,Company Id,Employee Name
        public List<ImpressEmployeeModel> GetAllEmployeeList(int DivisionId, int CompanyId, string EmployeeName)
        {
            connection();
            // old method
            List<ImpressEmployeeModel> empList = new List<ImpressEmployeeModel>();
            using (SqlCommand com = new SqlCommand("TS_SearchEmpList", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@DivisionRowId", DivisionId);
                com.Parameters.AddWithValue("@CompanyRowId", CompanyId);
                com.Parameters.AddWithValue("@EmpName", EmployeeName);
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        empList.Add(new ImpressEmployeeModel
                        {
                            ID = dr["EmpRowId"].ToString(),
                            Name = dr["EmpName"].ToString()
                        });
                    }
                }
            }
            return empList;
        }
        #endregion

        #region Get Employee Detail With Emp Row Id
        public ImpressSearchEmployeeDetailModel GetEmployeeDetail(int empRowId)
        {
            ImpressSearchEmployeeDetailModel model = new ImpressSearchEmployeeDetailModel();

            using (SqlConnection con = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("TS_SetEmpDTL", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmpRowId", SqlDbType.Int).Value = empRowId;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];

                            model.EmpRowId = empRowId;
                            model.EmpName = dr["EmpName"].ToString();
                            model.Zone = dr["Zone"].ToString();
                            model.Region = dr["Region"].ToString();
                            model.Designation = dr["Designation"].ToString();
                        }
                        else
                        {
                            model.EmpRowId = 0;
                            model.EmpName = string.Empty;
                            model.Zone = string.Empty;
                            model.Region = string.Empty;
                            model.Designation = string.Empty;
                        }
                    }
                }
            }

            return model;
        }
        #endregion

        #region Payment Cycle List By Date dd/mm/yyyy
        public List<ImpressPaymentCycleModel> PaymentCycleList(string d2)
        {
            connection();

            List<ImpressPaymentCycleModel> list = new List<ImpressPaymentCycleModel>();

            SqlCommand cmd = new SqlCommand("TS_GetPaymentCycle", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@D2", d2);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ImpressPaymentCycleModel
                {
                    DisplayValue = row["CboDisplay"].ToString(),
                    Value = row["CboValue"].ToString()
                });
            }

            return list;
        }
        #endregion

        #region Impress Table Max Number
        public int GetMaxNumber()
        {
            int maxNumber = 1;

            connection();

            SqlCommand cmd = new SqlCommand(
                "SELECT ISNULL(MAX(No),0) FROM TS_ImprestDetails", con);

            con.Open();
            maxNumber = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            con.Close();

            return maxNumber;
        }
        #endregion

        #region Save Impress Details
        public bool SaveImprest(SaveImpressModel m)
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
                    cmd = new SqlCommand("SELECT * FROM TS_ImprestDetails WHERE 1<>1", con);
                else
                    cmd = new SqlCommand("SELECT * FROM TS_ImprestDetails WHERE RowID=" + m.RowID, con);

                da = new SqlDataAdapter(cmd);
                cb = new SqlCommandBuilder(da);
                da.Fill(ds, "TS_ImprestDetails");
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

                // ================= BASIC =================
                dr["No"] = m.No;
                dr["DivisionRowId"] = m.DivisionRowId;
                dr["CompanyRowID"] = m.CompanyRowID;
                dr["EmpRowID"] = m.EmpRowId;

                dr["IMonth"] = m.IMonth;
                dr["IYear"] = m.IYear;
                dr["ClaimedAmount"] = m.ClaimedAmount;

                dr["TourFrom"] = m.TourFrom.HasValue ? (object)m.TourFrom : DBNull.Value;
                dr["TourTo"] = m.TourTo.HasValue ? (object)m.TourTo : DBNull.Value;

                if (m.RowID <= 0)
                    dr["EntryLog"] = DateTime.Now;

                // ================= COMMERCIAL =================
                if (m.IsCommercial)
                {
                    dr["CommercialReceiptDate"] =
                        m.CommercialDate.HasValue ? (object)m.CommercialDate : DBNull.Value;
                    dr["CommercialNoOfDays"] = m.CommercialNoOfDays;

                    if (m.CommercialDate.HasValue)
                        dr["CommercialReceiptDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["CommercialReceiptDate"] = DBNull.Value;
                    dr["CommercialNoOfDays"] = 0;
                    dr["CommercialReceiptDateLogDate"] = DBNull.Value;
                }

                // ================= CROSS CHECK =================
                if (m.IsCrossCheck)
                {
                    dr["SendToCrossCheckingDate"] =
                        m.CrossCheckingDate.HasValue ? (object)m.CrossCheckingDate : DBNull.Value;
                    dr["SendToCrossCheckingNoOfDays"] = m.CrossCheckingNoOfDays;

                    if (m.CrossCheckingDate.HasValue)
                        dr["SendToCrossCheckingDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["SendToCrossCheckingDate"] = DBNull.Value;
                    dr["SendToCrossCheckingNoOfDays"] = 0;
                    dr["SendToCrossCheckingDateLogDate"] = DBNull.Value;
                }

                // ================= HOD =================
                if (m.IsHeadApproval)
                {
                    dr["SendToHeadForApprovalDate"] =
                        m.HeadForApprovalDate.HasValue ? (object)m.HeadForApprovalDate : DBNull.Value;
                    dr["SendToHeadForApprovalNoOfDays"] = m.HeadForApprovalNoOfDays;

                    if (m.HeadForApprovalDate.HasValue)
                        dr["SendToHeadForApprovalDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["SendToHeadForApprovalDate"] = DBNull.Value;
                    dr["SendToHeadForApprovalNoOfDays"] = 0;
                    dr["SendToHeadForApprovalDateLogDate"] = DBNull.Value;
                }

                // ================= DIRECTOR =================
                if (m.IsDirectorApproval)
                {
                    dr["SendToDirectorForApprovalDate"] =
                        m.DirectorForApprovalDate.HasValue ? (object)m.DirectorForApprovalDate : DBNull.Value;
                    dr["SendToDirectorForApprovalNoOfDays"] = m.DirectorForApprovalNoOfDays;
                    dr["PassedAmount"] = m.PassedAmount;

                    if (m.DirectorForApprovalDate.HasValue)
                        dr["SendToDirectorForApprovalDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["SendToDirectorForApprovalDate"] = DBNull.Value;
                    dr["SendToDirectorForApprovalNoOfDays"] = 0;
                    dr["PassedAmount"] = 0;
                    dr["SendToDirectorForApprovalDateLogDate"] = DBNull.Value;

                }

                // ================= SUBMITTED =================
                if (m.IsSubmitted)
                {
                    dr["SubmittedInAccountsDate"] =
                        m.SubmittedInAccountsDate.HasValue ? (object)m.SubmittedInAccountsDate : DBNull.Value;
                    dr["SubmittedInAccountsNoOfDays"] = m.SubmittedInAccountsNoOfDays;

                    if (m.SubmittedInAccountsDate.HasValue)
                        dr["SubmittedInAccountsDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["SubmittedInAccountsDate"] = DBNull.Value;
                    dr["SubmittedInAccountsNoOfDays"] = 0;
                    dr["SubmittedInAccountsDateLogDate"] = DBNull.Value;
                }

                // ================= PAYMENT =================
                if (m.IsPayment)
                {
                    dr["PaymentDate"] = m.PaymentDate.HasValue ? (object)m.PaymentDate : DBNull.Value;

                    dr["PaidAmount"] = m.PaidAmount;

                    if (m.PaymentDate.HasValue)
                        dr["PaymentDateLogDate"] = DateTime.Now;
                }
                else
                {
                    dr["PaymentDate"] = DBNull.Value;
                    dr["PaidAmount"] = 0;
                    dr["PaymentDateLogDate"] = DBNull.Value;
                }

                //// ================= CONFIRMATION =================
                //if (m.IsConfmSend)
                //{
                //    dr["Imp_Bal_Con_SendDate"] =
                //        m.ConfirmationSendDate.HasValue ? (object)m.ConfirmationSendDate : DBNull.Value;
                //    dr["Imp_Bal_Con_SendNoOfDays"] = m.ConfirmationSendNoOfDays;

                //    if (m.ConfirmationSendDate.HasValue)
                //        dr["Imp_Bal_Con_SendDateLogDate"] = DateTime.Now;
                //}
                //else
                //{
                //    dr["Imp_Bal_Con_SendDate"] = DBNull.Value;
                //    dr["Imp_Bal_Con_SendNoOfDays"] = 0;
                //    dr["Imp_Bal_Con_SendDateLogDate"] = DBNull.Value;
                //}

                //if (m.IsConfmRecieved)
                //{
                //    dr["Imp_Bal_Con_ReceivedDate"] =
                //        m.ConfirmationReceivedDate.HasValue ? (object)m.ConfirmationReceivedDate : DBNull.Value;

                //    if (m.ConfirmationReceivedDate.HasValue)
                //        dr["Imp_Bal_Con_ReceivedDateLogDate"] = DateTime.Now;
                //}
                //else
                //{
                //    dr["Imp_Bal_Con_ReceivedDate"] = DBNull.Value;
                //    dr["Imp_Bal_Con_ReceivedDateLogDate"] = DBNull.Value;
                //}

                // ================= PAYMENT =================
                dr["PaymentCycle"] = m.PaymentCycle ?? "";

                // ================= TOTAL DAYS =================
                dr["TotalDays"] = m.TotalDays;

                // ================= REMARKS =================
                if (dt.Columns.Contains("Remarks") &&
                    dr["Remarks"].ToString() != (m.Remarks ?? ""))
                {
                    dr["Remarks"] = m.Remarks ?? "";
                    dr["RemarksLogDate"] = DateTime.Now;
                }

                // ================= EXPENSES < 10000 =================
                dr["TollExps"] = m.Less_TollExps;
                dr["CourierExps"] = m.Less_CourierExps;
                dr["XeroxExps"] = m.Less_Stationery_XeroxExps;
                dr["InternetExps"] = m.Less_InternetExps;
                dr["FareExps"] = m.Less_FareExps;
                dr["TravelingExps"] = m.Less_TravelingExps;
                dr["SalesPromotionExps"] = m.Less_SalesPromotionExps;
                dr["FreightExps"] = m.Less_FreightExps;
                dr["OtherExps"] = m.Less_OtherExps;

                // ================= EXPENSES > 10000 =================
                dr["Outwardexps"] = m.Greater_OutwardExps;
                dr["ConveyanceExps"] = m.Greater_ConveyanceExps;
                dr["SundryExps"] = m.Greater_StationeryExps;
                dr["MaintenanceExps"] = m.Greater_MaintenanceExps;
                dr["PostageExps"] = m.Greater_PostageExps;
                dr["PrintingExps"] = m.Greater_PrintingExps;
                dr["SalesProExps"] = m.Greater_SalesProExps;
                dr["TelephonExps"] = m.Greater_TelephonExps;
                dr["WelfareExps"] = m.Greater_WelfareExps;
                dr["AllOtherExps"] = m.Greater_AllOtherExps;

                // ================= OTHER =================
                dr["Rent"] = m.Other_Rent;
                dr["InternetCharges"] = m.Other_InternetCharges;
                dr["MiscCharges"] = m.Other_MiscCharges;
                dr["ElectricityBill"] = m.Other_ElectricityBill;
                dr["RoImprest"] = m.Other_RoImprest;
                dr["Maintenance"] = m.Other_Maintenance;

                // ================= ADD ROW =================
                if (m.RowID <= 0)
                    dt.Rows.Add(dr);

                da.Update(ds, "TS_ImprestDetails");

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Get Edit Impress By RowId
        public GetImpressEditModel GetEditImpressModel(int rowId)
        {
            GetImpressEditModel model = new GetImpressEditModel();

            try
            {
                connection();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM TS_ImprestDetails WHERE RowID = @RowID", con))
                {
                    cmd.Parameters.AddWithValue("@RowID", rowId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count == 0)
                            return null;

                        DataRow dr = dt.Rows[0];

                        // ===== BASIC =====
                        model.RowID = rowId;
                        model.No = dr["No"] == DBNull.Value ? 0 : Convert.ToInt32(dr["No"]);
                        model.DivisionRowId = dr["DivisionRowID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DivisionRowID"]);
                        model.CompanyRowID = dr["CompanyRowID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CompanyRowID"]);
                        model.EmpRowId = dr["EmpRowID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EmpRowID"]);

                        model.IMonth = dr["IMonth"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IMonth"]);
                        model.IYear = dr["IYear"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IYear"]);

                        model.ClaimedAmount = dr["ClaimedAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ClaimedAmount"]);
                        model.PassedAmount = dr["PassedAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PassedAmount"]);

                        // ===== DATES =====
                        model.TourFrom = dr["TourFrom"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TourFrom"]);
                        model.TourTo = dr["TourTo"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TourTo"]);

                        model.CommercialDate = dr["CommercialReceiptDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommercialReceiptDate"]);
                        model.CommercialNoOfDays = dr["CommercialNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CommercialNoOfDays"]);

                        model.CrossCheckingDate = dr["SendToCrossCheckingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SendToCrossCheckingDate"]);
                        model.CrossCheckingNoOfDays = dr["SendToCrossCheckingNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SendToCrossCheckingNoOfDays"]);

                        model.HeadForApprovalDate = dr["SendToHeadForApprovalDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SendToHeadForApprovalDate"]);
                        model.HeadForApprovalNoOfDays = dr["SendToHeadForApprovalNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SendToHeadForApprovalNoOfDays"]);

                        model.DirectorForApprovalDate = dr["SendToDirectorForApprovalDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SendToDirectorForApprovalDate"]);
                        model.DirectorForApprovalNoOfDays = dr["SendToDirectorForApprovalNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SendToDirectorForApprovalNoOfDays"]);

                        model.SubmittedInAccountsDate = dr["SubmittedInAccountsDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SubmittedInAccountsDate"]);
                        model.SubmittedInAccountsNoOfDays = dr["SubmittedInAccountsNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SubmittedInAccountsNoOfDays"]);

                        model.PaymentDate = dr["PaymentDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["PaymentDate"]);
                        model.PaidAmount = dr["PaidAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PaidAmount"]);

                        //model.ConfirmationSendDate = dr["Imp_Bal_Con_SendDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Imp_Bal_Con_SendDate"]);
                        //model.ConfirmationSendNoOfDays = dr["Imp_Bal_Con_SendNoOfDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Imp_Bal_Con_SendNoOfDays"]);

                        //model.ConfirmationReceivedDate = dr["Imp_Bal_Con_ReceivedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Imp_Bal_Con_ReceivedDate"]);

                        // ===== OTHER =====
                        model.PaymentCycle = dr["PaymentCycle"] == DBNull.Value ? "" : dr["PaymentCycle"].ToString();
                        model.TotalDays = dr["TotalDays"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalDays"]);
                        model.Remarks = dr["Remarks"] == DBNull.Value ? "" : dr["Remarks"].ToString();

                        // ===== LESS THAN 10000 =====
                        model.Less_TollExps = dr["TollExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TollExps"]);
                        model.Less_CourierExps = dr["CourierExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CourierExps"]);
                        model.Less_Stationery_XeroxExps = dr["XeroxExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["XeroxExps"]);
                        model.Less_InternetExps = dr["InternetExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["InternetExps"]);
                        model.Less_FareExps = dr["FareExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FareExps"]);
                        model.Less_TravelingExps = dr["TravelingExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TravelingExps"]);
                        model.Less_SalesPromotionExps = dr["SalesPromotionExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SalesPromotionExps"]);
                        model.Less_FreightExps = dr["FreightExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["FreightExps"]);
                        model.Less_OtherExps = dr["OtherExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["OtherExps"]);

                        // ===== GREATER THAN 10000 =====
                        model.Greater_OutwardExps = dr["Outwardexps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Outwardexps"]);
                        model.Greater_ConveyanceExps = dr["ConveyanceExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ConveyanceExps"]);
                        model.Greater_StationeryExps = dr["SundryExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SundryExps"]);
                        model.Greater_MaintenanceExps = dr["MaintenanceExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["MaintenanceExps"]);
                        model.Greater_PostageExps = dr["PostageExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PostageExps"]);
                        model.Greater_PrintingExps = dr["PrintingExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PrintingExps"]);
                        model.Greater_SalesProExps = dr["SalesProExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["SalesProExps"]);
                        model.Greater_TelephonExps = dr["TelephonExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TelephonExps"]);
                        model.Greater_WelfareExps = dr["WelfareExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["WelfareExps"]);
                        model.Greater_AllOtherExps = dr["AllOtherExps"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["AllOtherExps"]);

                        // ===== OTHER EXPENSES =====
                        model.Other_Rent = dr["Rent"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Rent"]);
                        model.Other_InternetCharges = dr["InternetCharges"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["InternetCharges"]);
                        model.Other_MiscCharges = dr["MiscCharges"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["MiscCharges"]);
                        model.Other_ElectricityBill = dr["ElectricityBill"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ElectricityBill"]);
                        model.Other_RoImprest = dr["RoImprest"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["RoImprest"]);
                        model.Other_Maintenance = dr["Maintenance"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Maintenance"]);

                        model.IsCommercial = dr["CommercialReceiptDate"] != DBNull.Value;
                        model.IsCrossCheck = dr["SendToCrossCheckingDate"] != DBNull.Value;
                        model.IsHeadApproval = dr["SendToHeadForApprovalDate"] != DBNull.Value;
                        model.IsDirectorApproval = dr["SendToDirectorForApprovalDate"] != DBNull.Value;
                        model.IsSubmitted = dr["SubmittedInAccountsDate"] != DBNull.Value;
                        model.IsPayment = dr["PaymentDate"] != DBNull.Value;
                        //model.IsConfmSend = dr["Imp_Bal_Con_SendDate"] != DBNull.Value;
                        //model.IsConfmRecieved = dr["Imp_Bal_Con_ReceivedDate"] != DBNull.Value;
                    }
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

        #endregion

        #region Impress Delete Details
        public bool DeleteImpress(int rowId)
        {
            try
            {
                connection();

                SqlCommand cmd = new SqlCommand("Delete From TS_ImprestDetails WHERE RowID = @RowID", con);

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

        #region Pending List - Impress

        #region Impress Pending DataTable
        public DataTable GetImpressPendingList(int tabNo)
        {
            connection();

            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("TS_GetImprestDetails_PendingList", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupId", tabNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }
        #endregion

        #region Impress Pending Model List
        public List<ImpressPendingModel> GetImpressPendingList_Model(int tabNo)
        {
            List<ImpressPendingModel> list = new List<ImpressPendingModel>();
            DataTable dt = GetImpressPendingList(tabNo);

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ImpressPendingModel
                {
                    RowID = dr["RowID"] != DBNull.Value ? Convert.ToInt32(dr["RowID"]) : 0,
                    SrNo = dr["SrNo"] != DBNull.Value ? Convert.ToInt32(dr["SrNo"]) : 0,
                    Company = dr["Company"].ToString(),
                    EmpName = dr["EmpName"].ToString(),
                    Month = dr["Month"].ToString(),
                    Year = dr["IYear"] != DBNull.Value ? Convert.ToInt32(dr["IYear"]) : 0,
                    ClaimedAmount = dr["ClaimedAmount"] != DBNull.Value ? Convert.ToDecimal(dr["ClaimedAmount"]) : 0,
                    PaidAmount = dt.Columns.Contains("PaidAmount") && dr["PaidAmount"] != DBNull.Value
                                    ? Convert.ToDecimal(dr["PaidAmount"]) : 0,
                    PassedAmount = dt.Columns.Contains("PassedAmount") && dr["PassedAmount"] != DBNull.Value
                                    ? Convert.ToDecimal(dr["PassedAmount"]) : 0,
                    Remarks = dr["Remarks"].ToString(),

                    CommercialReceiptDate = dt.Columns.Contains("CommercialReceiptDate") && dr["CommercialReceiptDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["CommercialReceiptDate"]) : (DateTime?)null,

                    SendToCrossCheckingDate = dt.Columns.Contains("SendToCrossCheckingDate") && dr["SendToCrossCheckingDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["SendToCrossCheckingDate"]) : (DateTime?)null,

                    SendToHeadForApprovalDate = dt.Columns.Contains("SendToHeadForApprovalDate") && dr["SendToHeadForApprovalDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["SendToHeadForApprovalDate"]) : (DateTime?)null,

                    SendToDirectorForApprovalDate = dt.Columns.Contains("SendToDirectorForApprovalDate") && dr["SendToDirectorForApprovalDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["SendToDirectorForApprovalDate"]) : (DateTime?)null,

                    SubmittedInAccountsDate = dt.Columns.Contains("SubmittedInAccountsDate") && dr["SubmittedInAccountsDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["SubmittedInAccountsDate"]) : (DateTime?)null,

                    PaymentDate = dt.Columns.Contains("PaymentDate") && dr["PaymentDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["PaymentDate"]) : (DateTime?)null,

                    Imp_Bal_Con_SendDate = dt.Columns.Contains("Imp_Bal_Con_SendDate") && dr["Imp_Bal_Con_SendDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["Imp_Bal_Con_SendDate"]) : (DateTime?)null,

                    Imp_Bal_Con_ReceivedDate = dt.Columns.Contains("Imp_Bal_Con_ReceivedDate") && dr["Imp_Bal_Con_ReceivedDate"] != DBNull.Value
                                            ? Convert.ToDateTime(dr["Imp_Bal_Con_ReceivedDate"]) : (DateTime?)null
                });
            }

            return list;
        }
        #endregion

        #region Impress Save Row Wise (Using SP)
        public void SaveImpressRowWise(int groupId, List<ImpressSaveModel> list)
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
                        using (SqlCommand cmd = new SqlCommand("TS_GetImprestDetails_SaveRowWise", con))
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

        #endregion
    }
}