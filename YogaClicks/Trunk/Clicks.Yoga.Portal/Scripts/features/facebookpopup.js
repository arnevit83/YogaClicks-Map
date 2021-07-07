$(document).ready(function () {
    $('.facebookPopupClose').click(function () {
        $('.facebookLogin').fadeOut();
    });

    $('.facebookButtonPopupClose').click(function (e) {
        e.preventDefault();
        window.open($(this).attr("href"), "popupWindow", "width=600,height=600,scrollbars=yes");
        $('.facebookLogin').fadeOut();
    });

    $('.facebookPopupCloseNoCookie').click(function () {
        $('.facebookLogin').fadeOut();
    });
    
});
