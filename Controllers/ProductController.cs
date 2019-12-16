using ProductSupplyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductSupplyManagement.Controllers
{
    public class ProductController : Controller
    {
        PsiDbContext db = new PsiDbContext();
        // GET: Product
        public ActionResult Index(int page=1)
        {
            int perPage = 5;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)db.Products.Count() / perPage);
            var data = db.Products
                .OrderBy(x => x.ProductId)
                .Skip((page - 1) * perPage)
                .Take(perPage);
            return View(data);
        }
        public ActionResult Inventories(int id, int page =1)
        {
            
            ViewBag.CurrentProduct = db.Products.First(x => x.ProductId == id).ProductName;
            int perPage = 5;
            
            var data = db.Inventories.Where(x => x.ProductId == id);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)data.Count() / perPage);
            ViewBag.Id = id;
            data = data
                .OrderBy(x => x.ProductId)
                .Skip((page - 1) * perPage)
                .Take(perPage);
            return View(data.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.SellUnits = new List<SelectListItem>
            {
                new SelectListItem{ Text="Nos", Value="Nos"},
                new SelectListItem{ Text="Kg", Value="Kg"},
                new SelectListItem{ Text="gm", Value="gm"},
                new SelectListItem{ Text="Litre", Value="Litre"},
                new SelectListItem{ Text="Galon", Value="Galon"},
                new SelectListItem{ Text="Ml", Value="Ml"},
                new SelectListItem{ Text="Dozen", Value="Dozen"},
                   new SelectListItem{ Text="Box", Value="Box"},
                   new SelectListItem{ Text="Gross", Value="Gross"},
                   new SelectListItem{ Text="Hali", Value="Hali"}
            };
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellUnits = new List<SelectListItem>
            {
                new SelectListItem{ Text="Nos", Value="Nos"},
                new SelectListItem{ Text="Kg", Value="Kg"},
                new SelectListItem{ Text="gm", Value="gm"},
                new SelectListItem{ Text="Litre", Value="Litre"},
                new SelectListItem{ Text="Galon", Value="Galon"},
                new SelectListItem{ Text="Ml", Value="Ml"},
                new SelectListItem{ Text="Dozen", Value="Dozen"},
               new SelectListItem{ Text="Box", Value="Box"},
               new SelectListItem{ Text="Gross", Value="Gross"},
               new SelectListItem{ Text="Hali", Value="Hali"}
            };
            return View(p);
        }
        public ActionResult Edit( int id)
        {
            ViewBag.SellUnits = new List<SelectListItem>
            {
                new SelectListItem{ Text="Nos", Value="Nos"},
                new SelectListItem{ Text="Kg", Value="Kg"},
                new SelectListItem{ Text="gm", Value="gm"},
                new SelectListItem{ Text="Litre", Value="Litre"},
                new SelectListItem{ Text="Galon", Value="Galon"},
                new SelectListItem{ Text="Ml", Value="Ml"},
                new SelectListItem{ Text="Dozen", Value="Dozen"},
               new SelectListItem{ Text="Box", Value="Box"},
               new SelectListItem{ Text="Gross", Value="Gross"},
               new SelectListItem{ Text="Hali", Value="Hali"}
            };
            return View(db.Products.First(x=> x.ProductId == id));
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellUnits = new List<SelectListItem>
            {
                new SelectListItem{ Text="Nos", Value="Nos"},
                new SelectListItem{ Text="Kg", Value="Kg"},
                new SelectListItem{ Text="gm", Value="gm"},
                new SelectListItem{ Text="Litre", Value="Litre"},
                new SelectListItem{ Text="Galon", Value="Galon"},
                new SelectListItem{ Text="Ml", Value="Ml"},
                new SelectListItem{ Text="Dozen", Value="Dozen"},
               new SelectListItem{ Text="Box", Value="Box"},
               new SelectListItem{ Text="Gross", Value="Gross"},
               new SelectListItem{ Text="Hali", Value="Hali"}
            };
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Products.First(x => x.ProductId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var p = new Product { ProductId = id };
            db.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}