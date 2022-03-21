$(document).ready(function () {
    $("#more-description").on("click", function (e) {
        $(this).hide();
        $("#short-description").hide();
        $("#long-description").show();
    });

    $("#DateStart").kendoDatePicker({
        format: "yyyy-MM-dd"
    });

    $("#DateEnd").kendoDatePicker({
        format: "yyyy-MM-dd"
    });

    $('form#search-schedule').submit(function (e) {
        e.preventDefault();
        let resultsDiv = $("#schedule-results");
        $.post("/Booking/Schedule", $(this).serialize(), function (response) {
            resultsDiv.html(response);
        });
    });
});