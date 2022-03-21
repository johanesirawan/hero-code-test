$(document).ready(function () {
    let bookingCart = $("#booking-cart");
    if (bookingCart.length) {
        $.get("/api/bookingApi/bookingcount", {}, function (response) {
            if (!response.status)
                return;
            bookingCart.text(response.content);
        });
    }
});