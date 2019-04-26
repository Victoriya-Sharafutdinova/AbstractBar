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
    public class FabricsController : Controller
    {
        private DataListSingleton inst = DataListSingleton.GetInstance();

        // GET: Fabrics
        public ActionResult Index()
        {
            return View(inst.Fabric.ToList());
        }

        // GET: Fabrics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabric fabric = inst.Fabric.Find(x => x.Id == id);
            if (fabric == null)
            {
                return HttpNotFound();
            }
            return View(fabric);
        }

        // GET: Fabrics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fabrics/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FabricName,Value")] Fabric fabric)
        {
            if (ModelState.IsValid)
            {
                inst.Fabric.Add(fabric);
               // inst.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fabric);
        }

        // GET: Fabrics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabric fabric = inst.Fabric.FirstOrDefault(x => x.Id == id);
            if (fabric == null)
            {
                return HttpNotFound();
            }
            return View(fabric);
        }

        // POST: Fabrics/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FabricName,Value")] Fabric fabric)
        {
            if (ModelState.IsValid)
            {
                //  inst.Entry(fabric).State = EntityState.Modified;
                //inst.SaveChanges();
                var fabricOld = inst.Fabric.FirstOrDefault(x => fabric.Id == x.Id);
                inst.Fabric.Remove(fabricOld);
                inst.Fabric.Add(fabric);
                return RedirectToAction("Index");
            }
            return View(fabric);
        }

        // GET: Fabrics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabric fabric = inst.Fabric.FirstOrDefault(x => x.Id == id);
            if (fabric == null)
            {
                return HttpNotFound();
            }
            return View(fabric);
        }

        // POST: Fabrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fabric fabric = inst.Fabric.Find(x => x.Id == id);
           // db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
             //   inst.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
