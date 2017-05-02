console.log("Custom transactions script loaded OK :-)");

$(function () {
    // Disable/enable debit/credit input fields on load and if transaction type changes
    toggleCreditDebit();
    $('#TypeId').change(toggleCreditDebit);

    // Calculate new loan balance when credit/debit values change
    //$('#Debit').change(calculateLoanBalance);
    //$('#Credit').change(calculateLoanBalance);

    // Disable credit or debit input
    // depending on whether transaction is penalty or not
    function toggleCreditDebit() {
        var transactionType = $('#TypeId').val();

        if (transactionType != 3)
        {
            $('#Credit').removeAttr("disabled");
            $('#Debit').val('0.00');
            $('#Debit').attr("disabled", "true");
        }
        else
        {
            $('#Credit').attr("disabled", "true");
            $('#Credit').val('0.00');
            $('#Debit').removeAttr("disabled");
        }
    }


    function transactionViewModel() {
        var self = this;
        self.credit = ko.observable('0.00');
        self.debit = ko.observable('0.00');
        self.currentBalance = parseInt($('#currentBalance').val());
        self.newBalance = ko.computed(function () {
            // calculate loan balance based on credit/debit input values
            var balance = self.currentBalance - parseFloat(self.credit()) + parseFloat(self.debit());
            return isNaN(balance) ? self.currentBalance : balance;
        }, self);

    }

    ko.applyBindings(new transactionViewModel());
});
