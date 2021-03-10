using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlkUniversity.Filter;
using AlkUniversity.Models;

namespace AlkUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly AlkUniversity_dbEntities db = new AlkUniversity_dbEntities();
        public ActionResult Index()
        {
            var oUser = (users)System.Web.HttpContext.Current.Session["User"];
            
            if (oUser.role_id == 1)
            {
                return RedirectToAction("Admin");
            }
            else
            {
                return RedirectToAction("Student");
            }
        }
        [AuthorizeUser(idOperation:1)]
        public ActionResult Admin()
        {
            return View();
        }
        [AuthorizeUser(idOperation: 2)]
        public ActionResult Student()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Access");
        }
    }
}