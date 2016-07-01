using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LoanManager.Models;
using System.IO;

namespace LoanManager.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Loan).Include(t => t.Type);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create(int? loanId)
        {
            ViewBag.TypeId = new SelectList(db.TransactionTypes, "Id", "Description");

            if (loanId != null)
            {
                var loan = db.Loans.Find(loanId);
                if (loan != null)
                {
                    ViewBag.LoanId = new SelectList(db.Loans.Where(l => l.Id == loanId), "Id", "Id");
                    ViewBag.CurrentBalance = loan.Balance;
                    var transaction = new Transaction()
                    {
                        Loan = loan,
                        Balance = loan.Balance,
                        Credit = 0,
                        Debit = 0
                    };

                    return View(transaction);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeId,LoanId,Details,Credit,Debit,Balance,Timestamp")] Transaction transaction, IEnumerable<HttpPostedFileBase> Attachments)
        {
            if (ModelState.IsValid)
            {
                foreach(var attachment in Attachments)
                {
                    if (attachment != null && attachment.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(attachment.FileName);
                        string attachmentsFolder = "~/UploadedFiles/Transaction_Attachments";
                        var path = Path.Combine(Server.MapPath(attachmentsFolder), fileName);

                        var transactionAttachment = new TransactionAttachment()
                        {
                            Path = attachmentsFolder + "/" + fileName,
                            Transaction = transaction
                        };

                        attachment.SaveAs(path);
                        db.TransactionAttachments.Add(transactionAttachment);
                    }
                }
                transaction.Timestamp = DateTime.Now;
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", transaction.LoanId);
            ViewBag.TypeId = new SelectList(db.TransactionTypes, "Id", "Description", transaction.TypeId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", transaction.LoanId);
            ViewBag.TypeId = new SelectList(db.TransactionTypes, "Id", "Description", transaction.TypeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeId,LoanId,Details,Credit,Debit,Balance,Timestamp")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", transaction.LoanId);
            ViewBag.TypeId = new SelectList(db.TransactionTypes, "Id", "Description", transaction.TypeId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
