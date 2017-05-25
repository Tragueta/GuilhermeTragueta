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
    public class CategoriesController : Controller
    {
        private EFContext context = new EFContext();


        #region Index
        public ActionResult Index()
        {
            var categories = context.Categories.OrderBy(s => s.Name);
            return View(categories);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Category category)
        {
            context.Categories.Add(category);
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
            var category = context.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        #endregion

        #region Delete
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = context.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(long id)
        {
            var category = context.Categories.Find(id);
            context.Categories.Remove(category);

            context.SaveChanges();

            TempData["Message"] = "Category " + category.Name.ToUpper() + " Deleted!";
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
            var category = context.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Details(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }
    }
    #endregion
}