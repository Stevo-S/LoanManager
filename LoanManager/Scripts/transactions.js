console.log("Custom transactions script loaded OK :-)");

$(function () {
    // Disable/enable debit/credit input fields on load and if transaction type changes
    toggleCreditDebit();
    $('#TypeId').change(toggleCreditDebit);

    // Calculate new loan balance when credit/debit values change
    $('#Debit').change(calculateLoanBalance);
    $('#Credit').change(calculateLoanBalance);

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

    // Calculate new loan balance
    function calculateLoanBalance()
    {
        var currentBalance = parseInt($('#currentBalance').val());
        var credit = parseFloat($('#Credit').val());
        var debit = parseFloat($('#Debit').val());

        $('#Balance').val(currentBalance - credit + debit);
    }
});
