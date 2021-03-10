using AlkUniversity.Filter;
using AlkUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AlkUniversity.Controllers
{
    public class InscriptionController : Controller
    {
        private AlkUniversity_dbEntities db = new AlkUniversity_dbEntities();

        // GET: Inscription
        public ActionResult Index()
        {
            var subjects = db.subjects.OrderBy(r => r.name).Include(s => s.schedules).Include(s => s.teachers);
            return View(subjects.ToList());
        }
        public ActionResult DetailsStudents(int? id)
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
            var lst = from c in db.user_subject
                      where c.subject_id == id
                      select c;
            var max_student = from c in db.subjects
                              where c.id == id
                              select c.max_student;
            int? max = max_student.First();
            ViewBag.remaining = max - lst.Count();
            return View(subjects);
        }
        [AuthorizeUser(idOperation: 3)]
        public ActionResult Register(int? id)
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

            var oUser = (users)System.Web.HttpContext.Current.Session["User"];
            //subjects subjects = db.subjects.Find(id);
            var lst = from c in db.user_subject
                      where c.user_id == oUser.id && c.subject_id == id
                      select c;
            if (lst.Count() == 0)
            {
                user_subject regist = new user_subject();
                regist.user_id = oUser.id;
                regist.subject_id = id;
                db.user_subject.Add(regist);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Already_registered");
            }
            return View(subjects);
        }
        public ActionResult Already_registered()
        {
            return View();
        }
    }
}