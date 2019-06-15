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
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetStoragesLoad()
        {
            var list = _service.GetStoragesLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetCustomerIndents(ReportBindingModel model)
        {
            var list = _service.GetCustomerIndents(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveFabricValue(ReportBindingModel model)
        {
            _service.SaveFabricValue(model);
        }

        [HttpPost]
        public void SaveStoragesLoad(ReportBindingModel model)
        {
            _service.SaveStoragesLoad(model);
        }

        [HttpPost]
        public void SaveCustomerIndents(ReportBindingModel model)
        {
            _service.SaveCustomerIndents(model);
        }
    }
}
