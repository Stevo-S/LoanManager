using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanManager.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string NationalID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Address { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime ModifiedAt { get; set; }
    }
}