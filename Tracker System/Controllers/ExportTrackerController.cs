using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tracker_System.Classes;
using Tracker_System.Models;
using WebGrease.Activities;
namespace Tracker_System.Controllers
{
    public class ExportTrackerController : Controller
    {

      
        [HttpGet]
        public ActionResult Index()
        {
            var model = new Exporttracker();
            if (Session["AgentName"] != null)
            {
                model.CCClearance_Agent_Name = Session["AgentName"].ToString();
            }

            return View(model);
        }
        public ActionResult CreateShipmentTracker()
        {
            var mode = "";
            var model = new Shipmenttracker();


           

            string employeeName = "";
            if (System.Web.HttpContext.Current.Session["UserName"] != null)
            {
                employeeName = System.Web.HttpContext.Current.Session["UserName"].ToString();
            }


            return View(model);
        }
        public ActionResult CreateAdvanceReport()
        {
            var mode = "";
            var model = new AdvanceTracker();


           

            string employeeName = "";
            if (System.Web.HttpContext.Current.Session["UserName"] != null)
            {
                employeeName = System.Web.HttpContext.Current.Session["UserName"].ToString();
            }


            return View(model);
        }
        public ActionResult CreateExportTracker()
        {
            var mode = "";
            var model = new Exporttracker(); 

           
            model.OtherCharges = new List<Exporttracker_OtherCharges>();
            
            model.OtherCharges.Add(new Exporttracker_OtherCharges());
            ClsExportTracker helper = new ClsExportTracker();
            model.lstInco_Terms = helper.GetAllListforIncoTerms();
        
            model.lstCleringPoint = helper.GetAllListCleringPoint();
            model.lstFCL = helper.GetAllListFCL(mode);
            List<string> invoiceNumbers = helper.GetInvoiceNumbers(); // however you fetch it
            ViewBag.InvoiceList = invoiceNumbers;
            var searchTypes = new List<SelectListItem>
                    {
                  
  
                        new SelectListItem { Text = "Pending", Value = "Pending", Selected = true },
                      new SelectListItem { Text = "Completed", Value = "Completed" },
                                            new SelectListItem { Text = "All", Value = "All", Selected = true }
                    };
            ViewBag.SearchTypes = searchTypes;

           string employeeName = "";
            if (System.Web.HttpContext.Current.Session["UserName"] != null)
            {
                employeeName =System.Web.HttpContext.Current.Session["UserName"].ToString();
            }


            return View(model);
        }
  
        public JsonResult GetDataInsertdetails(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();    
                bool isInserted = helper.GetDataInsertData(rowsData); 
                string status = isInserted ? "Inserted" : "Failed";

                return Json(new
                {
                    Status = isInserted,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
        [HttpGet]

        public JsonResult GetColumnWiseSuggestions(string column_Name, string term_value = "")
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();
                var data = helper.GetColumnWiseSuggestions();

               
                var dict = data
                    .GroupBy(x => x.Key)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.Value).ToList());

                if (dict.TryGetValue(column_Name, out List<string> values))
                {
                    if (!string.IsNullOrEmpty(term_value))
                    {
               
                        values = values
                            .Where(v => v != null && v.IndexOf(term_value, StringComparison.OrdinalIgnoreCase) >= 0)
                            .Distinct()
                            .ToList();
                    }

                    return Json(new { values = values }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { values = new List<string>() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckRecordExists(string invNo)
        {
            ClsExportTracker helper = new ClsExportTracker();    
            bool exists = helper.RecordExists(invNo);
            return Json(new { exists });
        }
    
    
        public JsonResult GetDataUpdateClearance(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();    
                bool isUpdated = helper.GetDataInsertClearanceData(rowsData); 
                string status = isUpdated ? "Update" : "Failed";

                return Json(new
                {
                    Status = isUpdated,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
   
        public JsonResult GetDataUpdateForwarderCharges(Exporttracker rowsDataList)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();
                List<object> resultRecords = new List<object>();

               
                    bool isUpdated = false;
                    string methodUsed = "";

                    if (rowsDataList.Forwarder_Details == "Sea")
                    {
                        isUpdated = helper.GetDataInsertForwarderSeaData(rowsDataList);
                        methodUsed = "Sea Method";
                    }
                    else if (rowsDataList.Forwarder_Details == "Air")
                    {
                        isUpdated = helper.GetDataInsertForwarderAirData(rowsDataList);
                        methodUsed = "Air Method";
                    }
                    else
                    {
                        resultRecords.Add(new
                        {
                            InvoiceNo = rowsDataList.Invoice_No,
                            ForwarderType = rowsDataList.Forwarder_Details,
                            Status = "Skipped - Unknown Forwarder Type"
                        });
                      
                    }

                    string status = isUpdated ? "Update" : "Failed";
                    resultRecords.Add(new
                    {
                        InvoiceNo = rowsDataList.Invoice_No,
                        ForwarderType = rowsDataList.Forwarder_Details,
                        Status = status,
                        MethodUsed = methodUsed
                    });
                

                return Json(new
                {
                    Status = true,
                    Records = resultRecords
                });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
  
        public JsonResult GetDataUpdateCFS(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();    
                bool isUpdated = helper.GetDataInsertCFSData(rowsData); 
                string status = isUpdated ? "Update" : "Failed";

                return Json(new
                {
                    Status = isUpdated,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
       
        public JsonResult GetDataUpdateTransportation(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();    
                bool isUpdated = helper.GetDataInsertTCData(rowsData);
                string status = isUpdated ? "Update" : "Failed";

                return Json(new
                {
                    Status = isUpdated,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
     
        public JsonResult GetDataUpdateAddTransportation(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();    
                bool isUpdated = helper.GetDataInsertAddTCData(rowsData); 
                string status = isUpdated ? "Update" : "Failed";

                return Json(new
                {
                    Status = isUpdated,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
      
        public JsonResult GetAllDataUpdate(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();    
                bool isUpdated = helper.GetAllDataFinalSave(rowsData); 
                string status = isUpdated ? "Update" : "Failed";

                return Json(new
                {
                    Status = isUpdated,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
        //public JsonResult GetAllDataEnable(String InvNo, Boolean FinalSave)
        //{
        //    try
        //    {
        //        ClsExportTracker helper = new ClsExportTracker();    // Call helper class
        //        bool isUpdated = helper.GetAllDataFinalSave(rowsData); // << Call your method here
        //        string status = isUpdated ? "Update" : "Failed";

        //        return Json(new
        //        {
        //            Status = isUpdated,
        //            Records = new[] {
        //        new {
        //            InvoiceNo = rowsData.Invoice_No,
        //            Status = status
        //        }
        //    }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
        //    }
        //}
        public JsonResult GetDataUpdateCOO(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();  
                bool isUpdated = helper.GetDataInsertCOOCData(rowsData); 
                string status = isUpdated ? "Update" : "Failed";

                return Json(new
                {
                    Status = isUpdated,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
        public JsonResult GetShipmentDataUpdate(List<Shipmenttracker> rowsData)
        {
            bool allUpdatesSuccessful = true;
            ClsExportTracker ObjClsComTracker = new ClsExportTracker();
            var result = new List<object>();
            if (rowsData != null && rowsData.Count > 0)
            {
                try
                {
                    foreach (var row in rowsData)
                    {

                    
                
                        bool isUpdated = false;
                        bool updated = ObjClsComTracker.GetShipmentDataUpdate(row);
                        isUpdated = true;
                        // Check if record exists first
                      
                        result.Add(new
                        {

                            Status = isUpdated ? "Updated" : "Skipped"
                        });
                    }

                    return Json(new
                    {
                        Status = true,
                        Records = result
                    });

                    
                }
                catch (Exception ex)
                {
                    return Json(new { returnVal = false, MSG = $"Error updating records: {ex.Message}" });
                }
            }
            else
            {
                return Json(new { returnVal = false, MSG = "No data received" });
            }
        }

        public JsonResult GetAdvanceDataUpdate(AdvanceTracker rowData)
        {
            try
            {
                ClsExportTracker ObjClsComTracker = new ClsExportTracker();
                bool updated = ObjClsComTracker.GetAdvanceDataUpdate(rowData);

                return Json(new
                {
                    Status = updated,
                    Message = updated ? "Updated successfully" : "Update failed"
                });
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = $"Error: {ex.Message}" });
            }
        }
        //public JsonResult GetShipmentDataUpdate(List<Shipmenttracker> rowsData)
        //{
        //    if (rowsData == null || rowsData.Count == 0)
        //    {
        //        return Json(new { returnVal = false, MSG = "No data received" });
        //    }

        //    bool allUpdatesSuccessful = true;
        //    ClsExportTracker ObjClsComTracker = new ClsExportTracker();
        //    var result = new List<object>();

        //    try
        //    {
        //        foreach (var row in rowsData)
        //        {
        //            bool recordExists = ObjClsComTracker.RecordExists(row.Invoice_No);
        //            bool isUpdated = false;

        //            if (recordExists)
        //            {
        //                // Perform update only if record exists
        //                bool updated = ObjClsComTracker.GetShipmentDataUpdate(row);
        //                isUpdated = updated;
        //            }

        //            result.Add(new
        //            {
        //                Status = recordExists
        //                    ? (isUpdated ? "Updated" : "Failed")
        //                    : "Skipped"   // skipped means record not found — no insert attempted
        //            });
        //        }

        //        return Json(new
        //        {
        //            Status = true,
        //            Records = result
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { returnVal = false, MSG = $"Error updating records: {ex.Message}" });
        //    }
        //}


        public JsonResult GetDataUpdateEIA(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();    
                bool isUpdated = helper.GetDataInsertEIACData(rowsData); 
                string status = isUpdated ? "Update" : "Failed";

                return Json(new
                {
                    Status = isUpdated,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }

        public JsonResult GetDataUpdateCFSOtherCharges(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();


                int insertedCount = helper.GetDataInsertCFSOCData(rowsData);

                string status = insertedCount > 0 ? "Success" : "No records affected";

                return Json(new
                {
                    Status = insertedCount >= 0,
                    InsertedCount = insertedCount,
                    Message = status

                });

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new
                {
                    Status = false,
                    Message = $"🚨 Error inserting record: {ex.Message}"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDataUpdateOtherCharges(Exporttracker rowsData) 
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();


              int insertedCount = helper.GetDataInsertOCData(rowsData);


                if (rowsData != null && rowsData.OtherCharges.Count > 0)
                {
                    try
                    {
                        if (rowsData.OtherCharges != null && rowsData.finalsave && rowsData.OtherCharges.Any())
                        {
                            foreach (var charge in rowsData.OtherCharges)
                            {
                                var withBlock1 = rowsData.OtherCharges;

                                string FROM = "auto.mail@italiagroup.in";
                                // string TO = "recipient@italiagroup.in";  // <-- change or fetch dynamically
                                string CC = "";
                                string BCC = "";

                                string Body = "";
                                var e_mail = new MailMessage();
                                e_mail.From = new MailAddress(FROM);
                                e_mail.To.Add("Ketan.Kanodia@italiagroup.in, mahendra.thakor@italiagroup.in");
                                if (Session["LoginEmail"] != null && !string.IsNullOrWhiteSpace(Session["LoginEmail"].ToString()))
                                {
                                    e_mail.CC.Add(Session["LoginEmail"].ToString());
                                }
                                e_mail.CC.Add("nehal.revaliya@italiagroup.in");
                                //e_mail.To.Add(TO);
                                e_mail.Subject = "BL Surrender Invoice";
                                e_mail.IsBodyHtml = true;
                                Body += "<div style='font-family: Futura, Calibri, Arial, sans-serif; font-size:14px;'>";

                                Body += "Dear Sir,";
                                Body += "<br><br>";
                                Body += "This email is to confirm that the BL surrender invoice was handed over to Accounts on: " + charge.OC_BLAdvPaymenton;
                                Body += "<br><br>";
                                Body += "<b><u>BL Surrender Details:</u></b>";
                                Body += "<br><br>";
                                // ===== Table Header =====
                                Body += "<table cellspacing='0' border='1' cellpadding='4' width='70%' style='border-collapse:collapse;margin-left: 5rem;font-family: futura;font-size:15px;border:1px solid black;'>";
                                Body += "<tr bgcolor='lightgrey' align='center' style='font-weight:bold;border:1px solid black;'>";
                                Body += "<td style='border:1px solid black;'>Invoice No</td>";
                                Body += "<td style='border:1px solid black;'>Vendor Name</td>";
                                Body += "<td style='border:1px solid black;'>Vendor Invoice No</td>";
                                Body += "<td style='border:1px solid black;'>Invoice Date</td>";
                                Body += "<td style='border:1px solid black;'>Particulars</td>";
                                Body += "<td style='border:1px solid black;'>Amount Before GST</td>";
                                Body += "<td style='border:1px solid black;'>GST %</td>";
                                Body += "<td style='border:1px solid black;'>GST Amount</td>";
                                Body += "<td style='border:1px solid black;'>Total</td>";
                                Body += "<td style='border:1px solid black;'>Account Submitted Date</td>";
                                Body += "<td style='border:1px solid black;'>Payment Date</td>";
                                Body += "</tr>";


                                // ===== Table Rows =====
                               
                                Body += "<tr align='center'>";
                                Body += "<td style='text-align: left; border:1px solid black;'>" + rowsData.Invoice_No+ "</td>";
                                Body += "<td style='text-align: left;border:1px solid black;'>" + charge.OC_Vendor+ "</td>";
                                Body += "<td style='text-align: left;border:1px solid black;'>" + charge.OC_Invoice_No + "</td>";
                                Body += "<td style='border:1px solid black;'>" + Convert.ToDateTime(charge.OC_InvDate).ToString("dd/MM/yy") + "</td>";
                                  
                                Body += "<td style='border:1px solid black;'>" + charge.OC_Particulars + "</td>";
                                Body += "<td  style='text-align: right;border:1px solid black;'>" + charge.OC_Amt_Before_GST + "</td>";
                                Body += "<td style='text-align: right;border:1px solid black;'>" + charge.OC_GSTPercentage + "</td>";
                                Body += "<td style='text-align: right;border:1px solid black;'>" + charge.OC_GSTAmount + "</td>";
                                Body += "<td style='text-align: right;border:1px solid black;'>" + charge.OC_Total + "</td>";
                                Body += "<td style='border:1px solid black;'>" + Convert.ToDateTime(charge.OC_BLAdvPaymenton).ToString("dd/MM/yy") + "</td>";
                                Body += "<td style='border:1px solid black;'>" + Convert.ToDateTime(charge.OC_BLPaymentDate).ToString("dd/MM/yy") + "</td>";
                                Body += "</tr>";
                     
                                Body += "</table>";

                                Body += "<br><br>";
                           
                                Body += "Note: Please do not reply to this mail as it is a computer generated mail.";
                             
                                Body += "<br><br>";
                                Body += "</div>";
                                e_mail.Body = Body;

                                // === Configure SMTP ===
                                SmtpClient smtpClient = new SmtpClient();
                                smtpClient.UseDefaultCredentials = false;
                                smtpClient.Credentials = new System.Net.NetworkCredential("auto.mail@italiagroup.in", "your-app-password");
                                smtpClient.Port = 587;
                                smtpClient.EnableSsl = true;
                                smtpClient.Host = "smtp.gmail.com";

                                smtpClient.Send(e_mail);
                            }
                        }
                   
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while sending mail: " + ex.Message);
                    }
                }



                string status = insertedCount > 0 ? "Success" : "No records affected";

                return Json(new
                {
                    Status = insertedCount >= 0,
                    InsertedCount = insertedCount,
                   Message = status
              
                });
                
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new
                {
                    Status = false,
                    Message = $"🚨 Error inserting record: {ex.Message}"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteOtherChargeRecords(Exporttracker model)
        {
            try
            {
                ClsExportTracker tracker = new ClsExportTracker();
                bool result = tracker.DeleteOtherChargeByRowId(model);

                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public JsonResult DeleteCFSOtherChargeRecords(Exporttracker model)
        {
            try
            {
                ClsExportTracker tracker = new ClsExportTracker();
                bool result = tracker.DeletecfsOtherChargeByRowId(model);

                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public JsonResult GetDataunloack(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();
                bool isUpdated = helper.GetDataunloack(rowsData);
                string status = isUpdated ? "Update" : "Failed";

                return Json(new
                {
                    Status = isUpdated,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
        public JsonResult GetDataUpdateDocumnetDetails(Exporttracker rowsData)
        {
            try
            {
                ClsExportTracker helper = new ClsExportTracker();   
                bool isUpdated = helper.GetDataInsertDCData(rowsData); 
                string status = isUpdated ? "Update" : "Failed";

                return Json(new
                {
                    Status = isUpdated,
                    Records = new[] {
                new {
                    InvoiceNo = rowsData.Invoice_No,
                    Status = status
                }
            }
                });
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = false, MSG = $"Error inserting record: {ex.Message}" });
            }
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dt = new DataTable(typeof(T).Name);

            // Get all properties
            var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var prop in props)
            {
                // Nullable types should be handled
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dt.Columns.Add(prop.Name, type);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null) ?? DBNull.Value;
                }
                dt.Rows.Add(values);
            }

            return dt;
        }

        public ActionResult ExportReport(String SearchType = " ")
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                CommonController ObjClsExportTracker = new CommonController();
                var exportListResult = ObjClsExportTracker.GetExportGridList(SearchType) as JsonResult; 
               
                var exportList = exportListResult?.Data as List<Exporttracker>;
       

                DataTable dt =ToDataTable(exportList);

                // If you want a DataSet
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
              


                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("ExportTracker Report");

                    int headerRow = 1;       // Main header starts here
                    int dataStartRow = headerRow + 3; // Data starts after 3 rows
                    int col = 1;

                    // ===== MAIN HEADERS =====
                    // Invoice Details (merge 3 rows)
                    ws.Cells[headerRow, col, headerRow + 1, col + 28].Merge = true;
                    ws.Cells[headerRow, col].Value = "Invoice Details";
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Size = 14;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col += 29;

                    // Clearance Charges (merge 3 rows)
                    ws.Cells[headerRow, col, headerRow + 1, col + 3].Merge = true;
                    ws.Cells[headerRow, col].Value = "Clearance Charges";
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Size = 14;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col += 4;

                    // Forwarder Charges (merge only 1 row, subheaders in row 2)
                    ws.Cells[headerRow, col, headerRow, col + 4].Merge = true;
                    ws.Cells[headerRow, col].Value = "Forwarder Charges";
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Size = 14;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;

                    // Subheaders for Forwarder Charges (row 2)
                    ws.Cells[headerRow + 1, col].Value = ""; // blank
                    ws.Cells[headerRow + 1, col + 3].Value = "Sea";
                    ws.Cells[headerRow + 1, col + 3].Style.Font.Size = 14;
                    ws.Cells[headerRow + 1, col + 4].Value = "Air";
                    ws.Cells[headerRow + 1, col + 4].Style.Font.Size = 14;
                    ws.Cells[headerRow + 1, col, headerRow + 1, col + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow + 1, col, headerRow + 1, col + 4].Style.Font.Bold = true;
                    col += 5;

                    // Transportation Charges (merge 2 rows)
                    ws.Cells[headerRow, col, headerRow + 1, col + 3].Merge = true;
                    ws.Cells[headerRow, col].Value = "Transportation Charges";
                    ws.Cells[headerRow, col].Style.Font.Size = 14;
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col += 4;

                    // CFS Charges (merge 2 rows)
                    ws.Cells[headerRow, col, headerRow + 1, col + 3].Merge = true;
                    ws.Cells[headerRow, col].Value = "CFS Charges";
                    ws.Cells[headerRow, col].Style.Font.Size = 14;
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col += 4;

                    // Additional Transportation Charges (merge 2 rows)
                    ws.Cells[headerRow, col, headerRow + 1, col + 3].Merge = true;
                    ws.Cells[headerRow, col].Value = "Additional Transportation Charges";
                    ws.Cells[headerRow, col].Style.Font.Size = 14;
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col += 4;

                    // COO Charges (merge 2 rows)
                    ws.Cells[headerRow, col, headerRow + 1, col + 3].Merge = true;
                    ws.Cells[headerRow, col].Value = "COO Charges";
                    ws.Cells[headerRow, col].Style.Font.Size = 14;
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col += 4;

                    // Export Inspection Agency Charges (merge 2 rows)
                    ws.Cells[headerRow, col, headerRow + 1, col + 3].Merge = true;
                    ws.Cells[headerRow, col].Value = "Export Inspection Agency Charges";
                    ws.Cells[headerRow, col].Style.Font.Size = 14;
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col += 4;

                    // Other Charges (repeat 3 times, merge 2 rows each)
                    for (int i = 0; i < 3; i++)
                    {
                        ws.Cells[headerRow, col, headerRow + 1, col + 3].Merge = true;
                        ws.Cells[headerRow, col].Value = "Other Charges (IF ANY)";
                        ws.Cells[headerRow, col].Style.Font.Size = 14;
                        ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[headerRow, col].Style.Font.Bold = true;
                        col += 4;
                    }

                    // Total / Avg / Document Details
                    ws.Cells[headerRow, col, headerRow + 1, col].Merge = true;
                    ws.Cells[headerRow, col].Value = "Total EXP (Excluding GST)";
                   
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col++;

                    ws.Cells[headerRow, col, headerRow + 1, col].Merge = true;
                    ws.Cells[headerRow, col].Value = "Average Cost Per SQM (Excluding GST)";

                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col++;

                    ws.Cells[headerRow, col, headerRow + 1, col].Merge = true;
                    ws.Cells[headerRow, col].Value = "Average Cost Per FCL (Excluding GST)";
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;
                    col++;

                    ws.Cells[headerRow, col, headerRow + 1, col + 3].Merge = true;
                    ws.Cells[headerRow, col].Value = "Document Details";
                    ws.Cells[headerRow, col].Style.Font.Size = 14;
                    ws.Cells[headerRow, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[headerRow, col].Style.Font.Bold = true;

                    // ===== SUBHEADERS ROW (row 3) =====
                    col = 1;
                    string[] invoiceSubHeaders = new string[]
                    {
        "Sr No","Division","Mfr/Out","InvoiceNo","InvDate","Bill To Name","Ship To Name",
        "Shipping Bill No.","Shipping Bill Date","EPCG Licence No.","Currency","Basic Amt Charges",
        "Sea Freight / Air Freight","Insurance Charged","Other Charged","Invoice Value","Quntity(SQM)",
        "Air Way Bill No / Bill of Lading No","Air Way Bill Date / Bill of Lading Date",
        "Clearing Point","Port of Lading","Port of discharge","Mode","Type","No of FCL","Country",
        "Inco Terms","Payment terms","Remarks"
                    };
                    foreach (var header in invoiceSubHeaders)
                    {
                        ws.Cells[headerRow + 2, col].Value = header;
                        ws.Cells[headerRow + 2, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[headerRow + 2, col].Style.Font.Bold = true;
                        col++;
                    }

                    // Clearance Charges
                    ws.Cells[headerRow + 2, col].Value = "Clearance Agent";
                    ws.Cells[headerRow + 2, col+1].Value = "Invoice No";
                    ws.Cells[headerRow + 2, col+2].Value = "InvDate";
                    ws.Cells[headerRow + 2, col + 3].Value = "Total";
                    col += 4;

                    // Forwarder Charges
                    ws.Cells[headerRow + 2, col].Value = "Forwarder Name";
                    ws.Cells[headerRow + 2, col+1].Value = "Invoice No";
                    ws.Cells[headerRow + 2, col+2].Value = "InvDate";
                    ws.Cells[headerRow + 2, col + 3].Value = "Total";
                    ws.Cells[headerRow + 2, col + 4].Value = "Total";
                    col += 5;

                    // Transportation Charges
                    ws.Cells[headerRow + 2, col].Value = "Transporter";
                    ws.Cells[headerRow + 2, col+1].Value = "Invoice No";
                    ws.Cells[headerRow + 2, col+2].Value = "InvDate";
                    ws.Cells[headerRow + 2, col + 3].Value = "Total";
                    col += 4;

                    // CFS Charges
                    ws.Cells[headerRow + 2, col].Value = "Vendor Name";
                    ws.Cells[headerRow + 2, col+1].Value = "Invoice No";
                    ws.Cells[headerRow + 2, col + 2].Value = "InvDate";
                    ws.Cells[headerRow + 2, col + 3].Value = "Total";
                    col += 4;

                    // Additional Transportation Charges
                    ws.Cells[headerRow + 2, col].Value = "AdditionalTransporter";
                    ws.Cells[headerRow + 2, col+1].Value = "Invoice No";
                    ws.Cells[headerRow + 2, col+2].Value = "InvDate";
                    ws.Cells[headerRow + 2, col + 3].Value = "Total";
                    col += 4;

                    // COO Charges
                    ws.Cells[headerRow + 2, col].Value = "Vendor Name";
                    ws.Cells[headerRow + 2, col + 1].Value = "Invoice No";
                    ws.Cells[headerRow + 2, col + 2].Value = "InvDate";
                    ws.Cells[headerRow + 2, col + 3].Value = "Total";
                    col += 4;

                    // Export Inspection Agency Charges
                    ws.Cells[headerRow + 2, col].Value = "Vendor";
                    ws.Cells[headerRow + 2, col+1].Value = "Invoice No";
                    ws.Cells[headerRow + 2, col+2].Value = "InvDate";
                    ws.Cells[headerRow + 2, col + 3].Value = "Total";
                    col += 4;

                    // Other Charges (repeat 3 times)
                    for (int i = 0; i < 3; i++)
                    {
                        ws.Cells[headerRow + 2, col].Value = "Vendor";
                        ws.Cells[headerRow + 2, col+1].Value = "Invoice No";
                        ws.Cells[headerRow + 2, col+2].Value = "InvDate";
                        ws.Cells[headerRow + 2, col + 3].Value = "Total";
                        col += 4;
                    }

                    // Total / Avg / Document Details
                    ws.Cells[headerRow + 2, col].Value = "Total";
                    col++;
                    ws.Cells[headerRow + 2, col].Value = "SQM";
                    col++;
                    ws.Cells[headerRow + 2, col].Value = "FCL";
                    col++;
                    ws.Cells[headerRow + 2, col].Value = "Courier Airway Bill No";
                    col++;
                    ws.Cells[headerRow + 2, col].Value = "Document Sent Through";
                    col++;
                    ws.Cells[headerRow + 2, col].Value = "Document Courier Date";
                    col++;
                    ws.Cells[headerRow + 2, col].Value = "Document Submitted Account On";

                    // ===== DATA ROWS =====
                    int dataRow = dataStartRow;
                    foreach (var item in exportList)
                    {
                        int c = 1;
                        ws.Cells[dataRow, c++].Value = item.SrNo;
                        ws.Cells[dataRow, c++].Value = item.Division;
                        ws.Cells[dataRow, c++].Value = item.Mfr_Out;
                        ws.Cells[dataRow, c++].Value = item.Invoice_No;
                        ws.Cells[dataRow, c++].Value = item.InvDate;
                        ws.Cells[dataRow, c++].Value = item.Bill_to_Name;
                        ws.Cells[dataRow, c++].Value = item.Ship_to_Name;
                        ws.Cells[dataRow, c++].Value = item.Shipping_Bill_No;
                        ws.Cells[dataRow, c++].Value = item.Shipping_Bill_Date;
                        ws.Cells[dataRow, c++].Value = item.EPCG_Licence_No;
                        ws.Cells[dataRow, c++].Value = item.Currency;
                        ws.Cells[dataRow, c++].Value = item.Basic_Amt_Charges;
                        ws.Cells[dataRow, c++].Value = item.Sea_Freight_Air_Freight;
                        ws.Cells[dataRow, c++].Value = item.Insurarnce_charged;
                        ws.Cells[dataRow, c++].Value = item.Other_Charges;
                        ws.Cells[dataRow, c++].Value = item.Invoice_Value; 
                        ws.Cells[dataRow, c++].Value = item.Quntity_SQM;
                        ws.Cells[dataRow, c++].Value = item.AirWay_BillNo_Bill_of_LodingNo;
                        ws.Cells[dataRow, c++].Value = item.AirWay_BillDate_Bill_of_LodingDate;
                        ws.Cells[dataRow, c++].Value = item.Clearing_Point;
                        ws.Cells[dataRow, c++].Value = item.Port_of_Loading;
                        ws.Cells[dataRow, c++].Value = item.Port_of_Discharge;
                        ws.Cells[dataRow, c++].Value = item.Mode;
                        ws.Cells[dataRow, c++].Value = item.Type;
                        ws.Cells[dataRow, c++].Value = item.No_of_FCL;
                        ws.Cells[dataRow, c++].Value = item.Country;
                        ws.Cells[dataRow, c++].Value = item.Inco_Terms;
                        ws.Cells[dataRow, c++].Value = item.Payment_Terms;
                        ws.Cells[dataRow, c++].Value = item.Remarks;

                        // Clearance Charges
                        ws.Cells[dataRow, c++].Value = item.CCClearance_Agent_Name;
                        ws.Cells[dataRow, c++].Value = item.CCInvoice_No;
                        ws.Cells[dataRow, c++].Value = item.CCInvDate;
                        ws.Cells[dataRow, c++].Value = item.CCAmt_Before_GST;

                        // Forwarder Charges
                        ws.Cells[dataRow, c++].Value = item.FCForwarder;
                        ws.Cells[dataRow, c++].Value = item.FCInvoice_No;
                        ws.Cells[dataRow, c++].Value = item.FCInvDate;
                        ws.Cells[dataRow, c++].Value = item.FCSAmt_Before_GST;
                        ws.Cells[dataRow, c++].Value = item.FCAAMT_before_GST;

                        // Transportation Charges
                        ws.Cells[dataRow, c++].Value = item.TC_Transporter;
                        ws.Cells[dataRow, c++].Value = item.TC_Invoice_No;
                        ws.Cells[dataRow, c++].Value = item.TC_InvDate;
                        ws.Cells[dataRow, c++].Value = item.TC_AMT_Before_GST;

                        // CFS Charges
                        ws.Cells[dataRow, c++].Value = item.CFS_Vendor;
                        ws.Cells[dataRow, c++].Value = item.CFS_Invoice_No;
                        ws.Cells[dataRow, c++].Value = item.CFS_InvDate;
                        ws.Cells[dataRow, c++].Value = item.CFS_Amt_Before_GST;

                        // Additional Transportation Charges
                        ws.Cells[dataRow, c++].Value = item.AddTC_Transporter;
                        ws.Cells[dataRow, c++].Value = item.AddTC_Invoice_No;
                        ws.Cells[dataRow, c++].Value = item.AddTC_InvDate;
                        ws.Cells[dataRow, c++].Value = item.AddTC_AMT_Before_GST;

                        // COO Charges
                        ws.Cells[dataRow, c++].Value = item.COO_Vendor;
                        ws.Cells[dataRow, c++].Value = item.COO_Invoice_No;
                        ws.Cells[dataRow, c++].Value = item.COO_InvDate;
                        ws.Cells[dataRow, c++].Value = item.COO_Amt_Before_GST;

                        // Export Inspection Agency Charges
                        ws.Cells[dataRow, c++].Value = item.EIA_Vendor;
                        ws.Cells[dataRow, c++].Value = item.EIA_Invoice_No;
                        ws.Cells[dataRow, c++].Value = item.EIA_InvDate;
                        ws.Cells[dataRow, c++].Value = item.EIA_Amt_Before_GST;

                        // Other Charges (repeat 3 times)
                        for (int i = 0; i < 3; i++)
                        {
                            if (item.OtherCharges != null && i < item.OtherCharges.Count)
                            {
                                ws.Cells[dataRow, c++].Value = item.OtherCharges[i].OC_Vendor;
                                ws.Cells[dataRow, c++].Value = item.OtherCharges[i].OC_Invoice_No;
                                ws.Cells[dataRow, c++].Value = item.OtherCharges[i].OC_InvDate;
                                ws.Cells[dataRow, c++].Value = item.OtherCharges[i].OC_Amt_Before_GST;
                            }
                            else
                            {
                                ws.Cells[dataRow, c++].Value = "";
                                ws.Cells[dataRow, c++].Value = "";
                                ws.Cells[dataRow, c++].Value = "";
                                ws.Cells[dataRow, c++].Value = "";
                            }
                        }

                        // Totals / Avg / Document Details
                        ws.Cells[dataRow, c++].Value = item.FinalTotal;
                        ws.Cells[dataRow, c++].Value = item.AvgCostPerSQM;
                        ws.Cells[dataRow, c++].Value = item.AvgCostPerFCL;
                        ws.Cells[dataRow, c++].Value = item.Doc_ForwardedNo;
                        ws.Cells[dataRow, c++].Value = item.Doc_Sent_Through;
                        ws.Cells[dataRow, c++].Value = item.Doc_Date;
                        ws.Cells[dataRow, c++].Value = item.Doc_Submitted_Account_On;

                        dataRow++;
                    }
                    for (int r = headerRow; r <= headerRow + 2; r++)
                    {
                        ws.Row(r).Height = 35;
                        ws.Cells[r, 1, r, col].Style.WrapText = true;
                        ws.Cells[r, 1, r, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[r, 1, r, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells[r, 1, r, col].Style.Font.Bold = true;
                    }
                    int lastDataRow = dataRow - 1;

                    //                // list of numeric/amount columns
                    int[] amountCols = new int[]
                    {
                        12,  // Basic Amt Charges
                        13,  // Sea Freight / Air Freight
                        14,  // Insurance Charged
                        15,  // Other Charged
                        16,  // Invoice Value
                        17,  // Invoice Value
                        25,  // NO of fcl 
                        33,  // CC Total
                        37,  // FC Sea Total
                        38,  // FC Air Total
                        42,  // TC Total
                        46,  // CFS Total
                        50,  // AddTC Total (Additional Transportation)
                        54,  // COO Total
                        58,  // EIA Total
                        62, 66, 70, // Other Charges totals (3 blocks)
                        71, // Final Total
                        72, // Avg Cost Per SQM
                        73  // Avg Cost Per FCL
                    };
                    int subHeaderRow = headerRow + 2;

                    // Loop through each column
                    for (int i = 1; i <= col; i++)
                    {
                        // Get header text
                        string headerVal = ws.Cells[subHeaderRow, i].Text.Trim();
                        int maxLength = headerVal.Length;

                        // Check all data rows for max text length in that column
                        for (int r = dataStartRow; r < dataRow; r++)
                        {
                            string cellText = ws.Cells[r, i].Text?.Trim() ?? "";
                            if (cellText.Length > maxLength)
                                maxLength = cellText.Length;
                            if (amountCols.Contains(i))
                            {
                                ws.Cells[r, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                ws.Cells[r, i].Style.Numberformat.Format = "#,##0.00";
                            }
                            else if (headerVal.ToLower().Contains("date"))
                            {
                                ws.Cells[r, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                ws.Cells[r, i].Style.Numberformat.Format = "dd/MM/yy";
                            }
                            else
                            {
                                ws.Cells[r, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                        }

                        // Dynamically set width based on max length
                        if (maxLength > 50)
                            ws.Column(i).Width = 60;  // very wide
                        else if (maxLength > 35)
                            ws.Column(i).Width = 45;
                        else if (maxLength > 25)
                            ws.Column(i).Width = 30;
                        else if (maxLength > 15)
                            ws.Column(i).Width = 20;
                        else
                            ws.Column(i).Width = 14;
                     
                        
                    }
          
                    var chargeBlocks = new List<(int StartCol, int EndCol)>
                    {
                        (1,29),  // Invoice Amount Charges block
                        (30,33),  // Clearance Charges
                        (34,38),  // Forwarder Charges
                        (39,42),  // Transportation Charges
                        (43,46),  // CFS Charges
                        (47,50),  // Additional Transportation Charges
                        (51,54),  // COO Charges
                        (55,58),  // EIA Charges
                        (59,62),   // Other Charges + totals (you can split further if needed)
                        (63,66),   // Other Charges + totals (you can split further if needed)
                        (67,70),   // Other Charges + totals (you can split further if needed)
                        (71, 71),  // Total
                        (72, 72),  // SQM
                        (73, 73),  // FCL
                        (74, 77),  // Courier/Airway Bill

                        // Other Charges + totals (you can split further if needed)
                    };
                    foreach (var block in chargeBlocks)
                    {
                        int startCol = block.StartCol;
                        int endCol = block.EndCol;

                        // Header rows (row 1-2)
                        using (var range = ws.Cells[headerRow, startCol, headerRow + 1, endCol])
                        {
                            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                            // Only left of first column
                            ws.Cells[headerRow, startCol, headerRow + 1, startCol].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                            ws.Cells[headerRow, startCol, headerRow + 1, startCol].Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);

                            // Only right of last column
                            ws.Cells[headerRow, endCol, headerRow + 1, endCol].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                            ws.Cells[headerRow, endCol, headerRow + 1, endCol].Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                        }

                        // Subheader row (row 3)
                        using (var range = ws.Cells[headerRow + 2, startCol, headerRow + 2, endCol])
                        {
                            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws.Cells[headerRow + 2, startCol].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                            ws.Cells[headerRow + 2, startCol].Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);

                            ws.Cells[headerRow + 2, endCol].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                            ws.Cells[headerRow + 2, endCol].Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                        }

                        // Data rows
                        using (var range = ws.Cells[dataStartRow, startCol, lastDataRow, endCol])
                        {
                            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            ws.Cells[dataStartRow, startCol, lastDataRow, startCol].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                            ws.Cells[dataStartRow, startCol, lastDataRow, startCol].Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);

                            ws.Cells[dataStartRow, endCol, lastDataRow, endCol].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                            ws.Cells[dataStartRow, endCol, lastDataRow, endCol].Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                        }
                    }
           

                    byte[] fileBytes = pck.GetAsByteArray();
                    //System.IO.MemoryStream mStream = new System.IO.MemoryStream(fileBytes);
                    //mStream.Write(fileBytes, 0, fileBytes.Length);
                    //mStream.Position = 0;
                    //TempData["ExcelFileName"] = mStream;
                    string fileName = $"ExportTrackerReport_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                    return File(
              fileBytes,
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              fileName
          );
                   // return Json(new { success = true, filename = fileName }, JsonRequestBehavior.AllowGet);
                    //Session["ExcelFile"] = fileBytes;
                    //Session["ExcelFileName"] = fileName;

                    //return Json(new { success = true, fileName }, JsonRequestBehavior.AllowGet);
                }




            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Download(string filetype, string file)
        {
           // var mStream = TempData["ExcelFile"] as System.IO.MemoryStream;
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            try
            {
                mStream = (MemoryStream)TempData["ExcelFile"];
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            if (filetype.ToUpper() == "PDF")
                return File(mStream, "application/pdf", file);
            else
                return File(mStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", file);
        }
        //public ActionResult DownloadExporttrackerReport(string fileName)
        //{
        //    var bytes = Session["ExcelFile"] as byte[];
        //    if (bytes == null)
        //    {
        //        return HttpNotFound("File not available");
        //    }

        //    return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //}
    }
}