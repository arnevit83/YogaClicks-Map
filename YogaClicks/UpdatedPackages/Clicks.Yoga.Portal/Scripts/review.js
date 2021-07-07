$(document).ready(function () {
    var insertStarButtons = function () {
        $('#reviewForm input[type="radio"]').each(function () {
            if ($(this).hasClass("starradio")) {
                $(this).before('<div class="star starFindUnselected"  name="' + $(this).attr('name') + '">');
                if ($(this).attr('checked') == 'checked') {
                    $(this).prevAll('.starFindUnselected[name="' + $(this).attr('name') + '"]').addClass('starFindSelected');
                }
            }

        });
    }
    insertStarButtons();
    var starFunctions = function () {
        $('.star').click(function () {
            var name = $(this).attr('name');
            $('.starradio[name="' + name + '"]').removeAttr('checked');
            $(this).next('.starradio').attr('checked', true);
            $('.star[name="' + name + '"]').removeClass('starFindSelected');
            $(this).addClass('starFindSelected');
            $(this).prevAll('.star').addClass('starFindSelected');
        });
    }
    starFunctions();
});