using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker_System.Models;
using Tracker_System.Classes;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using JsonResult = System.Web.Mvc.JsonResult;
using ActionResult = System.Web.Mvc.ActionResult;

namespace Tracker_System.Controllers
{
    public class CommissiontrackerController : Controller
    {
        // GET: Commisiontracker
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateCommissionTracker()
        {
           
            Commissiontracker ObjFT = new Commissiontracker();
            ClsCommissiontracker ObjClscCommissionTracker = new ClsCommissiontracker();
            ObjFT.lstFyear = ObjClscCommissionTracker.GetAllFyear();
            DateTime currentDate = DateTime.Now;
            var searchTypes = new List<SelectListItem>
                    {


                        new SelectListItem { Text = "Pending", Value = "Pending", Selected = true },
                      new SelectListItem { Text = "Completed", Value = "Completed" },
                      new SelectListItem { Text = "All", Value = "All" },

                    };
            ViewBag.SearchTypes = searchTypes;
            return View(ObjFT);

            // Your logic here
            //return View();
        }
        public ActionResult CreateTDCommissionTracker()
        {

            Commissiontracker ObjFT = new Commissiontracker();
            ClsCommissiontracker ObjClscCommissionTracker = new ClsCommissiontracker();
            ObjFT.lstFyear = ObjClscCommissionTracker.GetAllFyear();
            DateTime currentDate = DateTime.Now;
            var searchTypes = new List<SelectListItem>
                    {


                        new SelectListItem { Text = "Pending", Value = "Pending", Selected = true },
                      new SelectListItem { Text = "Completed", Value = "Completed" },
                      new SelectListItem { Text = "All", Value = "All" },

                    };
            ViewBag.SearchTypes = searchTypes;
            return View(ObjFT);

            // Your logic here
            //return View();
        }
        


        public ActionResult Nullvalue()
        {
            var model = new Commissiontracker();
            model.SrNo = "";  // or model.emty = null;
            model.TaggedOnPI = false;
           
            return View(model);
        }
        
        // For Delete recored 
        public JsonResult DetailsDeleteRecored(string PI_No, string OCF_No, string Invoice_No)
        {
            bool returnVal = false;
            try
            {
                ClsCommissiontracker ObjClsCommissionTracker = new ClsCommissiontracker();

             

                    returnVal = ObjClsCommissionTracker.GetDataDetailsDeleteRecoreds(PI_No, OCF_No, Invoice_No );
                        if (returnVal == true)
                        {
                            return Json(new { returnVal = returnVal, MSG = "Delete Recode" });
                        }
                        else
                        {
                            return Json(new { returnVal = 0, MSG = " Not Delete Recode" });
                        }
                   
               
            }
            catch (Exception ex)
            {
                return Json(new { returnVal = 0, MSG = "" });
            }
            return Json("");
        }
        [System.Web.Mvc.HttpPost]
        // for save data in details tab 
        public JsonResult GetDataUpadateDetails(string OCF_No,string Invoice_No,string PI_No,string ActualCommission, string Claimform_Received, string CommissionInvoice_Received, string CommissionworkingsenttoVendor_Employee, string Credit_Advice_Prepared, string Credit_Advice_Details,string Credit_Note_Prepared,  string Commission_Vendor_Name,string Vendor_Code,string FromNo, string CreditNotePreparedNo)
        {
            var result = new List<object>();
            ClsCommissiontracker ObjClsComTracker = new ClsCommissiontracker();

           
                        string success = ObjClsComTracker.GetDataUpadateDetails(OCF_No, Invoice_No, PI_No, ActualCommission, Claimform_Received, CommissionInvoice_Received, CommissionworkingsenttoVendor_Employee, Credit_Advice_Prepared, Credit_Advice_Details, Credit_Note_Prepared, Commission_Vendor_Name, Vendor_Code, FromNo, CreditNotePreparedNo);
            /*result.Add(new
            {
                Status = success ? "Updated or Inserted" : "Failed"
            });*/

            result.Add(new { Status = success });
            return Json(new
            {
                Status = success == "Updated" || success == "Inserted",
                Records = result
            });




        }

        //For PreMarketing update and insert recored
        public JsonResult GetDataUpadatePreMarketing(List<Commissiontracker> rowsData)
        {
            bool allUpdatesSuccessful = true;
            ClsCommissiontracker ObjClsComTracker = new ClsCommissiontracker();
            var result = new List<object>();
            if (rowsData != null && rowsData.Count > 0)
            {
                try
                {
                    foreach (var row in rowsData)
                    {
                     
                        var FromNo = row.FromNo;
                        var vendorNo = row.Vendor_Code;
                        bool isInserted = false;
                        bool isUpdated = false;

                        // Check if record exists first
                        if (ObjClsComTracker.RecordExists(FromNo, vendorNo))
                        {

                            bool updated = ObjClsComTracker.GetDataUpadatePreMarketing(row);
                            isUpdated = true;

                           
                        }
                        else
                        {
                            bool insert = ObjClsComTracker.GetDataUpadatePreMarketing(row);
                            allUpdatesSuccessful = false;
                            isInserted = true;
                           
                        }
                        result.Add(new
                        {

                            Status = isInserted ? "Inserted" : isUpdated ? "Updated" : "Skipped"
                        });
                    }

                    return Json(new
                    {
                        Status = true,
                        Records = result
                    });

                  /*  return Json(new
                    {
                        returnVal = allUpdatesSuccessful,
                        MSG = allUpdatesSuccessful ? "Records inserted/updated successfully!" : "Some records failed.",
                        Status = allUpdatesSuccessful
                    });*/
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

        //for commercial update and insert recored 
        public JsonResult GetDataUpadateCommercial(List<Commissiontracker> rowsData)
        {
            bool allUpdatesSuccessful = true;
            ClsCommissiontracker ObjClsComTracker = new ClsCommissiontracker();
            var result = new List<object>();

            if (rowsData != null && rowsData.Count > 0)
            {
                try
                {
                    foreach (var row in rowsData)
                    {
                       /* var CreditAdviceDetails = row.Credit_Advice_Details;
                        var CreditAdvicePrepared = row.Credit_Advice_Prepared;*/
                        var FromNo = row.FromNo;
                        var vendorNo = row.Vendor_Code;
                        bool isInserted = false;
                        bool isUpdated = false;

                        // Check if record exists first
                        if (ObjClsComTracker.RecordExists(FromNo, vendorNo))
                        {

                            bool updated = ObjClsComTracker.GetDataUpadateCommercial(row);
                            isUpdated = true;
                          
                        }
                        else
                        {
                            bool insert = ObjClsComTracker.GetDataUpadateCommercial(row);
                            allUpdatesSuccessful = false;
                            isInserted = true;
                            // You can log or collect the missing records for feedback
                        }
                        result.Add(new
                        {
                          
                            Status = isInserted ? "Inserted" : isUpdated ? "Updated" : "Skipped"
                        });
                    }
                    return Json(new
                    {
                        Status = true,
                        Records = result
                    });

                    /* return Json(new
                     {
                         returnVal = allUpdatesSuccessful,
                         MSG = allUpdatesSuccessful ? "Records inserted/updated successfully!" : "Some records failed.",
                         Status = allUpdatesSuccessful
                     });*/
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
        //Update post marketing  in commission tracker 
        public JsonResult GetDataUpadatePostMarketing(List<Commissiontracker> rowsData)
        {
            bool returnVal = false;
            ClsCommissiontracker ObjClsComTracker = new ClsCommissiontracker();
            if (rowsData != null && rowsData.Count > 0)
            {
                try
                {
                    for (int i = 0; i < rowsData.Count; i++)
                    {
                        var row = rowsData[i];
                        var CommissionInvoiceReceived = row.CommissionInvoice_Received;
                        var ClaimFormReceived = row.Claimform_Received;
                        var FromNo = row.FromNo;
                        var vendorNo = row.Vendor_Code;


                        if(CommissionInvoiceReceived != null && ClaimFormReceived != null)
                        {
                            returnVal = ObjClsComTracker.GetDataUpadatePostMarketing(CommissionInvoiceReceived, ClaimFormReceived, FromNo, vendorNo);
                        }
                      
                     
                    }
                    if (returnVal == true)
                    {
                        return Json(new { returnVal = returnVal, MSG = "Records updated successfully!", Status = true });
                    }
                    else
                    {
                        return Json(new { returnVal = false, MSG = "Error updating records!", Status = false });
                    }
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
        // update Logistic in commission tracker 
        public JsonResult GetDataUpadateLogistic(List<Commissiontracker> rowsData)
        {
            bool returnVal = false;
            ClsCommissiontracker ObjClsComTracker = new ClsCommissiontracker();
            if (rowsData != null && rowsData.Count > 0)
            {
                try
                {
                    for (int i = 0; i < rowsData.Count; i++)
                    {
                        var row = rowsData[i];
                        var CreditAdviceDetails = row.Credit_Advice_Details;
                        var CreditAdvicePrepared = row.Credit_Advice_Prepared;
                        var FromNo = row.FromNo;
                        var vendorNo = row.Vendor_Code;



                        returnVal = ObjClsComTracker.GetDataUpadateLogistic(CreditAdviceDetails,CreditAdvicePrepared, FromNo, vendorNo);
                    }
                    if (returnVal == true)
                    {
                        return Json(new { returnVal = returnVal, MSG = "Records updated successfully!", Status = true });
                    }
                    else
                    {
                        return Json(new { returnVal = false, MSG = "Error updating records!", Status = false });
                    }
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
        // update Account in commission tracker 
        public JsonResult GetDataUpadateAccount(List<Commissiontracker> rowsData)
        {
            bool allSuccess = true;
            bool anySuccess = false;
            ClsCommissiontracker ObjClsComTracker = new ClsCommissiontracker();
            if (rowsData != null && rowsData.Count > 0)
            {
                try
                {
                    foreach (var row in rowsData)
                    {
                        var success = ObjClsComTracker.GetDataUpadateAccount(
                                      row.Credit_Note_Prepared,
                                      row.FromNo,
                                      row.Vendor_Code,
                                      row.CreditNotePreparedNo
                                     );



                        if (success) anySuccess = true;
                        else allSuccess = false;
                    }

                    if (allSuccess)
                    {
                        return Json(new { returnVal = true, MSG = "All records updated successfully!", Status = true });
                    }
                    else if (anySuccess)
                    {
                        return Json(new { returnVal = false, MSG = "Some records updated, some failed!", Status = false });
                    }
                    else
                    {
                        return Json(new { returnVal = false, MSG = "No records were updated!", Status = false });
                    }
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


    }
}