using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiveGroup.Models;

namespace FiveGroup.Controllers
{
    public class DoctorController : Controller
    {
        Project2Entities db = new Project2Entities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.doctor.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(doctor doctor)
        {
            /*將資料庫ID由大到小排序，取出第一筆資料，並呼叫方法取得新ID*/
            var Doc_Desc = from m in db.doctor
                           orderby m.doc_id descending
                           select m.doc_id;

            string Doc_First = Doc_Desc.FirstOrDefault(), New_id;
            New_id = New_Doc_id(Doc_First);
            /***********************************************************/
            doctor.doc_id = New_id;

            db.doctor.Add(doctor);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var doctor = db.doctor.Where(m => m.doc_id == id).FirstOrDefault();
            return View(doctor);
        }
        [HttpPost]
        public ActionResult Edit(string doc_id, string doc_name, string doc_history, Boolean doc_display)
        {
            var doctor = db.doctor.Where(m => m.doc_id == doc_id).FirstOrDefault();
            doctor.doc_id = doc_id;
            doctor.doc_name = doc_name;
            doctor.doc_history = doc_history;
            doctor.doc_display = doc_display;

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        //public ActionResult Delete(string id)
        //{
        //    var doctor = db.doctor.Where(m => m.doc_id == id).FirstOrDefault();
        //    db.doctor.Remove(doctor);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        /*********************************************************/
        /******************此為測試New_Doc_id程式*****************/
        //public void Test()
        //{
        //    var Doc_Desc = from m in db.doctor
        //                   orderby m.doc_id descending
        //                   select m.doc_id;

        //    string Doc_First = Doc_Desc.FirstOrDefault(), New_id;
        //    New_id = New_Doc_id(Doc_First);
        //    Response.Write(New_id);
        //}
        /*********************************************************/
        /********************************************************/

        public string New_Doc_id(string id)
        {
            int num = 2, sum = 0;
            string Doc_id;

            /*迴圈功能為取出ID裡的數字 例.DR001=>取001可得整數1,DR101=>取101可得整數101,以此類推...*/
            do
            {
                int i = int.Parse(id.Substring(num, 1));/*由第num個字串轉換成整數型態*/
                num++;
                sum = sum * 10;
                sum += i;
            } while (num < 5);

            sum++;
            string id_str = Convert.ToString(sum);/*將sum轉換為字串*/

            if (sum < 10)
                Doc_id = "DR00" + id_str;
            else if (sum >= 10 && sum < 100)
                Doc_id = "DR0" + id_str;
            else
                Doc_id = "DR" + id_str;

            return Doc_id;
        }

    }
}