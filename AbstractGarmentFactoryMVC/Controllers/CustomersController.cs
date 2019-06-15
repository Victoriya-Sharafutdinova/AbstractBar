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
using AbstractGarmentFactoryServiceDAL.ViewModel;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class CustomersController : Controller
    {
        public ICustomerService service = Globals.CustomerService;
        public ActionResult Index()
        {
            List<CustomerViewModel> list = APICustomer.GetRequest<List<CustomerViewModel>>("api/Customer/GetList");
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult CreatePost()
        {
            APICustomer.PostRequest<CustomerBindingModel, bool>("api/Customer/AddElement", new CustomerBindingModel
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
            APICustomer.PostRequest<CustomerBindingModel, bool>("api/Customer/UpdElement", new CustomerBindingModel
            {
                Id = int.Parse(Request["Id"]),
                CustomerFIO = Request["CustomerFIO"]
            });
           
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            APICustomer.PostRequest<CustomerBindingModel, bool>("api/Customer/DelElement", new CustomerBindingModel { Id = id });
            return RedirectToAction("Index");
        }
    }
}
