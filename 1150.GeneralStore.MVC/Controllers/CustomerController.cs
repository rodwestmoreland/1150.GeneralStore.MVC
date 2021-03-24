using _1150.GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1150.GeneralStore.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        
        public ActionResult Create() =>         View();
        public ActionResult Delete(int? id) =>  ValidateIdInput(id);
        public ActionResult Edit(int? id) =>    ValidateIdInput(id);

        private ActionResult ValidateIdInput(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Customer customer = _context.Customers.Find(id);

            if (customer == null) return HttpNotFound();

            return View(customer);
        }

        private ActionResult SaveAndRedirect()
        {
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            List<Customer> customerList = _context.Customers.ToList();
            List<Customer> orderedList = customerList.OrderBy(c => c.LastName).ToList();
            return View(orderedList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                return SaveAndRedirect();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Customer customer = _context.Customers.Find(id);
            _context.Customers.Remove(customer);
            return SaveAndRedirect();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                return SaveAndRedirect();
            }
            return View(customer);
        }

    }
}