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
using AbstractGarmentFactoryServiceDAL.ViewModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceDAL.BindingModel;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class IndentsController : Controller
    {
        private IFabricService fabricService = Globals.FabricService;
        private IMainService mainService = Globals.MainService;
        private ICustomerService customerService = Globals.CustomerService;


        // GET: FabricIndent
        public ActionResult Index()
        {
            return View(mainService.GetList());
        }

        public ActionResult Create()
        {
            var fabrics = new SelectList(fabricService.GetList(), "Id", "FabricName");
            var customers = new SelectList(customerService.GetList(), "Id", "CustomerFIO");
            ViewBag.Fabrics = fabrics;
            ViewBag.Customers = customers;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost()
        {
            var fabricId = int.Parse(Request["FabricId"]);
            var customerId = int.Parse(Request["CustomerId"]);
            var amount = int.Parse(Request["Amount"]);
            var total = CalcSum(fabricId, amount);

            mainService.CreateIndent(new IndentBindingModel
            {
                FabricId = fabricId,
                CustomerId = customerId,                
                Amount = amount,
                Total = total

            });
            return RedirectToAction("Index");
        }

        private Decimal CalcSum(int fabricId, int fabricCount)
        {
            FabricViewModel fabric = fabricService.GetElement(fabricId);
            return fabricCount * fabric.Value;
        }

        public ActionResult SetStatus(int id, string status)
        {
            try
            {
                switch (status)
                {
                    case "Processing":
                        mainService.TakeIndentInWork(new IndentBindingModel { Id = id });
                        break;
                    case "Ready":
                        mainService.FinishIndent(new IndentBindingModel { Id = id });
                        break;
                    case "Paid":
                        mainService.PayIndent(new IndentBindingModel { Id = id });
                        break;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }


            return RedirectToAction("Index");
        }
    }
}
