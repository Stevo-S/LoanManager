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
using Rotativa;

namespace LoanManager.Controllers
{
    public class BorrowersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Borrowers
        public ActionResult Index()
        {
            return View(db.Borrowers.ToList());
        }

        // GET: Borrowers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Where(b => b.Id == id).Include(b => b.Assets).FirstOrDefault();
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // GET: Borrowers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NationalID,FirstName,MiddleName,LastName,Email,PhoneNumber1,PhoneNumber2,Address,CreatedAt,ModifiedAt")] Borrower borrower, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                SavePhoto(borrower, Photo);

                borrower.CreatedAt = DateTime.Now;
                borrower.ModifiedAt = DateTime.Now;
                db.Borrowers.Add(borrower);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = borrower.Id });
            }

            return View(borrower);
        }

        // GET: Borrowers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NationalID,FirstName,MiddleName,LastName,Email,PhoneNumber1,PhoneNumber2,Address,CreatedAt,ModifiedAt")] Borrower borrower, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                SavePhoto(borrower, Photo);

                borrower.ModifiedAt = DateTime.Now;
                db.Entry(borrower).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        // Print Customer Statement
        public ActionResult StatementPdf(int id)
        {
            return new ActionAsPdf("Statement", new { borrowerId = id }) { FileName = db.Borrowers.Find(id).LastName + "_Statement.pdf" };
        }

        public ActionResult Statement(int borrowerId)
        {
            return View(db.Borrowers.Find(borrowerId).Assets.SelectMany(a => a.Loans).SelectMany(l => l.Transactions).ToList());
        }
        // GET: Borrowers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = db.Borrowers.Find(id);
            db.Borrowers.Remove(borrower);
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

        private void SavePhoto(Borrower borrower, HttpPostedFileBase Photo)
        {
            if (Photo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Photo.FileName);
                var photosFolder = "~/UploadedFiles/Borrower_Photos";
                var path = Path.Combine(Server.MapPath(photosFolder), fileName);
                Photo.SaveAs(path);
                borrower.Photo = photosFolder + "/" + fileName;
            }

            return;
        }
    }
}
