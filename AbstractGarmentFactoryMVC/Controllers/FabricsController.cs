using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AbstractGarmentFactoryMVC.Models;
using AbstractGarmentFactoryModel;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class FabricsController : Controller
    {
        private IFabricService service = Globals.FabricService;
        private IStockingService ingredientService = Globals.StockingService;

        // GET: Fabrics
        public ActionResult Index()
        {
            if (Session["Fabric"] == null)
            {
                var fabric = new FabricViewModel();
                fabric.FabricStocking = new List<FabricStockingViewModel>();
                Session["Fabric"] = fabric;
            }
            return View((FabricViewModel)Session["Fabric"]);
        }

        public ActionResult AddStocking()
        {
            var ingredients = new SelectList(ingredientService.GetList(), "Id", "StockingName");
            ViewBag.Stockings = ingredients;
            return View();
        }

        [HttpPost]
        public ActionResult AddStockingPost()
        {
            var fabric = (FabricViewModel)Session["Fabric"];
            var ingredient = new FabricStockingViewModel
            {
                StockingId = int.Parse(Request["Id"]),
                StockingName = ingredientService.GetElement(int.Parse(Request["Id"])).StockingName,
                Amount = int.Parse(Request["Amount"])
            };
            fabric.FabricStocking.Add(ingredient);
            Session["Fabric"] = fabric;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateFabricPost()
        {
            var fabric = (FabricViewModel)Session["Fabric"];
            var fabricStockings = new List<FabricStockingBindingModel>();
            for (int i = 0; i < fabric.FabricStocking.Count; ++i)
            {
                fabricStockings.Add(new FabricStockingBindingModel
                {
                    Id = fabric.FabricStocking[i].Id,
                    FabricId = fabric.FabricStocking[i].FabricId,
                    StockingId = fabric.FabricStocking[i].StockingId,
                    Amount = fabric.FabricStocking[i].Amount
                });
            }
            service.AddElement(new FabricBindingModel
            {
                FabricName = Request["FabricName"],
                Value = Convert.ToDecimal(Request["Value"]),
                FabricStocking = fabricStockings
            });
            Session.Remove("Fabric");
            return RedirectToAction("Index", "Fabrics");
        }
    }
}
