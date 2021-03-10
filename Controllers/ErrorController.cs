using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlkUniversity.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HttpGet]
        public ActionResult UnauthorizedOperation(String operation, String module, String msgeErrorExcepcion)
        {
            ViewBag.operation = operation;
            ViewBag.module = module;
            ViewBag.msgeErrorExcepcion = msgeErrorExcepcion;
            return View();
        }
    }
}