$(document).ready(function () {
    $("#btn-pay").click(function (e) {
        //$.get("/api/bookingApi/bookingvalidate", {}, function (response) {
        //    if (response.content.isValid)
        //        return getPaymentView();
        //    Swal.fire({
        //        icon: 'error',
        //        title: 'Oops...',
        //        text: response.content.errors && response.content.errors.quote,
        //    });
        //});
        getPaymentView();
    });

    function getPaymentView() {
        $.get("/Booking/Pay", {}, showPaymentModal);
    }

    function showPaymentModal(html) {
        Swal.fire({
            title: 'Payment',
            html: html,
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonText: 'Pay',
            confirmButtonColor: '#6c757d',
        }).then((result) => {
            if (result.isConfirmed) {
                doPayment();
            }
        });
    }

    function doPayment() {
        let amount = parseFloat($('#Amount').val().replace(',', '.')) || 0;
        let method = parseInt($('#Method').val()) || 0;
        var formData = {
            amount: amount,
            method: method
        };
        $.ajax({
            url: "/api/bookingApi/payments",
            type: "POST",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (!response.status) {
                    alert(response.message);
                    return getPaymentView();
                }
                Swal.fire({
                    icon: 'success',
                    title: 'Payment Success',
                    html: "<p>Payment success, receipt number : <b>" + response.content.receiptNumber + "</b>.</p>"
                }).then((result) => {
                    if (result.isConfirmed) {
                        location.reload();
                    }
                });
            }
        });

    }

    $("#claim-voucher").click(function (e) {
        $.get("/api/bookingApi/vouchers", {}, function (response) {
            if (!response.status)
                return;
            window.open(response.content.voucherUrl, '_blank').focus();
        });
    });

});