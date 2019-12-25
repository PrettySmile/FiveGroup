using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using FiveGroup.Models;
using System.Configuration;

namespace FiveGroup.Controllers
{
    public class HomeController : Controller
    {
        Project2Entities db = new Project2Entities();
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand Cmd = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        private void ExecuteSql(string sql)
        {
            Cmd.CommandText = sql;
            Cmd.Connection = Conn;

            Conn.Open();
            Cmd.ExecuteNonQuery();
            Conn.Close();
        }
        private DataTable QuerySql(string sql)
        {

            Cmd.CommandText = sql;
            Cmd.Connection = Conn;
            adp.SelectCommand = Cmd;
            DataSet ds = new DataSet();
            adp.Fill(ds);

            return ds.Tables[0];
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection post)
        {
            Session.Clear();
            string account = post["id"];
            string password = post["pwd"];
            string sql = "select * from administrator where admin_account =@account  and admin_pass =@password";
            Cmd.Parameters.AddWithValue("@account", account);
            Cmd.Parameters.AddWithValue("@password", password);
            Cmd.CommandText = sql;
            Cmd.Connection = Conn;
            SqlDataReader rd;
            Conn.Open();

            Session["ADM"] = account;

            rd = Cmd.ExecuteReader();
            if (rd.Read())
            {
                Session["account"] = rd["admin_account"].ToString();

                Conn.Close();
                return RedirectToAction("Index", "Announcement");
            }
            Conn.Close();
            TempData["LoginErr"] = "帳號或密碼有誤!!";
            return RedirectToAction("Index");
            //var admin = db.administrator.Where(d => d.admin_account == account && d.admin_pass == password).FirstOrDefault();
                    }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}