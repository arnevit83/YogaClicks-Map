

//stLight.options({ publisher: "baec43a7-3517-4d20-9ae5-53c3322c76ae", doNotHash: false, doNotCopy: false, hashAddressBar: false });


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

//    (function (i, s, o, g, r, a, m) {
//        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
//            (i[r].q = i[r].q || []).push(arguments)
//        }, i[r].l = 1 * new Date(); a = s.createElement(o),
//        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
//    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

//ga('create', 'UA-46903593-1', 'auto');
//ga('require', 'displayfeatures');
//ga('require', 'linkid', 'linkid.js');
//ga('send', 'pageview');

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    $(document).ready(function () {

  

    $('.registerRevealBtn').click(function(e) {

        if ($('#registrationPage').val() == 'true') {
            e.preventDefault();
            $("#createPopUp").click();
        };
    });

    ////Segment Code

    !function () {
        var analytics = window.analytics = window.analytics || []; if (!analytics.initialize) if (analytics.invoked) window.console && console.error && console.error("Segment snippet included twice."); else {
            analytics.invoked = !0; analytics.methods = ["trackSubmit", "trackClick", "trackLink", "trackForm", "pageview", "identify", "reset", "group", "track", "ready", "alias", "page", "once", "off", "on"]; analytics.factory = function (t) { return function () { var e = Array.prototype.slice.call(arguments); e.unshift(t); analytics.push(e); return analytics } }; for (var t = 0; t < analytics.methods.length; t++) { var e = analytics.methods[t]; analytics[e] = analytics.factory(e) } analytics.load = function (t) { var e = document.createElement("script"); e.type = "text/javascript"; e.async = !0; e.src = ("https:" === document.location.protocol ? "https://" : "http://") + "cdn.segment.com/analytics.js/v1/" + t + "/analytics.min.js"; var n = document.getElementsByTagName("script")[0]; n.parentNode.insertBefore(e, n) }; analytics.SNIPPET_VERSION = "3.1.0";
            analytics.load("5P6LoRKjobstHFaXbNNjNampdYh0WKrH");
            analytics.page();
        }
        }();







    function ShopClick() {
        analytics.identify({

            type: 'identify',

            traits: {
                'Shop-visited': 'true'
            }

        }, function () {
            $(location).attr('href', 'https://www.yogaclicks.store/');

        });
    }
    /////////////////////////





});
