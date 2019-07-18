$(document).ready(function () {
    redirectTo('main');
});

function search() {
    localStorage.setItem('searchId', $('#searchId').val());
    redirectTo('billPaymentHistory');
}

function save() {
    $.ajax({
        url: 'http://localhost:65215/api/customer/',
        type: 'POST',
        data: JSON.stringify({
            'CUST_NAME': $('#CustomerName').val(),
            'CUST_EMAIL': $('#Email').val(),
            'CUST_MOBILE': $('#Mobile').val()
        }),
        contentType: 'application/json',
        success: function (result) {
            var result = JSON.stringify({
                'CUST_REG_ID': result,
                'CUST_NAME': $('#CustomerName').val(),
                'CUST_EMAIL': $('#Email').val(),
                'CUST_MOBILE': $('#Mobile').val()
            });
            localStorage.setItem('customerDetails', result);
            redirectTo('customerDetails');
        },
        error: function (errorMessage) {
            console.log(errorMessage);
        }
    });
}

function redirectTo(screen) {
    $('#contentPlaceHolder div').empty();
    $('#' + screen).template(screen);

    if (screen === 'billPaymentHistory')
        getBillPaymentHistory(parseInt(localStorage.getItem('searchId')), screen);
    else if (screen === 'customerDetails') {
        $.tmpl(screen, JSON.parse(localStorage.getItem('customerDetails'))).appendTo("#contentPlaceHolder");
        localStorage.removeItem('customerDetails')
    }
    else
        $.tmpl(screen, null).appendTo("#contentPlaceHolder");

    $('#pageHead').text($('#' + screen).attr('aria-describedby'));
}

function getBillPaymentHistory(customerId, screen) {
    $.ajax({
        url: "http://localhost:65215/api/customer/billpayments/" + customerId,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.tmpl(screen, result).appendTo("#contentPlaceHolder");
            localStorage.removeItem('searchId');
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}