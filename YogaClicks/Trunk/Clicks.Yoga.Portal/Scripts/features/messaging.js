(function ($) {
    $.fn.yogaMessageDeliveries = function () {
        var $actions = this.find('div[data-role=Actions]');
        var $messages = this.find('div[data-role=Messages]');

        $actions.find('input[data-role=Search]').on('keyup', function () {
            var $this = $(this);
            var text = $this.val();
            var keywords = text.split(/\s+/);

            $messages.find('div[data-role=Message]').each(function () {
                var $message = $(this);
                var content = $message.find('input[data-role=Content]').val();
                
                for (var i in keywords) {
                    var keyword = keywords[i];
                    
                    if (content.search(keyword) > -1) {
                        $message.show();
                        return;
                    }

                    $message.hide();
                }
            });
        });

        $actions.find('button[data-role=ActionButton]').on('click', function () {
            $.ajax({
                type: 'POST',
                url: $(this).attr('data-url'),
                data: $messages.find('input[type=checkbox]').serialize(),
                success: function (response) {
                    $actions.find('input[data-role=Search]').val("");
                    $messages.html(response);
                    
                    $.ajax({
                        type: 'POST',
                        url: '/Messaging/UnreadCount',
                        success: function (value) {
                            $('.MessagesIndicator').each(function () {
                                var $this = $(this);
                                var format = $this.attr('data-format') || "{0}";
                                var text = value > 0 ? format.replace('{0}', '<span>' + value + '</span>') : '';
                                $this.html(text);
                            });
                        }
                    });
                }
            });
        });

        $messages.on('click', 'a[data-role=ExpandButton]', function () {
            var $this = $(this);
            var $message = $this.closest('[data-role=Message]');
            
            $message.find('[data-role=Preview]').hide();
            $message.find('[data-role=Content]').show();
        });
    };
})(jQuery);

(function ($) {
    $.fn.yogaMessageConversation = function () {
        var $messages = this.find('output');
        var $reply = this.find('div[data-role=Reply]');
        var $senderPicker = this.find('[data-role=ActorPicker]');
        var $replyContent = $reply.find('textarea');
        var $replyButton = $reply.find('button');

        $replyButton.on('click', function () {
            $.ajax({
                type: 'POST',
                url: $replyButton.attr('data-url'),
                contentType: 'application/json',
                data: JSON.stringify({
                    ConversationId: $replyButton.attr('data-conversation-id'),
                    Content: $replyContent.val()
                }),
                success: function (response) {
                    $messages.html(response);
                    $replyContent.val('');
                }
            });
        });
    };
})(jQuery);


$(function () {
    var $indicators = $('.MessagesIndicator');
    var $popup = $('#MessagesPopup');
    var $output = $popup.find('output');

    var count = function () {
        $.ajax({
            type: 'POST',
            url: '/Messaging/UnreadCount',
            success: function (value) {
                $indicators.each(function () {
                    var $this = $(this);
                    var format = $this.attr('data-format') || "{0}";
                    var text = value > 0 ? format.replace('{0}', '<span>' + value + '</span>') : '';
                    $this.html(text);
                });
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
            url: '/Messaging/PopupPartial',
            success: function (response) {
                $popup.slideToggle();
                $output.html(response);
                $('.notifications a').removeClass('active');
                $('.MessagesIndicator').addClass('active');
                $('#NotificationsPopup').hide();
                $('#FriendRequestsPopup').hide();
                $('.profileDropDown').hide();
                count();
            }
        });
    });

    count();
});

