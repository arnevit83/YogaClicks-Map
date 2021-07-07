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
//# sourceMappingURL=Unused.js.map