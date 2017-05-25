using Guilherme_Tragueta.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Guilherme_Tragueta.Models;
using System.Net;

namespace Guilherme_Tragueta.Controllers
{
    public class ProductsController : Controller
    {
        private EFContext context = new EFContext();

        #region Index

        public ActionResult Index()
        {
            var products = context.Products.Include(c => c.Category).
                Include(s => s.Supplier).OrderBy(n => n.Name);
            return View(products);
        }

        #endregion

        #region Details
        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product product = context.Products.Where(p => p.ProductId ==
            id).Include(c => c.Category).Include(f => f.Supplier).
            First();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        #endregion

        #region Create
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.Categories.
               OrderBy(b => b.Name), "CategoryId", "Name");
            ViewBag.SupplierId = new SelectList(context.Suppliers.
                 OrderBy(b => b.Name), "SupplierId", "Name");
            return View();
        }
        

        
        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(product);
            }
        } 
        #endregion

        #region Edit
        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(context.Categories.
            OrderBy(b => b.Name), "CategoryId", "Name", product.
            CategoryId);
            ViewBag.SupplierId = new SelectList(context.Suppliers.
            OrderBy(b => b.Name), "SupplierId", "Name", product.
            SupplierId);
            return View(product);

        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Entry(product).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }

        #endregion

        #region Delete
        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
              var product = context.Products.Where(p => p.ProductId ==
              id).Include(c => c.Category).Include(f => f.Supplier).
              First();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Product product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
                TempData["Message"] = "Product " + product.Name.ToUpper()
                    + " was removed!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    } 
    #endregion
}
