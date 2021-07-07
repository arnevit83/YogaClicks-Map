//////////////////////////////Layout
if (!Modernizr.svg) {$('#LogoMain').attr("src", "/images/global/logo.png");}

$("#left-menu").click(function () {
    $("#left-slider").fadeToggle("fast");
    var darkvoid = true;
    if ($("#right-nav").is(":hidden")) { } else { $("#right-nav").hide(); darkvoid = false }
    if ($("#mainsearch").is(":hidden")) { } else { $("#mainsearch").hide(); darkvoid = false }
    if (darkvoid) { $(".dark-overlay").fadeToggle("slow"); };
});
$("#Search-menu").click(function () {

    $("#mainsearch").slideToggle("fast");
    var darkvoid = true;
    if ($("#right-nav").is(":hidden")) { } else { $("#right-nav").hide(); darkvoid = false }
    if ($("#left-slider").is(":hidden")) { } else { $("#left-slider").hide(); darkvoid = false }
    if (darkvoid) { $(".dark-overlay").fadeToggle("slow"); };
    document.getElementsByName("keywords")[1].focus();
});
$("#right-menu").click(function () {
    $("#left-slider").hide();
    $(".dark-overlay").fadeToggle("slow");
    $("#right-nav").fadeToggle("fast");
});
$(".dark-overlay").click(function () {
    $("#left-slider").hide();
    $("#right-nav").hide();
    $("#mainsearch").hide();
    $(".dark-overlay").fadeToggle("slow");
});

var $contentlength = $("#right-nav").html();
if ($contentlength.indexOf('<') !== -1) {
    //Set full width
    $("#right-nav").css("display", "").addClass("col-sm-2 ");
    $("#maincontent").removeClass("col-sm-11").addClass("col-sm-9");
    $("#Innercontent").addClass("maincontentmidSize").removeClass("maincontentlargSize");
}
$(function() {
    $('[data-toggle="tooltip"]').tooltip();
});




$(document).ready(function () {
    $(".footerMenu").show();
    resizeDiv();
});
window.onresize = function (event) {
    resizeDiv();
}
function resizeDiv(forgetcheck) {
    var vph = Math.max($("#maincontent").height(), $(window).height() - 75);
    vph = Math.max(vph, $("#left-slider").height() + 125);
   




    if (vph < 4000 || forgetcheck){
    $("#left-slider").css({ 'height': vph  + "px" });
    $("#right-nav").css({ 'height': vph + "px" });
    $(".dark-overlay").css({ 'height': vph + 75 + "px" });
    }
}




//function HideDiv() {
//    //    $("#left-slider").css({ 'height': vph + "px" });
//    //   $("#right-nav").css({ 'height': vph + "px" });
//    $("#left-slider").hide();
//    $("#right-nav").hide();
//    $("#mainsearch").hide();
//    $(".dark-overlay").hide();
//}

//////////////////////////////////////////////////////////////



//   resizeDiv();




//$(function () {
//    var $forms = $('form[data-role=Registration]');

//    $forms.on('click', 'button[type=submit]', function (e) {
//        e.preventDefault();

//        var $button = $(this);
//        var $form = $button.closest('form');
//        var $inputs = $form.find('input, select');

//        $('.disabledSignUp').show();
//        $('.activeSignup').hide();

//        $.ajax({
//            type: 'POST',
//            url: $form.attr('data-partial-action'),
//            data: $inputs.serialize(),
//            success: function (response) {
//                if (response) {
//                    $form.html(response);
//                    $('.disabledSignUp').hide();
//                    $('.activeSignup').show();
//                }
//                else {
//                    location.href = '@Url.Action("CreateAccountComplete", "Registration")';
//                }
//            }
//        });
//    });

//    $("#createPopUp").fancybox();

//    $("body").delegate(".reg-form-popup", "click", function (e) {
//        if ($(this).attr("href").indexOf("/") == 0) {
//            e.preventDefault();
//            $('.reg-title').html('You need to be a member for this. It\'s free and it\'s easy.');
//            $("#createPopUp").click();
//        }
//    });

//    $("body").delegate(".reg-form-header", "click", function (e) {
//        if ($(this).attr("href").indexOf("/") == 0) {
//            e.preventDefault();
//            $('.reg-title').html('Join YogaClicks!<br /> It\'s free and it\'s easy.');
//            $("#createPopUp").click();
//        }
//    });
//});

$('.close-login').click(function (e) {
    e.preventDefault();
});




var isOn;

//LOADING FRAME
function initSpinnerFunction() {
    $("body").prepend("<div id ='PleaseWait' style='display:none;'><div class='pleaseWait-loading'></div></div>");
};

$(document).on("ajaxStart", function () {
    turnOn(true);
});

$(document).on("ajaxStop", function () {
    turnOn(false);
});

// Add the jQuery center() method...
jQuery.fn.center = function () {
    this.css("position", "absolute");
    this.css("width", $(document).width() + "px");
    this.css("height", $(document).height() + "px");
    return this;
};

function turnOn(on) {
    //Center in window
    //$('#PleaseWait').center();

    if (on) // In case I want to return true
    {
        //alert(on);
        isOn = true;
        window.setTimeout(showIt, 1000);
        return true;
    }
    else // I want to return false
    {
        //alert(on);
        isOn = false;
        window.clearTimeout(showIt);
        window.setTimeout(function () { $('#PleaseWait').hide(); }, 1000); // Manually hide the LoadingElementId
        return false;
    }
}

function showIt() {
    if (isOn) {
        $('#PleaseWait').show();
    }
}

$(document).on("click", "#share-link", function (e) {
    e.preventDefault();
    $("#footer-navigation").slideDown(100);
});

$(document).on("click", "#close-footer", function (e) {
    e.preventDefault();
    $("#footer-navigation").slideUp(100);
});

$(document).on("keypress", "#login-form input", function (event) {

    if (event.which == 13) {
        event.preventDefault();
        $("#login-button").click();
    }

});

$(document).on("click", "#login-button", function (e) {
    e.preventDefault();

    var form = $("#login-form");

    $.ajax({
        type: "POST",
        url: form.attr("action"),
        data: form.serialize(),
        success: function (data) {

            if (data != null) {
                $("#login-container").html(data);

                $('.loginRevealBtn').click(function () {
                    $('.loginReveal').slideToggle();
                });
            }

            if ($("#login-container").find('.validation-summary-errors').length == 0) {
                var newUrl = window.location.href;
                if (newUrl.indexOf("register") > 0) {
                    newUrl = "/";
                } else {
                    newUrl = window.location.href.replace("#", "");
                }
                //window.location.href = newUrl;
                window.location.replace(newUrl);
            } else {
                $('.loginReveal').show();
            }
        }
    });

});

$(document).ready(function () {
    /*
       // $('.sub-menu').slideUp();
    
        $('#menu').click(function () {
            //$('.sub-menu').removeClass('selected');
    
            // $('.sub-menu').slideToggle();
    
            $('.sub-menu').slideDown();
    
            return false;
        });
    
        $('.menu').click(function () {
    
            if ($(this).next('.sub-menu').is(':visible')) {
                $('.menu').removeClass('plus');
                $(this).addClass('plus');
            } else {
                $('.sub-menu').slideUp();
                $(this).next('.sub-menu').slideDown();
                $('.menu').removeClass('minus');
                $(this).addClass('minus');
            }
    
            return false;
        });
        
            if ( $('.content-col').hasClass('discover-strip') ) {
            
              //  $('#left-nav a#discover').next('.sub-menu').slideDown();
            
            } else if ( $('.content-col').hasClass('practice-strip') ) {
            
               // $('#left-nav a#practice').next('.sub-menu').slideDown();
            
            } else if ( $('.content-col').hasClass('wisdom-strip') ) {
            
                // $('#left-nav a#wisdom').next('.sub-menu').slideDown();
            
            }
        */


    $('.modal-box-gallery-show').click(function () {
        $('.modal-mask, .modal-box, .modal-box-gallery').fadeIn();
        return false;
    });

    $('.modal-box-add-page-show').click(function () {
        $('.modal-mask, .modal-box, .modal-box-add-page').fadeIn();
        return false;
    });

    $('.modal-box-gallery-close').click(function () {
        $('.modal-mask, .modal-box, .modal-box-gallery').fadeOut();
        return false;
    });

    $('.modal-box-add-page-next').click(function () {
        $('.modal-box-add-page').hide();
        $('.modal-box-add-page2').fadeIn();
        return false;
    });
    $('.modal-box-add-page-next2').click(function () {
        $('.modal-box-add-page2').hide();
        $('.modal-box-add-page3').fadeIn();
        return false;
    });
    $('.modal-box-add-page-close').click(function () {
        $('.modal-mask, .modal-box, .modal-box-add-page3').fadeOut();
        return false;
    });
    /*
        $('.icons.friend').click(function () {
            if ( $('.fly-down.news').is(':visible') ) {
                $('.fly-down.news').slideUp('fast');
                $('.icons.news').removeClass('active');
            }
            $(this).toggleClass('active');
            $('.fly-down.friend').slideToggle('fast');
            return false;
        });
	
        $('.icons.news').click(function () {
            if ( $('.fly-down.friend').is(':visible') ) {
                $('.fly-down.friend').slideUp('fast');
                $('.icons.friend').removeClass('active');
            }
            $(this).toggleClass('active');
            $('.fly-down.news').slideToggle('fast');
            return false;
        });*/

    /*$('.icons.friend').click(function () {
        $('#MessagesPopup').hide();
        $('#NotificationsPopup').hide();
    }*/

    /*$('.icons.messages').click(function () {
        $('#NotificationsPopup').hide();
        $('#FriendRequestsPopup').hide();
    }*/
    /*
        $('.icons.news').click(function () {
            $('#MessagesPopup').hide();
            $('#FriendRequestsPopup').hide();
        }
        */
    /*
        $('.fly-down').on("mouseenter",function(){
            $(this).show();
        }).on('mouseleave',function(){
            setTimeout(function () {
                $('.fly-down').hide();
                $('.icons').removeClass('active');
            }, 1000);		
        });

        */

    // Table show and hide content


    // Table functionality 


    // Profile dropdown functionality



    $("header.main .toggle").click(function (event) {
        $('.profileDropDown').slideToggle();
        $('.fly-down').slideUp();
        event.stopPropagation();
    });

    $('.profileDropDown').click(function (event) {
        event.stopPropagation();
    });
    $('html').click(function () {
        $('.profileDropDown').slideUp();
        $('#FriendRequestsPopup').slideUp();
        $('#MessagesPopup').slideUp();
        $('#NotificationsPopup').slideUp();
        $('.notifications .icons').removeClass('active');
    });
    $('#FriendRequestsPopup').click(function (event) {
        event.stopPropagation();
    });
    $('#MessagesPopup').click(function (event) {
        event.stopPropagation();
    });
    $('#NotificationsPopup').click(function (event) {
        event.stopPropagation();
    });


    $('.toggleNavItem').click(function () {
        $('.toggleNavItem').removeClass('open');
        $('.toggleReveal').slideUp();
        $(this).next('.toggleReveal').slideToggle();
        $(this).toggleClass('open');
    });

    // disable autocomplete
    $('input').attr('autocomplete', 'off');


    //Radio buttons

    $('.activityType label, .activityType div').click(function () {
        $('.activityType label, .activityType div').removeClass('selected');
        $(this).addClass('selected');
    });

    // Level box

    
    $('.questionMark').click(function () {
        $('.levelBox').toggle();
    });

    $('.levelBox .close').click(function (event) {
        event.preventDefault();
        $('.levelBox').hide();
    });


    $('[data-toggle="popover"]').popover({ html: true });

  

    // Textarea resize height

    $('.shareSomething textarea').focus(function () {
        $(this).animate({ height: "150px" }, 500);
    });

    $('.replyTextarea').focus(function () {
        $(this).animate({ height: "150px" }, 500);
    });

    // Show and hide Class/ability length

    $('.classLengthAbilityLevelRow').hide();

    $('#allVideos').click(function () {
        $('.classLengthAbilityLevelRow').hide();
    });

    $('#classes').click(function () {
        $('.classLengthAbilityLevelRow').show();
    });

    // Login reveal

    $('.loginRevealBtn').click(function () {
        $('.loginReveal').slideToggle();
        $('#Username').focus();
    });

    $('.loginRevealBtnRegister').click(function () {
        //close popup
        closeModal();
        $('.loginReveal').slideToggle();
    });


    // Scroll to functionality

    $(function () {
        $('.scrollTo').click(function () {
            if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
                var target = $(this.hash);
                target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                if (target.length) {
                    $('html,body').animate({
                        scrollTop: target.offset().top
                    }, 1000);
                    return false;
                }
            }
        });
    });


    // Equal height divs
    /*
        $.fn.setAllToMaxHeight = function () {
            return this.height(Math.max.apply(this, $.map(this, function (e) { return $(e).height() })));
        }

        $('.galleryItemWrap, .galleryList .block-section').setAllToMaxHeight();

    */

    // Add selected class to tab 


    /* Carousel */
    $(".imageGalleryImages").owlCarousel({
        items: 3,
        navigation: true,
        navigationText: ["prev", "next"],
        pagination: false,
        responsive: false
    });


    //
    $('.loginRevealBtn').click(function () {
        $('.loginReveal').show();
    });


    initSpinnerFunction();
});

if (Modernizr.touch) {
    $('body').addClass('touch');
}


$(document).ready(function () {
    var fbLink = 'http://www.facebook.com/share.php?u=http://www.yogaclicks.com' + window.location.pathname + $('.facebook-link-div').html();
    var twitterLink = 'http://www.twitter.com/share?url=http://www.yogaclicks.com' + window.location.pathname + $('.twitter-link-div').html() + '&hashtags=YogaHood';
    var pintrestLink = 'http://pinterest.com/pin/create/button/?url=http://www.yogaclicks.com' + window.location.pathname + $('.pintrest-link-div').html();
    $('.facebook-share-link').attr('href', fbLink);
    $('.twitter-share-link').attr('href', twitterLink);
    $('.pintrest-share-link').attr('href', pintrestLink);
});