

stLight.options({ publisher: "baec43a7-3517-4d20-9ae5-53c3322c76ae", doNotHash: false, doNotCopy: false, hashAddressBar: false });


//<!-- Read More JS -->

    $(document).ready(function () {
        $('.more-block').readmore();
    });

//<!-- Cookies -->

    $(document).ready(function () {
        if ($.cookie('noInviteFBFriends')) $('.facebookLogin').hide();
        else {
            $(".facebookPopupClose").click(function (e) {
                e.preventDefault();
                $(".facebookLogin").fadeOut(1000);
                var expires = new Date();
                expires.setFullYear(expires.getFullYear() + 1);
                $.cookie('noInviteFBFriends', true, { expires: expires });
            });

            $(".facebookButtonPopupClose").click(function () {
                $(".facebookLogin").fadeOut(1000);
                var expires = new Date();
                expires.setFullYear(expires.getFullYear() + 1);
                $.cookie('noInviteFBFriends', true, { expires: expires });
            });
        }
    });

    $(document).ready(function () {
        $('article .scale').parent('div').addClass('banner');
    });

    (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

ga('create', 'UA-46903593-1', 'auto');
ga('require', 'displayfeatures');
ga('require', 'linkid', 'linkid.js');
ga('send', 'pageview');


$(document).ready(function() {

    $('.registerRevealBtn').click(function(e) {

        if ($('#registrationPage').val() == 'true') {
            e.preventDefault();
            $("#createPopUp").click();
        };
    });

});
