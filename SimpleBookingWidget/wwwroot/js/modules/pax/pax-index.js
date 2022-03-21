$(document).ready(function () {
    var inputMobilePhone = $("#mobile-phone");
    if (inputMobilePhone.length) {

        var intTelPhone = intlTelInput(inputMobilePhone.get(0), {
            initialCountry: "id",
            utilsScript: "lib/intl-tel-input/js/utils.js"
        });
    }

    $('form#create-pax').submit(function (e) {
        var phoneNumber = $("#mobile-phone");
        if (!intTelPhone.isValidNumber()) {
            e.preventDefault();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Phone number invalid.',
            });
            return;
        }
        phoneNumber.val(intTelPhone.getNumber());
    });
});