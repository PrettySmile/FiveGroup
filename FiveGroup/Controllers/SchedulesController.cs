using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FiveGroup.Models;
using FiveGroup.ViewModel;
using PagedList.Mvc;
using PagedList;

namespace FiveGroup.Controllers
{
    public class ScheduleController : Controller
    {
        private Project2Entities db = new Project2Entities();
        private const int PageSize = 20;

        public ActionResult Index(int SchedulePage=1)
        {
            IQueryable<schedule> schedules;
            schedules = from m in db.schedule
                       select m;

            int pageDataSize = 20;
            int pageCurrent = SchedulePage < 1 ? 1 : SchedulePage;
            IPagedList<schedule> Schedulepagedlist = schedules.ToList().ToPagedList(pageCurrent, pageDataSize);
            ScheduleView scheduleView = new ScheduleView()
            {
                Doctors = db.doctor.ToList(),
                Schedules = db.schedule.ToList(),
                Hospitals = db.hospital.ToList(),
                schedulesList = Schedulepagedlist
            };

            return View(scheduleView);

        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sn,doc_id,dep_id,hos_id,starttime,endtime,s_display")] schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.schedule.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var schedule = db.schedule.Where(m => m.sn == id).FirstOrDefault();
            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sn,doc_id,dep_id,hos_id,starttime,endtime,s_display")] schedule schedule)
        {
            var newschedule = db.schedule.Where(m => m.sn == schedule.sn).FirstOrDefault();
            newschedule.starttime = schedule.starttime.ToLocalTime().ToUniversalTime();
            newschedule.endtime = schedule.endtime.ToLocalTime().ToUniversalTime();
            newschedule.s_display = schedule.s_display;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var schedule = db.schedule.Where(m => m.sn == id).FirstOrDefault();
            db.schedule.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
