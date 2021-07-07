(function ($) {
    $.fn.yogaWall = function (options) {
        var $container = $(this);
        var $media = $container.find('div[data-role=PostMedia]');

        var $addVideoButton = $container.find('button[data-role=AddVideoButton]');
        var $addPhotosButton = $container.find('button[data-role=UploadButton]');
        var $postsEmpty = $('#page-content', window.parent.document).find('[data-role=PostsEmpty]');

        options = $.extend({
            afterPost: function (html) {
                $container.find('[data-role=Posts]').prepend(html);
            },
            afterShare: function () {},
            afterDelete: function ($button) {
                $button.closest('[data-role=Post]').remove();

                if ($media.find('[data-role=Posts]').length > 0) return;

                $postsEmpty.show();
            }
        }, options);

        $container.find('[data-role=PostContainer]').yogaGallery({
            beforeUpload: function () {
                $container.find('[data-role=PostMediaResource]').remove();
            },
            afterUpload: function() {
                disableButtons($addPhotosButton, $addVideoButton);
            }
        });

        var scanMedia = function () {
            if ($media.find('[data-role=PostMediaResource]').length > 0) return;

            var $textarea = $(this);
            var match = /(?:https?:\/\/)?([a-z][a-z0-9-]+(?:\.[a-z][a-z0-9-]+)+[^\s]*)/i.exec($textarea.val());

            if (match) {
                var url = match[0];

                if (!/^https?:\/\//.test(url)) {
                    url = 'http://' + url;
                }

                $.ajax({
                    type: 'POST',
                    url: $container.data('media-preview-url'),
                    data: { uri: url },
                    success: function (html) {
                        if (!html) return;

                        $container.find('[data-role=PostMediaResource]').remove();

                        var $resource = $(html);
                        var $image = $resource.find('[data-role=PostMediaImage]');

                        $media.append($resource);

                        disableButtons($addPhotosButton, $addVideoButton);

                        $.ajax({
                            type: 'POST',
                            url: $image.data('scan-url'),
                            data: { uri: $resource.data('resource-uri') },
                            success: function (html) {
                                $image.html(html);
                            }
                        });
                    }
                });
            }
        };

        this.on('keyup', 'textarea', _.debounce(scanMedia, 1000));
        this.on('change paste', 'textarea', scanMedia);

        this.on('click', 'button[data-role=PostButton]', function (e) {
            e.preventDefault();
            var $button = $(this);
            var $container = $button.closest('[data-role=PostContainer]');
            var $content = $container.find('[data-role=PostContent]');
            var $postsEmpty = $('#page-content', window.parent.document).find('[data-role=PostsEmpty]');

            var url = $button.attr('data-url');
            var content = $content.val();

            var $resources = $container.find('[data-role=PostMediaResource]');
            var resourceUris = [];

            $resources.each(function () {
                var $resource = $(this);
                var uri = $resource.attr('data-resource-uri');
                resourceUris.push(uri);
            });

            if (content.trim() == "" && resourceUris.length == 0) return;

            if (content.split(/[^\w'-]+/i).length > 300) {
                alert('Please enter 300 words or less.');
                return;
            }

            $.ajax({
                type: 'POST',
                url: url,
                contentType: 'application/json',
                data: JSON.stringify({
                    Content: content,
                    ResourceUris: resourceUris
                }),
                success: function (response) {
                    options['afterPost'](response);
                    toLocalDateTime($(".post .localDateTime", ".posts").eq(0));
                    $content.val('');
                    $resources.remove();
                    $postsEmpty.hide();
                    enableButtons($addPhotosButton, $addVideoButton);
                }
            });
        });

        this.on('click', 'button[data-role=SharePostButton]', function (e) {
            e.preventDefault();

            var $button = $(this);

            var url = $button.attr('data-url');
            var postId = $button.attr('data-post-id');

            $.ajax({
                type: 'POST',
                url: url,
                data: { PostId: postId },
                success: function (response ) {
                    options['afterShare'](response);
                    window.alert('Post shared.');
                }
            });
        });

        this.on('click', 'a[data-role=DeletePostButton]', function (e) {
            e.preventDefault();

            if (!window.confirm("Are you sure you want to delete this?"))
                return;

            var $button = $(this);

            var url = $button.attr('data-url');
            var postId = $button.attr('data-post-id');

            $.ajax({
                type: 'POST',
                url: url,
                data: { PostId: postId },
                success: function () {
                    options['afterDelete']($button);
                }
            });
        });

        this.on('click', 'button[data-role=AddVideoButton]', function (e) {
            e.preventDefault();

            var $button = $(this);
            openModal($button.data('url'), false);
        });

        this.on('click', 'div[data-role=PostMediaResource] a[data-role=RemoveButton]', function (e) {
            e.preventDefault();

            $(this).closest('[data-role=PostMediaResource]').remove();

            if ($media.find('[data-role=PostMediaResource]').length > 0) return;

            enableButtons($addPhotosButton, $addVideoButton);
        });
    };

    $.fn.yogaWallAddVideoModal = function () {
        var $container = this;
        var $resources = $('#page-content', window.parent.document).find('[data-role=PostMedia]');
        var $addVideoButton = $('#page-content', window.parent.document).find('button[data-role=AddVideoButton]');
        var $addPhotosButton = $('#page-content', window.parent.document).find('button[data-role=UploadButton]');

        $container.find('button[data-role=AddButton]').on('click', function (e) {
            e.preventDefault();

            var $button = $(this);

            $.ajax({
                type: 'POST',
                url: $button.attr('data-url'),
                data: $button.parent().find('input').serialize(),
                success: function (video) {
                    $.ajax({
                        type: 'POST',
                        url: $container.data('media-preview-url'),
                        data: { uri: 'urn:yogaclicks:entity:Video:' + video.Id },
                        success: function (html) {
                            $resources.find('[data-role=PostMediaResource]').remove();
                            $resources.append(html);
                            disableButtons($addPhotosButton, $addVideoButton);
                            closeModal();
                        }
                    });
                },
                error: function () {
                    alert("Please provide a valid YouTube or Vimeo video URL.");
                }
            });
        });
    };

    var disableButtons = function(photosButton, videoButton) {
        photosButton.attr('disabled', 'disabled');
        photosButton.css('opacity', 0.6);
        videoButton.attr('disabled', 'disabled');
        videoButton.css('opacity', 0.6);
    };

    var enableButtons = function (photosButton, videoButton) {
        photosButton.removeAttr('disabled');
        photosButton.css('opacity', 1);
        videoButton.removeAttr('disabled');
        videoButton.css('opacity', 1);
    };
})(jQuery);