(function ($) {
    $.fn.yogaDatePicker = function () {
        var $picker = $(this);
        var $year = $picker.find('[data-role=Year]');
        var $month = $picker.find('[data-role=Month]');
        var $day = $picker.find('[data-role=Day]');

        $year.add($month).on('change', function () {
            var year = $year.val();
            var month = $month.val() - 1;

            for (var i = 29; i <= 31; i++) {
                var date = new Date(year, month, i, 0, 0, 0, 0);
                var valid = date.getFullYear() == year && date.getMonth() == month && date.getDate() == i;
                var $option = $day.find('option[value=' + i + ']');
                var exists = $option.length > 0;

                if (valid && !exists) {
                    $option = $('<option value="' + i + '">' + i + '</option>');
                    $day.append($option);
                }
                else if (!valid && exists) {
                    if ($option.prop('selected')) {
                        $option.prev().prop('selected', true);
                    }

                    $option.remove();
                }
            }
        });
    };
})(jQuery);