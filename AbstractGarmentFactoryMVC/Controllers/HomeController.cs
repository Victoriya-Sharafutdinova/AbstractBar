using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Stockings()
        {
            return RedirectToAction("Index", "Stockings");
        }

        public ActionResult Fabrics()
        {
            return RedirectToAction("Index", "Fabrics");
        }

        public ActionResult Indents()
        {
            return RedirectToAction("Index", "Indents");
        }
    }
}