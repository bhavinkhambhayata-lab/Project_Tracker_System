using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Tracker_System.Classes;
public class SQLHelper
{
    #region Class Variables

    private SqlTransaction _sqltrn = null;
    private SqlConnection _sqlcon = null;
    private SqlCommand _sqlcom = null;
    private Boolean _result = false;
    private String typevalue = String.Empty;
    private Dictionary<String, SqlDbType> _output = null;
    private Dictionary<String, String> _paranames = null;
    public string ConnectionString;
    public static string ConnectionStringFromDirect;
    #endregion
    public SQLHelper()
    {
        if (HttpContext.Current != null && HttpContext.Current.Session != null)
            ConnectionString = "Data Source=ITALIA-DATA\\ICLDATASERVER;Initial Catalog=HRMS;User ID=internal;Password=ch@ngeson0419; Timeout=700000";
      //  ConnectionString = "Data Source=ITALIA-DATA\\192.168.1.100,1100;Initial Catalog=HRMS;User ID=internal;Password=ch@ngeson0419; Timeout=700000";
    }
    public void CreateObjects(Boolean istransaction)
    {
        _sqlcon = new SqlConnection(ConnectionString);
        _sqlcon.Open();
        if (istransaction)
            _sqltrn = _sqlcon.BeginTransaction(IsolationLevel.Serializable);
        _sqlcom = new SqlCommand();
        _sqlcom.Connection = _sqlcon;
        if (istransaction)
            _sqlcom.Transaction = _sqltrn;
    }
    public void CommitTransaction()
    {
        try
        {
            if (_sqltrn != null)
                _sqltrn.Commit();
        }
        catch (Exception ex)
        {
           
            throw ex;
        }
    }
    public void RollBackTransaction()
    {
        try
        {
            if (_sqltrn != null)
                _sqltrn.Rollback();
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }
    public void ClearObjects()
    {
        if (_sqlcom != null)
        {
            if (_sqlcom.Parameters.Count != 0)
                _sqlcom.Parameters.Clear();
            _sqlcom.Cancel();
            _sqlcom.Dispose();
            _sqlcom = null;
        }
        if (_sqltrn != null)
        {
            _sqltrn.Dispose();
            _sqltrn = null;
        }
        if (_sqlcon != null)
        {
            if (_sqlcon.State == ConnectionState.Open)
                _sqlcon.Close();
            _sqlcon.Dispose();
            _sqlcon = null;
        }

        if (_output != null)
            _output = null;
        if (_paranames != null)
            _paranames = null;
    }
    public DataSet SelectProcDataDS(string Proc, SqlCommand cmd, int cmdTimeout = 700)
    {
        DataSet _data = null;
        string strAction = "";
        try
        {
            CreateObjects(false);
            if (cmd.Parameters.Contains("@Action"))
                strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

            _data = null;
            _sqlcom.CommandText = Proc;
            _sqlcom.CommandType = CommandType.StoredProcedure;
            _sqlcom.CommandTimeout = cmdTimeout;
            _sqlcom.Parameters.Clear();
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                SqlParameter spNew = new SqlParameter();
                spNew.Value = parameter.Value;
                spNew.ParameterName = parameter.ParameterName;

                _sqlcom.Parameters.Add(spNew);
            }
            SqlDataAdapter _adapter = new SqlDataAdapter(_sqlcom);
            DataSet dataset = new DataSet("SQLHelper");
            _adapter.Fill(dataset);
            _data = dataset;
        }
        catch (Exception ex)
        {
            RollBackTransaction();
            ClearObjects();
            
            return new DataSet();
        }
        return _data;
    }
    public string ExecuteScalarByQuery(string Query)
    {
        string str = "";
        try
        {
            CreateObjects(false);
            _sqlcom.CommandText = Query;
            _sqlcom.CommandType = CommandType.Text;
            _sqlcom.Parameters.Clear();
            SqlDataAdapter _adapter = new SqlDataAdapter(_sqlcom);
            DataSet ds = new DataSet("SQLHelper");
            _adapter.Fill(ds);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            str += Convert.ToString(dt.Rows[i][0]);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            RollBackTransaction();
            ClearObjects();
          
            return str;
        }
        return str;
    }
}
