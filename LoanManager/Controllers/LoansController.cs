using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoanManager.Models;
using System.Transactions;

namespace LoanManager.Controllers
{
    public class LoansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Loans
        public ActionResult Index()
        {
            var loans = db.Loans.Include(l => l.Asset);
            return View(loans.ToList());
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            ViewBag.AssetId = new SelectList(db.Assets, "Id", "AssetName");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Principal,Interest,Date,Cleared,AssetId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.CreatedAt = DateTime.Now;
                loan.ModifiedAt = DateTime.Now;
                try {
                    using (TransactionScope trans = new TransactionScope())
                    {
                        db.Loans.Add(loan);
                        Models.Transaction transaction = new Models.Transaction()
                        {

                            Loan = loan,
                            Balance = loan.Principal + loan.Interest,
                            Credit = 0,
                            Debit = loan.Principal + loan.Interest,
                            Details = "Loan Disbursement",
                            Type = db.TransactionTypes.First(t => t.Description == "Loan Disbursement"),
                        };
                        db.Transactions.Add(transaction);

                        DuePayment duepayment = new DuePayment()
                        {
                            Amount = transaction.Balance,
                            Loan = loan
                        };
                        db.DuePayments.Add(duepayment);

                        db.SaveChanges();
                        trans.Complete();
                    }
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                { }
            }

            ViewBag.AssetId = new SelectList(db.Assets, "Id", "AssetName", loan.AssetId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetId = new SelectList(db.Assets, "Id", "AssetName", loan.AssetId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Principal,Interest,Date,Cleared,AssetId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.ModifiedAt = DateTime.Now;
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetId = new SelectList(db.Assets, "Id", "AssetName", loan.AssetId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
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
