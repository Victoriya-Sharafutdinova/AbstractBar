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
    public class FabricController : ApiController
    {
        private readonly IFabricService _service;

        public FabricController(IFabricService service)
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


        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(FabricBindingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(FabricBindingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(FabricBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
