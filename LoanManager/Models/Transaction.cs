using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int TypeId { get; set; }
        [Required]
        public int LoanId { get; set; }
        [Required]
        public string Details { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Credit { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Debit { get; set; }
        [Required]
        [Display(Name = "Loan Balance")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Balance { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }

        public Transaction()
        {
            Credit = Debit = 0;
            Details = "";
            TransactionAttachments = new List<TransactionAttachment>();
        }

        public virtual TransactionType Type { get; set; }
        public virtual Loan Loan { get; set; }
        public virtual ICollection<TransactionAttachment> TransactionAttachments { get; set; }
    }
}