using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public class resourceSettings
{
    #region "Resource Functions"

    public static string getResourceLable(string strLableId, string strResourceFileName, string DefaultLabelId = "")
    {
        SQLHelperMaster ObjSQLHelperMaster = new SQLHelperMaster();
        try
        { 
            int LanguageId;
            if (HttpContext.Current.Session == null || Common.ConvertDBnullToString(HttpContext.Current.Session["LanguageId"]) == "")
                LanguageId = 1;
            else
                LanguageId = Common.ConvertDBnullToInt32(HttpContext.Current.Session["LanguageId"]);
             
            string lang = "en-US";
            switch (LanguageId)
            {
                case 1:
                    lang = "en-US";
                    break;
                case 2:
                    lang = "hi-IN";
                    break;
                case 3:
                    lang = "gu-IN";
                    break;
                case 4:
                    lang = "fr-FR";
                    break;
                case 5:
                    lang = "th-TH";
                    break;
            }

            // ----------------- Resource File Coding --------------------
            //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            //System.Resources.ResourceManager rm = new System.Resources.ResourceManager("ZipBooks.App_LocalResources." + strResourceFileName + lang, System.Reflection.Assembly.GetExecutingAssembly());  //this.GetType().Assembly

            //var entry =
            //    rm.GetResourceSet(ci, true, true)
            //      .OfType<DictionaryEntry>()
            //      .FirstOrDefault(e => e.Key.ToString() == strLableId);

            //var Value = entry.Value == null ? "" : entry.Value.ToString();
            // ----------------- Resource File Coding END --------------------


            if (lang == "en-US") // Do not Call DB if Language is English
            {
                return DefaultLabelId;
            }


            // ----------------- DB Coding --------------------
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.AddWithValue("@FormId", strResourceFileName);
            cmd.Parameters.AddWithValue("@ResourceId", strLableId);
            cmd.Parameters.AddWithValue("@LangKey", lang);
            cmd.Parameters.AddWithValue("@Action", "SELECT");

            DataSet ds = new DataSet();
            ds = ObjSQLHelperMaster.SelectProcDataDS("LangResources_sp", cmd);

            if (ds == null || ds.Tables.Count == 0)
            {
                return DefaultLabelId;
            }
            dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                create(strLableId, strResourceFileName, DefaultLabelId, lang);
                return DefaultLabelId;
            }
            var Value = Common.ConvertDBnullToString(dt.Rows[0]["Value"]);
            // ----------------- DB Coding End--------------------
            return Value;
        }
        catch (Exception ex)
        {
            Common.LogActivity(ex.Message + " " + strLableId + " " + strResourceFileName);
            return ex.Message;
        }
        finally
        {
            ObjSQLHelperMaster.ClearObjects();
        }
    }

    public static void create(string strLableId, string strResourceFileName, string DefaultLabelId, string lang)
    {
        SQLHelperMaster ObjSQLHelperMaster = new SQLHelperMaster();
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Parameters.AddWithValue("@FormId", strResourceFileName.Trim());
            cmd.Parameters.AddWithValue("@ResourceId", strLableId.Trim());
            cmd.Parameters.AddWithValue("@LangKey", lang.Trim());
            cmd.Parameters.AddWithValue("@Value", DefaultLabelId.Trim());
            cmd.Parameters.AddWithValue("@Action", "CREATEDEFAULT");
            if (ObjSQLHelperMaster.IUDProcData("LangResources_sp", cmd))
            {
                ObjSQLHelperMaster.CommitTransaction();
            }
            else
            {
                ObjSQLHelperMaster.RollBackTransaction();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ObjSQLHelperMaster.ClearObjects();
        }
    }



    public static string getMessages(string strFormID = "", string Action = "", string DefaultMessage = "", string param1 = "", string param2 = "")
    {
        SQLHelperMaster ObjSQLHelperMaster = new SQLHelperMaster();
        try
        {
            int LanguageId;
            if (HttpContext.Current.Session == null || Common.ConvertDBnullToString(HttpContext.Current.Session["LanguageId"]) == "")
                LanguageId = 1;
            else
                LanguageId = Common.ConvertDBnullToInt32(HttpContext.Current.Session["LanguageId"]);

            string lang = "en-US";
            switch (LanguageId)
            {
                case 1:
                    lang = "en-US";
                    break;
                case 2:
                    lang = "hi-IN";
                    break;
                case 3:
                    lang = "gu-IN";
                    break;
                case 4:
                    lang = "fr-FR";
                    break;
                case 5:
                    lang = "th-TH";
                    break;
            }
            // ----------------- DB Coding --------------------
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@FormId", strFormID);
            cmd.Parameters.AddWithValue("@ResourceId", Action);
            cmd.Parameters.AddWithValue("@LangKey", lang);
            cmd.Parameters.AddWithValue("@Action", "GETMESSAGES");

            DataSet ds = new DataSet();
            ds = ObjSQLHelperMaster.SelectProcDataDS("LangResources_sp", cmd);

            if (ds == null || ds.Tables.Count == 0)
                return DefaultMessage;
            dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                create(Action, strFormID, DefaultMessage, lang);
                if (param1 != "")
                    DefaultMessage = DefaultMessage.Replace("@param1", param1);

                if (param2 != "")
                    DefaultMessage = DefaultMessage.Replace("@param2", param2);
                return DefaultMessage;
            }
            var Value = Common.ConvertDBnullToString(dt.Rows[0]["Value"]);
            if (param1 != "")
                Value = Value.Replace("@param1", param1);

            if (param2 != "")
                Value = Value.Replace("@param2", param2);

            // ----------------- DB Coding End--------------------

            ObjSQLHelperMaster.ClearObjects();
            return Value;
        }
        catch (Exception ex)
        {
            Common.LogActivity(ex.Message + " " + strFormID);
            return ex.Message;
        }
        finally
        {
            ObjSQLHelperMaster.ClearObjects();
        }
    }

    public static string getLangKey()
    {
        try
        {
            int LanguageId = Common.ConvertDBnullToInt32(HttpContext.Current.Session["LanguageId"]);

            string lang = "en-US";
            switch (LanguageId)
            {
                case 1:
                    lang = "en-US";
                    break;
                case 2:
                    lang = "hn-IN";
                    break;
                case 3:
                    lang = "gu-IN";
                    break;
                case 4:
                    lang = "fr-FR";
                    break;
                case 5:
                    lang = "th-TH";
                    break;
            }
            return lang;
        }
        catch (Exception ex)
        {
            Common.LogActivity(ex.Message);
            return ex.Message;
        }

    }

    #endregion


    #region "Language Label Conversion Function"

    public static string getLabelConversion(string DefaultLabelName)
    {
        SQLHelperMaster objSQLHelperMaster = new SQLHelperMaster();
        try
        {
            int LanguageId;
            if (HttpContext.Current.Session == null || Common.ConvertDBnullToString(HttpContext.Current.Session["LanguageId"]) == "")
                LanguageId = 1;
            else
                LanguageId = Common.ConvertDBnullToInt32(HttpContext.Current.Session["LanguageId"]);

            string lang = "en-US";
            switch (LanguageId)
            {
                case 1:
                    lang = "en-US";
                    break;
                case 2:
                    lang = "hi-IN";
                    break;
                case 3:
                    lang = "gu-IN";
                    break;
                case 4:
                    lang = "fr-FR";
                    break;
                case 5:
                    lang = "th-TH";
                    break;
            }

            if (lang == "en-US") // Do not Call DB if Language is English
            {
                return DefaultLabelName;
            }

            // ----------------- DB Coding --------------------
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.AddWithValue("@Value",DefaultLabelName);
            cmd.Parameters.AddWithValue("@LangKey",lang);
            cmd.Parameters.AddWithValue("@Action","SELECT");

            DataSet ds = new DataSet();
            ds = objSQLHelperMaster.SelectProcDataDS("LangLabelConversion_SP", cmd);

            if (ds == null || ds.Tables.Count == 0)
            {
                return DefaultLabelName;
            }
            dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                if (lang == "en-US")
                {
                    CreateLabel(DefaultLabelName, lang);
                }
                return DefaultLabelName;
            }
            var Value = Common.ConvertDBnullToString(dt.Rows[0]["LangValue"]);
            // ----------------- DB Coding End--------------------
            return Value;
        }
        catch(Exception ex)
        {
            Common.LogActivity(ex.Message + " " + DefaultLabelName);
            return ex.Message;
        }
        finally
        {
            objSQLHelperMaster.ClearObjects();
        }
    }

    public static void CreateLabel(string DefaultLabelName, string lang)
    {
        SQLHelperMaster ObjSQLHelperMaster = new SQLHelperMaster();
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Parameters.AddWithValue("@Value", DefaultLabelName.Trim());
            cmd.Parameters.AddWithValue("@LangKey", lang.Trim());
            cmd.Parameters.AddWithValue("@LangValue", DefaultLabelName.Trim());
            cmd.Parameters.AddWithValue("@Action", "CREATE");
            if (ObjSQLHelperMaster.IUDProcData("LangLabelConversion_SP", cmd))
            {
                ObjSQLHelperMaster.CommitTransaction();
            }
            else
            {
                ObjSQLHelperMaster.RollBackTransaction();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ObjSQLHelperMaster.ClearObjects();
        }
    }
    #endregion
}
