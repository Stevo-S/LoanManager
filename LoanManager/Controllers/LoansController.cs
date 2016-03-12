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
using System.Data.Entity.Validation;
using System.Diagnostics;

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
        public ActionResult Create(int? BorrowerId)
        {
            var assets = BorrowerId == null ? db.Assets : db.Assets.Where(a => a.BorrowerId == BorrowerId);
            ViewBag.AssetId = new SelectList(assets, "Id", "AssetName");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Principal,Interest,Date,Cleared,AssetId,InitialInstallments")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.CreatedAt = DateTime.Now;
                loan.ModifiedAt = DateTime.Now;
                //try {
                //    using (TransactionScope trans = new TransactionScope())
                //    {
                //        db.Loans.Add(loan);
                //        Models.Transaction transaction = new Models.Transaction()
                //        {

                //            Loan = loan,
                //            Balance = loan.Principal + loan.Interest,
                //            Credit = 0,
                //            Debit = loan.Principal + loan.Interest,
                //            Details = "Loan Disbursement",
                //            Type = db.TransactionTypes.First(t => t.Description == "Loan Disbursement"),
                //        };
                //        db.Transactions.Add(transaction);

                //        DuePayment duepayment = new DuePayment()
                //        {
                //            Amount = transaction.Balance,
                //            Loan = loan
                //        };
                //        db.DuePayments.Add(duepayment);

                //        db.SaveChanges();
                //        trans.Complete();
                //    }
                //    return RedirectToAction("Index");
                //}
                //catch(Exception ex)
                //{ }
                loan.PendingInstallments = loan.InitialInstallments;
                loan.Balance = loan.Principal + loan.Interest;

                // Add corresponding transaction
                var transaction = new Models.Transaction()
                {
                    Type = db.TransactionTypes.First(tt => tt.Description.Contains("OPEN")),
                    Loan = loan,
                    Balance = loan.Balance,
                    Debit = loan.Balance,
                    Details = "Loan Disbursment",
                    Timestamp = DateTime.Now
                };
                db.Transactions.Add(transaction);

                // Record the next due payment
                var duePayment = new DuePayment()
                {
                    Loan = loan,
                    Amount = loan.Balance / loan.PendingInstallments
                };
                db.DuePayments.Add(duePayment);

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var errors = "";

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        errors += "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" +
                            eve.Entry.Entity.GetType().Name + eve.Entry.State + "\n";
                        foreach (var ve in eve.ValidationErrors)
                        {
                            errors += "- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"" +
                            ve.PropertyName +
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName) +
                            ve.ErrorMessage + "\n";
                        }
                    }

                    ViewBag.Errors = errors;
                }

                return RedirectToAction("Index");

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
