using AbstractGarmentFactoryServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class FabricssController : Controller
    {
        public IFabricService service = Globals.FabricService;

        // GET: Fabricss
        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}