using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManager.Models
{
    public class DuePayment
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Amount { get; set; }
        [Required]
        public int LoanId { get; set; }

        public bool IsPaid { get; set; }

        public virtual Loan Loan { get; set; }

        public DuePayment()
        {
            IsPaid = false;
            DueDate = DateTime.Now.AddMonths(1);
        }
    }
}