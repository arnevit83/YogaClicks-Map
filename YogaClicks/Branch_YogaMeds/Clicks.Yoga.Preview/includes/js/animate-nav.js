$(document).ready(function () {
    $('#menu').click(function () {

        var toggleWidth = $("#left-nav").width() == 190 ? "75px" : "190px";
        $('#left-nav').animate({ 'width': toggleWidth }, 200);

        var navWidth = $("#left-nav").width() == 190 ? "375px" : "490px";
        $('#content-slider').animate({ 'marginLeft': navWidth }, 200);

        return false;
    });
});