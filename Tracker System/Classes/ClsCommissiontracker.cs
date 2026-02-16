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
    public class ClsCommissiontracker
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
      public int employeeRowId = 0;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        public List<Commissiontracker> GetAllFyear()
        {
            connection();
            List<Commissiontracker> lstFyear = new List<Commissiontracker>();

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
                Commissiontracker ObjFT = new Commissiontracker();
                ObjFT.FyearId = Convert.ToString(dr["SYear"]);
                ObjFT.EYear = Convert.ToString(dr["FYear"]);
                lstFyear.Add(ObjFT);
            }

            return lstFyear;
        }

  
      // this for update commission list 

        public bool RecordExists(string FromNo, string VendorNo)
        {
            connection();
            SqlCommand com = new SqlCommand("CTCommissionTrackerData", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "CheckRecord");
            com.Parameters.AddWithValue("@VenderCode", FromNo);
            com.Parameters.AddWithValue("@FromNo", VendorNo);

            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            bool exists = reader.HasRows; // checks if any rows were returned
            reader.Close();
            con.Close();

            return exists;
        }

        // for Delete Recored 
        public bool GetDataDetailsDeleteRecoreds(string PI_No, string OCF_No, string Invoice_No)
        {
            connection();
            SqlCommand com = new SqlCommand("CTCommissionTrackerDetails_For_Update_Data", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "DeleteRecord");
            com.Parameters.AddWithValue("@InvoiceNo", Invoice_No);
            com.Parameters.AddWithValue("@OCF_No", OCF_No);

            com.Parameters.AddWithValue("@PINo", PI_No);
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
        // for save the data in details 
        public string GetDataUpadateDetails(string OCF_No, string Invoice_No, string PI_No,String ActualCommission, string Claimform_Received, string CommissionInvoice_Received, string CommissionworkingsenttoVendor_Employee, string Credit_Advice_Prepared, string Credit_Advice_Details, string Credit_Note_Prepared, string Commission_Vendor_Name, string Vendor_Code, string FromNo,string CreditNotePreparedNo)
        {
            connection();
            SqlCommand com = new SqlCommand("CTCommissionTrackerDetails_For_Update_Data", con);
            com.CommandType = CommandType.StoredProcedure;

            // Set the correct action
            com.Parameters.AddWithValue("@Action", "SaveRecored");

            // Required parameters
            com.Parameters.AddWithValue("@InvoiceNo", Invoice_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@OCF_No", OCF_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@PINo", PI_No ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Claim_from_recived", Claimform_Received ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@ActualCommission", ActualCommission ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Commissionworkingtovendor", CommissionworkingsenttoVendor_Employee ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Commission_Invoice_Received", CommissionInvoice_Received);
            com.Parameters.AddWithValue("@Credit_Advice_Prepared", Credit_Advice_Prepared ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Credit_Advice_Details", Credit_Advice_Details ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Credit_Note_Prepared", Credit_Note_Prepared ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@VenderName", Commission_Vendor_Name ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@VenderCode", Vendor_Code ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@FromNo", FromNo ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@CreditNotePreparedNo", CreditNotePreparedNo ?? (object)DBNull.Value);
            com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
    
            if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
            {
                employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
            }
            com.Parameters.AddWithValue("@Login_ID", employeeRowId);
            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            string result = "Failed";

            if (reader.Read())
            {
                result = reader["Result"].ToString();  // Assume your stored proc returns this
            }

            con.Close();
            return result;
        }

        // for PreMarketing u pdate and insert value 
        public bool GetDataUpadatePreMarketing(Commissiontracker model)
             {
                    connection();
                    SqlCommand com = new SqlCommand("CTCommissionTrackerData", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Action", "PreMarketing");
                    if (string.IsNullOrEmpty(model.OCF_No) || string.IsNullOrEmpty(model.Invoice_No))
                    {
                        com.Parameters.AddWithValue("@InvoiceNo", model.Invoice_No);
                        com.Parameters.AddWithValue("@OCF_No", model.OCF_No);
                    }
                    com.Parameters.AddWithValue("@PINo", model.PI_No);
                    com.Parameters.AddWithValue("@Claim_from_recived", model.Claimform_Received);
                    com.Parameters.AddWithValue("@Remarks", model.Remarks);
                    com.Parameters.AddWithValue("@VenderName", model.Commission_Vendor_Name);
                    com.Parameters.AddWithValue("@VenderCode", model.Vendor_Code);
                    com.Parameters.AddWithValue("@FromNo", model.FromNo);
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
        // for commericial update and insert value 
        public bool GetDataUpadateCommercial(Commissiontracker model)
        {
            connection();
            SqlCommand com = new SqlCommand("CTCommissionTrackerData", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "Commercial");
           
                com.Parameters.AddWithValue("@InvoiceNo", model.Invoice_No);
                com.Parameters.AddWithValue("@OCF_No", model.OCF_No);
          
            com.Parameters.AddWithValue("@PINo", model.PI_No);
            com.Parameters.AddWithValue("@Claim_from_recived", model.Claimform_Received);
            com.Parameters.AddWithValue("@Commissionworkingtovendor", model.CommissionworkingsenttoVendor_Employee);
            com.Parameters.AddWithValue("@Remarks", model.Remarks);
            com.Parameters.AddWithValue("@ActualCommission", model.ActualCommission);
            com.Parameters.AddWithValue("@VenderName", model.Commission_Vendor_Name);
            com.Parameters.AddWithValue("@VenderCode", model.Vendor_Code);
            com.Parameters.AddWithValue("@FromNo", model.FromNo);
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
        //update method for post marketing get two value 
        public bool GetDataUpadatePostMarketing(string CommissionInvoiceReceived, string ClaimFormReceived, string FromNo, string VendorNo)
        {
            connection();
            SqlCommand com = new SqlCommand("CTCommissionTrackerData", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "UpadatePostMarketing");
            com.Parameters.AddWithValue("@Commission_Invoice_Received", CommissionInvoiceReceived);
            com.Parameters.AddWithValue("@Claim_from_recived", ClaimFormReceived);
            com.Parameters.AddWithValue("@VenderCode", VendorNo);
            com.Parameters.AddWithValue("@FromNo", FromNo);
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
        //update method for post marketing get only one value 
        //public bool GetDataUpadatePostMar(string ClaimFormReceived, string FromNo, string VendorNo)
        //{
        //    connection();
        //    SqlCommand com = new SqlCommand("CTCommissionTrackerData", con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@Action", "UpadatePostMarketing");
        //    com.Parameters.AddWithValue("@Claim_from_recived", ClaimFormReceived);
       
        //    com.Parameters.AddWithValue("@VenderCode", VendorNo);
        //    com.Parameters.AddWithValue("@FromNo", FromNo);
        //    com.Parameters.AddWithValue("@Login_DateTime", DateTime.Now);
        //    if (System.Web.HttpContext.Current.Session["EmplRowId"] != null)
        //    {
        //        employeeRowId = Convert.ToInt32(System.Web.HttpContext.Current.Session["EmplRowId"]);
        //    }
        //    com.Parameters.AddWithValue("@Login_ID", employeeRowId);
        //    con.Open();
        //    int i = com.ExecuteNonQuery();
        //    con.Close();
        //    if (i >= 1)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        // method for logistics recored update 
        public bool GetDataUpadateLogistic(string CreditAdviceDetails, string CreditAdvicePrepared, string FromNo, string VendorNo)
        {
            connection();
            SqlCommand com = new SqlCommand("CTCommissionTrackerData", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "Upadatelogistic");
            com.Parameters.AddWithValue("@Credit_Advice_Details", CreditAdviceDetails);
            com.Parameters.AddWithValue("@Credit_Advice_Prepared", CreditAdvicePrepared);
            com.Parameters.AddWithValue("@VenderCode", VendorNo);

            com.Parameters.AddWithValue("@FromNo", FromNo);

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
        public bool GetDataUpadateAccount(string CreditNotePrepared,string FromNo, string VendorNo, string CreditNotePreparedNo)
        {
            connection();
            SqlCommand com = new SqlCommand("CTCommissionTrackerData", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "UpdateAccount");
            com.Parameters.AddWithValue("@Credit_Note_Prepared", CreditNotePrepared);
            com.Parameters.AddWithValue("@VenderCode", VendorNo);
            com.Parameters.AddWithValue("@CreditNotePreparedNo", CreditNotePreparedNo);

            com.Parameters.AddWithValue("@FromNo", FromNo);
          
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
    }
}