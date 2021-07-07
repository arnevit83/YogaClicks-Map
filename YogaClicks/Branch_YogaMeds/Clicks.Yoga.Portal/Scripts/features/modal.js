function openModal(url, reload, width, height, close) {
    reload = typeof reload == "undefined" ? true : reload;
    width = typeof width == "undefined" ? 605 : width;
    height = typeof height == "undefined" ? 500 : height;

    $.fancybox({
        type: 'iframe',
        scrolling: 'no',
        href: url,
        autoScale: false,         //if using older fancybox
        autoSize: false,         //if using newer fancybox
        autoDimensions: false,
        fitToView: true,
        width: width,
        height: height,
        closeBtn: true,
        nextSpeed: 0, //important
        prevSpeed: 0, //important
        helpers: {
            overlay: {
                closeClick: false
            }
        },
        beforeShow: function () {
            //this.width = $('.fancybox-iframe').contents().find('html').width();
            //this.height = $('.fancybox-iframe').contents().find('html').height();
        },
        beforeClose: function () {
            if (close) {
                close();
            }

            if (reload) {
                setTimeout(function () {
                    top.location.reload();
                }, 0);
            }
        }
    });

    /*
        $.fancybox({
            type: 'iframe',
            href: url,
            scrolling: 'no',
            width: 605,
            height: 600,
            helpers: {
                overlay: {
                    css: { 'overflow': 'hidden' }
                }
            }
        });
        */
}

function closeModal(destination) {
    if (destination) {
        top.location.href = destination;
    }
    else {
        top.$.fancybox.close();
    }
}

/*
$('a', $('#youriframeID').contentDocument).bind('click', function() {
    preventDefault();
    $('#yourIframeID').attr('src', $(this).attr('href'));
    $('#yourIframeID').load(function() {
        $.fancybox.resize;
    });
});

*/

$(document).ready(function () {
    $('.pink').click(function () {

        var $this = $(this),
            $input = $this.closest('input'),
            $disable = $this.data('disable');

        if ($disable == undefined) {
            $input.after('<input class="button pink large greybg" type="button" value="' + $input.val() + '" autocomplete="off" disabled="disabled">');
            $input.hide();
        }
    });

    $('.repeat-radio-true').change(function () {
        if ($(this).is(':checked')) {
            $('.repeat-frequency').show();
        }
    });

    $('.repeat-radio-false').change(function () {
        if ($(this).is(':checked')) {
            $('.repeat-frequency').hide();
        }
    });

    $('#RepeatFrequency_Selection').change(function () {
        if ($(this).val() == 3) {
            $('.repeat-days-container').show();
        }
        else {
            $('.repeat-days-container').hide();
        }
    });

    if ($('.repeat-radio-true').is(':checked')) {
        $('.repeat-frequency').show();
    }

    if ($('#RepeatFrequency_Selection :selected').val() == 3) {
        $('.repeat-days-container').show();
    }
});


