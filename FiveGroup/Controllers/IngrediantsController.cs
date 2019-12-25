using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FiveGroup.Models;
using PagedList.Mvc;
using PagedList;

namespace FiveGroup.Controllers
{
    [LoginRule]
    public class IngrediantsController : Controller
    {
        private Project2Entities db = new Project2Entities();

        // GET: ingrediants
        public ActionResult Index(string search, int? page)
        {

            List<ingrediant> listmap = db.ingrediant.ToList();

            return View(db.ingrediant.Where(m => m.ing_category.StartsWith(search) || search ==null).ToList().ToPagedList(page ?? 1,10));
        }

        // GET: ingrediants/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ingrediant ingrediant = db.ingrediant.Find(id);
            if (ingrediant == null)
            {
                return HttpNotFound();
            }
            return View(ingrediant);
        }

        // GET: ingrediants/Create
        public ActionResult Create()
        {
            var c = db.ingrediant.Count();
            string id;
            id = New_Ing_id(c);
            ViewBag.Ing_id = id;
            

            return View();
        }

        // POST: ingrediants/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ing_id,ing_name,ing_eng_name,ing_category,ing_restricted,ing_limitation")] ingrediant ingrediant)
        {
            if (ModelState.IsValid)
            {
                db.ingrediant.Add(ingrediant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ingrediant);
        }

        // GET: ingrediants/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ingrediant ingrediant = db.ingrediant.Find(id);
            if (ingrediant == null)
            {
                return HttpNotFound();
            }
            return View(ingrediant);
        }

        // POST: ingrediants/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ing_id,ing_name,ing_eng_name,ing_category,ing_restricted,ing_limitation")] ingrediant ingrediant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingrediant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingrediant);
        }

        // GET: ingrediants/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ingrediant ingrediant = db.ingrediant.Find(id);
            if (ingrediant == null)
            {
                return HttpNotFound();
            }
            return View(ingrediant);
        }

        // POST: ingrediants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ingrediant ingrediant = db.ingrediant.Find(id);
            db.ingrediant.Remove(ingrediant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        public string New_Ing_id(int id)
        {
            string Ing_id, h;
            id++;
            if (id < 10)
            {
                h = Convert.ToString(id);/*將id轉換為字串*/
                Ing_id = "I000" + id;
            }
            else if (id >= 10 && id < 100)
            {
                h = Convert.ToString(id);
                Ing_id = "I00" + id;
            }
            else
            {
                h = Convert.ToString(id);
                Ing_id = "I0" + id;
            }
            return Ing_id;
        }

    }
}
