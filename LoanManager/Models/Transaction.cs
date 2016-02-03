using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanManager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Balance { get; set; }
        public int LoanId { get; set; }
        public int TypeId { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual TransactionType Type { get; set; }
        public virtual Loan Loan { get; set; }
    }
}