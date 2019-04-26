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
    public class FabricStockingsController : Controller
    {
        private DataListSingleton inst = DataListSingleton.GetInstance();

        // GET: FabricStockings
        public ActionResult Index()
        {
            return View(inst.FabricStocking.ToList());
        }

        // GET: FabricStockings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FabricStocking fabricStocking = inst.FabricStocking.Find(x => x.Id == id);
            if (fabricStocking == null)
            {
                return HttpNotFound();
            }
            return View(fabricStocking);
        }

        // GET: FabricStockings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FabricStockings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FabricId,StockingId,Amount")] FabricStocking fabricStocking)
        {
            if (ModelState.IsValid)
            {
                inst.FabricStocking.Add(fabricStocking);
           //     inst.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fabricStocking);
        }

        // GET: FabricStockings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FabricStocking fabricStocking = inst.FabricStocking.FirstOrDefault(x => x.Id == id);
            if (fabricStocking == null)
            {
                return HttpNotFound();
            }
            return View(fabricStocking);
        }

        // POST: FabricStockings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FabricId,StockingId,Amount")] FabricStocking fabricStocking)
        {
            if (ModelState.IsValid)
            {
                //   inst.Entry(fabricStocking).State = EntityState.Modified;
                //     inst.SaveChanges();
                var fabricStockingOld = inst.FabricStocking.FirstOrDefault(x => fabricStocking.Id == x.Id);
                inst.FabricStocking.Remove(fabricStockingOld);
                inst.FabricStocking.Add(fabricStocking);
                return RedirectToAction("Index");
            }
            return View(fabricStocking);
        }

        // GET: FabricStockings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FabricStocking fabricStocking = inst.FabricStocking.FirstOrDefault(x => x.Id == id);
            if (fabricStocking == null)
            {
                return HttpNotFound();
            }
            return View(fabricStocking);
        }

        // POST: FabricStockings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FabricStocking fabricStocking = inst.FabricStocking.Find(x => x.Id == id);
            inst.FabricStocking.Remove(fabricStocking);
        //    inst.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
              //  inst.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
