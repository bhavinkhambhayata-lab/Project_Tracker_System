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


            cmd.Parameters.AddWithValue("@FormId", strResourceFileName);
            cmd.Parameters.AddWithValue("@ResourceId", strLableId);
            cmd.Parameters.AddWithValue("@LangKey", lang);
            cmd.Parameters.AddWithValue("@Action", "SELECT");

            DataSet ds = new DataSet();
           

            if (ds == null || ds.Tables.Count == 0)
            {
                return DefaultLabelId;
            }
            dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
               
                return DefaultLabelId;
            }
            var Value = Common.ConvertDBnullToString(dt.Rows[0]["Value"]);
            // ----------------- DB Coding End--------------------
            return Value;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {
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
            return ex.Message;
        }

    }

    #endregion


}
