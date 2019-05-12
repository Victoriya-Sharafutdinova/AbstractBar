using AbstractGarmentFactoryRestApi.Services;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceDAL.ViewModel;
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

        private readonly IImplementerService _serviceImplementer;

        public MainController(IMainService service, IImplementerService serviceImplementer)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
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
        public void CreateIndent(IndentBindingModel model)
        {
            _service.CreateIndent(model);
        }
         
        [HttpPost]
        public void TakeIndentInWork(IndentBindingModel model)
        {
            _service.TakeIndentInWork(model);
        }

        [HttpPost]
        public void FinishIndent(IndentBindingModel model)
        {
            _service.FinishIndent(model);
        } 

        [HttpPost]
        public void PayIndent(IndentBindingModel model)
        {
            _service.PayIndent(model);
        }

        [HttpPost]
        public void PutStockingOnStorage(StorageStockingBindingModel model)
        {
            _service.PutStockingOnStorage(model);
        }

        [HttpPost] public void StartWork()
        {
            List<IndentViewModel> indents = _service.GetFreeIndents();
            foreach (var indent in indents)
            {
                ImplementerViewModel impl = _serviceImplementer.GetFreeWorker();
                if (impl == null)
                {
                    throw new Exception("Нет сотрудников");
                }
                new WorkImplementer(_service, _serviceImplementer, impl.Id, indent.Id);
            }
        }
    }
}
