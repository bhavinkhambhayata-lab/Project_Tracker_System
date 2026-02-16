using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tracker_System.Classes;
using Tracker_System.Models;

namespace Tracker_System.Controllers
{
    public class TrackerSystemController : Controller
    {
        
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: TrackerSystem
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }        
        [HttpPost]
        public ActionResult Login(TrackerSystemModel objTS)
        {
            connection();
            con.Open();
            com.Connection = con;
            // com.CommandText = "select * from Master_UserMaster where UserName='" + objTS.UserName + "'and Password='" + objTS.Password + "'";          
            com.CommandText = @"
    SELECT U.*, E.MailID_Office 
    FROM Master_UserMaster U
    INNER JOIN Master_EmployeeMaster E ON U.EmployeeRowID = E.RowID
    WHERE U.UserName = @UserName AND U.Password = @Password";
            com.Parameters.AddWithValue("@UserName", objTS.UserName);
            com.Parameters.AddWithValue("@Password", objTS.Password);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                ClsTrackerSystem objTrackerSystem = new ClsTrackerSystem();
                DataSet ds;
                DataSet ds1;
                DataSet DSU;
                ds = objTrackerSystem.UserImpge(objTS);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objTS.EmployeeRowId = Common.ConvertDBnullToInt32(ds.Tables[0].Rows[0]["EmployeeRowId"]);
                    Session["EmployeeRowId"] = objTS.EmployeeRowId + ".jpeg";
                    Session["EmplRowId"] = objTS.EmployeeRowId;
                    var TSFreightTracker = Common.ConvertDBnullToInt32(ds.Tables[0].Rows[0]["TS_FrieghtTracker"]);
                    Session["TS_FrieghtTracker"] = TSFreightTracker;

                    string currentEmail = dr["MailID_Office"] != DBNull.Value ? dr["MailID_Office"].ToString() : "";
                    Session["LoginEmail"] = currentEmail;
                    if (objTS.EmployeeRowId == 1093 && TSFreightTracker == 1)
                    {
                        Session["ExportSales"] = true;
                    }
                    else if(objTS.EmployeeRowId == 429 || objTS.EmployeeRowId == 534 && TSFreightTracker == 1)
                    {
                        Session["ExportSales"] = false;
                    }
                    else
                    {
                        Session["ExportSalesAll"] = "ALL";
                    }
                 /*   int currentEmployeeId = objTS.EmployeeRowId;
                    List<int> empIds = new List<int> { currentEmployeeId };*/
                    DataTable dtMenus = objTrackerSystem.GetUserMenuItems(objTS.EmployeeRowId);
                    Session["UserMenus"] = dtMenus;
                   

                }
                DSU = objTrackerSystem.UserFristLastName(objTS);
                if (DSU.Tables[0].Rows.Count > 0)
                {
                    Session["UserName"] = Common.ConvertDBnullToNullString(DSU.Tables[0].Rows[0]["Name"]);
                }
                 ds1 = objTrackerSystem.UserImpgePath(objTS);
                if (ds1 != null   && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    object imgObj = ds1.Tables[0].Rows[0]["Images"];
                    if (imgObj != DBNull.Value)
                    {
                        byte[] base64String = Common.ConvertDBnullTobyteNull(ds1.Tables[0].Rows[0]["Images"]);
                        using (MemoryStream ms = new MemoryStream(base64String))
                        {
                            using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms))
                            {
                                string folderPath = Server.MapPath("~/UserImage/");
                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                                string imagePath = Path.Combine(folderPath, objTS.EmployeeRowId + ".jpeg");
                                image.Save(imagePath);
                            }
                        }

                    }
                }
                con.Close();
                return View("Index");
            }
            else
            {
                con.Close();
                return View("Login");            
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["EmplRowId"] == null)
            {
                return RedirectToAction("Login", "TrackerSystem");
            }

            return View();
        }
      
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Login", "TrackerSystem");
        }
    }
  }