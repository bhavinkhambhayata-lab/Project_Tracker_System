using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;


public class DataBase
{
    public DataSet GetDataSet(string ProcedureName, SortedDictionary<string, object> sd, bool IsMasterCall = false)
    {
        string ConnectionString = string.Empty;
        try
        {
            DataSet ds = new DataSet();

            if (IsMasterCall)
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["ZIPMASTER"].ToString();
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand(ProcedureName, conn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        foreach (KeyValuePair<string, object> pair in sd)
                        {
                            SqlParameter spNew = new SqlParameter();
                            spNew.Value = pair.Value;
                            spNew.ParameterName = pair.Key;
                            da.SelectCommand.Parameters.Add(spNew);
                        }
                        da.Fill(ds, ProcedureName);
                        DataTable dt = ds.Tables["SourceTable_Name"];
                        return ds;
                    }
                }
            }
            else
            {
                //ConnectionString = Global.ConvertDBnullToString(System.Web.HttpContext.Current.Session["ConnectionString"]);
                ConnectionString = Common.GetCSByEnterpriseId(sd["@EnterpriseId"].ToString());
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand(ProcedureName, conn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.CommandTimeout = 600;
                        foreach (KeyValuePair<string, object> pair in sd)
                        {
                            SqlParameter spNew = new SqlParameter();
                            spNew.Value = pair.Value;
                            spNew.ParameterName = pair.Key;
                            //if (pair.Key != "@EnterpriseId")
                                da.SelectCommand.Parameters.Add(spNew);
                        }
                        da.Fill(ds, ProcedureName);
                        DataTable dt = ds.Tables["SourceTable_Name"];
                        return ds;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            DataSet ds = new DataSet();
            DataTable statustable = new DataTable();
            statustable.Columns.Add("ResponseCode", typeof(int));
            statustable.Columns.Add("ResponseMessage", typeof(string));
            statustable.Columns.Add("MessageType", typeof(int));
            statustable.Rows.Add(0, ex.Message, 0);
            ds.Tables.Add(statustable);
            return ds;
        }
    }
}

