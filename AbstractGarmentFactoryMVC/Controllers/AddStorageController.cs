using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class AddStorageController : Controller
    {
        private IStockingService stockingService = Globals.StockingService;
        private IStorageService storageService = Globals.StorageService;
        private IMainService mainService = Globals.MainService;

        public ActionResult Index()
        {
            var stockings = new SelectList(stockingService.GetList(), "Id", "StockingName");
            ViewBag.Stockings = stockings;

            var storages = new SelectList(storageService.GetList(), "Id", "StorageName");
            ViewBag.Storages = storages;
            return View();
        }

        [HttpPost]
        public ActionResult AddStockingPost()
        {
            mainService.PutStockingOnStorage(new StorageStockingBindingModel
            {
                StockingId = int.Parse(Request["StockingId"]),
                StorageId = int.Parse(Request["StorageId"]),
                Amount = int.Parse(Request["Amount"])
            });
            return RedirectToAction("Index", "Home");
        }
    }
}