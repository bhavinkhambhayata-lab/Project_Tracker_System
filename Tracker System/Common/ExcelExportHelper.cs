using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;


public class ExcelExportHelper
{
    public static string ExcelContentType
    {
        get
        { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
    }

    public static DataTable ListToDataTable<T>(List<T> data)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable dataTable = new DataTable();

        for (int i = 0; i < properties.Count; i++)
        {
            PropertyDescriptor property = properties[i];
            dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
        }

        object[] values = new object[properties.Count];
        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = properties[i].GetValue(item);
            }

            dataTable.Rows.Add(values);
        }
        return dataTable;
    }


 
   

    public static List<string> GetColumnList(string ReportHeader)
    {
        List<string> columns = new List<string>();
        string[] columnslist = ReportHeader.Split('|');
        for (int i = 0; i < columnslist.Length; i++)
            columns.Add(columnslist[i]);
        return columns;
    }
    public static void ReNameColumnNames(ref DataTable dataTable, string OriginalNames, string ReportNames, ref DataTable NumericdataTable, bool ReOrder = false)
    {
        string[] OriginalColumnsList = OriginalNames.Split('|');
        string[] ReportColumnsList = ReportNames.Split('|');

        if (ReOrder)
        {
            for (int i = 0; i < OriginalColumnsList.Length; i++)
            {
                try
                {
                    dataTable.Columns[(OriginalColumnsList[i]==" "? OriginalColumnsList[i]: OriginalColumnsList[i].Trim())].SetOrdinal(i);
                    NumericdataTable.Columns[(OriginalColumnsList[i] == " " ? OriginalColumnsList[i] : OriginalColumnsList[i].Trim())].SetOrdinal(i);
                }
                catch (Exception ex)
                {
                }
            }
        }

        dataTable.PrimaryKey = null;
        NumericdataTable.PrimaryKey = null;
        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
            string ColName = dataTable.Columns[i].ColumnName==" "? dataTable.Columns[i].ColumnName: dataTable.Columns[i].ColumnName.Trim();
            if (!Array.Exists(OriginalColumnsList, x => x.ToUpper() == ColName.ToUpper()) && dataTable.Columns[i].ColumnName != "cmdLine" && dataTable.Columns[i].ColumnName != "_Style")
            {
                dataTable.Columns.Remove(dataTable.Columns[i].ColumnName);
                dataTable.AcceptChanges();

                NumericdataTable.Columns.Remove(NumericdataTable.Columns[i].ColumnName);
                NumericdataTable.AcceptChanges();
                i = i - 1;
            }
        }

        // rename column
        for (int i = 0; i < OriginalColumnsList.Length; i++)
        {
            try
            {
                dataTable.Columns[(OriginalColumnsList[i]==" "? OriginalColumnsList[i]: OriginalColumnsList[i].Trim())].ColumnName = (ReportColumnsList[i]==" "? ReportColumnsList[i]: ReportColumnsList[i].Trim());
                NumericdataTable.Columns[(OriginalColumnsList[i]==" "? OriginalColumnsList[i]: OriginalColumnsList[i].Trim())].ColumnName = (ReportColumnsList[i]==" "? ReportColumnsList[i]: ReportColumnsList[i].Trim());
            }
            catch (Exception ex)
            {
            }
        }

    }
}

