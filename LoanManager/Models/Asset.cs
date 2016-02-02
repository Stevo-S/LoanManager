using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanManager.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string LogBookId { get; set; }
    }
}