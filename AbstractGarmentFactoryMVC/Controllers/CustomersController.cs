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
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceDAL.BindingModel;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class CustomersController : Controller
    {
        public ICustomerService service = Globals.CustomerService;
        public ActionResult Index()
        {
            return View(service.GetList());
        }

        public ActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult CreatePost()
        {
            service.AddElement(new CustomerBindingModel
            {
                CustomerFIO = Request["CustomerFIO"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = service.GetElement(id);
            var bindingModel = new CustomerBindingModel
            {
                Id = id,
                CustomerFIO = viewModel.CustomerFIO
            };
            return View(bindingModel);
        }

        [HttpPost]
        public ActionResult EditPost()
        {
            service.UpdElement(new CustomerBindingModel
            {
                Id = int.Parse(Request["CustomerId"]),
                CustomerFIO = Request["CustomerFIO"]
            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            service.DelElement(id);
            return RedirectToAction("Index");
        }
    }
}
