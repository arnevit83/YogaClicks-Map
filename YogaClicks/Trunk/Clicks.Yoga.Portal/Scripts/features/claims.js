(function ($) {
    $.fn.yogaClaimPanel = function () {
        var $panel = this;

        $panel.find('button[data-role="ClaimButton"]').on('click', function (e) {
            e.preventDefault();

            var $button = $(this);
            
            var entityId = $button.attr('data-entity-id');
            var entityTypeName = $button.attr('data-entity-type-name');
            
            if (entityTypeName == "Venue") {
                openModal("/Venues/Claim?Id=" + entityId);
                return;
            }

            if (entityTypeName == "TeacherTrainingOrganisation" && !window.confirm("You are about to claim this.")) {
                return;
            }

            $.ajax({
                type: 'POST',
                url: $button.attr('data-url'),
                data: {
                    EntityId: entityId,
                    EntityTypeName: entityTypeName
                },
                success: function (response) {
                    $panel.replaceWith(response);
                }
            });
        });

        $panel.find('button[data-role="UnclaimButton"]').on('click', function (e) {
            e.preventDefault();

            var $button = $(this);

            var entityId = $button.attr('data-entity-id');
            var entityTypeName = $button.attr('data-entity-type-name');

            if (!window.confirm("Are you sure you wish to unclaim this " + entityTypeName + "?")) return;

            $.ajax({
                type: 'POST',
                url: $button.attr('data-url'),
                data: {
                    EntityId: entityId,
                    EntityTypeName: entityTypeName
                },
                success: function (response) {
                    location.href = response.Location;
                }
            });
        });
    };
})(jQuery);
