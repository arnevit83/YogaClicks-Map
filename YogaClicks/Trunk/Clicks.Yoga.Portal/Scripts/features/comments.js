(function ($) {
    $.fn.yogaComments = function () {
        this.on('click', 'button[data-role=CommentButton]', function (e) {
            e.preventDefault();

            var $button = $(this);
            var $container = $button.parent();
            var $content = $container.find('[data-role=CommentContent]');

            var url = $button.attr('data-url');
            var subjectId = $button.attr('data-subject-id');
            var subjectTypeName = $button.attr('data-subject-type-name');
            var content = $content.val();

            if (content.trim() == "") return;
            
            if (content.split(/[^\w'-]+/i).length > 300) {
                alert('Please enter 300 words or less.');
                return;
            }

            $.ajax({
                type: 'POST',
                url: url,
                contentType: 'application/json',
                data: JSON.stringify({
                    Subject: {
                        EntityId: subjectId,
                        EntityTypeName: subjectTypeName,
                    },
                    Content: content
                }),
                success: function (response) {
                    var $commentContainer = $button.closest('[data-role=CommentsContainer]');
                    $commentContainer.find('[data-role=Comments]').append(response);
                    toLocalDateTime($(".comment .localDateTime", $commentContainer).last());
                    $content.val('');
                }
            });
        });

        this.on('click', 'a[data-role=DeleteCommentButton]', function (e) {
            e.preventDefault();

            if (!window.confirm("Are you sure you want to delete this comment?"))
                return;

            var $button = $(this);
            var $comment = $button.closest('[data-role=Comment]');

            var url = $button.attr('data-url');
            var subjectId = $button.attr('data-subject-id');
            var subjectTypeName = $button.attr('data-subject-type-name');
            var commentId = $button.attr('data-comment-id');

            $.ajax({
                type: 'POST',
                url: url,
                contentType: 'application/json',
                data: JSON.stringify({
                    Subject: {
                        EntityId: subjectId,
                        EntityTypeName: subjectTypeName,
                    },
                    CommentId: commentId
                }),
                success: function () {
                    $comment.remove();
                }
            });
        });
    };
})(jQuery);