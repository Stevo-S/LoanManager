using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanManager.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string LogBookId { get; set; }
        public int BorrowerId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime ModifiedAt { get; set; }

        public virtual Borrower Borrower { get; set; }

    }
}