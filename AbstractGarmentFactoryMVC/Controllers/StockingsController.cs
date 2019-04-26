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
    public class StockingsController : Controller
    {
        private DataListSingleton source = DataListSingleton.GetInstance();

        // GET: Stockings
        public ActionResult Index()
        {
            return View(source.Stocking.ToList());
        }

        // GET: Stockings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocking stocking = source.Stocking.Find(x => x.Id == id);
            if (stocking == null)
            {
                return HttpNotFound();
            }
            return View(stocking);
        }

        // GET: Stockings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stockings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StockingName")] Stocking stocking)
        {

            int maxId = 0;
            for (int i = 0; i < source.Stocking.Count; ++i)
            {
                if (source.Stocking[i].Id > maxId)
                {
                    maxId = source.Stocking[i].Id;
                }
                if (source.Stocking[i].StockingName == stocking.StockingName)
                {
                    return Redirect("/Exception/Index/1");
                }
            }
            source.Stocking.Add(new Stocking
            {
                Id = maxId + 1,
                StockingName = stocking.StockingName
            });
            return RedirectToAction("Index");
        }

        // GET: Stockings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocking stocking = source.Stocking.FirstOrDefault(x => x.Id == id);
            if (stocking == null)
            {
                return HttpNotFound();
            }
            return View(stocking);
        }

        // POST: Stockings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StockingName")] Stocking stocking)
        {
            if (ModelState.IsValid)
            {

                var stockingOld = source.Stocking.FirstOrDefault(x => stocking.Id == x.Id);
                if (/*customerOld.CustomerFIO != customer.CustomerFIO &&*/ null != source.Stocking.FirstOrDefault(x => stocking.StockingName == x.StockingName))
                {
                    return Redirect("/Exception/Index/1");
                }
                source.Stocking.Remove(stockingOld);
                source.Stocking.Add(stocking);
                return RedirectToAction("Index");
            }
            return View(stocking);
        }

        // GET: Stockings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocking stocking = source.Stocking.FirstOrDefault(x => x.Id == id);
            if (stocking == null)
            {
                return HttpNotFound();
            }
            return View(stocking);
        }

        // POST: Stockings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stocking stocking = source.Stocking.Find(x => x.Id == id);
            source.Stocking.Remove(stocking);
         //   inst.SaveChanges();
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
