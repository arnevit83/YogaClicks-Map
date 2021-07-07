jQuery("document").ready(function($) {

    var nav = $('.Menusubparent');
    var navbody = $('.container');
    $('.Menubarlinks').each(function () {
        $(this).addClass("MenubarlinksTrans");
    });

    

    $(window).scroll(function() {
        if ($(this).scrollTop() > 171 && $(window).width() > 752)
        {   nav.addClass("f-nav"); navbody.addClass("b-nav"); }
        else
        { nav.removeClass("f-nav"); navbody.removeClass("b-nav"); }
    });






});

