using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductSupplyManagement.Models;

namespace ProductSupplyManagement.Controllers
{
    public class InventoriesController : Controller
    {
        private PsiDbContext db = new PsiDbContext();

        // GET: Inventories
        public ActionResult Index(int page=1, string search="", string sort="")
        {
            ViewBag.DateSort = String.IsNullOrEmpty(sort) ? "date_desc" : "";
            ViewBag.CompanySort = sort == "company" ? "company_desc" : "company";
            var inventories = db.Inventories.Include(i => i.Supplier).Include(i => i.Product);
            if (!string.IsNullOrEmpty(search))
                inventories=inventories
                    .Where(x => x.Supplier.SupplierCompany.ToLower().StartsWith(search.ToLower()));
            switch (sort)
            {
                case "date":
                    inventories = inventories.OrderBy(x => x.Date);
                    break;
                case "date_desc":
                    inventories = inventories.OrderByDescending(x => x.Date);
                    break;
                case "company":
                    inventories = inventories.OrderBy(x => x.Supplier.SupplierCompany);
                    break;
                case "company_desc":
                    inventories = inventories.OrderByDescending(x => x.Supplier.SupplierCompany);
                    break;
                default:
                    inventories = inventories.OrderBy(x => x.Date);
                    break;
            }
            int perPage = 5;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)db.Inventories.Count() / perPage);
            ViewBag.Sort = sort;
            var data = inventories
                .Skip((page - 1) * perPage)
                .Take(perPage);
            if (IsAjaxRequest())
                return PartialView("_Inventories", data.ToList());
            else
                return View(data.ToList());
        }

        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventories/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierCompany");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "InventoryId,Date,ProductId,SupplierId,LotNo,Quantity")] Inventory inventory)
        {
            string lotNo = db.FnGETLOTNO(inventory.SupplierId, inventory.ProductId).Single();
            inventory.LotNo = lotNo;
            if (ModelState.IsValid)
            {
                
                db.Inventories.Add(inventory);
                db.SaveChanges();
                return Json(new { success = true, data = inventory });
            }


            return Json(new { success = false, data = inventory });
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierCompany", inventory.SupplierId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", inventory.ProductId);
            return View(inventory);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventoryId,Date,ProductId,SupplierId,LotNo,Quantity")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { succes = true, data = inventory });
            }


            return Json(new { succes = false, data = inventory });
            //ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierCompany", inventory.SupplierId);
            //ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", inventory.ProductId);
            //return View(inventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public  bool IsAjaxRequest( )
        {
            
            if (Request["X-Requested-With"] == "XMLHttpRequest")
                return true;
            if (Request.Headers != null)
                return Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
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
