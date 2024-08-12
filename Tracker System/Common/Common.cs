using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections;
using System.Web.UI.HtmlControls;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

using System.Web.Mvc;
using System.Security.Cryptography;
using System.Web.Routing;
using Tracker_System.Controllers;
using Tracker_System.Classes;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using Tracker_System.Models;
using System.Globalization;
using Newtonsoft.Json;

public class Common
{

    #region Constants

    public static string constEncKey = "accukeyabcdefghijklmnopqrstuvwxyzencryption";
    public static string LicenceEncKey = "ziplicenceabcdefghijklmnopqrstuvwxyzencryptionkey";
    public static string ConStringFull = "";
    public static string PhysicalApplicationPath = "";

    #endregion

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

    public static string ConvertByteArrayToImageURL(byte[] a)
    {
        if (a == null)
            return "";

        return Convert.ToBase64String(a, 0, a.Length);
    }

    public static DataTable ConvertSessionToDataTable(object obj)
    {
        try
        {
            return (DataTable)obj;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Guid ConvertDBnullToGuid(object obj)
    {
        try
        {
            return Guid.Parse(ConvertDBnullToString(obj));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DataTable ListToDataTable<T>(List<T> items)
    {
        if (items == null)
            return null;

        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(x => x.MetadataToken).ToArray();
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }

    public static string NumberFormat(decimal x, int NoOfDecimals = 2, bool ZeroAsBlank = false)
    {
        if (ZeroAsBlank == true && x == 0)
            return "";

        if (HttpContext.Current != null && HttpContext.Current.Session != null
                && HttpContext.Current.Session["IndianGST"] != null && Convert.ToBoolean(HttpContext.Current.Session["IndianGST"]) == true)
            return IndianNumberFormat(x, NoOfDecimals, ZeroAsBlank);

        string a;
        if (NoOfDecimals == 0)
        {
            a = x.ToString("#,##0");
        }
        else
        {
            string tt;
            tt = "";
            tt = tt.PadLeft(NoOfDecimals, '0');
            a = x.ToString("#,###0." + tt);
        }

        return a;

        //string a = x.ToString("#,##0.00");
        //return a;    

    }

    public static string IndianNumberFormat(decimal x, int NoOfDecimals = 2, bool ZeroAsBlank = false)
    {
        //decimal parsed = decimal.Parse(Convert.ToString(x), CultureInfo.InvariantCulture);
        //CultureInfo hindi = new CultureInfo("hi-IN");
        //string text = string.Format(hindi, "{0:c}", parsed);

        if (ZeroAsBlank == true && x == 0)
        {
            return "";
        }
        CultureInfo cultureInfo = new CultureInfo("en-IN", false);
        cultureInfo.NumberFormat.NumberDecimalDigits = NoOfDecimals;
        var s = string.Format(cultureInfo, "{0:n}", Convert.ToDouble(x));
        return s;
    }


    public static string Number2WordsConvert(string Number, string R_T, string CurrencyDisplayName = "Rupees", string CurrencySubUnit = "Paise")
    {
        string STR1 = Number;
        string S1 = null;
        string S2 = null;
        string S3 = null;
        string S4 = null;
        string S5 = null;
        string SS1 = null;
        string SS2 = null;
        string SS3 = null;
        string SS4 = null;
        string SS5 = null;
        string SS6 = null;
        string SS7 = null;
        string SS8 = null;
        string SS9 = null;
        string SS10 = null;
        string SS11 = null;
        string PSS1 = null;
        string PSS2 = "";
        object NTEMP = null;
        object NTEMP1 = null;
        string NumTowords = "";

        string[] NDigits = new string[20];
        string[] NTens = new string[10];

        NDigits[1] = "One ";
        NDigits[2] = "Two ";
        NDigits[3] = "Three ";
        NDigits[4] = "Four ";
        NDigits[5] = "Five ";
        NDigits[6] = "Six ";
        NDigits[7] = "Seven ";
        NDigits[8] = "Eight ";
        NDigits[9] = "Nine ";
        NDigits[10] = "Ten ";
        NDigits[11] = "Eleven ";
        NDigits[12] = "Twelve ";
        NDigits[13] = "Thirteen ";
        NDigits[14] = "Fourteen ";
        NDigits[15] = "Fifteen ";
        NDigits[16] = "Sixteen ";
        NDigits[17] = "Seventeen ";
        NDigits[18] = "Eighteen ";
        NDigits[19] = "Nineteen ";

        NTens[1] = " ";
        NTens[2] = "Twenty ";
        NTens[3] = "Thirty ";
        NTens[4] = "Forty ";
        NTens[5] = "Fifty ";
        NTens[6] = "Sixty ";
        NTens[7] = "Seventy ";
        NTens[8] = "Eighty ";
        NTens[9] = "Ninety ";

        string SS12 = null;
        string SS13 = null;
        string SS0 = "";
        string SS14 = "";


        CurrencySubUnit = CurrencySubUnit + " ";
        CurrencyDisplayName = CurrencyDisplayName + " ";
        R_T = R_T.ToUpper();
        STR1 = STR1.Replace(",", "");

        if (STR1.Length == 1)
        {
            S1 = Simulate.Val(STR1).ToString();
        }
        else
        {
            S1 = Simulate.Val(STR1.Substring(STR1.Length - 2, 2)).ToString();
        }
        if (STR1.Length > 2)
        {
            S2 = Simulate.Val(STR1.Substring((STR1.Length - 2) - 1, 1)).ToString();
        }
        else
        {
            S2 = 0.ToString();
        }
        if (STR1.Length > 4)
        {
            S3 = Simulate.Val(STR1.Substring((STR1.Length - 4) - 1, 2)).ToString();
        }
        else
        {
            S3 = 0.ToString();
        }
        if (S3 == 0.ToString())
        {
            if (STR1.Length > 3)
            {
                S3 = Simulate.Val(STR1.Substring((STR1.Length - 3) - 1, 1)).ToString();
            }
            else
            {
                S3 = 0.ToString();
            }
        }

        if (STR1.Length > 6)
        {
            S4 = Simulate.Val(STR1.Substring((STR1.Length - 6) - 1, 2)).ToString();
        }
        else
        {
            S4 = 0.ToString();
        }
        if (S4 == 0.ToString())
        {
            if (STR1.Length > 5)
            {
                S4 = Simulate.Val(STR1.Substring((STR1.Length - 5) - 1, 1)).ToString();
            }
            else
            {
                S4 = 0.ToString();
            }
        }

        if (STR1.Length > 8)
        {
            S5 = Simulate.Val(STR1.Substring((STR1.Length - 8) - 1, 2)).ToString();
        }
        else
        {
            S5 = 0.ToString();
        }
        if (S5 == 0.ToString())
        {
            if (STR1.Length > 7)
            {
                S5 = Simulate.Val(STR1.Substring((STR1.Length - 7) - 1, 1)).ToString();
            }
            else
            {
                S5 = 0.ToString();
            }
        }

        if (R_T == "R")
        {
            SS0 = CurrencyDisplayName;
        }

        SS1 = "";
        SS2 = "";
        SS3 = "";
        SS4 = "";
        SS5 = "";
        SS6 = "";
        SS7 = "";
        SS8 = "";
        SS9 = "";
        SS10 = "";
        SS11 = "";
        SS12 = "";
        SS13 = "";


        if (S5 != 0.ToString())
        {
            if (S5.CompareTo(19.ToString()) > 0)
            {
                NTEMP = Simulate.Val(S5.ToString().Substring(S5.ToString().Length - 1));
                NTEMP1 = (Convert.ToDouble(S5) - Convert.ToDouble(NTEMP)) / 10;
                SS1 = NTens[Convert.ToInt32(NTEMP1)];
                if (Convert.ToInt32(NTEMP) != 0)
                {
                    SS2 = NDigits[Convert.ToInt32(NTEMP)];
                }
            }
            else
            {
                SS1 = NDigits[Convert.ToInt32(S5)];
                SS2 = "";
            }
            SS3 = "Crore ";
        }


        if (S4 != 0.ToString())
        {
            if (S4.CompareTo(19.ToString()) > 0)
            {
                NTEMP = Simulate.Val(S4.ToString().Substring(S4.ToString().Length - 1));
                NTEMP1 = (Convert.ToDouble(S4) - Convert.ToDouble(NTEMP)) / 10;
                SS4 = NTens[Convert.ToInt32(NTEMP1)];
                if (Convert.ToInt32(NTEMP) != 0)
                {
                    SS5 = NDigits[Convert.ToInt32(NTEMP)];
                }
            }
            else
            {
                SS4 = NDigits[Convert.ToInt32(S4)];
                SS5 = "";
            }
            SS6 = "Lakh ";
        }


        if (S3 != 0.ToString())
        {
            if (S3.CompareTo(19.ToString()) > 0)
            {
                NTEMP = Simulate.Val(S3.ToString().Substring(S3.ToString().Length - 1));
                NTEMP1 = (Convert.ToDouble(S3) - Convert.ToDouble(NTEMP)) / 10;
                SS7 = NTens[Convert.ToInt32(NTEMP1)];
                if (Convert.ToInt32(NTEMP) != 0)
                {
                    SS8 = NDigits[Convert.ToInt32(NTEMP)];
                }
            }
            else
            {
                SS7 = NDigits[Convert.ToInt32(S3)];
                SS8 = "";
            }
            SS9 = "Thousand ";
        }

        if (S2 != 0.ToString())
        {
            SS10 = NDigits[Convert.ToInt32(S2)];
            SS11 = "Hundred ";
        }

        if (S1 != 0.ToString())
        {
            if (S1.CompareTo(19.ToString()) > 0)
            {
                NTEMP = Simulate.Val(S1.ToString().Substring(S1.ToString().Length - 1));
                NTEMP1 = (Convert.ToDouble(S1) - Convert.ToDouble(NTEMP)) / 10;
                SS12 = NTens[Convert.ToInt32(NTEMP1)];
                if (Convert.ToInt32(NTEMP) != 0)
                {
                    SS13 = NDigits[Convert.ToInt32(NTEMP)];
                }
            }
            else
            {
                SS12 = NDigits[Convert.ToInt32(S1)];
                SS13 = "";
            }
        }

        NumTowords = SS1 + SS2 + SS3 + SS4 + SS5 + SS6 + SS7 + SS8 + SS9 + SS10 + SS11 + SS12 + SS13 + PSS1 + PSS2 + SS14;
        return NumTowords;
    }

    public static string Number2Word_Million(string MyNumber, string StrCurrencyName = "Rupee", string strSubUnit = "Paise", int CurrencyDecimalPlacess = 2)
    {
        string Dollars, Cents, Temp, Number2Word;
        int DecimalPlace, Count;
        string[] Place = new string[9];
        Place[2] = "Thousand ";
        Place[3] = "Million ";
        Place[4] = "Billion ";
        Place[5] = "Trillion ";
        Cents = "";
        Dollars = "";

        MyNumber = Convert.ToString(MyNumber);
        DecimalPlace = MyNumber.IndexOf(".");
        if (DecimalPlace > 0)
        {
            string MyNum = MyNumber.Substring(DecimalPlace) + "000";
            //Cents = GetTens1(MyNum.Substring(1, CurrencyDecimalPlacess));
            Cents = Number2WordsConvert(MyNum.Substring(1, CurrencyDecimalPlacess), "", StrCurrencyName, strSubUnit);
            MyNumber = MyNumber.Substring(0, DecimalPlace).Trim();
        }

        Count = 1;
        while (MyNumber != "")
        {
            if (MyNumber.Length >= 3)
            {
                Temp = GetHundreds(MyNumber.Substring(MyNumber.Length - 3));
            }
            else
                Temp = GetHundreds(MyNumber);
            if (Temp != "")
                Dollars = Temp + Place[Count] + Dollars;
            if (MyNumber.Length > 3)
            {
                MyNumber = MyNumber.Substring(0, MyNumber.Length - 3);
            }
            else
            {
                MyNumber = "";
            }

            Count = Count + 1;
        }
        switch (Dollars)
        {
            case "":
                Dollars = "Zero " + StrCurrencyName;
                break;
            case "one":
                Dollars = "One " + StrCurrencyName;
                break;
            default:
                Dollars = Dollars + " " + StrCurrencyName;
                break;
        }

        switch (Cents)
        {
            case "":
                Cents = "";
                break;
            case "one":
                Cents = " and One " + strSubUnit;
                break;
            default:
                Cents = " and " + Cents.ToString() + "" + strSubUnit;
                break;
        }
        Number2Word = Dollars + Cents + " Only";
        return Number2Word;
    }


    private static string GetDigit1(string Digit)
    {
        string GetDigit = "";
        switch (Digit)
        {
            case "1":
                GetDigit = "One ";
                break;
            case "2":
                GetDigit = "Two ";
                break;
            case "3":
                GetDigit = "Three ";
                break;
            case "4":
                GetDigit = "Four ";
                break;
            case "5":
                GetDigit = "Five ";
                break;
            case "6":
                GetDigit = "Six ";
                break;
            case "7":
                GetDigit = "Seven ";
                break;
            case "8":
                GetDigit = "Eight ";
                break;
            case "9":
                GetDigit = "Nine ";
                break;
            default:
                GetDigit = "";
                break;
        }
        return GetDigit;
    }

    private static string GetTens1(string TensText)
    {
        string Result;
        Result = "";
        if (TensText.Substring(0, 1) == "1")
        {
            switch (TensText)
            {
                case "10":
                    Result = "Ten ";
                    break;
                case "11":
                    Result = "Eleven ";
                    break;
                case "12":
                    Result = "Twelve ";
                    break;
                case "13":
                    Result = "Thirteen ";
                    break;
                case "14":
                    Result = "Fourteen ";
                    break;
                case "15":
                    Result = "Fifteen ";
                    break;
                case "16":
                    Result = "Sixteen ";
                    break;
                case "17":
                    Result = "Seventeen ";
                    break;
                case "18":
                    Result = "Eighteen ";
                    break;
                case "19":
                    Result = "Nineteen ";
                    break;

            }
        }
        else
        {
            switch (TensText.Substring(0, 1))
            {
                case "2":
                    Result = "Twenty ";
                    break;
                case "3":
                    Result = "Thirty ";
                    break;
                case "4":
                    Result = "Forty ";
                    break;
                case "5":
                    Result = "Fifty ";
                    break;
                case "6":
                    Result = "Sixty ";
                    break;
                case "7":
                    Result = "Seventy ";
                    break;
                case "8":
                    Result = "Eighty ";
                    break;
                case "9":
                    Result = "Ninety ";
                    break;

            }

            Result = Result + GetDigit1(TensText.Substring(TensText.Length - 1));
        }
        ;
        return Result;
    }

    private static string GetHundreds(string MyNumber)
    {
        string Result = "";
        if (MyNumber == "0")
            return "";
        MyNumber = "000" + MyNumber;
        MyNumber = MyNumber.Substring(MyNumber.Length - 3);
        if (MyNumber.Substring(0, 1) != "0")
        {
            Result = GetDigit1(MyNumber.Substring(0, 1)) + "Hundred ";
        }

        if (MyNumber.Substring(1, 1) != "0")
        {
            Result = Result + GetTens1(MyNumber.Substring(1));
        }
        else
        {
            Result = Result + GetDigit1(MyNumber.Substring(2));
        }

        return Result;
    }

    public static string GetNoOfDecimal()
    {
        if (HttpContext.Current.Session != null)
            return "f" + HttpContext.Current.Session["CoCurrencyDecimalPlaces"];
        else
            return "f3";
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
    public static string Get_SQLDate_From_DateStringClub(string myDateString)
    {
        try
        {
            if (myDateString == null)
            {
                return null;
            }

            string line = myDateString.ToString();
            string[] dates = line.Split('/');
            //string returnDate = Convert.ToInt32(dates[1]) + "/" + Convert.ToInt32(dates[0]) + "/" + Convert.ToInt32(dates[2]);
            string returnDate = Convert.ToInt32(dates[2]) + "-" + Convert.ToInt32(dates[1]) + "-" + Convert.ToInt32(dates[0]);

            return returnDate;
        }
        catch (Exception)
        {
            return "01/01/1900";
        }
    }
    public static string Get_SQLDate_From_DateString_YYYYMMDD(string myDateString)
    {
        try
        {
            if (myDateString == null)
            {
                return null;
            }

            string line = myDateString.ToString();
            string[] dates = line.Split('/');
            //string returnDate = Convert.ToInt32(dates[1]) + "/" + Convert.ToInt32(dates[0]) + "/" + Convert.ToInt32(dates[2]);
            string returnDate = Convert.ToInt32(dates[2]) + "/" + Convert.ToInt32(dates[1]) + "/" + Convert.ToInt32(dates[0]);

            return returnDate;
        }
        catch (Exception)
        {
            return "01/01/1900";
        }
    }
    public static string Get_SQLDate_From_DateString_YYYYMMDD_HHMMSS(string myDateString)
    {
        try
        {
            if (myDateString == null)
            {
                return null;
            }

            string line = myDateString.ToString();
            string[] DateTimeString = line.Split(' ');
            string[] dates = DateTimeString[0].Split('/');
            string[] times = DateTimeString[1].Split(':');
            //string returnDate = Convert.ToInt32(dates[1]) + "/" + Convert.ToInt32(dates[0]) + "/" + Convert.ToInt32(dates[2]);
            string returnDate = Convert.ToInt32(dates[2]) + "/" + Convert.ToInt32(dates[1]) + "/" + Convert.ToInt32(dates[0]) + " " + Convert.ToInt32(times[0]) + ":" + Convert.ToInt32(times[1]) + ":" + Convert.ToInt32(times[2]);

            return returnDate;
        }
        catch (Exception)
        {
            return "1900/01/01";
        }
    }
    public static string Get_SQLDateTime_From_DateTimeString(string myDateString)
    {
        try
        {

            string line = myDateString.ToString();
            string[] DateTimeString = line.Split(' ');
            string[] dates = DateTimeString[0].Split('/');
            string[] times = DateTimeString[1].Split(':');

            string returnDate = Convert.ToInt32(dates[2]) + "-" + Convert.ToInt32(dates[1]) + "-" + Convert.ToInt32(dates[0]) + " " + Convert.ToInt32(times[0]) + ":" + Convert.ToInt32(times[1]);

            return returnDate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DateTime ConvertDateToSysFormat(string myDateString)
    {
        try
        {
            string line = myDateString.ToString();
            string[] dates = line.Split('/');
            DateTime returnDate = new DateTime(Convert.ToInt32(dates[2]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[0]));

            return returnDate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string Get_DateString_From_SQLDate(object mySQLDate)
    {
        try
        {
            string line = ConvertDBnullToString(mySQLDate);
            string[] dates = line.Split('/');
            //string returnDate = Convert.ToInt32(dates[0]) + "/" + Convert.ToInt32(dates[1]) + "/" + Convert.ToInt32(dates[2].Substring(0, Math.Min(dates[2].Length, 4)));
            string returnDate = Common.ConvertDBnullToString(Convert.ToInt32(dates[0])).PadLeft(2, '0') + "/" + Common.ConvertDBnullToString(Convert.ToInt32(dates[1])).PadLeft(2, '0') + "/" + Convert.ToInt32(dates[2].Substring(0, Math.Min(dates[2].Length, 4)));

            return returnDate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string Get_DateString_From_SQLDateClub(object mySQLDate)
    {
        try
        {
            string line = ConvertDBnullToString(mySQLDate);
            string[] dates = line.Split('/');
            //string returnDate = Convert.ToInt32(dates[0]) + "/" + Convert.ToInt32(dates[1]) + "/" + Convert.ToInt32(dates[2].Substring(0, Math.Min(dates[2].Length, 4)));
            string returnDate = Common.ConvertDBnullToString(Convert.ToInt32(dates[0])).PadLeft(2, '0') + "/" + Common.ConvertDBnullToString(Convert.ToInt32(dates[1])).PadLeft(2, '0') + "/" + Convert.ToInt32(dates[2].Substring(0, Math.Min(dates[2].Length, 4)));

            return returnDate;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static string Get_DateString_From_SQLDateHRMS(object mySQLDate)
    {
        try
        {
            //string line = ConvertDBnullToString(mySQLDate);
            //string[] dates = line.Split('/');
            ////string returnDate = Convert.ToInt32(dates[0]) + "/" + Convert.ToInt32(dates[1]) + "/" + Convert.ToInt32(dates[2].Substring(0, Math.Min(dates[2].Length, 4)));
            //string returnDate = Common.ConvertDBnullToString(Convert.ToInt32(dates[0])).PadLeft(2, '0') + "/" + Common.ConvertDBnullToString(Convert.ToInt32(dates[1])).PadLeft(2, '0') + "/" + Convert.ToInt32(dates[2].Substring(0, Math.Min(dates[2].Length, 4)));

            //return returnDate;

            DateTime dt = Convert.ToDateTime(mySQLDate);
            return dt.Day + "/" + dt.Month + "/" + dt.Year;
        }
        catch (Exception)
        {
            //return "01/01/1900";
            return "";
        }
    }

    public static string Get_DateString_From_Date(DateTime myDate)
    {
        string returnDateString = "";
        try
        {
            returnDateString = myDate.Day.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Month.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Year.ToString().PadLeft(2, '0').ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnDateString;
    }

    public static string Get_DateString_From_Date(DateTime? myDate)
    {
        string returnDateString = "";
        try
        {
            if (myDate != null)
                returnDateString = myDate.Value.Day.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Value.Month.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Value.Year.ToString().PadLeft(2, '0').ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnDateString;
    }

    public static string Get_DateTimeString_From_Date(DateTime? myDate) // MM/dd/yyyy hh:mm:ss
    {
        string returnDateString = "";
        try
        {
            if (myDate != null)
                returnDateString = myDate.Value.Month.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Value.Day.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Value.Year.ToString().PadLeft(2, '0').ToString() + " " + myDate.Value.Hour.ToString().PadLeft(2, '0').ToString() + ":" + myDate.Value.Minute.ToString().PadLeft(2, '0').ToString() + ":" + myDate.Value.Second.ToString().PadLeft(2, '0').ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnDateString;
    }

    public static string Get_DateCustomString_From_Date(DateTime? myDate) //  dd-MMM-yyyy
    {
        string returnDateString = "";
        try
        {
            if (myDate != null)
                returnDateString = myDate.Value.Day.ToString().PadLeft(2, '0').ToString() + "-" + myDate.Value.ToString("MMM") + "-" + myDate.Value.Year.ToString().PadLeft(2, '0').ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnDateString;
    }

    public static string Get_TimeString_From_Date(DateTime? myDate) // hh:mm AM/PM
    {
        string returnDateString = "";
        try
        {
            if (myDate != null)
                returnDateString = myDate.Value.ToShortTimeString();
        }
        catch (Exception)
        {
            return "";
        }
        return returnDateString;
    }

    public static string Get_DateTimeHHMMString_From_Date(DateTime? myDate) // MM/dd/yyyy hh:mm:ss
    {
        string returnDateString = "";
        try
        {
            if (myDate != null)
                returnDateString = myDate.Value.Month.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Value.Day.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Value.Year.ToString().PadLeft(2, '0').ToString() + " " + myDate.Value.Hour.ToString().PadLeft(2, '0').ToString() + ":" + myDate.Value.Minute.ToString().PadLeft(2, '0').ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnDateString;
    }

    public static string Get_DateString_From_Date_DDMM(DateTime? myDate) // dd/MM/yyyy hh:mm:ss
    {
        string returnDateString = "";
        try
        {
            if (myDate != null)
                returnDateString = myDate.Value.Day.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Value.Month.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Value.Year.ToString().PadLeft(2, '0').ToString() + " " + myDate.Value.Hour.ToString().PadLeft(2, '0').ToString() + ":" + myDate.Value.Minute.ToString().PadLeft(2, '0').ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnDateString;
    }

    public static string Get_TimeString_From_Date_HHMM(DateTime? myDate) //  hh:mm
    {
        string returnDateString = "";
        try
        {
            if (myDate != null)
                returnDateString = myDate.Value.Hour.ToString().PadLeft(2, '0').ToString() + ":" + myDate.Value.Minute.ToString().PadLeft(2, '0').ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnDateString;
    }

    public static string Get_DispalyDateString_From_Date(DateTime? myDate, bool IsYear = false) // 23rd Dec
    {
        string returnDateString = "";
        try
        {
            if (myDate != null)
            {
                returnDateString = myDate.Value.Day.ToString().PadLeft(2, '0').ToString() + GetMonthCaption(myDate.Value.Day) + " " + myDate.Value.ToString("MMM");

                if (IsYear)
                {
                    returnDateString += " " + myDate.Value.Year.ToString().PadLeft(2, '0').ToString();
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnDateString;
    }

    public static string GetMonthCaption(int Day)
    {
        return (Day % 10 == 1 && Day != 11) ? "st" : (Day % 10 == 2 && Day != 12) ? "nd" : (Day % 10 == 3 && Day != 13) ? "rd" : "th";
    }

    public static DateTime Get_DateTime_From_DateTimeString(string myDateString)
    {
        try
        {

            string line = myDateString.ToString();

            string[] DateTimeString = line.Split(' ');

            DateTimeString[0] = DateTimeString[0].Replace('-', '/').Replace('.', '/');

            string[] dates = DateTimeString[0].Split('/');
            string[] times = DateTimeString[1].Split(':');

            DateTime returnDate = new DateTime(Convert.ToInt32(dates[2]), Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]),
                                               Convert.ToInt32(times[0]), Convert.ToInt32(times[1]), Convert.ToInt32(times[2]));

            return returnDate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static TimeSpan ConvertDBNullToTimeSpan(object obj)
    {
        try
        {
            string TimeString = obj.ToString().ToLower();


            if (TimeString.Contains("pm"))
            {
                TimeString = TimeString.Replace("pm", "");
                TimeSpan tsp = new TimeSpan(12, 0, 0);
                TimeSpan ReturnedTsp = TimeSpan.Parse(TimeString);

                if (ReturnedTsp < tsp)
                    ReturnedTsp = ReturnedTsp.Add(tsp);

                return ReturnedTsp;
            }
            else
            {
                TimeString = TimeString.Replace("am", "");
                return TimeSpan.Parse(TimeString);
            }

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

    public static DateTime ConvertDateStringToDateTime(object obj)
    {
        try
        {
            string line = obj.ToString();
            string[] dates = line.Split('/');
            DateTime dt = new DateTime(Convert.ToInt32(dates[2]), Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]));

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Boolean IsDateStrGreaterThanEqualToday(string obj)
    {
        try
        {
            return (ConvertDBNullToDateTime(obj) >= DateTime.Today.Date ? true : false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Boolean IsValidDateStr(object obj)
    {
        try
        {
            string line = obj.ToString();
            string[] dates = line.Split('/');
            DateTime dt = new DateTime(Convert.ToInt32(dates[2]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[0]));

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static string ConvertDateToString(object obj)
    {
        try
        {
            if (Convert.ToDateTime(obj) > Convert.ToDateTime("01/01/1900"))
            {
                return Convert.ToDateTime(obj).ToString("MM/dd/yyyy");
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static string ConvertTimeToString(object obj)
    {
        try
        {
            TimeSpan ts = TimeSpan.Parse(obj.ToString());
            return ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0');
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DateTime ConvertDDMMYYYYToDate(string strDate)
    {
        try
        {
            int dd = 0;
            int MM = 0;
            int YYYY = 0;
            string dtDate;
            dd = Convert.ToInt32(strDate.Split('-')[0]);
            MM = Convert.ToInt32(strDate.Split('-')[1]);
            YYYY = Convert.ToInt32(strDate.Split('-')[2].ToString().Substring(0, 4));
            dtDate = (YYYY + "-" + MM + "-" + dd);
            dtDate = (YYYY + "-" + MM.ToString().PadLeft(2, '0') + "-" + dd.ToString().PadLeft(2, '0'));
            return DateTime.Parse(dtDate);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static Boolean IsDateGreaterThanEqualToday(DateTime obj)
    {
        try
        {
            return (obj.Date >= DateTime.Today.Date ? true : false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Boolean IsValidTime(TimeSpan obj)
    {
        try
        {
            return (obj.Hours > 0 || obj.Minutes > 0 ? true : false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DateTime getDate(string strDate)
    {
        try
        {
            IFormatProvider provider = new System.Globalization.CultureInfo("en-US", true);
            String datetime = strDate;
            DateTime dt = DateTime.Parse(datetime, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DateTime? ConvertDBNullToDateTimeNull(object obj)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(obj);

            return dt;
        }
        catch (Exception)
        {
            return null;
        }

    }

    #endregion

    #region "Enum Dropdown fill Methods"



    public static DataTable FillComboByEnum(Type objEnum, bool AddBalnk = false, string BlankVal = "")
    {
        try
        {
            DataTable DtFillCombo = new DataTable();

            DtFillCombo.Columns.Add("Name");
            DtFillCombo.Columns.Add("Value", typeof(Int16));

            if (AddBalnk == true)
            {
                DataRow Item = DtFillCombo.NewRow();
                Item["Name"] = BlankVal;
                Item["Value"] = -1;
                DtFillCombo.Rows.Add(Item);
            }

            var _with1 = Enum.GetValues(objEnum);
            for (Int16 i = 0; i <= _with1.GetUpperBound(0); i++)
            {
                DataRow Item = DtFillCombo.NewRow();
                Item["Name"] = Enum.GetName(objEnum, _with1.GetValue(i)).Replace("_", " ");
                Item["Value"] = _with1.GetValue(i);
                DtFillCombo.Rows.Add(Item);
            }

            return DtFillCombo;
        }
        catch (Exception)
        {
            return new DataTable();
        }

    }

    public static string GetEnumDescription(Enum emn)
    {
        string desc = string.Empty;
        FieldInfo field = emn.GetType().GetField(emn.ToString());
        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        desc = attribute.Description;
        return desc;
    }

    public static int GetEnumValue(Enum enm)
    {
        return Convert.ToInt32(enm);
    }

    #endregion

    #region "General Functions"

    public static bool IsSessionDataTableExists(object obj)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = (DataTable)obj;
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;

        }
        catch (Exception)
        {
            return false;
        }
    }



    public static string GetIdByNameGeneralisedMethod(string getColumn, string byColumnName, string tableName, string byColumnValue, string TenantId, string WhereCondition = "")
    {
        if (getColumn != null && getColumn != "" && byColumnName != null && byColumnName != "" && tableName != null && tableName != "" && !string.IsNullOrEmpty(byColumnValue))
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SQLHelper ObjSQLHelper = new SQLHelper();
            try
            {

                if (byColumnValue.Contains("'"))
                {
                    byColumnValue = byColumnValue.Replace("'", "''");
                }

                cmd.Parameters.AddWithValue("@GetColumnName", getColumn);
                cmd.Parameters.AddWithValue("@ByColumnName", byColumnName);
                cmd.Parameters.AddWithValue("@ByColumnValue", byColumnValue);
                cmd.Parameters.AddWithValue("@TableName", tableName);

                if (!string.IsNullOrEmpty(TenantId))
                    cmd.Parameters.AddWithValue("@TenantId", TenantId);

                cmd.Parameters.AddWithValue("@WhereCondition", WhereCondition);
                ds = ObjSQLHelper.SelectProcDataDS("GetIdByNameGeneralised", cmd);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Common.ConvertDBnullToString(ds.Tables[0].Rows[0]["Value"]) != null && Common.ConvertDBnullToString(ds.Tables[0].Rows[0]["Value"]) != "")
                        {
                            return Common.ConvertDBnullToString(ds.Tables[0].Rows[0]["Value"]);
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
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
        return "";
    }

    public static bool IsCompanyTransactionDataExist(string TenantId)
    {
        bool ReturnVal = true;
        SqlCommand cmd = new SqlCommand();
        SQLHelper ObjSQLHelper = new SQLHelper();

        try
        {
            cmd.Parameters.AddWithValue("@Action", "IsCompanyTransactionDataExist");
            cmd.Parameters.AddWithValue("@TenantId", TenantId);
            if (Common.ConvertDBnullToInt32(ObjSQLHelper.ExecuteScalarByDataset("GlobalUseSP", cmd)) > 0)
            {
                ReturnVal = true;
            }
            else
            {
                ReturnVal = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ObjSQLHelper.ClearObjects();
        }
        return ReturnVal;
    }

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

    #endregion

    public static string GetFYearFromDate(string DocDate, string TenantId)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper objSQLHelper = new SQLHelper();
        string Date = "";
        try
        {
            cmd.Parameters.AddWithValue("@Action", "GetFYearFromDate");
            cmd.Parameters.AddWithValue("@TenantId", TenantId);
            cmd.Parameters.AddWithValue("@DocDate", Get_SQLDate_From_DateString(DocDate));
            Date = objSQLHelper.ExecuteScalarByDataset("CommonSP", cmd);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objSQLHelper.ClearObjects();
        }
        return Date;
    }


    public static DataSet GetItemProjectedClBl(string TenantId, string ItemId, string DocDate, string PlantId)
    {
        SqlCommand cmd = new SqlCommand();
        SQLHelper objSQLHelper = new SQLHelper();
        DataSet ds = new DataSet();
        try
        {
            cmd.Parameters.AddWithValue("@Action", "GetItemProjectedClBl");
            if (!string.IsNullOrEmpty(TenantId))
                cmd.Parameters.AddWithValue("@TenantId", TenantId);
            else
                cmd.Parameters.AddWithValue("@TenantId", HttpContext.Current.Session["TenantId"]);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            if (!string.IsNullOrEmpty(PlantId))
                cmd.Parameters.AddWithValue("@PlantId", PlantId);
            cmd.Parameters.AddWithValue("@DocDate", Get_SQLDate_From_DateString(DocDate));

            ds = objSQLHelper.SelectProcDataDS("CommonSP", cmd);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objSQLHelper.ClearObjects();
        }
    }
    public static string GetCSByEnterpriseId(string EnterpriseId)
    {
        DataSet ds = new DataSet();
        string ConnectionString = ConvertDBnullToString(ConfigurationManager.ConnectionStrings["ConnectionStringMaster"]);
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("GetConnectionStringByEnterpriseCode", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter spNew = new SqlParameter();
                spNew.Value = EnterpriseId;
                spNew.ParameterName = "@EnterpriseId";
                da.SelectCommand.Parameters.Add(spNew);

                da.Fill(ds, "GetConnectionStringByEnterpriseCode");
                DataTable dt = ds.Tables["SourceTable_Name"];
            }
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Columns.Contains("ConnectionString"))
                return ds.Tables[0].Rows[0]["ConnectionString"].ToString();
            else
                return "";
        }
        else
            return "";
    }
    public static bool CheckConnectionTrans()
    {
        SqlConnection con = new SqlConnection();
        SQLHelper ObjSQLHelper = new SQLHelper();
        try
        {
            con.ConnectionString = ObjSQLHelper.ConnectionString;
            con.Open();
            con.Close();
            return true;
        }
        catch (Exception ex)
        {
         
            HttpContext.Current.Session["UserId"] = "";
            return false;
        }
        finally
        {
            con.Dispose();
            ObjSQLHelper.ClearObjects();
        }
    }


   


    public static bool ValidateJSON(string json)
    {
        try
        {
            List<dynamic> JSONList = new List<dynamic>();
            JSONList = JsonConvert.DeserializeObject<List<dynamic>>(json);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}


class RandomStringGenerate
{
    static readonly char[] AvailableCharacters = {
    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
    'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
  };

    internal static string GenerateIdentifier(int length)
    {
        char[] identifier = new char[length];
        byte[] randomData = new byte[length];

        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomData);
        }

        for (int idx = 0; idx < identifier.Length; idx++)
        {
            int pos = randomData[idx] % AvailableCharacters.Length;
            identifier[idx] = AvailableCharacters[pos];
        }

        return new string(identifier);
    }
}

public static partial class Simulate
{
    public static double Val(string expression)
    {
        if (expression == null)
            return 0;

        //try the entire string, then progressively smaller
        //substrings to simulate the behavior of VB's 'Val',
        //which ignores trailing characters after a recognizable value:
        for (int size = expression.Length; size > 0; size--)
        {
            double testDouble;
            if (double.TryParse(expression.Substring(0, size), out testDouble))
                return testDouble;
        }

        //no value is recognized, so return 0:
        return 0;
    }
    public static double Val(object expression)
    {
        if (expression == null)
            return 0;

        double testDouble;
        if (double.TryParse(expression.ToString(), out testDouble))
            return testDouble;

        //VB's 'Val' function returns -1 for 'true':
        bool testBool;
        if (bool.TryParse(expression.ToString(), out testBool))
            return testBool ? -1 : 0;

        //VB's 'Val' function returns the day of the month for dates:
        System.DateTime testDate;
        if (System.DateTime.TryParse(expression.ToString(), out testDate))
            return testDate.Day;

        //no value is recognized, so return 0:
        return 0;
    }
    public static int Val(char expression)
    {
        int testInt;
        if (int.TryParse(expression.ToString(), out testInt))
            return testInt;
        else
            return 0;
    }
}

public static partial class DataTableToList
{
    public static List<T> ConvertDataTable<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }

    private static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    pro.SetValue(obj, dr[pro.Name] == DBNull.Value ? null : dr[column.ColumnName], null);//pro.SetValue(obj, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return obj;
    }
}
