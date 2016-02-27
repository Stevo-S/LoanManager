using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoanManager.Models;

namespace LoanManager.Controllers
{
    public class DuePaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DuePayments
        public ActionResult Index()
        {
            var duePayments = db.DuePayments.Include(d => d.Loan);
            return View(duePayments.ToList());
        }

        // GET: DuePayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuePayment duePayment = db.DuePayments.Find(id);
            if (duePayment == null)
            {
                return HttpNotFound();
            }
            return View(duePayment);
        }

        // GET: DuePayments/Create
        public ActionResult Create()
        {
            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id");
            return View();
        }

        // POST: DuePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DueDate,Amount,LoanId")] DuePayment duePayment)
        {
            if (ModelState.IsValid)
            {
                db.DuePayments.Add(duePayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", duePayment.LoanId);
            return View(duePayment);
        }

        // GET: DuePayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuePayment duePayment = db.DuePayments.Find(id);
            if (duePayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", duePayment.LoanId);
            return View(duePayment);
        }

        // POST: DuePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DueDate,Amount,LoanId")] DuePayment duePayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(duePayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", duePayment.LoanId);
            return View(duePayment);
        }

        // GET: DuePayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DuePayment duePayment = db.DuePayments.Find(id);
            if (duePayment == null)
            {
                return HttpNotFound();
            }
            return View(duePayment);
        }

        // POST: DuePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DuePayment duePayment = db.DuePayments.Find(id);
            db.DuePayments.Remove(duePayment);
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
