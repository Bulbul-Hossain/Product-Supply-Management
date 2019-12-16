using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductSupplyManagement.Models;
using ProductSupplyManagement.ViewModels;

namespace ProductSupplyManagement.Controllers
{
    public class SuppliersController : Controller
    {
        private PsiDbContext db = new PsiDbContext();

        // GET: Suppliers
        public ActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }
        public ActionResult Supplies(int id)
        {
            ViewBag.SupplierName = db.Suppliers.First(x => x.SupplierId == id).SupplierCompany;
            var data = db.Inventories
                  .Where(x => x.SupplierId == id)
                  .Select(x => new SupplyViewModel { SupplierId=x.SupplierId, Date = x.Date, ProductName = x.Product.ProductName, LotNo = x.LotNo, Quantity = x.Quantity })
                  .ToList();
            return View(data);
        }
        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,SupplierCompany,ContactNo,Address,Email")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierId,SupplierCompany,ContactNo,Address,Email")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
