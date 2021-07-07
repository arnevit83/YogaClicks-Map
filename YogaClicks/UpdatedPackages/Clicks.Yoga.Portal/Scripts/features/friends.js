$(function () {
    var $indicators = $('.FriendRequestsIndicator');
    var $lists = $('.FriendRequests');
    var $popup = $('#FriendRequestsPopup');
    var $output = $popup.find('output');
    var $empties = $lists.find('[data-role=EmptyMessage]');

    var count = function () {
        $.ajax({
            type: 'POST',
            url: '/Notifications/FriendRequestCount',
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

    var complete = function (element, action) {
        var $element = $(element);
        var requestId = $element.attr('data-id');

        $.ajax({
            type: 'POST',
            url: '/Notifications/' + action,
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
            url: '/Notifications/FriendRequestsPopupPartial',
            success: function (response) {
                $popup.slideToggle();
                $output.html(response);
                $('.notifications a').removeClass('active');
                $('.FriendRequestsIndicator').addClass('active');
                $('#MessagesPopup').hide();
                $('#NotificationsPopup').hide();
                $('.profileDropDown').hide();
                count();
            }
        });
    });

    $lists.on('click', '[data-role=AcceptButton]', function () {
        complete(this, 'Accept');
    });

    $lists.on('click', '[data-role=RejectButton]', function () {
        complete(this, 'Reject');
    });

    $lists.on('click', '[data-role=BlockButton]', function () {
        complete(this, 'Block');
    });

    count();
});