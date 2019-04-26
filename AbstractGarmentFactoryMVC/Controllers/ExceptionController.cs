using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Exception
        public ActionResult Index(string messege)
        {
            ViewData["Message"] = messege;
            return View();
        }
    }
}