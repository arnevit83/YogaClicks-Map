Modernizr.load({
  test: Modernizr.csstransitions,
  yep : '',
  nope: 'includes/js/animate-nav.js'
});


$(document).ready(function(){
	
	$('#menu').click(function() {
	  $('body').toggleClass('open-nav');
	  return false;
	});
	
	$('.filter a').click(function() {
	  $(this).toggleClass('selected');
	  return false;
	});
		
	$('.filter a').bind('touchend', function(e) {
        e.preventDefault();
        $(this).toggleClass('selected');
    });

	resizeDiv();
	$('#filter-wrap').sly('reload');
	$('#venues-wrap').sly('reload');
	$('#teachers-wrap').sly('reload');

});

window.onresize = function(event) {
	resizeDiv();
	$('#filter-wrap').sly('reload');
	$('#venues-wrap').sly('reload');
	$('#teachers-wrap').sly('reload');
}

function resizeDiv() {
	vpw = $(window).width(); 
	vph = $(window).height();
	$('#filter-wrap').css({'height': vph - 138 + 'px'});
	$('#venues-wrap').css({'height': vph - 138 + 'px'});
	$('#teachers-wrap').css({'height': vph - 138 + 'px'});
	$('.set-height').css({'height': vph - 138 + 'px'});
	$('.map-wrapper').css({'height': vph - 138 + 'px'});
	
	totalWidth = $('#navwrap').outerWidth(true);
	$('#content-slider').css({"marginLeft": totalWidth + 'px' });
	
}

$('#filter-wrap').sly({
	horizontal: 0,
	itemNav: null,
	dragHandle: 1,
	dynamicHandle: 1,
	//dragging: 1,
	startAt: 0,
	scrollBy: 20,
	scrollBar: '.scrollbar-vert',
	clickBar: 1,
	speed: 0
});


// FULL SCREEN BG CODE

$("body.unregistered #container").backstretch("images/backgrounds/unregistered-homepage.jpg");

$("body.register-page #container").backstretch("images/backgrounds/unregistered-homepage.jpg");


// FULL SCREEN BG CODE END

$(document).ready(function(){

	$('.fancylabels input').each(function(){	
	
		if( $(this).val().length >= 1){
			$(this).parent().addClass('filled');
		}
		
	});

});

		
$('.fancylabels input').focus(function(){	

	if( $(this).val().length == 0){
		$(this).parent().addClass('focus');
		$(this).parent().removeClass('filled');
	}
	
});

$('.fancylabels input').blur(function(){	

	if( $(this).val().length == 0){
		$(this).parent().removeClass('focus');
	}
	
});

$('.fancylabels input').keyup(function(){
	
	if( $(this).val().length == 0){
		$(this).parent().addClass('focus');
		$(this).parent().removeClass('filled');
	}
	
	if( $(this).val().length >= 1){
		$(this).parent().removeClass('focus');
		$(this).parent().addClass('filled');
	}

});

$('header input[type="search"]').focus(function(){	

	if( $(this).val().length == 0){
		$(this).parent().addClass('focus');
		$(this).parent().removeClass('filled');
	}
	
});

$('header input[type="search"]').blur(function(){	

	if( $(this).val().length == 0){
		$(this).parent().removeClass('focus');
	}
	
});

$('header input[type="search"]').keyup(function(){
	
	if( $(this).val().length == 0){
		$(this).parent().addClass('focus');
		$(this).parent().removeClass('filled');
	}
	
	if( $(this).val().length >= 1){
		$(this).parent().removeClass('focus');
		$(this).parent().addClass('filled');
	}

});