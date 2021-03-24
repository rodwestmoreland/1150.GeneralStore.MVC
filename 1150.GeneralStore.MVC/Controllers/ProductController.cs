using _1150.GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1150.GeneralStore.MVC.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        
        public ActionResult Create() =>         View();
        public ActionResult Delete(int? id) =>  ValidateIdInput(id);
        public ActionResult Edit(int? id) =>    ValidateIdInput(id);
        public ActionResult Details(int? id) => ValidateIdInput(id);

        private ActionResult ValidateIdInput(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Product product = _context.Products.Find(id);

            if (product == null) return HttpNotFound();

            return View(product);
        }
        private ActionResult SaveAndRedirect()
        {
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index() {
            
            List<Product> productList = _context.Products.ToList();
            List<Product> orderedList = productList.OrderBy(p => p.Name).ToList();
            
            return View(orderedList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                return SaveAndRedirect();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            return SaveAndRedirect();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                return SaveAndRedirect();
            }
            return View(product);
        }
    }
}