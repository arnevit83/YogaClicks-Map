(function ($) {
    $.fn.yogaVideos = function () {
        var $container = this;
        var $videos = $container.find('[data-role=Videos]');
        var $filters = $container.find('[data-role=Filters]');


        $container.find('button[data-role=CreateButton]').on('click', function (e) {
            e.preventDefault();

            var $this = $(this);

            $.ajax({
                type: 'POST',
                url: $this.attr('data-url'),
                data: $this.parent().find('input').serialize(),
                success: function () {
                    location.reload();
                },
                error: function () {
                    alert("Please provide a valid YouTube or Vimeo video URL.");
                }
            });
        });
        
        $container.find('button[data-role=UpdateButton]').on('click', function (e) {
            e.preventDefault();

            var $this = $(this);

            $.ajax({
                type: 'POST',
                url: $this.attr('data-url'),
                data: $this.closest('[data-role=Video]').find('input, select, textarea').serialize(),
                success: function () {
                    openModal($this.data("success"), false, 400, 150);
                }
            });
        });

        $videos.on('click', 'a[data-role=DeleteButton]', function (e) {
            e.preventDefault();

            if (!window.confirm("Are you sure you want to delete this video?")) return;
            
            var $this = $(this);

            $.ajax({
                type: 'POST',
                url: $this.attr('data-url'),
                data: {
                    VideoId: $this.attr('data-id'),
                    EntityId: $this.attr('data-entity-id'),
                    EntityTypeName: $this.attr('data-entity-type-name')
                },
                success: function () {
                    location.reload();
                }
            });
        });

        var filter = function () {
            $.ajax({
                type: 'POST',
                url: '/galleries/' + $("#EntityId").val() + '/filteredvideospartial',
                data: $filters.find('input, select, textarea').serialize(),
                success: function (data) {
                    $("#Videos").html(data);
                }
            });
        };

        $filters.find('input, select').on('change', function () {
            filter();
        });

        $videos.on('click', 'a[data-role=SelectPage]', function (e) {
            e.preventDefault();

            $("#page").val($(this).data("page"));
            filter();
        });
    };
})(jQuery);