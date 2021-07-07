//$(document).ready(function() {
//var insertCheckboxes = function () {
//    $('input[type="checkbox"]').each(function () {
//        $(this).before('<div class="check"  name="' + $(this).attr('id') + '">');
//    });
//}
//    insertCheckboxes();
//    var checkFunctions = function () {
//        $('.check').click(function () {
//            if (!$(this).hasClass('checkSelected') && !$(this).hasClass('checkDisabled') && !$(this).hasClass('checkSelectedDisabled')) {
//                $(this).nextAll('input[value="true"]').attr('checked', true);
//                $(this).nextAll('input[value="false"]').removeAttr('checked');
//                $(this).addClass('checkSelected');
//            }
//            else if ($(this).hasClass('checkSelected') && !$(this).hasClass('checkSelectedDisabled')) {
//                $(this).nextAll('input[value="false"]').attr('checked', true);
//                $(this).nextAll('input[value="true"]').removeAttr('checked');
//                $(this).removeClass('checkSelected');
//            }
//        });
//    }
//    checkFunctions();
//});