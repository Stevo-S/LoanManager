using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanManager.Models
{
    public partial class Asset
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Logbook")]
        [Remote("IsLogBookId_Usable", "Validation")]
        public string LogBookId { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 7)]
        public string RegistrationNo { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Value { get; set; }
        [Required]
        [Display(Name = "Owner")]
        public int BorrowerId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime ModifiedAt { get; set; }

        public Asset()
        {
            Loans = new List<Loan>();
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }

        public virtual Borrower Borrower { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }

    public partial class Asset
    {
        [Display(Name = "Asset")]
        public string AssetName
        {
            get { return Description + " - " + LogBookId; }
        }
    }
}