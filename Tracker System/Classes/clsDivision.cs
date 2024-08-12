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
    public class clsDivision
    {
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        public List<DivisionModel> GetAllList()
        {
            connection();
            List<DivisionModel> lstDivision = new List<DivisionModel>();

            SqlCommand com = new SqlCommand("GetAllList", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", "GetDivision");
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            // Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                DivisionModel OBjDM = new DivisionModel();
                OBjDM.ID = Convert.ToString(dr["RowId"]);
                OBjDM.Name = Convert.ToString(dr["DivisionName"]);
                lstDivision.Add(OBjDM);
            }

            return lstDivision;
        }
    }


    }
