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

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class CustomersController : Controller
    {
        //private AbstractGarmentFactoryMVCContext db = new AbstractGarmentFactoryMVCContext();
        private DataListSingleton source = DataListSingleton.GetInstance();
        // GET: Customers
        public ActionResult Index()
        {
            return View(source.Customer.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = source.Customer.Find(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerFIO")] Customer customer)
        {
            int maxId = 0;
            for (int i = 0; i < source.Customer.Count; ++i)
            {
                if (source.Customer[i].Id > maxId)
                {
                    maxId = source.Customer[i].Id;
                }
                if (source.Customer[i].CustomerFIO == customer.CustomerFIO)
                {
                    //throw new Exception("Уже есть клиент с таким ФИО");
                    return Redirect("/Exception/Index/0");
                }
            }
            source.Customer.Add(new Customer { Id = maxId + 1, CustomerFIO = customer.CustomerFIO });

            //return View(customer);
            return RedirectToAction("Index");
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = source.Customer.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerFIO")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //inst.Entry(customer).State = EntityState.Modified;
                //inst.SaveChanges();

                var customerOld = source.Customer.FirstOrDefault(x => customer.Id == x.Id);

                if (/*customerOld.CustomerFIO != customer.CustomerFIO &&*/ null != source.Customer.FirstOrDefault(x => customer.CustomerFIO == x.CustomerFIO))
                {
                    //throw new Exception("Уже есть клиент с таким ФИО");
                    return Redirect("/Exception/Index/0");
                }

                source.Customer.Remove(customerOld);
                source.Customer.Add(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = source.Customer.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = source.Customer.Find(x => x.Id == id);
            source.Customer.Remove(customer);
            //inst.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //inst.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
