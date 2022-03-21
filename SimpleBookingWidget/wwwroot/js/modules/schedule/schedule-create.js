$(document).ready(function () {
    $(document).on("submit", "form.create-booking", function (e) {
        e.preventDefault();
        let formData = $(this).serialize();
        $.post("/Booking/Pricing", formData, function (response) {
            showPricingModal(response, formData);
        });
    });

    function showPricingModal(html, formData) {
        Swal.fire({
            title: 'Confirm your order.',
            html: html,
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonText: 'Book Now!',
            confirmButtonColor: '#6c757d',
        }).then((result) => {
            if (result.isConfirmed) {
                doBooking(formData);
            }
        });
    }

    function doBooking(formData) {
        $.post("/Booking/Create", formData, function (response) {
            Swal.fire({
                icon: 'success',
                title: 'Successfully Booked!',
                html: "<p>Please complete  your booking on your <a href='/Booking/Cart'>shopping cart</a>!</p>"
            }).then((result) => {
                if (result.isConfirmed) {
                    location.reload();
                }
            });
        });
    }
});