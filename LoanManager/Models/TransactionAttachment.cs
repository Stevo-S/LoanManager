using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanManager.Models
{
    public class TransactionAttachment
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int TransactionId { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}