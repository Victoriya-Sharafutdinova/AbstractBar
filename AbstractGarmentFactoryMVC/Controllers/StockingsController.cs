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
        private DataListSingleton inst = DataListSingleton.GetInstance();

        // GET: Stockings
        public ActionResult Index()
        {
            return View(inst.Stocking.ToList());
        }

        // GET: Stockings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocking stocking = inst.Stocking.Find(x => x.Id == id);
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
            if (ModelState.IsValid)
            {
                inst.Stocking.Add(stocking);
          //      inst.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stocking);
        }

        // GET: Stockings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocking stocking = inst.Stocking.FirstOrDefault(x => x.Id == id);
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
                //  inst.Entry(stocking).State = EntityState.Modified;
                //  inst.SaveChanges();
                var stockingOld = inst.Stocking.FirstOrDefault(x => stocking.Id == x.Id);
                inst.Stocking.Remove(stockingOld);
                inst.Stocking.Add(stocking);
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
            Stocking stocking = inst.Stocking.FirstOrDefault(x => x.Id == id);
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
            Stocking stocking = inst.Stocking.Find(x => x.Id == id);
            inst.Stocking.Remove(stocking);
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
