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
    public string ConnectionString;// = Common.ConvertDBnullToString(HttpContext.Current.Session["ConnectionString"]); //.Length > 0 ?  Common.ConvertDBnullToString(HttpContext.Current.Session["ConnectionString"]) : ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;   
    public static string ConnectionStringFromDirect;
    #endregion


    public SQLHelper()
    {
        if (HttpContext.Current != null && HttpContext.Current.Session != null)
            ConnectionString = Common.ConvertDBnullToString(HttpContext.Current.Session["ConnectionString"]);      
    }

    public SQLHelper(string ConnectionStringLocal)
    {
        ConnectionString = ConnectionStringLocal;
        ConnectionStringFromDirect = ConnectionStringLocal;
        if (System.Web.HttpContext.Current != null)
            System.Web.HttpContext.Current.Items["APIConnectionString"] = ConnectionStringLocal;
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

    public Boolean IUDProcData(string Proc, SqlCommand cmd, Boolean istran = true, int CommandTimeout = 180)
    {
        string strAction = "";
        try
        {
            if (istran == true)
                CreateObjects(istran);

            if (cmd.Parameters.Contains("@Action"))
                strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

            _result = false;
            _sqlcom.CommandText = Proc;
            _sqlcom.CommandType = CommandType.StoredProcedure;
            _sqlcom.CommandTimeout = CommandTimeout;
            _sqlcom.Parameters.Clear();
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                SqlParameter spNew = new SqlParameter();
                spNew.Value = parameter.Value;
                spNew.ParameterName = parameter.ParameterName;
                _sqlcom.Parameters.Add(spNew);
            }
            _sqlcom.ExecuteNonQuery();
            _result = true;
        }
        catch (Exception ex)
        {
           
            RollBackTransaction();
           
            return false;
        }
        return _result;
    }

    public string IUDProcDataWithOutputParameter(string Proc, SqlCommand cmd, bool isNewTran = true, int cmdTimeout = 60)
    {
        string strAction = ""; string ReturnVal = "";
        try
        {
            if (isNewTran == true)
                CreateObjects(isNewTran);

            if (cmd.Parameters.Contains("@Action"))
                strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

            _sqlcom.CommandText = Proc;
            _sqlcom.CommandType = CommandType.StoredProcedure;
            _sqlcom.CommandTimeout = cmdTimeout;
            _sqlcom.Parameters.Clear();
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                SqlParameter spNew = new SqlParameter();
                spNew.Value = parameter.Value;
                spNew.ParameterName = parameter.ParameterName;
                if (parameter.Direction == ParameterDirection.Output)
                {
                    spNew.Direction = parameter.Direction;
                    spNew.Size = parameter.Size;
                    spNew.SqlDbType = parameter.SqlDbType;
                }
                _sqlcom.Parameters.Add(spNew);
            }
            _sqlcom.ExecuteNonQuery();
            ReturnVal = Common.ConvertDBnullToString(_sqlcom.Parameters["@OutputId"].Value);
        }
        catch (Exception ex)
        {
            RollBackTransaction();
            ClearObjects();
           
            return "";
        }
        return ReturnVal;
    }

    public DataSet SelectProcDataDS(string Proc, SqlCommand cmd, int cmdTimeout = 300)
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

    public DataSet SelectProcDataDS(string Proc, SqlCommand cmd, bool isNewTran, int cmdTimeout = 300)
    {
        DataSet _data = null;
        string strAction = "";
        try
        {
            if (isNewTran == true)
                CreateObjects(isNewTran);

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


    public string ExecuteScalarByDataset(string Proc, SqlCommand cmd, int cmdTimeout = 180)
    {
        string str = "";
        string strAction = "";
        try
        {
            CreateObjects(false);

            if (cmd.Parameters.Contains("@Action"))
                strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

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

    public decimal ExecuteScalarReturnDecimal(string strProcName, SqlCommand cmd)
    {
        string strAction = "";
        try
        {
            CreateObjects(false);

            if (cmd.Parameters.Contains("@Action"))
                strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

            _sqlcom.CommandText = strProcName;
            _sqlcom.CommandType = CommandType.StoredProcedure;
            _sqlcom.Parameters.Clear();
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                SqlParameter spNew = new SqlParameter();
                spNew.Value = parameter.Value;
                spNew.ParameterName = parameter.ParameterName;
                _sqlcom.Parameters.Add(spNew);
            }
            object tmpObj = null;
            tmpObj = _sqlcom.ExecuteScalar();
            if (!string.IsNullOrEmpty(Convert.ToString(tmpObj)))
            {
                return Convert.ToDecimal(tmpObj);
            }
            else
            {
                return 0;
            }
        }
        catch (InvalidCastException ex)
        {
            RollBackTransaction();
            ClearObjects();
           
            return 0;
        }
        catch (Exception ex)
        {
            RollBackTransaction();
            ClearObjects();
        
            return 0;
        }
    }

    public bool ExecuteScalar(string strProcName, SqlCommand cmd)
    {
        string strAction = "";
        try
        {
            CreateObjects(false);

            if (cmd.Parameters.Contains("@Action"))
                strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

            _sqlcom.CommandText = strProcName;
            _sqlcom.CommandType = CommandType.StoredProcedure;
            _sqlcom.Parameters.Clear();
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                SqlParameter spNew = new SqlParameter();
                spNew.Value = parameter.Value;
                spNew.ParameterName = parameter.ParameterName;
                _sqlcom.Parameters.Add(spNew);
            }
            object tmpObj = null;
            tmpObj = _sqlcom.ExecuteScalar();

            if (!string.IsNullOrEmpty(Convert.ToString(tmpObj)))
            {
                return Convert.ToBoolean(tmpObj);
            }
            else
            {
                return false;
            }
        }
        catch (InvalidCastException ex)
        {
            RollBackTransaction();
            ClearObjects();
           
            return false;
        }
        catch (Exception ex)
        {
            RollBackTransaction();
            ClearObjects();
          
            return false;
        }
    }

    public int ExecuteScalarReturnInteger(string strProcName, SqlCommand cmd)
    {
        string strAction = "";
        try
        {
            CreateObjects(false);

            if (cmd.Parameters.Contains("@Action"))
                strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

            _sqlcom.CommandText = strProcName;
            _sqlcom.CommandType = CommandType.StoredProcedure;
            _sqlcom.Parameters.Clear();
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                SqlParameter spNew = new SqlParameter();
                spNew.Value = parameter.Value;
                spNew.ParameterName = parameter.ParameterName;
                _sqlcom.Parameters.Add(spNew);
            }
            object tmpObj = null;
            tmpObj = _sqlcom.ExecuteScalar();

            if (!string.IsNullOrEmpty(Convert.ToString(tmpObj)))
            {
                return Convert.ToInt32(tmpObj);
            }
            else
            {
                return 0;
            }
        }
        catch (InvalidCastException ex)
        {
            RollBackTransaction();
            ClearObjects();
          
            return 0;
        }
        catch (Exception ex)
        {
            RollBackTransaction();
            ClearObjects();
          
            return 0;
        }
    }
    public int ExecuteScalarReturnInteger(string strProcName, SqlCommand cmd, bool isNewTran = false)
    {
        string strAction = "";
        try
        {

            if (isNewTran == true)
                CreateObjects(isNewTran);

            //CreateObjects(false);

            if (cmd.Parameters.Contains("@Action"))
                strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

            _sqlcom.CommandText = strProcName;
            _sqlcom.CommandType = CommandType.StoredProcedure;
            _sqlcom.Parameters.Clear();
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                SqlParameter spNew = new SqlParameter();
                spNew.Value = parameter.Value;
                spNew.ParameterName = parameter.ParameterName;
                _sqlcom.Parameters.Add(spNew);
            }
            object tmpObj = null;
            tmpObj = _sqlcom.ExecuteScalar();

            if (!string.IsNullOrEmpty(Convert.ToString(tmpObj)))
            {
                return Convert.ToInt32(tmpObj);
            }
            else
            {
                return 0;
            }
        }
        catch (InvalidCastException ex)
        {
            RollBackTransaction();
            ClearObjects();
         
            return 0;
        }
        catch (Exception ex)
        {
            RollBackTransaction();
            ClearObjects();
           
            return 0;
        }
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


    public DataSet ExecuteQuery(string Query)
    {
        DataSet _data = null;
        try
        {
            CreateObjects(false);
            _sqlcom.CommandText = Query;
            _sqlcom.CommandType = CommandType.Text;
            _sqlcom.Parameters.Clear();
            SqlDataAdapter _adapter = new SqlDataAdapter(_sqlcom);
            DataSet ds = new DataSet("SQLHelper");
            _adapter.Fill(ds);
            _data = ds;
        }
        catch (Exception ex)
        {
            RollBackTransaction();
            ClearObjects();
         
            return new DataSet();
        }
        return _data;
    }

}
