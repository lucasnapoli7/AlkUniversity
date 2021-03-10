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
    public class TeacherController : Controller
    {
        private AlkUniversity_dbEntities db = new AlkUniversity_dbEntities();

        // GET: Teacher
        [AuthorizeUser(idOperation: 9)]
        public ActionResult Index()
        {
            var teachers = db.teachers.Include(t => t.states);
            return View(teachers.ToList());
        }

        // GET: Teacher/Details/5
        [AuthorizeUser(idOperation: 10)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teachers teachers = db.teachers.Find(id);
            if (teachers == null)
            {
                return HttpNotFound();
            }
            return View(teachers);
        }

        // GET: Teacher/Create
        [AuthorizeUser(idOperation: 11)]
        public ActionResult Create()
        {
            ViewBag.state_id = new SelectList(db.states, "id", "name");
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idOperation: 11)]
        public ActionResult Create([Bind(Include = "id,name,lastname,dni,state_id")] teachers teachers)
        {
            if (ModelState.IsValid)
            {
                db.teachers.Add(teachers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.state_id = new SelectList(db.states, "id", "name", teachers.state_id);
            return View(teachers);
        }

        // GET: Teacher/Edit/5
        [AuthorizeUser(idOperation: 13)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teachers teachers = db.teachers.Find(id);
            if (teachers == null)
            {
                return HttpNotFound();
            }
            ViewBag.state_id = new SelectList(db.states, "id", "name", teachers.state_id);
            return View(teachers);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(idOperation: 13)]
        public ActionResult Edit([Bind(Include = "id,name,lastname,dni,state_id")] teachers teachers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teachers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.state_id = new SelectList(db.states, "id", "name", teachers.state_id);
            return View(teachers);
        }

        // GET: Teacher/Delete/5
        [AuthorizeUser(idOperation: 12)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teachers teachers = db.teachers.Find(id);
            if (teachers == null)
            {
                return HttpNotFound();
            }
            return View(teachers);
        }

        // POST: Teacher/Delete/5
        [AuthorizeUser(idOperation: 12)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            teachers teachers = db.teachers.Find(id);
            db.teachers.Remove(teachers);
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
