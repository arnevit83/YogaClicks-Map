(function ($) {
    $.fn.yogaSearchMap = function (criteria) {
        for (var i in criteria) {
            if (criteria[i] === null) delete criteria[i];
        }

        var $container = this;
        var $map = $container.find('div[data-role=Map]');
        var $results = $container.find('div[data-role=Results]');
        var $template = $results.find('div[data-role=ResultTemplate]');
        var $counts = $("#containerhead").find('[data-role=ResultCount]');
        var $multiple = $("#containerhead").find('[data-role=ResultMultiple]');

        var template = Hogan.compile($('<div/>').append($template.remove()).html().trim());
        var information = new google.maps.InfoWindow();
        var _noResults = false;
        var _isFirst = true;

        var zoom = null;

        if (!criteria.Latitude || !criteria.Longitude || (!criteria.Radius && !criteria.LocationRadius)) {
            zoom = 4;
        }
        else {
            var radius = (criteria.LocationRadius || 0) + (criteria.Radius || 0);

            if (radius < 2000)
                zoom = 17;
            else if (radius < 5000)
                zoom = 16;
            else if (radius < 10000)
                zoom = 15;
            else if (radius < 20000)
                zoom = 13;
            else if (radius < 50000)
                zoom = 11;
            else if (radius < 100000)
                zoom = 10;
            else if (radius < 200000)
                zoom = 8;
            else if (radius < 1000000)
                zoom = 7;
            else if (radius < 2500000)
                zoom = 6;
            else if (radius < 5000000)
                zoom = 5;
            else
                zoom = 4;
        }


        var latitude = 0;
        var longitude = 0;

        if (criteria.Latitude && criteria.Longitude) {
            latitude = criteria.Latitude;
            longitude = criteria.Longitude;
        }
        else {
            latitude = 55.3780510;
            longitude = -3.4359730;
        }

        var image = new google.maps.MarkerImage('/images/blank-map-icon3.png',
                new google.maps.Size(35, 35),  // size
                new google.maps.Point(0, 0),     // origin
                new google.maps.Point(17, 35) // anchor
            );

        var map;

        if (criteria.Types == "Teacher") {
            map = new google.maps.Map(
                $map.get(0),
                {
                    center: new google.maps.LatLng(latitude, longitude),
                    zoom: zoom,
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    minZoom: 4,
                    maxZoom: 15
                });
        } else {
            map = new google.maps.Map(
                $map.get(0),
                {
                    center: new google.maps.LatLng(latitude, longitude),
                    zoom: zoom,
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    minZoom: 4
                });
        }


        var clusterer = new MarkerClusterer(map);
        var markers = {};

        var render = function (ajaxResult) {
            var results = ajaxResult.results;
            if (ajaxResult.empty)
                _noResults = true;
            else _noResults = false;

            var ids = [];

            for (var i in results) {
                var result = results[i];

                if (markers[result.Id]) continue;
                if (!result.Latitude || !result.Longitude) continue;

                ids.push(result.Id);

                var marker;

                if (criteria.Types == "Teacher") {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(result.Latitude, result.Longitude),
                        title: result.Name,
                        result: result,
                        icon: image
                    });
                } else {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(result.Latitude, result.Longitude),
                        title: result.Name,
                        result: result
                    });
                }

                clusterer.addMarker(marker);

                markers[result.Id] = marker;

                google.maps.event.addListener(marker, 'click', function () {
                    var sender = this;

                    $.ajax({
                        type: 'POST',
                        url: $map.attr('data-information-url'),
                        data: { EntityId: sender.result.Id },
                        success: function (response) {
                            information.setContent(response);
                            information.open(map, sender);
                        }
                    });
                });
            }

            for (var id in markers) {
                if ($.inArray(id, ids)) continue;

                var removed = markers[id];
                clusterer.removeMarker(removed);
                delete markers[id];
            }

            $results.empty();

            for (var j in results) {
                var result = results[j];
                $(template.render(result).trim()).appendTo($results).show();
            }

            $counts.html(results.length);

            if (results.length != 1) {
                $multiple.html("results");
            } else {
                $multiple.html("result");
            }
        };

        var search = function () {

            if (_noResults && _isFirst) {
                _isFirst = false;
                openModal("/Search/NoResults", false, 500, 300);
            }

            var bounds = map.getBounds();
            var center = map.getCenter();

            var params = {
                SortOrder: 'Distance'
            };

            if (center) {
                params.Latitude = center.lat();
                params.Longitude = center.lng();
            }

            if (bounds) {
                var ne = bounds.getNorthEast();
                var sw = bounds.getSouthWest();

                params.NorthBound = ne.lat();
                params.SouthBound = sw.lat();
                params.EastBound = ne.lng();
                params.WestBound = sw.lng();
            }

            $.ajax({
                type: 'POST',
                url: $map.attr('data-search-url'),
                data: $.extend(criteria, params),
                success: render
            });
        };

        (function () {
            var timeout = null;

            google.maps.event.addListener(map, 'bounds_changed', function () {
                if (timeout) clearTimeout(timeout);
                timeout = window.setTimeout(search, 250);
            });
        })();

        search();
    };
})(jQuery);
