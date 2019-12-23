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

namespace FiveGroup.Controllers
{
    [LoginRule]
    public class HospitalsController : Controller
    {
        private Project2Entities db = new Project2Entities();

        // GET: Hospitals
        public ActionResult Index(int cityname = 1)
        {
            HospitalLocation HL = new HospitalLocation()
            {
                Hospitals = db.hospital.Where(m => m.c_id == cityname).ToList(),
                Cities = db.city.ToList(),
                Districts = db.district.ToList()
            };

            ViewBag.cityname = db.city.Where(m => m.c_id == cityname).FirstOrDefault().city_name;
            ViewBag.citynameID = cityname;

            return View(HL);

        }


        // GET: Hospitals/Create
        public ActionResult Create(int? cityname)
        {
            if (cityname == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            /*取資料筆數並呼叫New_Hos_id獲取新id，再透過ViewBag傳至前端*/
            var c = db.hospital.Count();
            string id;
            id = New_Hos_id(c);
            ViewBag.hos_id = id;
            /************************************************************/

            ViewBag.citynameID = cityname;
            ViewBag.cityname = new SelectList(db.city.Where(m => m.c_id == cityname), "c_id", "city_name");
            ViewBag.d_id = new SelectList(db.district.Where(m => m.c_id == cityname), "d_id", "district_name");
            return View();
        }

        // POST: Hospitals/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hos_id,hos_name,hos_eng_name,hos_address,hos_phone,hos_display,c_id,d_id")] hospital hospital, int cityname)
        {
            if (ModelState.IsValid)
            {
                db.hospital.Add(hospital);
                db.SaveChanges();
                return RedirectToAction("Index", new { cityname = cityname });
            }

            var c = db.hospital.Count();
            string id;
            id = New_Hos_id(c);
            ViewBag.hos_id = id;

            ViewBag.cityname = new SelectList(db.city.Where(m => m.c_id == cityname), "c_id", "city_name");
            ViewBag.d_id = new SelectList(db.district.Where(m => m.c_id == cityname), "d_id", "district_name");

            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        public ActionResult Edit(string id, int cityname)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hospital hospital = db.hospital.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }

            ViewBag.citynameID = cityname;
            ViewBag.c_id = new SelectList(db.city, "c_id", "city_name", hospital.c_id);
            ViewBag.d_id = new SelectList(db.district, "d_id", "district_name", hospital.d_id);
            return View(hospital);
        }

        // POST: Hospitals/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hos_id,hos_name,hos_eng_name,hos_address,hos_phone,hos_display,c_id,d_id")] hospital hospital, int cityname)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hospital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { cityname = cityname });
            }
            ViewBag.c_id = new SelectList(db.city, "c_id", "city_name", hospital.c_id);
            ViewBag.d_id = new SelectList(db.district, "d_id", "district_name", hospital.d_id);
            return View(hospital);
        }

        public string New_Hos_id(int id)
        {
            string Hos_id, h;
            id++;
            h = Convert.ToString(id);
            if (id < 10)
            {
                /*將id轉換為字串*/
                Hos_id = "H000" + h;
            }
            else if (id >= 10 && id < 100)
            {

                Hos_id = "H00" + h;
            }
            else if (id >= 100 && id < 1000)
            {

                Hos_id = "H0" + h;
            }
            else
            {

                Hos_id = "H" + h;
            }
            return Hos_id;
        }

    }
}
