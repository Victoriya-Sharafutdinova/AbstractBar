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
using AbstractGarmentFactoryServiceDAL.ViewModel;
using AbstractGarmentFactoryView;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class StockingsController : Controller
    {
        private IStockingService service = Globals.StockingService;
        // GET: Stockings
        public ActionResult Index()
        {
            List<StockingViewModel> list = APICustomer.GetRequest<List<StockingViewModel>>("api/Stocking/GetList");
            return View(list);
        }


        // GET: Stockings/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreatePost()
        {
            APICustomer.PostRequest<StockingBindingModel, bool>("api/Stocking/AddElement", new StockingBindingModel
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
            APICustomer.PostRequest<StockingBindingModel, bool>("api/Stocking/UpdElement", new StockingBindingModel
            {
                Id = int.Parse(Request["Id"]),
                StockingName = Request["StockingName"]
            });
            return RedirectToAction("Index");
        }


        // GET: Stockings/Delete/5
        public ActionResult Delete(int id)
        {
            APICustomer.PostRequest<StockingBindingModel, bool>("api/Stocking/DelElement", new StockingBindingModel { Id = id });
            return RedirectToAction("Index");
        }
    }
}
