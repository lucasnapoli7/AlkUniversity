using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlkUniversity.Filter;
using AlkUniversity.Models;

namespace AlkUniversity.Controllers
{
    public class SubjectController : Controller
    {
        private AlkUniversity_dbEntities db = new AlkUniversity_dbEntities();

        // GET: Subject
        [AuthorizeUser(idOperation: 4)]
        public ActionResult Index()
        {
            var subjects = db.subjects.Include(s => s.schedules).Include(s => s.teachers);
            return View(subjects.ToList());
        }

        // GET: Subject/Details/5
        [AuthorizeUser(idOperation: 5)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subjects subjects = db.subjects.Find(id);
            if (subjects == null)
            {
                return HttpNotFound();
            }
            return View(subjects);
        }

        // GET: Subject/Create
        [AuthorizeUser(idOperation: 6)]
        public ActionResult Create()
        {
            ViewBag.schedule_id = new SelectList(db.schedules, "id", "name");
            ViewBag.teacher_id = new SelectList(db.teachers, "id", "name");
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idOperation: 6)]
        public ActionResult Create([Bind(Include = "id,name,schedule_id,description,teacher_id,max_student")] subjects subjects)
        {
            if (ModelState.IsValid)
            {
                db.subjects.Add(subjects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.schedule_id = new SelectList(db.schedules, "id", "name", subjects.schedule_id);
            ViewBag.teacher_id = new SelectList(db.teachers, "id", "name", subjects.teacher_id);
            return View(subjects);
        }

        // GET: Subject/Edit/5
        [AuthorizeUser(idOperation: 8)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subjects subjects = db.subjects.Find(id);
            if (subjects == null)
            {
                return HttpNotFound();
            }
            ViewBag.schedule_id = new SelectList(db.schedules, "id", "name", subjects.schedule_id);
            ViewBag.teacher_id = new SelectList(db.teachers, "id", "name", subjects.teacher_id);
            return View(subjects);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idOperation: 8)]
        public ActionResult Edit([Bind(Include = "id,name,schedule_id,description,teacher_id,max_student")] subjects subjects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.schedule_id = new SelectList(db.schedules, "id", "name", subjects.schedule_id);
            ViewBag.teacher_id = new SelectList(db.teachers, "id", "name", subjects.teacher_id);
            return View(subjects);
        }

        // GET: Subject/Delete/5
        [AuthorizeUser(idOperation: 7)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subjects subjects = db.subjects.Find(id);
            if (subjects == null)
            {
                return HttpNotFound();
            }
            return View(subjects);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idOperation: 7)]
        public ActionResult DeleteConfirmed(int id)
        {
            subjects subjects = db.subjects.Find(id);
            db.subjects.Remove(subjects);
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
        

    }
}
