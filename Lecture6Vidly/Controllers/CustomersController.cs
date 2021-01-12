using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lecture6Vidly.Models;

namespace Lecture6Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private VidlyContext db = new VidlyContext();

        // GET: Customers
        public ActionResult Index()
        {

            var customers = db.Customers.Include(c => c.MembershipType);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            //ViewBag.MembershipTypeId = new SelectList(db.MembershipTypes, "Id", "Name");
            var membershipTypes = db.MembershipTypes.ToList();
            var viewModel = new CustomerViewModel
            {
                MembershipTypes=membershipTypes
            };

            return View(viewModel);
        }

        public ActionResult Test(int? PageIndex,string SortBy)
        {
            if (!PageIndex.HasValue)
            {
                PageIndex = 1;
            }

            if (String.IsNullOrWhiteSpace(SortBy))
            {
                SortBy = "Name";
            }

            return Content(String.Format("pageindex={0}&sortby={1}",PageIndex,SortBy));
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsSubscribedToNewsletter,MembershipTypeId,Birthdate")] Customer customer)
        {
            if (customer.Id==0)
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                var membershipTypes = db.MembershipTypes.ToList();
                var viewModel = new CustomerViewModel
                {
                    MembershipTypes = membershipTypes
                };

                return View(viewModel);
            }
            else
            {
                var customerInDb = db.Customers.Single(c=> c.Id==customer.Id);
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.Name = customer.Name;

                TryUpdateModel(customerInDb);

                return RedirectToAction("Index");
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var membershipTypes = db.MembershipTypes.ToList();
            var viewModel = new CustomerViewModel
            {
                MembershipTypes = membershipTypes,
                Customer=customer
            };

            return View("Create",viewModel);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsSubscribedToNewsletter,MembershipTypeId,Birthdate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MembershipTypeId = new SelectList(db.MembershipTypes, "Id", "Name", customer.MembershipTypeId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
