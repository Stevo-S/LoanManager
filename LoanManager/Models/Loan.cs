using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanManager.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public DateTime Date { get; set; }
        public bool Cleared { get; set; }
        public int AssetId { get; set; }

        public virtual Asset Asset { get; set; }
    }
}