﻿using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbstractGarmentFactoryWeb.Controllers
{
    public class StockingController : ApiController
    {
        private readonly IStockingService _service;

        public StockingController(IStockingService service)
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
        public void AddElement(StockingBindingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(StockingBindingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(StockingBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
