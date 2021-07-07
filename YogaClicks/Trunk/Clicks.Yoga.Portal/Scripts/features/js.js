function toLocalDateTime(selector) {
    selector.text(moment(moment.utc(moment(selector.text())).toDate()).format("Do MMMM YYYY, h:mma"));
};

jQuery(document).ready(function ($) {
    //initialise Stellar.js
    $(window).stellar();

    //Provides local time formatting
    $("span.localDateTime").each(function () {
        toLocalDateTime($(this));
    });

});

