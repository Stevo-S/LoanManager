using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManager.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Transaction Type")]
        public string Description { get; set; }
        public TransactionType()
        {
            Transactions = new List<Transaction>();
        }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}