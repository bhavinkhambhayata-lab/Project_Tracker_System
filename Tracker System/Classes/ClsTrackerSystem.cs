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
    public class ClsTrackerSystem
    {
        #region Variable Declaration

        private SQLHelper ObjSQLHelper;
        private SqlCommand cmd;
        private DataSet ds;

        #endregion
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        
        public List<CompanyModel> GetAllList()
        {
            connection();
            List<CompanyModel> lstCompany = new List<CompanyModel>();

            SqlCommand com = new SqlCommand("GetAllList", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "GetCompany");
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            // Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                CompanyModel OBjCM = new CompanyModel();
                OBjCM.ID = Convert.ToString(dr["RowID"]);
                OBjCM.Name = Convert.ToString(dr["Name"]);
                lstCompany.Add(OBjCM);
            }

            return lstCompany;
        }
        public List<ImprestDetailsModel> GetAllListNo()
        {
            connection();
            List<ImprestDetailsModel> ImprestDetailsList = new List<ImprestDetailsModel>();
            SqlCommand com = new SqlCommand("GetAllList", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "ImprestDetailsNo");
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            // Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                ImprestDetailsModel OBjCM = new ImprestDetailsModel();
                OBjCM.No = Convert.ToString(dr["No"]);
                ImprestDetailsList.Add(OBjCM);
            }
            return ImprestDetailsList;
        }
        public DataSet GetAllListEmployee(string EmployeeName)
        {
            ds = new DataSet();
            cmd = new SqlCommand();
            ObjSQLHelper = new SQLHelper();
            try
            {
                cmd.Parameters.AddWithValue("@EmpName", EmployeeName);             
                ds = ObjSQLHelper.SelectProcDataDS("TS_SearchEmpList", cmd);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjSQLHelper.ClearObjects();
            }           
        }
    }
}