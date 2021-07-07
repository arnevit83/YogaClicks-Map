(function ($) {
    $.fn.yogaGlobalActorPicker = function () {
        var $picker = $(this);
        var $radios = $picker.find('input');

        $radios.on('change', function (e) {
            var $radio = $(this);

            $.ajax({
                type: 'POST',
                url: $radio.data('action'),
                contentType: 'application/json',
                data: JSON.stringify({
                    id: $radio.data('actor-id'),
                    typeName: $radio.data('actor-type-name')
                }),
                success: function (actor) {
                    var uri = '/' + actor.EntityType.Controller.toLowerCase() + '/' + actor.Id;

                    window.location = uri;
                    return;
                    var $label = $radio.closest('label');

                    $label.siblings().removeClass('selected');
                    $label.addClass('selected');

                    $('a.current-actor-link').attr('href', uri);
                    $('*.current-actor-name').text(actor.Name);

                    $('img.current-actor-image').each(function () {
                        var $image = $(this);
                        $image.attr('src', actor.Image.Url + $image.data('query'));
                    });

                    $('*.current-actor-placeholder').each(function () {
                        var $element = $(this);
                        $element.attr('placeholder', $element.data('placeholder-template').replace('{0}', actor.Name));
                    });

                    $('*.current-actor-human').each(function () {
                        $(this).data('actor-human', actor.EntityType.IsHuman);
                    });

                    $('.profileDropDown').slideUp();
                },
                error: function () {
                    e.preventDefault();
                }
            });
        });
    };

    $.fn.yogaActorSelector = function () {
        var $container = $(this);
        var $entityId = $container.find('[data-role=EntityId]');
        var $entityTypeName = $container.find('[data-role=EntityTypeName]');
        var $imageUrl = $container.find('[data-role=ImageUrl]');
        var $selection = $container.find('[data-role=Selection]');
        var $selectionImage = $selection.find('img');
        var $selectionName = $selection.find('span');
        var $optionsContainer = $container.find('[data-role=OptionsContainer]');
        var $options = $optionsContainer.find('> div');

        var refreshOptions = function () {
            $options.each(function () {
                var $option = $(this);
                var selected =
                    $option.data('entity-id') == $entityId.val() &&
                    $option.data('entity-type-name') == $entityTypeName.val();

                $option.toggle(!selected);
            });
        };

        if ($options.length > 1) {
            $selection.on('click', function (e) {
                $optionsContainer.show();
                e.stopPropagation();
            });

            $options.on('click', function (e) {
                var $option = $(this);
                var $image = $option.find('img');

                $entityId.val($option.data('entity-id'));
                $entityTypeName.val($option.data('entity-type-name'));
                $imageUrl.val($image.data('uri'));

                $entityId.trigger('change');
                $entityTypeName.trigger('change');
                $imageUrl.trigger('change');

                $selectionImage.attr('src', $image.attr('src'));
                $selectionName.text($option.data('name'));

                refreshOptions();
            });

            $(document).on('click', function () {
                $optionsContainer.hide();
            });

            refreshOptions();
        }

        $imageUrl.val($selectionImage.data('uri'));
    };
})(jQuery);