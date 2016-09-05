using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManager.Models
{
    public class CompanyProfile
    {
        public int Id { get; set; }

        [StringLength(256)]
        public string Logo { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(10)]
        public string PostOfficeBox { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string ProvinceStateCounty { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(256)]
        [DataType(DataType.MultilineText)]
        public string Slogan { get; set; }
    }
}