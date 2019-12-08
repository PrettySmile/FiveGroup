using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveGroup.Models;
using FiveGroup.ViewModel;

namespace FiveGroup.Controllers
{
    public class FeedbackController : Controller
    {
        Project2Entities db = new Project2Entities();
        // GET: Feedback
        public ActionResult Index(string f_class = "F0001")
        {
            string[] f_c = { "F0001", "F0002", "F0003", "F0004" };

            ViewBag.f_c01 = fb_count(f_c[0]);
            ViewBag.f_c02 = fb_count(f_c[1]);
            ViewBag.f_c03 = fb_count(f_c[2]);
            ViewBag.f_c04 = fb_count(f_c[3]);

            var a = db.feedback_class.Where(m => m.f_class == f_class);

            ViewBag.f_class = a.FirstOrDefault().f_class;
            ViewBag.class_content = a.FirstOrDefault().class_content;

            FeedbackClass fb = new FeedbackClass()
            {
                Feedbacks = db.feedback.Where(m => m.f_class == f_class).ToList(),
                Feedback_Classes = db.feedback_class.ToList()
            };
            
            return View(fb);
        }

        public ActionResult Create()
        {
            ViewBag.fb_class = new SelectList(db.feedback_class, "f_class", "class_content");
            return View();
        }

        [HttpPost]
        public ActionResult Create(feedback feedback)
        {


            /*************************獲取新ID**************************/
            var Fb_Desc = from m in db.feedback
                          orderby m.f_sn descending
                          select m.f_sn;
            int New_id = Fb_Desc.FirstOrDefault();
            New_id++;
            /**********************************************************/


            feedback.f_sn = New_id;
            feedback.f_date = DateTime.Now;
            feedback.f_checked = false;

            db.feedback.Add(feedback);
            db.SaveChanges();

            return Content("<script>alert('感謝您的回報~!!');</script>");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var feedback = db.feedback.Where(m => m.f_sn == id).FirstOrDefault();
            ViewBag.fb_class = new SelectList(db.feedback_class, "f_class", "class_content");

            return View(feedback);
        }
        [HttpPost]
        public ActionResult Edit(int f_sn, string f_class, string f_content, DateTime f_date, Boolean f_checked)
        {
            

            var feedback = db.feedback.Where(m => m.f_sn == f_sn).FirstOrDefault();
            feedback.f_sn = f_sn;
            feedback.f_class = f_class;
            feedback.f_content = f_content;
            feedback.f_date = f_date;
            feedback.f_checked = f_checked;

            db.SaveChanges();
            return RedirectToAction("Index", new { f_class = f_class });
        }

        public ActionResult Details(int? id, string f_class = "F0001")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.f_class = db.feedback_class.Where(m => m.f_class == f_class).FirstOrDefault().f_class;
            FeedbackClass fb = new FeedbackClass()
            {
                Feedbacks = db.feedback.Where(m => m.f_sn == id).ToList(),
                Feedback_Classes = db.feedback_class.Where(m => m.f_class == f_class).ToList()
            };

            return View(fb);
        }


        public ActionResult Delete(int? id, string f_class)
        {
            var feedback = db.feedback.Where(m => m.f_sn == id).FirstOrDefault();
            db.feedback.Remove(feedback);
            db.SaveChanges();

            return RedirectToAction("Index", new { f_class = f_class });
        }

 

        public int fb_count(string f_class) {

            var f_c_num = from m in db.feedback
                    where
                      m.f_class == f_class &&
                      m.f_checked == false
                    select new
                    {
                        m.f_checked
                    };

            int f_c_count = f_c_num.Count();

            return f_c_count;
        }
    }
}