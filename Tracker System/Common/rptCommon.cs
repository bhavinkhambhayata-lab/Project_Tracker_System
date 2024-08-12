using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Mvc;


public class rptCommon
{

    #region Report Common Function

    public static decimal FindOpBal(string AccId, string FromDate, string TenantId, string FyFrom, string FyTo)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper ObjSQLHelper = new SQLHelper();
        DataSet ds = new DataSet();
        decimal LedOpBal = 0;
        //LedOpBalance = FindAccountOpBal(AccId, TenantId);

        try
        {
            cmd.Parameters.AddWithValue("@Action", "FindVoucherOpBal");
            cmd.Parameters.AddWithValue("@TenantId", !string.IsNullOrEmpty(TenantId) ? TenantId : null);
            cmd.Parameters.AddWithValue("@AccId", !string.IsNullOrEmpty(AccId) ? AccId : null);
            cmd.Parameters.AddWithValue("@VDate", Common.Get_SQLDate_From_DateString(FromDate));
            cmd.Parameters.AddWithValue("@FyFrom", Common.Get_SQLDate_From_DateString(FyFrom));
            cmd.Parameters.AddWithValue("@FyTo", Common.Get_SQLDate_From_DateString(FyTo));
            LedOpBal = ObjSQLHelper.ExecuteScalarReturnDecimal("rptCommon", cmd);
            return LedOpBal;
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

    public static decimal FindClBal(string AccId, string ToDate, string TenantId, string FyFrom, string FyTo)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper ObjSQLHelper = new SQLHelper();
        DataSet ds = new DataSet();
        decimal LedOpBal = 0;
        //LedClBalance = FindAccountOpBal(AccId, TenantId);
        try
        {
            cmd.Parameters.AddWithValue("@Action", "FindVoucherClBal");
            cmd.Parameters.AddWithValue("@TenantId", !string.IsNullOrEmpty(TenantId) ? TenantId : null);
            cmd.Parameters.AddWithValue("@AccId", !string.IsNullOrEmpty(AccId) ? AccId : null);
            cmd.Parameters.AddWithValue("@VDate", Common.Get_SQLDate_From_DateString(ToDate));
            cmd.Parameters.AddWithValue("@FyFrom", Common.Get_SQLDate_From_DateString(FyFrom));
            cmd.Parameters.AddWithValue("@FyTo", Common.Get_SQLDate_From_DateString(FyTo));
            LedOpBal = ObjSQLHelper.ExecuteScalarReturnDecimal("rptCommon", cmd);
            return LedOpBal;
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

    public static decimal FindClBal_ChqDate(string AccId, string ToDate, string TenantId, string FyFrom, string FyTo)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper ObjSQLHelper = new SQLHelper();
        DataSet ds = new DataSet();
        decimal LedOpBal = 0;
        try
        {
            cmd.Parameters.AddWithValue("@Action", "FindVoucherClBal_ChqDate");
            cmd.Parameters.AddWithValue("@TenantId", TenantId);
            cmd.Parameters.AddWithValue("@AccId", AccId);
            cmd.Parameters.AddWithValue("@VDate", Common.Get_SQLDate_From_DateString(ToDate));
            cmd.Parameters.AddWithValue("@FyFrom", Common.Get_SQLDate_From_DateString(FyFrom));
            cmd.Parameters.AddWithValue("@FyTo", Common.Get_SQLDate_From_DateString(FyTo));
            LedOpBal = ObjSQLHelper.ExecuteScalarReturnDecimal("rptCommon", cmd);
            return LedOpBal;
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

    public static decimal FindOpBal_ByCurrencyId(string AccId, string CurrencyId,string FromDate, string TenantId, string FyFrom, string FyTo)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper ObjSQLHelper = new SQLHelper();        
        decimal LedOpBal = 0;       
        try
        {
            cmd.Parameters.AddWithValue("@Action", "FindVoucherOpBal_ByCurrencyId");
            cmd.Parameters.AddWithValue("@TenantId", TenantId);
            cmd.Parameters.AddWithValue("@AccId", AccId);
            cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId);
            cmd.Parameters.AddWithValue("@VDate", Common.Get_SQLDate_From_DateString(FromDate));
            cmd.Parameters.AddWithValue("@FyFrom", Common.Get_SQLDate_From_DateString(FyFrom));
            cmd.Parameters.AddWithValue("@FyTo", Common.Get_SQLDate_From_DateString(FyTo));
            LedOpBal = ObjSQLHelper.ExecuteScalarReturnDecimal("rptCommon", cmd);
            return LedOpBal;
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

    public static decimal FindClBal_ByCurrencyId(string AccId, string CurrencyId, string ToDate, string TenantId, string FyFrom, string FyTo)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper ObjSQLHelper = new SQLHelper();        
        decimal LedOpBal = 0;        
        try
        {
            cmd.Parameters.AddWithValue("@Action", "FindVoucherClBal_ByCurrencyId");
            cmd.Parameters.AddWithValue("@TenantId", TenantId);
            cmd.Parameters.AddWithValue("@AccId", AccId);
            cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId);
            cmd.Parameters.AddWithValue("@VDate", Common.Get_SQLDate_From_DateString(ToDate));
            cmd.Parameters.AddWithValue("@FyFrom", Common.Get_SQLDate_From_DateString(FyFrom));
            cmd.Parameters.AddWithValue("@FyTo", Common.Get_SQLDate_From_DateString(FyTo));
            LedOpBal = ObjSQLHelper.ExecuteScalarReturnDecimal("rptCommon", cmd);
            return LedOpBal;
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

    public static decimal FindClBal_ByCurrencyId_ByChqDate(string AccId, string CurrencyId, string ToDate, string TenantId, string FyFrom, string FyTo)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper ObjSQLHelper = new SQLHelper();
        decimal LedOpBal = 0;
        try
        {
            cmd.Parameters.AddWithValue("@Action", "FindVoucherClBal_ByCurrencyId_ByChqDate");
            cmd.Parameters.AddWithValue("@TenantId", TenantId);
            cmd.Parameters.AddWithValue("@AccId", AccId);
            cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId);
            cmd.Parameters.AddWithValue("@VDate", Common.Get_SQLDate_From_DateString(ToDate));
            cmd.Parameters.AddWithValue("@FyFrom", Common.Get_SQLDate_From_DateString(FyFrom));
            cmd.Parameters.AddWithValue("@FyTo", Common.Get_SQLDate_From_DateString(FyTo));
            LedOpBal = ObjSQLHelper.ExecuteScalarReturnDecimal("rptCommon", cmd);
            return LedOpBal;
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

    public static decimal FindCostCenterOpBal(string CCId, string FromDate, string TenantId, string AccountLedgerId, string FyFrom, string FyTo)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper ObjSQLHelper = new SQLHelper();
        DataSet ds = new DataSet();
        decimal LedOpBal = 0;       
        try
        {
            cmd.Parameters.AddWithValue("@Action", "FindCostCenterOpBal");
            cmd.Parameters.AddWithValue("@TenantId", TenantId);
            cmd.Parameters.AddWithValue("@CCId", CCId);
            cmd.Parameters.AddWithValue("@VDate", Common.Get_SQLDate_From_DateString(FromDate));
            if (!string.IsNullOrEmpty(AccountLedgerId))
            {
                cmd.Parameters.AddWithValue("@AccountLedgerId", AccountLedgerId);
            }
            cmd.Parameters.AddWithValue("@FyFrom", Common.Get_SQLDate_From_DateString(FyFrom));
            cmd.Parameters.AddWithValue("@FyTo", Common.Get_SQLDate_From_DateString(FyTo));
            LedOpBal = ObjSQLHelper.ExecuteScalarReturnDecimal("rptCommon", cmd);
            return LedOpBal;
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

    public static decimal FindCostCenterClBal(string CCId, string ToDate, string TenantId, string AccountLedgerId, string FyFrom, string FyTo)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper ObjSQLHelper = new SQLHelper();
        DataSet ds = new DataSet();
        decimal LedOpBal = 0;
     
        try
        {
            cmd.Parameters.AddWithValue("@Action", "FindCostCenterClBal");
            cmd.Parameters.AddWithValue("@TenantId", TenantId);
            cmd.Parameters.AddWithValue("@CCId", CCId);
            cmd.Parameters.AddWithValue("@VDate", Common.Get_SQLDate_From_DateString(ToDate));
            if (!string.IsNullOrEmpty(AccountLedgerId))
            {
                cmd.Parameters.AddWithValue("@AccountLedgerId", AccountLedgerId);
            }
            cmd.Parameters.AddWithValue("@FyFrom", Common.Get_SQLDate_From_DateString(FyFrom));
            cmd.Parameters.AddWithValue("@FyTo", Common.Get_SQLDate_From_DateString(FyTo));
            LedOpBal = ObjSQLHelper.ExecuteScalarReturnDecimal("rptCommon", cmd);
            return LedOpBal;
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



    #endregion
}
