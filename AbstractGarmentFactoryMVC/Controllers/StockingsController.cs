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
using AbstractGarmentFactoryServiceDAL.Interfaces;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class StockingsController : Controller
    {
        private IStockingService service = Globals.StockingService;
        // GET: Stockings
        public ActionResult Index()
        {
            return View(service.GetList());
        }


        // GET: Stockings/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new StockingBindingModel
            {
                StockingName = Request["StockingName"]
            });
            return RedirectToAction("Index");
        }


        // GET: Stockings/Edit/5
        public ActionResult Edit(int id)
        {
            var viewModel = service.GetElement(id);
            var bindingModel = new StockingBindingModel
            {
                Id = id,
                StockingName = viewModel.StockingName
            };
            return View(bindingModel);
        }


        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdElement(new StockingBindingModel
            {
                Id = int.Parse(Request["Id"]),
                StockingName = Request["StockingName"]
            });
            return RedirectToAction("Index");
        }


        // GET: Stockings/Delete/5
        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}
