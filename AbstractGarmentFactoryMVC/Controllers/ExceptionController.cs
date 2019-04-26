using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Exception/
        public ActionResult Index(int id)
        {
            string message = "Неизвестная ошибка";

            if (id == 0)
            {
                message = "Уже есть такой клиент.";
            }
            if (id == 1)
            {
                message = "Уже есть такой компонент.";
            }

            ViewBag.Message = id;
            return View();
        }
    }
}