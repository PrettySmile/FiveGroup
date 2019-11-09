using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FiveGroup.Models;
using FiveGroup.ViewModel;
using PagedList;

namespace FiveGroup.Controllers
{
    public class SymptomDepartmentController : Controller
    {
        Project2Entities db = new Project2Entities();

        public ActionResult Index(int page=1, string pId="", string sId="", string dId="")
        {
            IQueryable<dep_sym_ref> dsRefLis;
            dsRefLis = from m in db.dep_sym_ref
                       select m;
            if (pId != "") dsRefLis = dsRefLis.Where(m => m.part_id == pId);
            if (sId != "") dsRefLis = dsRefLis.Where(m => m.sym_id == sId);
            if (dId != "") dsRefLis = dsRefLis.Where(m => m.dep_id == dId);

            //回傳分頁過的list回前端---------------------------------------
            int pageDataSize = 20;
            int pageCurrent = page < 1 ? 1 : page;
            IPagedList<dep_sym_ref> pagedlist = dsRefLis.ToList().ToPagedList(pageCurrent, pageDataSize);
            SymptomDepartment sd = new SymptomDepartment()
            {
                bodypartList = db.bodypart.ToList(),
                symptomList = db.symptom.ToList(),
                departmentList = db.department.ToList(),
                depSymRefPagedList = pagedlist                
            };
            ViewBag.pId = pId;
            ViewBag.sId = sId;
            ViewBag.dId = dId;
            return View(sd);
        }

        public ActionResult Create()
        {
            SymptomDepartment sd = new SymptomDepartment()
            {
                bodypartList = db.bodypart.ToList(),
                symptomList = db.symptom.ToList(),
                departmentList = db.department.ToList()
            };
            return View(sd);
        }

        [HttpPost]
        public ActionResult Create(dep_sym_ref sd)
        {
            db.dep_sym_ref.Add(sd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? sn,string pId, string sId, string dId)
        {
            if (sn == null || pId==null || sId==null || dId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dep_sym_ref data = (from m in db.dep_sym_ref
                          where m.dep_sym_sn==sn && m.part_id==pId && m.sym_id == sId && m.dep_id == dId
                          select m).FirstOrDefault();
            if (data == null)
            {
                return HttpNotFound();
            }
            SymptomDepartment sd = new SymptomDepartment()
            {
                bodypartList = db.bodypart.ToList(),
                symptomList = db.symptom.ToList(),
                departmentList = db.department.ToList(),
                depSymRef = data
            };
            return View(sd);
        }

        [HttpPost]
        public ActionResult Edit(dep_sym_ref sd)
        {
            dep_sym_ref data = (from m in db.dep_sym_ref
                                where m.dep_sym_sn == sd.dep_sym_sn
                                select m).FirstOrDefault();
            if (data == null)
            {
                return HttpNotFound();
            }
            data.part_id = sd.part_id;
            data.sym_id = sd.sym_id;
            data.dep_id = sd.dep_id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int sn, string pId, string sId, string dId)
        {
            dep_sym_ref data = (from m in db.dep_sym_ref
                                where m.dep_sym_sn == sn && m.part_id == pId && m.sym_id == sId && m.dep_id == dId
                                select m).FirstOrDefault();
            db.dep_sym_ref.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
     
    }
}