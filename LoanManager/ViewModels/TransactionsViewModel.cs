using LoanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanManager.ViewModels
{
    public class TransactionsViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public Borrower Borrower { get; set; }
        public CompanyProfile Company { get; }

        public TransactionsViewModel()
        {
            using(var db = new ApplicationDbContext())
            {
                Company = db.CompanyProfiles.First();
            }
        }
    }
}