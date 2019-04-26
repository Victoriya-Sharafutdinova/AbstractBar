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
    public class IndentsController : Controller
    {
        private DataListSingleton inst = DataListSingleton.GetInstance();

        // GET: Indents
        public ActionResult Index()
        {
            return View(inst.Indents.ToList());
        }

        // GET: Indents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indent indent = inst.Indents.Find(x => x.Id == id);
            if (indent == null)
            {
                return HttpNotFound();
            }
            return View(indent);
        }

        // GET: Indents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Indents/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,FabricId,Amount,Total,Condition,DateCreate,DateImplement")] Indent indent)
        {
            if (ModelState.IsValid)
            {
                inst.Indents.Add(indent);
            //    inst.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(indent);
        }

        // GET: Indents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indent indent = inst.Indents.FirstOrDefault(x => x.Id == id);
            if (indent == null)
            {
                return HttpNotFound();
            }
            return View(indent);
        }

        // POST: Indents/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,FabricId,Amount,Total,Condition,DateCreate,DateImplement")] Indent indent)
        {
            if (ModelState.IsValid)
            {
                //    inst.Entry(indent).State = EntityState.Modified;
                //     inst.SaveChanges();
                var indentOld = inst.Indents.FirstOrDefault(x => indent.Id == x.Id);
                inst.Indents.Remove(indentOld);
                inst.Indents.Add(indent);
                return RedirectToAction("Index");
            }
            return View(indent);
        }

        // GET: Indents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indent indent = inst.Indents.FirstOrDefault(x => x.Id == id);
            if (indent == null)
            {
                return HttpNotFound();
            }
            return View(indent);
        }

        // POST: Indents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Indent indent = inst.Indents.Find(x => x.Id == id);
            inst.Indents.Remove(indent);
           // inst.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            //    inst.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
