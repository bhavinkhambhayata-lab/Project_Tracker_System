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
        public DataTable GetUserMenuItems(int employeeId)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"
        SELECT MenuName, Link, Is_Under_Development,ParentId,Display_Order,  MenuId
        FROM MenuNavigation
        WHERE IsActive = 1
          AND (
              EmployeeId IS NULL 
              OR ',' + EmployeeId + ',' LIKE '%,' + CAST(@EmpId AS VARCHAR) + ',%'
          )
        ORDER BY  ISNULL(ParentId, 0), Display_Order";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EmpId", employeeId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
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
        public DataSet UserImpge(TrackerSystemModel objTS)
        {
            cmd = new SqlCommand();
            ObjSQLHelper = new SQLHelper();
            ds = new DataSet();
            try
            {
                cmd.Parameters.AddWithValue("@Action", "GetUserImpge");
                cmd.Parameters.AddWithValue("@UserName", objTS.UserName);
                cmd.Parameters.AddWithValue("@Password", objTS.Password);
                ds = ObjSQLHelper.SelectProcDataDS("GetAllList", cmd);
                ObjSQLHelper.ClearObjects();
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
        public DataSet UserImpgePath(TrackerSystemModel objTS)
        {
            cmd = new SqlCommand();
            ObjSQLHelper = new SQLHelper();
            ds = new DataSet();
            try
            {
                cmd.Parameters.AddWithValue("@Action", "UserImpgePath");
                cmd.Parameters.AddWithValue("@EmployeeRowId", objTS.EmployeeRowId);
                ds = ObjSQLHelper.SelectProcDataDS("GetAllList", cmd);
                ObjSQLHelper.ClearObjects();
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
        public DataSet UserFristLastName(TrackerSystemModel objTS)
        {
            cmd = new SqlCommand();
            ObjSQLHelper = new SQLHelper();
            ds = new DataSet();
            try
            {
                cmd.Parameters.AddWithValue("@Action", "GetUserFristLastName");
                cmd.Parameters.AddWithValue("@EmployeeRowId", objTS.EmployeeRowId);
                ds = ObjSQLHelper.SelectProcDataDS("GetAllList", cmd);
                ObjSQLHelper.ClearObjects();
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
    }
}