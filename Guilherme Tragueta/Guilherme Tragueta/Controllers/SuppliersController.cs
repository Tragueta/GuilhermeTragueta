using Guilherme_Tragueta.Contexts;
using Guilherme_Tragueta.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Guilherme_Tragueta.Controllers
{
    public class SuppliersController : Controller
    {
        private EFContext context = new EFContext();

        #region Index
        public ActionResult Index()
        {
            var suppliers = context.Suppliers.OrderBy(s => s.Name);
            return View(suppliers);
        } 
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Supplier supplier)
        {
            context.Suppliers.Add(supplier);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = context.Suppliers.Find(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Supplier supplier)
        {
            if(ModelState.IsValid)
            {
                context.Entry(supplier).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        #endregion

        #region Delete
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = context.Suppliers.Find(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(long id)
        {
            var supplier = context.Suppliers.Find(id);
            context.Suppliers.Remove(supplier);

            context.SaveChanges();

            TempData["Message"] = "Supplier " + supplier.Name.ToUpper() + " Deleted!";
            return RedirectToAction("Index");

        }
        #endregion

        #region Details

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = context.Suppliers.Where(f => f.SupplierId ==
                id).Include("Products.Category").First();

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Details(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                context.Entry(supplier).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(supplier);
        }
    } 
    #endregion
}