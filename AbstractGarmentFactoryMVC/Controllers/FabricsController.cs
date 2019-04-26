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
        private DataListSingleton source = DataListSingleton.GetInstance();

        // GET: Fabrics
        public ActionResult Index()
        {
            return View(source.Fabric.ToList());
        }

        // GET: Fabrics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabric fabric = source.Fabric.Find(x => x.Id == id);
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
            int maxId = 0; for (int i = 0; i < source.Fabric.Count; ++i)
            {
                if (source.Fabric[i].Id > maxId)
                {
                    maxId = source.Fabric[i].Id;
                }
                if (source.Fabric[i].FabricName == fabric.FabricName)
                {
                    return Redirect("/Exception/Index/2");
                }
            }
            source.Fabric.Add(new Fabric
            {
                Id = maxId + 1,
                FabricName = fabric.FabricName,
                Value = fabric.Value
            });
            // компоненты для изделия             
            int maxPCId = 0;
            for (int i = 0; i < source.FabricStocking.Count; ++i)
            {
                if (source.FabricStocking[i].Id > maxPCId)
                {
                    maxPCId = source.FabricStocking[i].Id;
                }
            }
            // убираем дубли по компонентам             
          /*  for (int i = 0; i < fabric.FabricStocking.Count; ++i)
            {
                for (int j = 1; j < fabric.FabricStocking.Count; ++j)
                {
                    if (fabric.FabricStocking[i].StockingId == fabric.FabricStocking[j].StockingId)
                    {
                        fabric.FabricStocking[i].Amount += fabric.FabricStocking[j].Amount;
                        fabric.FabricStocking.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты             
            for (int i = 0; i < fabric.FabricStocking.Count; ++i)
            {
                source.FabricStocking.Add(new FabricStocking
                {
                    Id = ++maxPCId,
                    FabricId = maxId + 1,
                    StockingId = fabric.FabricStocking[i].StockingId,
                    Amount = fabric.FabricStocking[i].Amount
                });
            }*/
           
            return RedirectToAction("Index");
           
        }

        // GET: Fabrics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabric fabric = source.Fabric.FirstOrDefault(x => x.Id == id);
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
                var fabricOld = source.Fabric.FirstOrDefault(x => fabric.Id == x.Id);
                if (/*customerOld.CustomerFIO != customer.CustomerFIO &&*/ null != source.Fabric.FirstOrDefault(x => fabric.FabricName == x.FabricName))
                {
                    //throw new Exception("Уже есть клиент с таким ФИО");
                    return Redirect("/Exception/Index/2");
                }
                source.Fabric.Remove(fabricOld);
                source.Fabric.Add(fabric);
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
            Fabric fabric = source.Fabric.FirstOrDefault(x => x.Id == id);
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
            Fabric fabric = source.Fabric.Find(x => x.Id == id);
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
