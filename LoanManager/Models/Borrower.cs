using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanManager.Models
{
    public partial class Borrower
    {
        public int Id { get; set; }
        [Required]
        [Editable(false)]
        [Index(IsUnique = true)]
        [Display(Name = "National ID")]
        [StringLength(10, MinimumLength = 5)]
        [Remote("IsNationalId_Usable", "Validation")]
        [RegularExpression(@"\d+", ErrorMessage = "The field National ID allows Numerics only")]
        public string NationalID { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(20)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^254\d+", ErrorMessage = "Phonenumber allows numerics only and should start with 254")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "The field Phonenumber1 must be a string with a length of 12")]
        public string PhoneNumber1 { get; set; }
        [RegularExpression(@"^254\d+", ErrorMessage = "Phonenumber allows numerics only and should start with 254")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "The field Phonenumber2 must be a string with a length of 12")]
        public string PhoneNumber2 { get; set; }
        [StringLength(255)]
        [Display(Name = "Postal Address")]
        public string Address { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime ModifiedAt { get; set; }

        public Borrower()
        {
            Assets = new List<Asset>();
        }

        public virtual ICollection<Asset> Assets { get; set; }
    }
    public partial class Borrower
    {
        public string Fullname
        {
            get
            {
                return FirstName + " " 
                    + (MiddleName == null ? "" : (MiddleName).Substring(0, 1).ToUpper() + ". ") 
                    + LastName;
            }
        }
    }
}