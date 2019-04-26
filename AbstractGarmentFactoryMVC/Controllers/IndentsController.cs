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
        private DataListSingleton source = DataListSingleton.GetInstance();

        // GET: Indents
        public ActionResult Index()
        {
            return View(source.Indents.ToList());
        }

        // GET: Indents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indent indent = source.Indents.Find(x => x.Id == id);
            if (indent == null)
            {
                return HttpNotFound();
            }
            return View(indent);
        }

        // GET: Indents/Create
        public ActionResult Create()
        {
            ViewBag.Customers = new SelectList(source.Customer, "Id", "CustomerFIO");
            ViewBag.Fabrics = new SelectList(source.Fabric, "Id", "FabricName");
            return View();
        }

        // POST: Indents/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,FabricId,Amount,Total,Condition,DateCreate,DateImplement")] Indent indent)
        {
            int maxId = 0;
            for (int i = 0; i < source.Indents.Count; ++i)
            {
                if (source.Indents[i].Id > maxId)
                {
                    maxId = source.Customer[i].Id;
                }
            }
            source.Indents.Add(new Indent
            {
                Id = maxId + 1,
                CustomerId = indent.CustomerId,
                FabricId = indent.FabricId,
                DateCreate = DateTime.Now,
                Amount = indent.Amount,
                Total = indent.Total,
                Condition = IndentStatus.Принят
            });

            return RedirectToAction("Index");

        }

        // GET: Indents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indent indent = source.Indents.FirstOrDefault(x => x.Id == id);
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
                var indentOld = source.Indents.FirstOrDefault(x => indent.Id == x.Id);
                source.Indents.Remove(indentOld);
                source.Indents.Add(indent);
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
            Indent indent = source.Indents.FirstOrDefault(x => x.Id == id);
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
            Indent indent = source.Indents.Find(x => x.Id == id);
            source.Indents.Remove(indent);
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
