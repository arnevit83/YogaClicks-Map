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

//placeholder for fb
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_GB/all.js#xfbml=1";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
