﻿using System;
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
    public class AssetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assets
        public ActionResult Index()
        {
            var assets = db.Assets.Include(a => a.Borrower);
            return View(assets.ToList());
        }

        // GET: Assets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: Assets/Create
        public ActionResult Create(int? BorrowerId)
        {
            var borrowers = BorrowerId == null ? db.Borrowers : db.Borrowers.Where(b => b.Id == BorrowerId);
            ViewBag.BorrowerId = new SelectList(borrowers, "Id", "FullName");
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LogBookId,RegistrationNo,Description,Value,BorrowerId")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                asset.CreatedAt = DateTime.Now;
                asset.ModifiedAt = DateTime.Now;
                db.Assets.Add(asset);
                db.SaveChanges();
                return RedirectToAction("Details", "Borrowers", new { id = asset.BorrowerId } );
            }

            ViewBag.BorrowerId = new SelectList(db.Borrowers, "Id", "NationalID", asset.BorrowerId);
            return View(asset);
        }

        // GET: Assets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            ViewBag.BorrowerId = new SelectList(db.Borrowers, "Id", "FullName", asset.BorrowerId);
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LogBookId,RegistrationNo,Description,Value,BorrowerId,CreatedAt,ModifiedAt")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                asset.ModifiedAt = DateTime.Now;
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BorrowerId = new SelectList(db.Borrowers, "Id", "NationalID", asset.BorrowerId);
            return View(asset);
        }

        // GET: Assets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asset asset = db.Assets.Find(id);
            db.Assets.Remove(asset);
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
