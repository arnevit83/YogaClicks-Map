function setFullHeight(target, widthOrHeight, offset) {

    function makeFullHeight() {

        // Subtract offset from window width for gallery
        if (offset === true) {
            var winWidth = $(window).width() - 290;
        } else {
            var winWidth = $(window).width();
        }

        var winHeight = $(window).height();
        var sPanelHeight = $('#locSearchPanel').height();

        //SET divHeight depending on window
        if (winHeight > sPanelHeight) {

            if (widthOrHeight == 'height') {
                target.css('height', winHeight);
            } else {
                // Unfixed ratio. Just skew dimensions to window
                target.css('width', winWidth);
                target.css('height', winHeight);
            }

        }
        else {

            if (widthOrHeight == 'height') {
                target.css('height', sPanelHeight);
            } else {
                // Unfixed ratio. Just skew dimensions to window
                target.css('width', sPanelHeight);
                target.css('height', sPanelHeight);
            }
        }

    }

    makeFullHeight();

    // Fire on window resize
    $(window).resize(makeFullHeight);

    $(window).resize();

}
