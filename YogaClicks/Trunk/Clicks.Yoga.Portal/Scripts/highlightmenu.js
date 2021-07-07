$(document).ready(function () {
    /*
    $('.sub-menu').slideUp();

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
		
            $('#left-nav a#discover').next('.sub-menu').slideDown();
		
        } else if ( $('.content-col').hasClass('practice-strip') ) {
		
            $('#left-nav a#practice').next('.sub-menu').slideDown();
		
        } else if ( $('.content-col').hasClass('wisdom-strip') ) {
		
            $('#left-nav a#wisdom').next('.sub-menu').slideDown();
		
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
	
       /* $('.icons.friend').click(function () {
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
        });
*/		
    /*
    $('.fly-down').on("mouseenter", function () {
            $(this).show();
        }).on('mouseleave',function(){
            setTimeout(function () {
                $('.fly-down').hide();
                $('.icons').removeClass('active');
            }, 1000);		
        });
*/
    });

   
