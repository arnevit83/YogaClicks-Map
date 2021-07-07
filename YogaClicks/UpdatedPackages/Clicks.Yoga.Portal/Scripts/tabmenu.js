$(document).ready(function () {
 
  

    $('#nav-tabs li a').each(function () {
        var str = location.href.toLowerCase();

 

        if (str.indexOf($(this).attr("href").toLowerCase(),0) > -1) {
        
            $("li.active").removeClass("active");
            $(this).parent().addClass("active");
        }
    });

    var full_path = location.href.split("#")[0];

    $(".sub-menu li a").each(function () {

        var $this = $(this);
 
        if ($this.prop("href").split("#")[0] == full_path) {
        
            $(this).parent().addClass("active");

        }


    });

    $(".the-sub-menu a").each(function () {

        var $this = $(this);

        if ($this.prop("href").split("#")[0] == full_path) {

            $(this).parent().addClass("active");

        }


    });

    //$('.galleries #tabsNav li').removeClass('active');
    //$('.galleries #tabsNav li.galleries').addClass('active');


     //Wall tab
    $('.wall #tabsNav li').removeClass('active');
    $('.wall li#wallTab').addClass('active');


});
