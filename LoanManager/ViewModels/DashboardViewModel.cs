using LoanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanManager.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Loan> ActiveLoans { get; set; }
    }
}