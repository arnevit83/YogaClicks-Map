$(function () {
    var $indicators = $('.NotificationsIndicator');
    var $lists = $('.Notifications');
    var $popup = $('#NotificationsPopup');
    var $output = $popup.find('output');
    var $empties = $lists.find('[data-role=EmptyMessage]');

    var count = function () {
        $.ajax({
            type: 'POST',
            url: '/Notifications/Count',
            success: function (value) {
                $indicators.each(function () {
                    var $this = $(this);
                    var format = $this.attr('data-format') || "{0}";
                    var text = value > 0 ? format.replace('{0}', '<span>' + value + '</span>') : '';
                    $this.html(text);
                });

                $empties.toggle(value == 0);
            }
        });
    };

    var complete = function (element, accept) {
        var $element = $(element);
        var requestId = $element.attr('data-id');

        $.ajax({
            type: 'POST',
            url: '/Notifications/' + (accept ? 'Accept' : 'Reject'),
            data: { RequestId: requestId },
            success: function () {
                $element.closest('[data-role=Request]').remove();
                count();
            }
        });
    };

    $indicators.on('click', function () {
        if ($popup.is(':visible')) {
            //$popup.hide();
            $popup.slideUp();
            return;
        }

        $.ajax({
            type: 'POST',
            url: '/Notifications/PopupPartial',
            success: function (response) {
                $popup.slideToggle();
                $output.html(response);
                $('.notifications a').removeClass('active');
                $('.NotificationsIndicator').addClass('active');
                $('#FriendRequestsPopup').hide();
                $('#MessagesPopup').hide();
                $('.profileDropDown').hide();  
                count();
            }
        });
    });

    $lists.on('click', '[data-role=AcceptButton]', function () {
        complete(this, true);
    });

    $lists.on('click', '[data-role=RejectButton]', function () {
        complete(this, false);
    });

    count();

});
