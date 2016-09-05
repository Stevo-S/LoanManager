using LoanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanManager.CustomHelpers
{
    public static class CompanyProfileHelpers
    {
        public static string CompanyLogoPath(this HtmlHelper helper, CompanyProfile companyProfile)
        {
            return !string.IsNullOrEmpty(companyProfile.Logo) ? companyProfile.Logo : "~/Content/Images/default_company_logo.jpg";
        }
    }
}