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
    public class CompanyProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CompanyProfiles
        //public ActionResult Index()
        //{
        //    return View(db.CompanyProfiles.ToList());
        //}

        // GET: CompanyProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyProfile companyProfile = db.CompanyProfiles.Find(id);
            if (companyProfile == null)
            {
                return HttpNotFound();
            }
            return View(companyProfile);
        }

        // GET: CompanyProfiles/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: CompanyProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Logo,Name,PostOfficeBox,City,ProvinceStateCounty,Country")] CompanyProfile companyProfile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CompanyProfiles.Add(companyProfile);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(companyProfile);
        //}

        // GET: CompanyProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyProfile companyProfile = db.CompanyProfiles.Find(id);
            if (companyProfile == null)
            {
                return HttpNotFound();
            }
            return View(companyProfile);
        }

        // POST: CompanyProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Logo,Name,PostOfficeBox,City,ProvinceStateCounty,Country")] CompanyProfile companyProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = companyProfile.Id});
            }
            return View(companyProfile);
        }

        // GET: CompanyProfiles/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CompanyProfile companyProfile = db.CompanyProfiles.Find(id);
        //    if (companyProfile == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(companyProfile);
        //}

        // POST: CompanyProfiles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CompanyProfile companyProfile = db.CompanyProfiles.Find(id);
        //    db.CompanyProfiles.Remove(companyProfile);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
