using LoanManager.Models;
using LoanManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanManager.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            var dashboard = new DashboardViewModel
            {
                ActiveLoans = db.Loans.Where(l => ! l.Cleared)
            };
            return View();
        }
    }
}