$(document).ready(function () {

    $("#YogaMapPopUp").fancybox();



    $('.YogaMapPopupRevealBtn').click(function (e) {

        if ($('#YogaMapPopupRevealBtn').val() == 'true') {
            e.preventDefault();
            $("#YogaMapPopUp").click();
        };
    });

});
