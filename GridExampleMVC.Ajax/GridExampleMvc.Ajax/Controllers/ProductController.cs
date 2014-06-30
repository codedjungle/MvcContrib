using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridExampleMvc.Ajax.Models;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using MvcContrib.Pagination;

namespace GridExampleMvc.Ajax.Controllers
{
    public class ProductController : Controller
    {
       

        //
        // GET: /Product/
         private readonly CodedJungleEntities db  ;
         public ProductController()
         {
             db = new CodedJungleEntities();
         }

         public ActionResult Index(int? page, GridSortOptions sort)
        {
            IQueryable<Product> products = db.Products.AsQueryable();

            // there should be default order by or else linq skip method will cause error 
         

            ViewData["sort"] = sort;


            if (!string.IsNullOrEmpty(sort.Column))
            {
                products = products.OrderBy(sort.Column, sort.Direction);
            }
            else
            {
                products = products.OrderBy(c => c.Id);
            } 

            IPagination<Product> pagedModel = products.AsPagination(page ?? 1, 10);

            return View(pagedModel);
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(long id = 0)
        {
            Product product = db.Products.Single(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.AddObject(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Product product = db.Products.Single(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Attach(product);
                db.ObjectStateManager.ChangeObjectState(product, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Product product = db.Products.Single(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Single(p => p.Id == id);
            db.Products.DeleteObject(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}