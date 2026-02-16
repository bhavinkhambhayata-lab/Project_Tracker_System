using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using Tracker_System.Models;


namespace Tracker_System.Classes
{
   
    public class ClsExportTracker
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
        Helpers _help = new Helpers();
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        public List<string> GetInvoiceNumbers()
        {
            List<string> invoiceNumbers = new List<string>();
            SQLHelper ObjSQLHelper = new SQLHelper();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = ObjSQLHelper.SelectProcDataDS("GetExportInvoiceNumbers", cmd);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    invoiceNumbers.Add(row["InvoiceNo_Name"].ToString());
                }
            }

            ObjSQLHelper.ClearObjects();
            return invoiceNumbers;
        }
        public bool GetDataInsertData(Exporttracker model)
        {
            connection();
            con.Open();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "INSERT");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@InvDate", model.InvDate ?? (object)DBNull.Value);
            DateTime tempDate;
            com.Parameters.AddWithValue("@InvDate",
                DateTime.TryParse(model.InvDate, out tempDate) ? (object)tempDate : DBNull.Value);
            com.Parameters.AddWithValue("@Division", model.Division ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Bill_to_Name", model.Bill_to_Name ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Ship_to_Name", model.Ship_to_Name ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@Shipping_Bill_No", model.Shipping_Bill_No);
            com.Parameters.AddWithValue("@Shipping_Bill_No",
string.IsNullOrEmpty(model.Shipping_Bill_No) ? "" : model.Shipping_Bill_No);

            com.Parameters.AddWithValue("@Shipment_Date",
      DateTime.TryParse(model.Shipping_Bill_Date, out tempDate)
          ? tempDate
          : new DateTime(1753, 1, 1));
            //  com.Parameters.AddWithValue("@Shipment_Date", model.Shipping_Bill_Date ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@Shipment_Date", DateTime.TryParse(model.Shipping_Bill_Date, out tempDate));

            //if (DateTime.TryParseExact(model.Shipping_Bill_Date, "dd/MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
            //{
            //    com.Parameters.AddWithValue("@Shipment_Date", tempDate);
            //}
            //else
            //{
            //    com.Parameters.AddWithValue("@Shipment_Date", "");
            //}
            com.Parameters.AddWithValue("@Currency_Code", model.Currency ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FOB_Value", model.Basic_Amt_Charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Freight_Amount", model.Sea_Freight_Air_Freight ?? (object)DBNull.Value);
  
            com.Parameters.AddWithValue("@Other_Charges", model.Other_Charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Payment_Terms_Code", model.Payment_Terms ?? (object)DBNull.Value);
            //com.Parameters.AddWithValue("@Port_of_Discharge", model.Port_of_Discharge );
            com.Parameters.AddWithValue("@Port_of_Discharge",
string.IsNullOrEmpty(model.Port_of_Discharge) ? "" : model.Port_of_Discharge);
            com.Parameters.AddWithValue("@Country", model.Country ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@MFROut", model.Mfr_Out ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EPCG_Licence_No", model.EPCG_Licence_No ?? (object)DBNull.Value);
            //com.Parameters.AddWithValue("@AirWay_BillNo_Bill_of_LodingNo", model.AirWay_BillNo_Bill_of_LodingNo );

            com.Parameters.AddWithValue("@AirWay_BillNo_Bill_of_LodingNo",
    string.IsNullOrEmpty(model.AirWay_BillNo_Bill_of_LodingNo) ? "" : model.AirWay_BillNo_Bill_of_LodingNo);
           

         
            com.Parameters.AddWithValue("@AirWay_BillDate_Bill_of_LodingDate",
            DateTime.TryParse(model.AirWay_BillDate_Bill_of_LodingDate, out tempDate)
                ? tempDate
                : new DateTime(1753, 1, 1));

            com.Parameters.AddWithValue("@Mode_of_Transport", model.Mode ?? (object)DBNull.Value);
            //com.Parameters.AddWithValue("@Port_of_Loading", model.Port_of_Loading);
            com.Parameters.AddWithValue("@Port_of_Loading",
string.IsNullOrEmpty(model.Port_of_Loading) ? "" : model.Port_of_Loading);
            var Clearing_PointValue = string.IsNullOrEmpty(model.Clearing_Point) || model.Clearing_Point == "-- Select --"
              ? (object)DBNull.Value
              : model.Clearing_Point;

            com.Parameters.AddWithValue("@Clearing_Point", Clearing_PointValue);
            
            var incotermsValue = string.IsNullOrEmpty(model.Inco_Terms) || model.Inco_Terms == "-- Select --"
                ? (object)DBNull.Value
                : model.Inco_Terms;

            com.Parameters.AddWithValue("@Inco_Terms", incotermsValue);
         
            com.Parameters.AddWithValue("@Remarks", model.Remarks ?? (object)DBNull.Value); 
            com.Parameters.AddWithValue("@Insurarnce_charged", model.Insurarnce_charged ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Invoice_Value", model.Invoice_Value ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
        
            com.Parameters.AddWithValue("@Weight", model.Weight ?? (object)DBNull.Value); 
            com.Parameters.AddWithValue("@Quntity_SQM", model.Quntity_SQM ?? (object)DBNull.Value);
            var typeValue = string.IsNullOrEmpty(model.Type) || model.Type == "-- Select --"
                 ? (object)DBNull.Value
                 : model.Type;

            com.Parameters.AddWithValue("@Type", typeValue);
            com.Parameters.AddWithValue("@No_of_FCL", model.No_of_FCL ?? (object)DBNull.Value);

            if (model.Due_Date != null)
            {
                com.Parameters.AddWithValue("@Due_Date", GetDbDate(model.Due_Date));
            }
            else
            {
                com.Parameters.AddWithValue("@Due_Date", new DateTime(1753, 1, 1));
            }

            int employeeRowId = 0;
            if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
            {
                employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
            }



            com.Parameters.AddWithValue("@Login_ID", employeeRowId);
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
        public List<KeyValuePair<string, string>> GetColumnWiseSuggestions()
        {
            List<KeyValuePair<string, string>> suggestions = new List<KeyValuePair<string, string>>();
            connection(); // This should initialize `con` as in your `GetDataInsertData` method

            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Mode", "GetColumnWiseSuggestions");

            con.Open();
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                suggestions.Add(new KeyValuePair<string, string>(
                    reader["ColumnName"].ToString(),
                    reader["Value"].ToString()
                ));
            }

            con.Close();

            return suggestions;
        }

        public List<Inco_TermsModel> GetAllListforIncoTerms()
        {
            connection();
            List<Inco_TermsModel> lstInco_Terms = new List<Inco_TermsModel>();

            using (SqlCommand com = new SqlCommand("GetAllList", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetInco_Terms");
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();

                
                    da.Fill(dt);
             

              
                    foreach (DataRow dr in dt.Rows)
                    {
                     
                        lstInco_Terms.Add(new Inco_TermsModel
                        {
                            ID = dr["RowId"].ToString(),
                            Name = dr["Name"].ToString()
                        });

                    }

                }
               

            }
     

            return lstInco_Terms;
        }
        public List<ClearingPointModel> GetAllListCleringPoint()
        {
            connection();
            List<ClearingPointModel> lstInco_Terms = new List<ClearingPointModel>();

            using (SqlCommand com = new SqlCommand("GetAllList", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetClearingPoint");
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();


                    da.Fill(dt);


                    // Bind EmpModel generic list using dataRow
                    foreach (DataRow dr in dt.Rows)
                    {
                        /*   Inco_TermsModel OBjDM = new Inco_TermsModel();
                           OBjDM.ID = Convert.ToString(dr["RowId"]);
                           OBjDM.Name = Convert.ToString(dr["Name"]);*/
                        lstInco_Terms.Add(new ClearingPointModel
                        {
                            ID = dr["RowId"].ToString(),
                            Name = dr["Name"].ToString()
                        });

                    }

                }


            }


            return lstInco_Terms;
        }

        public List<FCLModel> GetAllListFCL(string mode)
        {
            connection();
            List<FCLModel> lstFCL = new List<FCLModel>();

            using (SqlCommand com = new SqlCommand("GetAllList", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetFCL");
                com.Parameters.AddWithValue("@Mode", mode);
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                       
                        lstFCL.Add(new FCLModel
                        {
                            ID = dr["RowId"].ToString(),
                            Name = dr["Name"].ToString()
                        });

                    }

                }


            }


            return lstFCL;
        }
        public bool RecordExists(string InvNo)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Mode", "CheckRecord");
            com.Parameters.AddWithValue("@InvNo", InvNo);


            con.Open();
            object result = com.ExecuteScalar(); // Get single value
            con.Close();

            int exists = Convert.ToInt32(result);
            return exists == 1;
        }
        //forwader charges for air 
        public bool GetDataInsertForwarderAirData(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Forwarder Charges");
            com.Parameters.AddWithValue("@Forwarder_Details", model.Forwarder_Details ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCForwarder", model.FCForwarder ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCInvNo", model.FCInvoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCInvDate", GetDbDate(model.FCInvDate));
            com.Parameters.AddWithValue("@AdvPaymenton", GetDbDate(model.AdvPaymenton) );
            com.Parameters.AddWithValue("@PaymentDate", GetDbDate(model.PaymentDate));
            //DateTime tempDate;
            //com.Parameters.AddWithValue("@FCInvDate",
            //    DateTime.TryParse(model.FCInvDate, out tempDate) ? (object)tempDate : DBNull.Value);
            //com.Parameters.AddWithValue("@FCInvDate", model.FCInvDate ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAir_Freight", model.FCAAir_Freight ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAir_Freight_GST", model.FCAAir_Freight_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAir_Freight_GSTAmount", model.FCAAir_Freight_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAir_Freight_Total", model.FCAAir_Freight_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCAMCC", model.FCAMCC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMCC_GST", model.FCAMCC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMCC_GSTAmount", model.FCAMCC_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMCC_Total", model.FCAMCC_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAX_Ray", model.FCAX_Ray ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAX_Ray_GST", model.FCAX_Ray_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAX_Ray_GSTAmount", model.FCAX_Ray_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAX_Ray_Total", model.FCAX_Ray_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMYC_Fuel", model.FCAMYC_Fuel ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMYC_Fuel_GST", model.FCAMYC_Fuel_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMYC_Fuel_GSTAmount", model.FCAMYC_Fuel_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMYC_Fuel_Total", model.FCAMYC_Fuel_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAMS", model.FCAAMS ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAMS_GST", model.FCAAMS_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAMS_GSTAmount", model.FCAAMS_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAMS_Total", model.FCAAMS_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAWB", model.FCAAWB ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCAAWB_GST", model.FCAAWB_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAWB_GSTAmount", model.FCAAWB_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAWB_Total", model.FCAAWB_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAPCA", model.FCAPCA ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAPCA_GST", model.FCAPCA_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAPCA_GSTAmount", model.FCAPCA_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAPCA_Total", model.FCAPCA_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAOthers", model.FCAOthers ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAOthers_GST", model.FCAOthers_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAOthers_GSTAmount", model.FCAOthers_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAOthers_Total", model.FCAOthers_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAmt_before_GST", model.FCAAMT_before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAGST", model.FCAGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCATOTAL", model.FCATotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
        //forwarder charges for sea 
        public bool GetDataInsertForwarderSeaData(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Forwarder Charges");
            com.Parameters.AddWithValue("@Forwarder_Details", model.Forwarder_Details ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCForwarder", model.FCForwarder ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCInvNo", model.FCInvoice_No);
            com.Parameters.AddWithValue("@AdvPaymenton", GetDbDate(model.AdvPaymenton ));
            com.Parameters.AddWithValue("@PaymentDate", GetDbDate(model.PaymentDate));

            //com.Parameters.AddWithValue("@FCInvDate", model.FCInvDate ?? (object)DBNull.Value);
            //     DateTime tempDate;
            //com.Parameters.AddWithValue("@FCInvDate",
            //    DateTime.TryParse(model.FCInvDate, out tempDate) ? (object)tempDate : DBNull.Value);
            com.Parameters.AddWithValue("@FCInvDate", GetDbDate(model.FCInvDate));
            com.Parameters.AddWithValue("@FCSFreight", model.FCSFreight ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSFreight_GST", model.FCSFreight_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSFreight_GSTAmount", model.FCSFreight_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSFreight_Total", model.FCSFreight_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSTHC", model.FCSTHC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSTHC_GST", model.FCSTHC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSTHC_GSTAmount", model.FCSTHC_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSTHC_Total", model.FCSTHC_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSBL", model.FCSBL ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSBL_GST", model.FCSBL_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSBL_GSTAmount", model.FCSBL_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSBL_Total", model.FCSBL_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSSEAL", model.FCSSeal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSEAL_GST", model.FCSSEAL_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSEAL_GSTAmount", model.FCSSEAL_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSEAL_Total", model.FCSSEAL_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSVGM", model.FCSVGM ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSVGM_GST", model.FCSVGM_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSVGM_GSTAmount", model.FCSVGM_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSVGM_Total", model.FCSVGM_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSMUC", model.FCSMUC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSMUC_GST", model.FCSMUC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSMUC_GSTAmount", model.FCSMUC_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSMUC_Total", model.FCSMUC_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSITHC", model.FCSITHC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSITHC_GST", model.FCSITHC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSITHC_GSTAmount", model.FCSITHC_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSITHC_Total", model.FCSITHC_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSDry_Port_charges", model.FCSDry_Port_charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSDry_Port_charges_GST", model.FCSDry_Port_charges_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSDry_Port_charges_GSTAmount", model.FCSDry_Port_charges_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSDry_Port_charges_Total", model.FCSDry_Port_charges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSAdministrative_charges", model.FCSAdministrative_charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSAdministrative_charges_GST", model.FCSAdministrative_charges_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSAdministrative_charges_GSTAmount", model.FCSAdministrative_charges_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSAdministrative_charges_Total", model.FCSAdministrative_charges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSSecurity_filling_fees", model.FCSSecurity_filling_fees ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSecurity_filling_fees_GST", model.FCSSecurity_filling_fees_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSecurity_filling_fees_GSTAmount", model.FCSSecurity_filling_fees_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSecurity_filling_fees_Total", model.FCSSecurity_filling_fees_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSOther", model.FCSOther ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSOther_GST", model.FCSOther_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSOther_GSTAmount", model.FCSOther_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSOther_Total", model.FCSOther_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSAmt_before_GST", model.FCSAmt_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSGST", model.FCSGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSTOTAL", model.FCSTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
        public bool GetDataunloack(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Update FinalSave");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);


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
        public bool GetDataInsertDCData(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Document Details");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@DocForwardedNo", model.Doc_ForwardedNo ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@DocSentThrough", model.Doc_Sent_Through ?? (object)DBNull.Value);
            //DateTime tempDate;
            //com.Parameters.AddWithValue("@DocDate",
            //    DateTime.TryParse(model.Doc_Date, out tempDate) ? (object)tempDate : DBNull.Value);
            com.Parameters.AddWithValue("@DocDate", GetDbDate(model.Doc_Date));
            com.Parameters.AddWithValue("@DocSubmittedAccountOn", GetDbDate(model.Doc_Submitted_Account_On));
            //com.Parameters.AddWithValue("@DocSubmittedAccountOn",
            //    DateTime.TryParse(model.Doc_Submitted_Account_On, out tempDate) ? (object)tempDate : DBNull.Value);
            // com.Parameters.AddWithValue("@DocSubmittedAccountOn", model.Doc_Submitted_Account_On ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
        public bool GetDataInsertClearanceData(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Clearance Charges");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCClearance_Agent", model.CCClearance_Agent_Name ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCInvNo", model.CCInvoice_No ?? (object)DBNull.Value);
            //com.Parameters.AddWithValue("@CCInvDate", model.CCInvDate ?? (object)DBNull.Value);
            //DateTime tempDate;
            //com.Parameters.AddWithValue("@CCInvDate",
            //    DateTime.TryParse(model.CCInvDate, out tempDate) ? (object)tempDate : DBNull.Value);

            com.Parameters.AddWithValue("@CCInvDate", GetDbDate(model.CCInvDate));

            com.Parameters.AddWithValue("@CCClearance_Agency_charges", model.CCClearance_agency_charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCClearance_Agency_chargesGST", model.CCClearance_Agency_chargesGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCClearance_Agency_chargesGSTAmount", model.CCClearance_Agency_chargesGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCClearance_Agency_charges_Total", model.CCClearance_Agency_charges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCEdi_Xeam_Charges", model.CCEDI_Xeam_charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCEdi_Xeam_ChargesGST", model.CCEdi_Xeam_ChargesGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCEdi_Xeam_ChargesGSTAmount", model.CCEdi_Xeam_ChargesGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCEdi_Xeam_Charges_Total", model.CCEdi_Xeam_Charges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCVGM", model.CCVGM ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCVGMGST", model.CCVGMGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCVGMGSTAmount", model.CCVGMGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCVGM_Total", model.CCVGM_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCGSEC", model.CCGSEC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCGSECGST", model.CCGSECGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCGSECGSTAmount", model.CCGSECGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCGSEC_Total", model.CCGSEC_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCCOO", model.CCCOO ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCOOGST", model.CCCOOGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCOOGSTAmount", model.CCCOOGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCOO_Total", model.CCCOO_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCExamination_CFS", model.CCExamination_CFS ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCExamination_CFSGST", model.CCExamination_CFSGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCExamination_CFSGSTAmount", model.CCExamination_CFSGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCExamination_CFS_Total", model.CCExamination_CFS_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCLift_on_Lift_off", model.CCLift_on_Lift_off ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCLift_on_Lift_offGST", model.CCLift_on_Lift_offGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCLift_on_Lift_offGSTAmount", model.CCLift_on_Lift_offGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCLift_on_Lift_off_Total", model.CCLift_on_Lift_off_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCFS", model.CCCFS ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCFSGST", model.CCCFSGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCFSGSTAmount", model.CCCFSGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCFSTotal", model.CCCFSTotal ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCOthers", model.CCOthers ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCOthersGST", model.CCOthersGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCOthersGSTAmount", model.CCOthersGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCOthers_Total", model.CCOthers_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCAmt_Before_GST", model.CCAmt_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCGST", model.CCGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCTotal", model.CC_total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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

        public bool GetDataInsertCFSData(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "CFS Charges");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No );
            com.Parameters.AddWithValue("@CFSVendor", model.CFS_Vendor );
            com.Parameters.AddWithValue("@CFSInvNo", model.CFS_Invoice_No );
           //com.Parameters.AddWithValue("@CFSInvDate", model.CFS_InvDate ?? (object)DBNull.Value);

            //DateTime tempDate;
            //com.Parameters.AddWithValue("@CFSInvDate",
            //    DateTime.TryParse(model.CFS_InvDate, out tempDate) ? (object)tempDate : DBNull.Value);
            com.Parameters.AddWithValue("@CFSInvDate", GetDbDate(model.CFS_InvDate));

            com.Parameters.AddWithValue("@CFSParticulars", model.CFS_Particulars );
            com.Parameters.AddWithValue("@CFSAmt_before_GST", model.CFS_Amt_Before_GST);
            com.Parameters.AddWithValue("@CFSGST", model.CFS_GST );
            com.Parameters.AddWithValue("@CFSTotal", model.CFS_Total);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
        public int GetDataInsertOCData(Exporttracker model)
        {
            int insertedCount = 0;
            if (model.OtherCharges != null && model.OtherCharges.Any())
            {
                connection(); // Initialize connection object (e.g., con)


                foreach (var charge in model.OtherCharges)
                {
               
                    int? masterId = null;
                    using (SqlCommand cmdGetMasterId = new SqlCommand("SELECT RowId FROM ExportTracker WHERE InvNo = @InvNo", con))
                    {
                        cmdGetMasterId.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);

                        if (con.State != ConnectionState.Open)
                            con.Open();

                        var masterIdResult = cmdGetMasterId.ExecuteScalar();
                        if (masterIdResult != null && masterIdResult != DBNull.Value)
                        {
                            masterId = Convert.ToInt32(masterIdResult);
                        }
                        else
                        {
                            // Handle missing Invoice_No case
                            masterId = 0; // or throw exception or handle error
                        }
                    }

                    int? ocRowId = null;
                    using (SqlCommand checkCmd = new SqlCommand(@"
                SELECT TOP 1 RowId 
                FROM ExportTracker_Others_Charge
                WHERE ETMasterId = @ETMasterId 
                  AND OCVendor = @OCVendor 
                  AND OCInvNo = @OCInvNo
            ", con))
                    {
                        checkCmd.Parameters.AddWithValue("@ETMasterId", masterId ?? 0);
                        checkCmd.Parameters.AddWithValue("@OCVendor", charge.OC_Vendor ?? "");
                        checkCmd.Parameters.AddWithValue("@OCInvNo", charge.OC_Invoice_No ?? "");

                        var row = checkCmd.ExecuteScalar();
                        if (row != null && int.TryParse(row.ToString(), out int existingRowId))
                            ocRowId = existingRowId; 
                    }

                    using (SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.AddWithValue("@Mode", "Other Charges");
                        com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@OCVendor", charge.OC_Vendor ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@OCInvNo", charge.OC_Invoice_No ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@ETMasterId", masterId ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@OC_RowId", ocRowId ?? (object)DBNull.Value);
                        //DateTime tempDate;
                        //com.Parameters.AddWithValue("@OCInvDate",
                        //    DateTime.TryParse(charge.OC_InvDate, out tempDate) ? (object)tempDate : DBNull.Value);
                        com.Parameters.AddWithValue("@OCInvDate", GetDbDate(charge.OC_InvDate));
                        com.Parameters.AddWithValue("@OCAdvPaymenton", GetDbDate(charge.OCAdvPaymenton));
                        com.Parameters.AddWithValue("@OC_BLAdvPaymenton", GetDbDate(charge.OC_BLAdvPaymenton));
                        com.Parameters.AddWithValue("@OC_BLPaymentDate", GetDbDate(charge.OC_BLPaymentDate));
                        com.Parameters.AddWithValue("@OCPaymentDate", GetDbDate(charge.OCPaymentDate));
                        com.Parameters.AddWithValue("@OCParticulars", charge.OC_Particulars ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@OCAmt_Before_GST", charge.OC_Amt_Before_GST ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@OCGSTPercentage", charge.OC_GSTPercentage ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@OCGST", charge.OC_GSTAmount ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@OCTotal", charge.OC_Total ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
                        com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
                        int employeeRowId = 0;
                        if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
                        {
                            employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
                        }
                        com.Parameters.AddWithValue("@Login_ID", employeeRowId);
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        int result = com.ExecuteNonQuery();
                        if (result > 0)
                            insertedCount++;
                    }
                }

                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            

            return insertedCount;
        }
        public int GetDataInsertCFSOCData(Exporttracker model)
        {
            int insertedCount = 0;
            if (model.CFSOtherCharges != null && model.CFSOtherCharges.Any())
            {
                connection(); // Initialize connection object (e.g., con)

                int employeeRowId = 0;
                if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
                {
                    employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
                }
                if (con.State != ConnectionState.Open)
                    con.Open();
                foreach (var charge in model.CFSOtherCharges)
                {
                    // int? existingRowId = null;

                    // 1. Get ETMasterId from ExportTracker based on Invoice_No
                    int? masterId = null;
                    using (SqlCommand cmdGetMasterId = new SqlCommand("SELECT RowId FROM ExportTracker WHERE InvNo = @InvNo", con))
                    {
                        cmdGetMasterId.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);



                        var masterIdResult = cmdGetMasterId.ExecuteScalar();
                        if (masterIdResult != null && masterIdResult != DBNull.Value)
                        {
                            masterId = Convert.ToInt32(masterIdResult);
                        }
                        else
                        {
                            // Handle missing Invoice_No case
                            masterId = 0; // or throw exception or handle error
                        }
                    }



                    using (SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.AddWithValue("@Mode", "Additional CFS Charges");
                        com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);

                        com.Parameters.AddWithValue("@Nature_of_Service", charge.Nature_of_Service ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@AmtBeforeGST", charge.AmtBeforeGST ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@masterId", masterId ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@CFSRowID", charge.Cfs_RowId ?? (object)DBNull.Value);

                        // com.Parameters.AddWithValue("@OCInvDate", dateValue);
                        com.Parameters.AddWithValue("@GstPercentage", charge.GstPercentage ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@GstAmount", charge.GstAmount ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@Total", charge.Total ?? (object)DBNull.Value);
                        com.Parameters.AddWithValue("@Login_ID", employeeRowId);
                        com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);


                        if (con.State != ConnectionState.Open)
                            con.Open();

                        int result = com.ExecuteNonQuery();
                        if (result > 0)
                            insertedCount++;
                    }
                }

                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return insertedCount;
        }

        public bool GetDataInsertAddTCData(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Additional Transportation Charges");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCTransporter", model.AddTC_Transporter ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCInvNo", model.AddTC_Invoice_No ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@TCInvDate", model.TC_InvDate ?? (object)DBNull.Value);

            //DateTime tempDate;
            //com.Parameters.AddWithValue("@AddTCInvDate",
            //    DateTime.TryParse(model.AddTC_InvDate, out tempDate) ? (object)tempDate : DBNull.Value);
            com.Parameters.AddWithValue("@AddTCInvDate", GetDbDate(model.AddTC_InvDate));
            com.Parameters.AddWithValue("@AddTCAdvPaymentOn", GetDbDate(model.AddTCAdvPaymentOn));
            com.Parameters.AddWithValue("@AddTCPaymentDate", GetDbDate(model.AddTCPaymentDate));
            com.Parameters.AddWithValue("@AddTC_Charges", model.AddTC_Charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCChargesGST", model.AddTCChargesGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCChargesGSTAmount", model.AddTCChargesGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCCharges_Total", model.AddTCCharges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@AddTC_VGM", model.AddTC_VGM ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCVGMGST", model.AddTCVGMGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCVGMGSTAmount", model.AddTCVGMGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCVGM_Total", model.AddTCVGM_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@AddTC_Other", model.AddTC_Other ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCOtherGST", model.AddTCOtherGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCOtherGSTAmount", model.AddTCOtherGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCOther_Total", model.AddTCOther_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCAmt_before_GST", model.AddTC_AMT_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCGST", model.AddTC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCTotal", model.AddTC_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
        public bool GetDataInsertTCData(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Transportation Charges");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCTransporter", model.TC_Transporter ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCInvNo", model.TC_Invoice_No ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@TCInvDate", model.TC_InvDate ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCInvDate", GetDbDate(model.TC_InvDate));
            //DateTime tempDate;
            //com.Parameters.AddWithValue("@TCInvDate",
            //    DateTime.TryParse(model.TC_InvDate, out tempDate) ? (object)tempDate : DBNull.Value);


            com.Parameters.AddWithValue("@TCCharges", model.TC_Charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCChargesGST", model.TCChargesGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCChargesGSTAmount", model.TCChargesGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCCharges_Total", model.TCCharges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@TCVGM", model.TC_VGM ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCVGMGST", model.TCVGMGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCVGMGSTAmount", model.TCVGMGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCVGM_Total", model.TCVGM_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@TC_Other", model.TC_Other ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCOtherGST", model.TCOtherGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCOtherGSTAmount", model.TCOtherGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCOther_Total", model.TCOther_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCAmt_before_GST", model.TC_AMT_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCGST", model.TC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCTotal", model.TC_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
        public bool GetDataInsertCOOCData(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "COO Charges");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOVendor", model.COO_Vendor ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOInvNo", model.COO_Invoice_No ?? (object)DBNull.Value);
            //com.Parameters.AddWithValue("@COOInvDate", model.COO_InvDate ?? (object)DBNull.Value);

            //DateTime tempDate;
            //com.Parameters.AddWithValue("@COOInvDate",
            //    DateTime.TryParse(model.COO_InvDate, out tempDate) ? (object)tempDate : DBNull.Value);

            com.Parameters.AddWithValue("@COOInvDate", GetDbDate(model.COO_InvDate));

            com.Parameters.AddWithValue("@COOParticulars", model.COO_Particulars ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOAmt_before_GST", model.COO_Amt_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOPercentage", model.COOGSTPercentage ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOGST", model.COO_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOTotal", model.COO_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
        public bool GetAdvanceDataUpdate(AdvanceTracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Update PaymentDate");
            com.Parameters.AddWithValue("@SourceType", model.SourceType);
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No);
            com.Parameters.AddWithValue("@FCInvNo", model.FCInvoiceNo);
       
            DateTime paymentDate = DateTime.ParseExact(model.PaymentDate, "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture);
            com.Parameters.AddWithValue("@PaymentDate", paymentDate);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
        public bool GetShipmentDataUpdate(Shipmenttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Shipment data");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Vessel_Airlines", model.Vessel_Airlines ?? (object)DBNull.Value);
          
            com.Parameters.AddWithValue("@ETD", GetDbDate(model.ETD));
            com.Parameters.AddWithValue("@ETA", GetDbDate(model.ETA));

            com.Parameters.AddWithValue("@Transit_Time", model.Transit_Time ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@BL_Express", model.BL_Express ?? (object)DBNull.Value);
      
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
        public bool GetAllDataFinalSave(Exporttracker model)
        {
            string[] formats = { "dd/MM/yy" }; // correct format

            // Helper method
            DateTime? ParseDate(string input)
            {
                if (!string.IsNullOrWhiteSpace(input) &&
                    DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                {
                    return dt;
                }
                return null;
            }
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "ALL FINAL SAVE");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@InvDate", model.InvDate ?? (object)DBNull.Value);

            /* DateTime parsedD56;
             if (DateTime.TryParseExact(model.InvDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedD56))
             {
                 com.Parameters.AddWithValue("@InvDate", parsedD56);
             }
             else
             {
                 com.Parameters.AddWithValue("@InvDate", DBNull.Value);
             }*/
            com.Parameters.AddWithValue("@Division", model.Division ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Bill_to_Name", model.Bill_to_Name ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Ship_to_Name", model.Ship_to_Name ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@Shipping_Bill_No", model.Shipping_Bill_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Shipping_Bill_No",
    string.IsNullOrEmpty(model.Shipping_Bill_No) ? "" : model.Shipping_Bill_No);
            com.Parameters.AddWithValue("@Currency_Code", model.Currency ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FOB_Value", model.Basic_Amt_Charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Freight_Amount", model.Sea_Freight_Air_Freight ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@Other_Charges", model.Other_Charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Payment_Terms_Code", model.Payment_Terms ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@Port_of_Discharge", model.Port_of_Discharge ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Port_of_Discharge",
string.IsNullOrEmpty(model.Port_of_Discharge) ? "" : model.Port_of_Discharge);
            com.Parameters.AddWithValue("@Country", model.Country ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@MFROut", model.Mfr_Out ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EPCG_Licence_No", model.EPCG_Licence_No ?? (object)DBNull.Value);
            //com.Parameters.AddWithValue("@AirWay_BillNo_Bill_of_LodingNo", model.AirWay_BillNo_Bill_of_LodingNo ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AirWay_BillNo_Bill_of_LodingNo",
    string.IsNullOrEmpty(model.AirWay_BillNo_Bill_of_LodingNo) ? "" : model.AirWay_BillNo_Bill_of_LodingNo);
            DateTime tempDate;
            //            com.Parameters.AddWithValue("@AirWay_BillDate_Bill_of_LodingDate",
            //DateTime.TryParse(model.AirWay_BillDate_Bill_of_LodingDate, out tempDate) ? (object)tempDate : DBNull.Value);
            com.Parameters.AddWithValue("@AirWay_BillDate_Bill_of_LodingDate",
            DateTime.TryParse(model.AirWay_BillDate_Bill_of_LodingDate, out tempDate)
                ? tempDate
                : new DateTime(1753, 1, 1));

            com.Parameters.AddWithValue("@Mode_of_Transport", model.Mode ?? (object)DBNull.Value);
            //com.Parameters.AddWithValue("@Port_of_Loading", model.Port_of_Loading ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Port_of_Loading",
string.IsNullOrEmpty(model.Port_of_Loading) ? "" : model.Port_of_Loading);
            com.Parameters.AddWithValue("@Clearing_Point", model.Clearing_Point ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Inco_Terms", model.Inco_Terms ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Remarks", model.Remarks ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Insurarnce_charged", model.Insurarnce_charged ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Invoice_Value", model.Invoice_Value ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCClearance_Agent", model.CCClearance_Agent_Name ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCInvNo", model.CCInvoice_No ?? (object)DBNull.Value);




            com.Parameters.AddWithValue("@CCClearance_Agency_charges", model.CCClearance_agency_charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCClearance_Agency_chargesGST", model.CCClearance_Agency_chargesGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCClearance_Agency_chargesGSTAmount", model.CCClearance_Agency_chargesGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCClearance_Agency_charges_Total", model.CCClearance_Agency_charges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCEdi_Xeam_Charges", model.CCEDI_Xeam_charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCEdi_Xeam_ChargesGST", model.CCEdi_Xeam_ChargesGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCEdi_Xeam_ChargesGSTAmount", model.CCEdi_Xeam_ChargesGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCEdi_Xeam_Charges_Total", model.CCEdi_Xeam_Charges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCVGM", model.CCVGM ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCVGMGST", model.CCVGMGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCVGMGSTAmount", model.CCVGMGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCVGM_Total", model.CCVGM_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCGSEC", model.CCGSEC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCGSECGST", model.CCGSECGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCGSECGSTAmount", model.CCGSECGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCGSEC_Total", model.CCGSEC_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCCOO", model.CCCOO ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCOOGST", model.CCCOOGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCOOGSTAmount", model.CCCOOGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCOO_Total", model.CCCOO_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCExamination_CFS", model.CCExamination_CFS ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCExamination_CFSGST", model.CCExamination_CFSGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCExamination_CFSGSTAmount", model.CCExamination_CFSGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCExamination_CFS_Total", model.CCExamination_CFS_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCLift_on_Lift_off", model.CCLift_on_Lift_off ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCLift_on_Lift_offGST", model.CCLift_on_Lift_offGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCLift_on_Lift_offGSTAmount", model.CCLift_on_Lift_offGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCLift_on_Lift_off_Total", model.CCLift_on_Lift_off_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCCFS", model.CCCFS ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCFSGST", model.CCCFSGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCFSGSTAmount", model.CCCFSGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCCFSTotal", model.CCCFSTotal ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@CCOthers", model.CCOthers ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCOthersGST", model.CCOthersGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCOthersGSTAmount", model.CCOthersGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCOthers_Total", model.CCOthers_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCAmt_Before_GST", model.CCAmt_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCGST", model.CCGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CCTotal", model.CC_total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCForwarder", model.FCForwarder ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCInvNo", model.FCInvoice_No ?? (object)DBNull.Value);


            com.Parameters.AddWithValue("@FCAAir_Freight", model.FCAAir_Freight ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAir_Freight_GST", model.FCAAir_Freight_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAir_Freight_GSTAmount", model.FCAAir_Freight_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAir_Freight_Total", model.FCAAir_Freight_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCAMCC", model.FCAMCC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMCC_GST", model.FCAMCC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMCC_GSTAmount", model.FCAMCC_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMCC_Total", model.FCAMCC_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAX_Ray", model.FCAX_Ray ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAX_Ray_GST", model.FCAX_Ray_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAX_Ray_GSTAmount", model.FCAX_Ray_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAX_Ray_Total", model.FCAX_Ray_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMYC_Fuel", model.FCAMYC_Fuel ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMYC_Fuel_GST", model.FCAMYC_Fuel_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMYC_Fuel_GSTAmount", model.FCAMYC_Fuel_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAMYC_Fuel_Total", model.FCAMYC_Fuel_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAMS", model.FCAAMS ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAMS_GST", model.FCAAMS_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAMS_GSTAmount", model.FCAAMS_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAMS_Total", model.FCAAMS_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAWB", model.FCAAWB ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCAAWB_GST", model.FCAAWB_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAWB_GSTAmount", model.FCAAWB_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAWB_Total", model.FCAAWB_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAPCA", model.FCAPCA ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAPCA_GST", model.FCAPCA_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAPCA_GSTAmount", model.FCAPCA_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAPCA_Total", model.FCAPCA_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAOthers", model.FCAOthers ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAOthers_GST", model.FCAOthers_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAOthers_GSTAmount", model.FCAOthers_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAOthers_Total", model.FCAOthers_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAAmt_before_GST", model.FCAAMT_before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCAGST", model.FCAGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCATOTAL", model.FCATotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSFreight", model.FCSFreight ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSFreight_GST", model.FCSFreight_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSFreight_GSTAmount", model.FCSFreight_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSFreight_Total", model.FCSFreight_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSTHC", model.FCSTHC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSTHC_GST", model.FCSTHC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSTHC_GSTAmount", model.FCSTHC_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSTHC_Total", model.FCSTHC_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSBL", model.FCSBL ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSBL_GST", model.FCSBL_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSBL_GSTAmount", model.FCSBL_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSBL_Total", model.FCSBL_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSSEAL", model.FCSSeal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSEAL_GST", model.FCSSEAL_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSEAL_GSTAmount", model.FCSSEAL_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSEAL_Total", model.FCSSEAL_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSVGM", model.FCSVGM ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSVGM_GST", model.FCSVGM_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSVGM_GSTAmount", model.FCSVGM_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSVGM_Total", model.FCSVGM_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSMUC", model.FCSMUC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSMUC_GST", model.FCSMUC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSMUC_GSTAmount", model.FCSMUC_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSMUC_Total", model.FCSMUC_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSITHC", model.FCSITHC ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSITHC_GST", model.FCSITHC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSITHC_GSTAmount", model.FCSITHC_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSITHC_Total", model.FCSITHC_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSDry_Port_charges", model.FCSDry_Port_charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSDry_Port_charges_GST", model.FCSDry_Port_charges_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSDry_Port_charges_GSTAmount", model.FCSDry_Port_charges_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSDry_Port_charges_Total", model.FCSDry_Port_charges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSAdministrative_charges", model.FCSAdministrative_charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSAdministrative_charges_GST", model.FCSAdministrative_charges_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSAdministrative_charges_GSTAmount", model.FCSAdministrative_charges_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSAdministrative_charges_Total", model.FCSAdministrative_charges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSSecurity_filling_fees", model.FCSSecurity_filling_fees ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSecurity_filling_fees_GST", model.FCSSecurity_filling_fees_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSecurity_filling_fees_GSTAmount", model.FCSSecurity_filling_fees_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSSecurity_filling_fees_Total", model.FCSSecurity_filling_fees_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@FCSOther", model.FCSOther ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSOther_GST", model.FCSOther_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSOther_GSTAmount", model.FCSOther_GSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSOther_Total", model.FCSOther_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSAmt_before_GST", model.FCSAmt_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSGST", model.FCSGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FCSTOTAL", model.FCSTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CFSVendor", model.CFS_Vendor ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CFSInvNo", model.CFS_Invoice_No ?? (object)DBNull.Value);


            com.Parameters.AddWithValue("@CFSParticulars", model.CFS_Particulars ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CFSAmt_before_GST", model.CFS_Amt_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CFSGST", model.CFS_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CFSTotal", model.CFS_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCTransporter", model.TC_Transporter ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCInvNo", model.TC_Invoice_No ?? (object)DBNull.Value);


            com.Parameters.AddWithValue("@TCCharges", model.TC_Charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCChargesGST", model.TCChargesGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCChargesGSTAmount", model.TCChargesGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCCharges_Total", model.TCCharges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@TCVGM", model.TC_VGM ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCVGMGST", model.TCVGMGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCVGMGSTAmount", model.TCVGMGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCVGM_Total", model.TCVGM_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@TC_Other", model.TC_Other ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCOtherGST", model.TCOtherGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCOtherGSTAmount", model.TCOtherGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCOther_Total", model.TCOther_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCAmt_before_GST", model.TC_AMT_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCGST", model.TC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@TCTotal", model.TC_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCTransporter", model.AddTC_Transporter ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCInvNo", model.AddTC_Invoice_No ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@TCInvDate", model.TC_InvDate ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCInvDate", GetDbDate(model.AddTC_InvDate));
            com.Parameters.AddWithValue("@AddTCAdvPaymentOn", GetDbDate(model.AddTCAdvPaymentOn));
            com.Parameters.AddWithValue("@AddTCPaymentDate", GetDbDate(model.AddTCPaymentDate));
            //DateTime AddTCInvDatetempDate;
            //com.Parameters.AddWithValue("@AddTCInvDate",
            //    DateTime.TryParse(model.AddTC_InvDate, out AddTCInvDatetempDate) ? (object)AddTCInvDatetempDate : DBNull.Value);

            com.Parameters.AddWithValue("@AddTC_Charges", model.AddTC_Charges ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCChargesGST", model.AddTCChargesGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCChargesGSTAmount", model.AddTCChargesGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCCharges_Total", model.AddTCCharges_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@AddTC_VGM", model.AddTC_VGM ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCVGMGST", model.AddTCVGMGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCVGMGSTAmount", model.AddTCVGMGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCVGM_Total", model.AddTCVGM_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@AddTC_Other", model.AddTC_Other ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCOtherGST", model.AddTCOtherGST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCOtherGSTAmount", model.AddTCOtherGSTAmount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCOther_Total", model.AddTCOther_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCAmt_before_GST", model.AddTC_AMT_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCGST", model.AddTC_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AddTCTotal", model.AddTC_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOVendor", model.COO_Vendor ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOInvNo", model.COO_Invoice_No ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@COOParticulars", model.COO_Particulars ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOAmt_before_GST", model.COO_Amt_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOPercentage", model.COOGSTPercentage ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOGST", model.COO_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@COOTotal", model.COO_Total ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@EIAVendor", model.EIA_Vendor ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAInvNo", model.EIA_Invoice_No ?? (object)DBNull.Value);

            com.Parameters.AddWithValue("@EIAParticulars", model.EIA_Particulars ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAAmt_before_GST", model.EIA_Amt_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAPercentage", model.EIAGSTPercentage ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAGST", model.EIA_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIATotal", model.EIA_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Weight", model.Weight ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Quntity_SQM", model.Quntity_SQM ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Type", model.Type ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@No_of_FCL", model.No_of_FCL ?? (object)DBNull.Value);
            //  com.Parameters.AddWithValue("@InvDate", ParseDate(model.InvDate) ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@InvDate",
     DateTime.TryParse(model.InvDate, out tempDate) ? (object)tempDate : DBNull.Value);

            //       com.Parameters.AddWithValue("@Shipment_Date",
            //DateTime.TryParse(model.Shipping_Bill_Date, out tempDate) ? (object)tempDate : DBNull.Value);
            com.Parameters.AddWithValue("@Shipment_Date",
      DateTime.TryParse(model.Shipping_Bill_Date, out tempDate)
          ? tempDate
          : new DateTime(1753, 1, 1));
            com.Parameters.AddWithValue("@CCInvDate", GetDbDate(model.CCInvDate));
            com.Parameters.AddWithValue("@FCInvDate", GetDbDate(model.FCInvDate));
            com.Parameters.AddWithValue("@CFSInvDate", GetDbDate(model.CFS_InvDate));
            com.Parameters.AddWithValue("@TCInvDate", GetDbDate(model.TC_InvDate));
            com.Parameters.AddWithValue("@COOInvDate", GetDbDate(model.COO_InvDate));
            com.Parameters.AddWithValue("@EIAInvDate", GetDbDate(model.EIA_InvDate));
            com.Parameters.AddWithValue("@DocDate", GetDbDate(model.Doc_Date));
            com.Parameters.AddWithValue("@DocSubmittedAccountOn", GetDbDate(model.Doc_Submitted_Account_On));

            // com.Parameters.AddWithValue("@EIAInvDate", ParseDate(model.EIA_InvDate) ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@DocForwardedNo", model.Doc_ForwardedNo ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@DocSentThrough", model.Doc_Sent_Through ?? (object)DBNull.Value);
            //DateTime parsedDate;
            //if (DateTime.TryParse(model.Doc_Date, out parsedDate))
            //{
            //    com.Parameters.AddWithValue("@DocDate", parsedDate);
            //}
            //else
            //{
            //    com.Parameters.AddWithValue("@DocDate", DBNull.Value);
            //}
            //com.Parameters.AddWithValue("@DocSubmittedAccountOn", model.Doc_Submitted_Account_On ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
            if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
            {
                employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
            }
            com.Parameters.AddWithValue("@Login_ID", employeeRowId);
            com.Parameters.AddWithValue("@FinalSave", true);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AvgCostPerFCL", model.AvgCostPerFCL ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@AvgCostPerSQM", model.AvgCostPerSQM ?? (object)DBNull.Value);

            if (model.Due_Date != null)
            {
                com.Parameters.AddWithValue("@Due_Date", GetDbDate(model.Due_Date));
            }

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
       
        //public DataSet GetExportGridListDS(string SearchType = " ")
        //{
        //                  DataSet dsCombined = new DataSet();
        //                  SQLHelper ObjSQLHelper = new SQLHelper();

        //                  try
        //                  {
        //                      // Master Data
        //                      SqlCommand cmdMaster = new SqlCommand();
        //                      cmdMaster.Parameters.AddWithValue("@Mode", "GET");
        //                      DataSet dsMaster = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdMaster);

        //                      // Other Charges
        //                      SqlCommand cmdOther = new SqlCommand();
        //                      cmdOther.Parameters.AddWithValue("@Mode", "OtherGet");
        //                      DataSet dsOther = ObjSQLHelper.SelectProcDataDS("Export_InsertInvoiceDetails", cmdOther);


        //                      // Add each table into one DataSet (like Excel method does)
        //                      if (dsMaster != null && dsMaster.Tables.Count > 0)
        //                          dsMaster.Tables[0].TableName = "Master";

        //                                  if (dsOther != null && dsOther.Tables.Count > 0)
        //                                  {
        //                                      DataTable dtOther = dsOther.Tables[0].Clone(); // Clone structure

        //                                      DataTable dtMaster = dsMaster.Tables[0];

        //                                      // Filter rows where ETMasterID exists in dsMaster.RowId
        //                                      var masterIds = dtMaster.AsEnumerable().Select(r => r.Field<int>("RowId")).ToList();

        //                                      foreach (DataRow r in dsOther.Tables[0].Rows)
        //                                      {
        //                                          int etMasterId = r["ETMasterID"] != DBNull.Value ? Convert.ToInt32(r["ETMasterID"]) : 0;
        //                                          if (masterIds.Contains(etMasterId))
        //                                          {
        //                                              dtOther.ImportRow(r); // Add only matching rows
        //                                          }
        //                                      }

        //                                      dtOther.TableName = "OtherCharges";

        //                                      // Now you can add filtered dtOther to dsCombined
        //                                      dsCombined.Tables.Add(dtOther);
        //                                  }
        //                          if (dsMaster != null && dsMaster.Tables.Count > 0)
        //                          dsCombined.Tables.Add(dsMaster.Tables[0].Copy());





        //          ObjSQLHelper.ClearObjects();
        //                  }
        //                  catch (Exception ex)
        //                  {
        //                      throw new Exception("Error fetching export grid list dataset: " + ex.Message, ex);
        //                  }

        //                  return dsCombined;
        //}

        private object GetDbDate(string dateString)
        {
            if (!string.IsNullOrWhiteSpace(dateString) && DateTime.TryParse(dateString, out DateTime dt))
            {
                return dt;   // ✅ returns actual DateTime if valid
            }
            return DBNull.Value;  // ❌ if no value or invalid → returns DBNull.Value
        }
        public bool GetDataInsertEIACData(Exporttracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("Export_InsertInvoiceDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Mode", "Export Inspection Agency Charges");
            com.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAVendor", model.EIA_Vendor ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAInvNo", model.EIA_Invoice_No ?? (object)DBNull.Value);
            // com.Parameters.AddWithValue("@EIAInvDate", model.EIA_InvDate ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAInvDate", GetDbDate(model.EIA_InvDate));
            //DateTime tempDate;
            //com.Parameters.AddWithValue("@EIAInvDate",
            //DateTime.TryParse(model.EIA_InvDate, out tempDate) ? (object)tempDate : DBNull.Value);
            com.Parameters.AddWithValue("@EIAParticulars", model.EIA_Particulars ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAAmt_before_GST", model.EIA_Amt_Before_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAPercentage", model.EIAGSTPercentage ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIAGST", model.EIA_GST ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@EIATotal", model.EIA_Total ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalTotal", model.FinalTotal ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalAmtBeforeGST", model.FinalAmtbeforegst ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FinalGSTAmount", model.Finalgstamount ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
            int employeeRowId = 0;
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
    
        public bool DeleteOtherChargeByRowId(Exporttracker model)
        {
            bool isDeleted = false;
            connection();

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                foreach (var charge in model.OtherCharges)
                {
                    int? ocRowId = charge.OC_RowId;

                    // If OC_RowId is not given, fetch from DB using other fields
                    if (ocRowId == null)
                    {
                        using (SqlCommand findCmd = new SqlCommand(@"
                    SELECT TOP 1 RowId 
                    FROM ExportTracker_Others_Charge 
                    WHERE 
                        OCVendor = @OCVendor AND
                        OCInvNo = @OCInvNo 
                      
                ", con))
                        {
                            findCmd.Parameters.AddWithValue("@OCVendor", charge.OC_Vendor ?? "");
                            findCmd.Parameters.AddWithValue("@OCInvNo", charge.OC_Invoice_No ?? "");
                            /*findCmd.Parameters.AddWithValue("@OCInvDate", string.IsNullOrEmpty(charge.OC_InvDate) ? DBNull.Value : (object)DateTime.ParseExact(charge.OC_InvDate, "dd/MM/yy", null));
                            findCmd.Parameters.AddWithValue("@OCParticulars", charge.OC_Particulars ?? "");
*/
                            object result = findCmd.ExecuteScalar();
                            if (result != null && int.TryParse(result.ToString(), out int fetchedId))
                            {
                                ocRowId = fetchedId;
                            }
                            else
                            {
                                // Skip this record if no match found
                                continue;
                            }
                        }
                    }

                    // Now perform the delete if we have a valid OC_RowId
                    if (ocRowId != null)
                    {
                        using (SqlCommand deleteCmd = new SqlCommand("Export_InsertInvoiceDetails", con))
                        {
                            deleteCmd.CommandType = CommandType.StoredProcedure;
                            deleteCmd.Parameters.AddWithValue("@Mode", "Other Charges Delete");
                            deleteCmd.Parameters.AddWithValue("@OC_RowId", ocRowId);

                            int rowsAffected = deleteCmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                                isDeleted = true;
                        }
                    }
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return isDeleted;
        }
        public bool DeletecfsOtherChargeByRowId(Exporttracker model)
        {
            bool isDeleted = false;
            connection();
            int? masterId = null;
            using (SqlCommand cmdGetMasterId = new SqlCommand("SELECT RowId FROM ExportTracker WHERE InvNo = @InvNo", con))
            {
                cmdGetMasterId.Parameters.AddWithValue("@InvNo", model.Invoice_No ?? (object)DBNull.Value);

                if (con.State != ConnectionState.Open)
                    con.Open();

                var masterIdResult = cmdGetMasterId.ExecuteScalar();
                if (masterIdResult != null && masterIdResult != DBNull.Value)
                {
                    masterId = Convert.ToInt32(masterIdResult);
                }
                else
                {
                    // Handle missing Invoice_No case
                    masterId = 0; // or throw exception or handle error
                }
            }
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                foreach (var charge in model.CFSOtherCharges)
                {
                    int? cfsrowid = charge.Cfs_RowId;

                    // If OC_RowId is not given, fetch from DB using other fields
                    if (cfsrowid == null)
                    {
                        using (SqlCommand findCmd = new SqlCommand(@"
                    SELECT TOP 1 RowId 
                    FROM ExportCFSChargeDetails 
                    WHERE 
                        Nature_of_Service = @Nature_of_Service AND
                    MasterId = @MasterId
                      
                ", con))
                        {
                            findCmd.Parameters.AddWithValue("@Nature_of_Service", charge.Nature_of_Service ?? "");
                            findCmd.Parameters.AddWithValue("@MasterId", masterId ?? 0);
                            /*findCmd.Parameters.AddWithValue("@OCInvDate", string.IsNullOrEmpty(charge.OC_InvDate) ? DBNull.Value : (object)DateTime.ParseExact(charge.OC_InvDate, "dd/MM/yy", null));
                            findCmd.Parameters.AddWithValue("@OCParticulars", charge.OC_Particulars ?? "");
*/
                            object result = findCmd.ExecuteScalar();
                            if (result != null && int.TryParse(result.ToString(), out int fetchedId))
                            {
                                cfsrowid = fetchedId;
                            }
                            else
                            {
                                // Skip this record if no match found
                                continue;
                            }
                        }
                    }

                    // Now perform the delete if we have a valid OC_RowId
                    if (cfsrowid != null)
                    {
                        using (SqlCommand deleteCmd = new SqlCommand("Export_InsertInvoiceDetails", con))
                        {
                            deleteCmd.CommandType = CommandType.StoredProcedure;
                            deleteCmd.Parameters.AddWithValue("@Mode", "CFS Charges Delete");
                            deleteCmd.Parameters.AddWithValue("@CFSRowID", cfsrowid);

                            int rowsAffected = deleteCmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                                isDeleted = true;
                        }
                    }
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return isDeleted;
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
    }
}