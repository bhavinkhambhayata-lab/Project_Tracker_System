using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker_System.Classes;
using Tracker_System.Models;

namespace Tracker_System.Controllers
{
    public class TrackerSystemController : Controller
    {
      ///  SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: TrackerSystem
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionStrings"].ToString();
            con = new SqlConnection(constr);
        }
        [HttpPost]
        public ActionResult Login(TrackerSystemModel objTS)
            {
            connection();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from Master_UserMaster where UserName='"+ objTS .UserName+ "'and Password='"+objTS.Password+"'";
            dr = com.ExecuteReader();
            if(dr.Read())
            {
                con.Close();
                return View("Index");               
            }
            else
            {
                con.Close();
                return View();
            }
        }
    }
  }