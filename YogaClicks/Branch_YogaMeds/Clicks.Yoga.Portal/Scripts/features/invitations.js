(function ($) {
    $.fn.yogaInvitationButtons = function () {
        var $container = $(this);

        $container.on('click', '[data-role=InviteByEmailButton]', function (e) {
            e.preventDefault();

            var $button = $(this);
            var $popup = $button.closest('[data-role=InvitationPopup]');

            $popup.hide();
            openModal($button.data('action'), false, 400, 300);
        });

        //$container.on('click', '[data-role=InviteFromFacebookButton]', function (e) {
        //    e.preventDefault();
            
        //    var $button = $(this);
        //    var $popup = $button.closest('[data-role=InvitationPopup]');

        //    $popup.hide();

        //    facebookConnect(function () {
        //        facebookInvite();
        //    });
        //});
    };
})(jQuery);