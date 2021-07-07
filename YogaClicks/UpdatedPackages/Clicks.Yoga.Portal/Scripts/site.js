
$(document).ready(function () {
    $('#left-menu').sidr({
        name: 'sidr-left',
        onOpen: darkenMain,
        onClose: lightenMain
    });
    $('#right-menu').sidr({
        name: 'sidr-right',
        side: 'right',
        onOpen: darkenMain,
        onClose: lightenMain
    });

    $('#left-menu').click(function (e) {
        e.preventDefault();
    });

    $('#right-menu').click(function (e) {
        e.preventDefault();
    });
});

function darkenMain() {
    $('.dark-overlay').fadeIn(250);
    $('#left-menu').hide();
    $('.container-fluid.page-container').addClass('fix-container');
};

function lightenMain() {
    $('.dark-overlay').fadeOut(150);
    $('#left-menu').show();
    $('.container-fluid.page-container').removeClass('fix-container');
};

$('.menu-parent').click(function () {
    var $this = $(this),
        thisIcon = $this.find('.glyphicon');

    $('.glyphicon').not(thisIcon).each(function () {
        $(this).removeClass('menu-parent-open');
    });

    $('.menu-parent').not($this).each(function () {
        $(this).removeClass('active');
    });

    thisIcon.toggleClass('menu-parent-open');
    $this.toggleClass('active');
})

var lastScrollTop = 0;
$(window).scroll(function (event) {
    var st = $(this).scrollTop();
    if (st > lastScrollTop) {
        $(".scroll-to-top").fadeIn(500);
    }
    else {
        if ($(window).scrollTop() === 0) {
            $(".scroll-to-top").fadeOut(500);
        }
    }
    lastScrollTop = st;
});

$(".scroll-to-top").click(function () {
    $("html, body").animate({ scrollTop: 0 }, "fast");
    $(".scroll-to-top").fadeOut(500);
});

$('.notifications-button').click(function (e) {
    e.preventDefault();

    var notificationType = $(this).data('notification-type'),
    thisPopup = $('.notifications-pop-up.' + notificationType);

    $('.notifications-pop-up').not(thisPopup).hide();
    thisPopup.toggle();

    //if (thisPopup.css('display') == 'none') {
    //    $('.container-fluid.page-container').removeClass('fix-container');
    //}

    //else {
    //    $('.container-fluid.page-container').addClass('fix-container');
    //}
});

$('.search-icon').click(function () {
    if ($('.search-container').is(':hidden')) {
        $('.search-container').slideDown(300);
    }
    else {
        $('.search-container').slideUp(100);
    }

});


