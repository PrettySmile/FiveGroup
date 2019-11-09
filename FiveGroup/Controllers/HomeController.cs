using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveGroup.Models;

namespace FiveGroup.Controllers
{
    public class HomeController : Controller
    {
        Project2Entities db = new Project2Entities();
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
            var admin = db.administrator.Where(d => d.admin_account == account && d.admin_pass == password).FirstOrDefault();
            if (Session["account"] != null || admin != null)
            {
                if (admin != null)
                {
                    Session["account"] = account;
                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.LoginErr = "帳號或密碼有誤!!";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}