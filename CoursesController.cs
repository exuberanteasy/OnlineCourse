using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;

using System.Web;
using System.Web.Mvc;
using TestStore.Models;

using PagedList;

namespace TestStore.Controllers
{
    public class CoursesController : Controller
    {
        private TestStoreDBModel db = new TestStoreDBModel();

        // GET: Courses
        public ActionResult Index(int page = 1)
        {
            int pagesize = 24;
            int pagecurrent = page < 1 ? 1 : page;

            //List<Course> list = new List<Course>();

            var list = db.Courses.ToList();

            var pagedlist = list.ToPagedList(pagecurrent, pagesize);

            if (Session["Member"] == null)
            {
                return View("Index", "_LayoutMemberIndex", pagedlist);
            }


            return View("Index", "_LayoutMember", pagedlist);


        }

        // GET: Courses
        public ActionResult IndexV2()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        //public ActionResult Details(string CId)
        //{
        //    if (CId == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Course course = db.Courses.Find(CId);
        //    if (course == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(course);

        //}

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult CourseDetails(string fCId)
        {
            if (fCId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Where(m => m.CId == fCId).FirstOrDefault();
            if (course == null)
            {
                return HttpNotFound();
            }

            string toCourse = "Course" + fCId;

            return RedirectToAction(toCourse, "CourseList");


        }


        public ActionResult CPlate01()
        {
            return View();
        }
        


        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CId,Name,Lector,Price,Img")] Course course)
        {
            if (ModelState.IsValid)
            {
                //db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        //GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CId,Name,Lector,Price,Img")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Course course = db.Courses.Find(id);
        //    if (course == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(course);
        //}

        // POST: Courses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Course course = db.Courses.Find(id);
        //    db.Courses.Remove(course);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //另存SQL的資料
        public JsonResult SaveNewFile()
        {
            var cours = from c in db.Courses
                        select c;
            return Json(cours,  JsonRequestBehavior.AllowGet);
        }


       

    }
}
