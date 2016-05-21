using LoanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LoanManager.CustomHelpers
{
    public static class LoansHelpers
    {
        public static string AddLoanActionsMenu(this HtmlHelper helper, Loan loan)
        {
            // This method extends HtmlHelper
            // What it does is take a Borrower instance and return a HTML dropdown menu,
            // inside a button, with links to "Grant Loan", "Add Asset"
            // as well as the "Delete", "Edit" and "Details" views of that instance
            var dropdownDiv = new TagBuilder("div");
            dropdownDiv.AddCssClass("dropdown");
            dropdownDiv.MergeAttribute("style", "color: gray");

            var actionDropdownButton = new TagBuilder("button");
            actionDropdownButton.GenerateId("loanActionMenu");
            actionDropdownButton.AddCssClass("btn");
            actionDropdownButton.AddCssClass("btn-default");
            actionDropdownButton.AddCssClass("dropdown-toggle");
            actionDropdownButton.MergeAttribute("type", "button");
            actionDropdownButton.MergeAttribute("data-toggle", "dropdown");
            actionDropdownButton.MergeAttribute("aria-expanded", "true");
            actionDropdownButton.MergeAttribute("aria-haspopup", "true");

            var caret = new TagBuilder("span");
            caret.AddCssClass("caret");

            var dropdownList = new TagBuilder("ul");
            dropdownList.AddCssClass("dropdown-menu");
            dropdownList.MergeAttribute("aria-labelledby", "loanActionMenu");

            var grantLoanLink = new TagBuilder("li");
            grantLoanLink.InnerHtml = LinkExtensions.ActionLink(helper, "New Transaction", "Create", "Transactions", new { loanId = loan.Id }, null).ToHtmlString();

            var addAssetLink = new TagBuilder("li");
            addAssetLink.InnerHtml = LinkExtensions.ActionLink(helper, "Add Asset", "Create", "Assets", new { loanId = loan.Id }, null).ToHtmlString();

            var menuDivider = new TagBuilder("li");
            menuDivider.AddCssClass("divider");
            menuDivider.MergeAttribute("role", "separator");

            var editLink = new TagBuilder("li");
            editLink.InnerHtml = LinkExtensions.ActionLink(helper, "Edit", "Edit", "Borrowers", new { id = loan.Id }, null).ToHtmlString();

            var detailsLink = new TagBuilder("li");
            detailsLink.InnerHtml = LinkExtensions.ActionLink(helper, "Details", "Details", "Borrowers", new { id = loan.Id }, null).ToHtmlString();

            var deleteLink = new TagBuilder("li");
            deleteLink.InnerHtml = LinkExtensions.ActionLink(helper, "Delete", "Delete", "Borrowers", new { id = loan.Id }, null).ToHtmlString();

            dropdownList.InnerHtml = grantLoanLink.ToString() + addAssetLink.ToString() +
                menuDivider.ToString() + editLink.ToString() + detailsLink.ToString() + deleteLink.ToString();

            actionDropdownButton.InnerHtml = "Actions" + caret.ToString();

            dropdownDiv.InnerHtml = actionDropdownButton.ToString() + dropdownList.ToString();

            return dropdownDiv.ToString();
        }
    }
}