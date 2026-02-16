using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;
using System.Globalization;
using Newtonsoft.Json;

public class Common
{
    #region Conversion Methods
    public static int ConvertDBnullToInt32(object obj)
    {
        try
        {
            if (obj == DBNull.Value) { return 0; }
            if (obj == "") { return 0; }
            return Convert.ToInt32(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Int64 ConvertDBnullToInt64(object obj)
    {
        try
        {
            return Convert.ToInt64(obj);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static string ConvertDBnullToString(object obj)
    {
        try
        {
            if (obj == DBNull.Value) { return ""; }
            return Convert.ToString(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string ConvertDBnullToStringNull(object obj)
    {
        try
        {
            if (obj == null)
                return null;

            return Convert.ToString(obj);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static byte[] ConvertObjectToByteArray(object obj)
    {
        try
        {
            return (byte[])obj;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static byte[] ConvertDBnullTobyteNull(object obj)
    {
        try
        {
            return (byte[])obj;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }   
    public static string ConvertDBnullToNullString(object obj)
    {
        try
        {
            if (obj == DBNull.Value) { return null; }
            return Convert.ToString(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static decimal ConvertDBnullToDecimal(object obj)
    {
        try
        {
            if (obj == DBNull.Value) { return 0; }
            else if (obj == "") { return 0; }
            return Convert.ToDecimal(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static decimal ConvertDBnullToDecimal(object obj, int round)
    {
        try
        {
            return Math.Round(Convert.ToDecimal(obj), round);
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public static double ConvertDBnullToDouble(object obj)
    {
        try
        {
            if (obj == DBNull.Value) { return 0; }
            else if (obj == "") { return 0; }
            return Convert.ToDouble(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static bool ConvertDBnullToBool(object obj)
    {
        try
        {
            if (obj == DBNull.Value) { return false; }
            if (string.IsNullOrEmpty(Convert.ToString(obj))) { return false; }
            return Convert.ToBoolean(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static char ConvertDBnullToChar(object obj)
    {
        try
        {
            if (DBNull.Value == obj) { return new System.Char(); }
            return Convert.ToChar(Convert.ToString(obj).Trim());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Date methods
    public static string Get_SQLDate_From_DateString(string myDateString)
    {
        try
        {
            if (myDateString == null || myDateString == "")
            {
                return null;
            }

            string line = myDateString.ToString();
            string[] dates = line.Split('/');
            string returnDate = Convert.ToInt32(dates[2]) + "-" + Convert.ToInt32(dates[1]) + "-" + Convert.ToInt32(dates[0]);

            return returnDate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static DateTime ConvertDBNullToDateTime(object obj)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(obj);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region "General Functions"
    public static string ExecuteScalarByQuery(string Query)
    {
        SQLHelper ObjSQLHelper = new SQLHelper();
        string Result = "";
        try
        {
            Result = ObjSQLHelper.ExecuteScalarByQuery(Query);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ObjSQLHelper.ClearObjects();
        }
        return Result;
    }
    public static DataTable ExecuteDataTableByQuery(string query, string salespersoncode)
    {
        string connectionString = ConvertDBnullToString(ConfigurationManager.ConnectionStrings["ConnectionStrings"]); 
        DataTable resultTable = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            // Add parameter to avoid SQL injection
            command.Parameters.AddWithValue("@SalespersonCode", salespersoncode);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                adapter.Fill(resultTable);
            }
        }
        return resultTable;
    }
    public static DataTable ExecuteDataTableByQueryInvoiceNoGet(string query, string InvoiceNo)
    {
        string connectionString = ConvertDBnullToString(ConfigurationManager.ConnectionStrings["ConnectionStrings"]);
        DataTable resultTable = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                adapter.Fill(resultTable);
            }
        }
        return resultTable;
    }
}

#endregion