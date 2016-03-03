using LoanManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace LoanManager.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class ValidationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Validation
        public JsonResult IsNationalId_Usable(string NationalId)
        {
            if(db.Borrowers.Any(b => b.NationalID == NationalId))            
                return Json(String.Format(CultureInfo.InvariantCulture, "{0} has already been registered", NationalId), JsonRequestBehavior.AllowGet);
            
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult IsLogBookId_Usable(string LogBookId)
        {
            if (db.Assets.Any(b => b.LogBookId == LogBookId))
                return Json(String.Format(CultureInfo.InvariantCulture, "{0} has already been registered", LogBookId), JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAsset_Encumbered(int AssetId)
        {
            if(db.Loans.Any(l => l.AssetId == AssetId && !l.Cleared))
                return Json(String.Format(CultureInfo.InvariantCulture, "The selected asset has been encumbered. Please select another asset"), JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Is_AmountLoanable(decimal Principal, int AssetId)
        {
            var asset = db.Assets.Find(AssetId);
            if(Principal > asset.Value/2)
                return Json(String.Format(CultureInfo.InvariantCulture, "Invalid Principal amount. Please enter any value less than or equal to KES "+(asset.Value/2).ToString("N2")), JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}