using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlkUniversity.Models;

namespace AlkUniversity.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Enter(string dni, string file)
        {
            try
            {
                using (AlkUniversity_dbEntities db = new AlkUniversity_dbEntities())
                {
                   
                    var lst = from d in db.users
                                where d.dni == dni && d.file == file
                                select d;
                    if (lst.Count() > 0)
                    {
                        Session["User"] = lst.First();
                        if (lst.First().role_id == 1) { 
                            return Content("1"); 
                        }
                        else
                        {
                            return Content("2");
                        }
                            
                    }
                    else
                    {
                        return Content("Incorrect login data");
                    }
                }

            }
            catch (Exception ex)
            {
                return Content("An error occurred: " + ex.Message);
            }
        }
    }
}