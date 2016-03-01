console.log("Custom loans script loaded OK :-)");

$(function () {
    $('input').change(calculateLoanDetails);

    function calculateLoanDetails() {
        var payments = Number($('#InitialInstallments').val());
        if (!isNaN(payments)) {

            var principal = Number($('#Principal').val());
            var rate = Number($('#interestRate').val());
            var interest = (rate / 100) * principal * payments;

            $('#Interest').val(interest);
        }
    }
});