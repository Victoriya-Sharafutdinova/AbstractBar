using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbstractGarmentFactoryRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;

        public MainController(IMainService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateOrder(IndentBindingModel model)
        {
            _service.CreateIndent(model);
        }

        [HttpPost]
        public void TakeOrderInWork(IndentBindingModel model)
        {
            _service.TakeIndentInWork(model);
        }

        [HttpPost]
        public void FinishOrder(IndentBindingModel model)
        {
            _service.FinishIndent(model);
        }

        [HttpPost]
        public void PayOrder(IndentBindingModel model)
        {
            _service.PayIndent(model);
        }

        [HttpPost]
        public void PutComponentOnStock(StorageStockingBindingModel model)
        {
            _service.PutStockingOnStorage(model);
        }
    }
}
