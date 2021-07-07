(function ($) {
    $.fn.yogaNewsfeed = function () {
        var $container = $(this);
        var $stories = $container.find('div[data-role=Stories]');

        var retrieveOlder = function () {
            var latest = $stories.find('div[data-role=Story]').filter(':last').attr('data-id');

            $.ajax({
                type: 'POST',
                url: $stories.attr('data-retrieve-url'),
                data: { Latest: latest, Limit: 10 },
                success: function (response) {
                    $stories.append(response);
                }
            });
        };

        var retrieveNewer = function () {
            var earliest = $stories.find('div[data-role=Story]').filter(':first').attr('data-id');

            $.ajax({
                type: 'POST',
                url: $stories.attr('data-retrieve-url'),
                data: { Earliest: earliest },
                success: function (response) {
                    $stories.prepend(response);
                    toLocalDateTime($(".story .localDateTime", $stories).eq(0));
                }
            });
        };

        this.on('click', 'a[data-role=RetrieveOlderButton]', function (e) {
            e.preventDefault();
            retrieveOlder();
        });

        this.on('click', 'button[data-role=ShareStoryButton]', function (e) {
            e.preventDefault();

            var $button = $(this);

            var url = $button.attr('data-url');
            var storyId = $button.attr('data-story-id');

            $.ajax({
                type: 'POST',
                url: url,
                data: { StoryId: storyId },
                success: function (response) {
                    $stories.prepend(response);
                    window.alert('Post shared.');
                }
            });
        });

        $container.yogaWall({
            afterPost: function () {
                retrieveNewer();
            },
            afterDelete: function ($button) {
                $button.closest('[data-role=Story]').remove();
            }
        });
    };
})(jQuery);