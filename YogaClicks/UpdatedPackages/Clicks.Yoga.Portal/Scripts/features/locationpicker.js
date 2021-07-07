(function ($) {
    $.fn.yogaLocationPicker = function (options) {
        if (!options) options = {};

        var $container = this;

        var $query = $container.find('input[data-role=Location]');
        var $latitude = $container.find('input[data-role=Latitude]');
        var $longitude = $container.find('input[data-role=Longitude]');
        var $radius = $container.find('input[data-role=Radius]');
        var $acceptButton = $container.find('button[data-role=AcceptButton]');
        var $submitButton = $container.find('button[data-role=SearchSubmit]');
        var $form = $container.find('form[data-role=SearchForm]');

        var $line1 = $container.find('input[data-role=Line1]');
        var $line2 = $container.find('input[data-role=Line2]');
        var $city = $container.find('input[data-role=City]');
        var $county = $container.find('input[data-role=Area]');
        var $country = $container.find('select[data-role=Country]');
        var $postcode = $container.find('input[data-role=Postcode]');

        var $mapContainer = $('#venueAddress');
        var $map = $container.find('div[data-role=Map]');
        var $staticMap = $container.find('[data-role=StaticMap]');
        var $suggestions = $container.find('div[data-role=LocationSuggestions]');
        
        var $countriesWithoutPostcodes = ["Angola", "Antigua and Barbuda", "Aruba", "Bahamas", "Belize", "Benin", "Botswana", "Burkina Faso", "Burundi", "Cameroon", "Central African Republic", "Comoros", "Congo", "Congo, the Democratic Republic of the", "Cook Islands", "Cote d'Ivoire", "Djibouti", "Dominica", "Equatorial Guinea", "Eritrea", "Fiji", "French Southern Territories", "Gambia", "Ghana", "Grenada", "Guinea", "Guyana", "Hong Kong", "Ireland", "Jamaica", "Kenya", "Kiribati", "Macao", "Malawi", "Mali", "Mauritania", "Mauritius", "Montserrat", "Nauru", "Netherlands Antilles", "Niue", "Korea, Democratic People's Republic of", "Panama", "Qatar", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia", "Sao Tome and Principe", "Saudi Arabia", "Seychelles", "Sierra Leone", "Solomon Islands", "Somalia", "South Africa", "Suriname", "Syrian Arab Republic", "Tanzania, United Republic of", "Timor-Leste", "Tokelau", "Tonga", "Trinidad and Tobago", "Tuvalu", "Uganda", "United Arab Emirates", "Vanuatu", "Yemen", "Zimbabwe"];
        
        var latitude;
        var longitude;
        var zoom;

        if ($latitude.val() != 0 && $longitude.val() != 0) {
            latitude = $latitude.val();
            longitude = $longitude.val();
            zoom = 16;
        }
        else if (google.loader.ClientLocation) {
            latitude = google.loader.ClientLocation.latitude;
            longitude = google.loader.ClientLocation.longitude;
            zoom = 16;
        }
        else {
            latitude = 55.3780510;
            longitude = -3.4359730;
            zoom = 5;
        }



        var center = new google.maps.LatLng(latitude, longitude);
        var map = null;
        var marker = null;

        if ($map.length > 0) {
            map = new google.maps.Map($map.get(0), {
                center: center,
                zoom: zoom,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            $container.data('map', map);

            marker = new google.maps.Marker({
                position: center,
                map: map,
                draggable: true
            });
        }

        var geocoder = new google.maps.Geocoder();

        var updateLocation = function (location) {
            if ($latitude) $latitude.val(location.lat());
            if ($longitude) $longitude.val(location.lng());

            reverseGeocode(location, function (results) {
                if (results.length == 0) return;
                updateAddress(results[0]);
            });
        };

        var updateBounds = function (bounds) {
            if ($radius.length > 0 && google.maps.geometry && bounds) {
                var sw = bounds.getSouthWest();
                var ne = bounds.getNorthEast();
                var distance = google.maps.geometry.spherical.computeDistanceBetween(sw, ne);
                var radius = parseInt(distance / 2);

                $radius.val(radius);
            }
            else {
                $radius.val('0');
            }
        };

        var updateStaticMap = function () {
            $staticMap.empty();

            if ($staticMap.length == 0) return;

            if ($latitude.val() && $longitude.val()) {
                var src = $staticMap.attr('data-src');
                src = src.replace(/\{latitude\}/g, $latitude.val());
                src = src.replace(/\{longitude\}/g, $longitude.val());
                $('<img/>', { src: src }).appendTo($staticMap);
            }
        };

        updateStaticMap();

        var updateAddress = function (result) {
            var firstLine = "";

            for (var j in result.address_components) {
                var component = result.address_components[j];
                var type = component.types[0];
                var longName = component.long_name;
                var shortName = component.short_name;
                var input = result.address_components[0].long_name;

                var city;
                var county;

                switch (type) {
                    case 'street_number':
                        if (firstLine.length == 0)
                            firstLine = longName + " ";
                        else
                            firstLine = longName + " " + firstLine;
                        break;
                    case 'route':
                        if (firstLine.length == 0)
                            firstLine = longName;
                        else
                            firstLine = firstLine + longName;
                        break;
                    case 'locality':
                        city = longName;
                        if ($city) $city.val(city);
                        break;
                    case 'administrative_area_level_1':
                    case 'administrative_area_level_2':
                        county = longName;
                        if ($county) $county.val(county);
                        break;
                    case 'country':
                        if ($country) {
                            var id = $("option[data-code='" + shortName + "']");
                            $country.val(id.attr("value"));
                        }
                        break;
                    case 'postal_code':
                    case 'postal_code_prefix':
                        if ($postcode) $postcode.val(longName);
                        break;
                }
            }

            var $selectedAddressCountry = $('#Address_Country_Selection').find(":selected").text();

            if (jQuery.inArray($selectedAddressCountry, $countriesWithoutPostcodes) >= 0)
            {

                $('#Address_Postcode').val($selectedAddressCountry.substring(0, 19));
                $('#addressfinderpostcode').hide();
                $('#scrolldownpagepostcodemessage').hide();
            } else {
                $('#Address_Postcode').val('');
                $('#addressfinderpostcode').show();
                $('#scrolldownpagepostcodemessage').show();
            }

            if (options.cityonly) {
                if (!city) {
                    if (county) {
                        if ($query) $query.val(county);
                    } else if
                        ($query) $query.val(input + ", " + longName);
                } else if (city == county) {
                    if ($query) $query.val(city);
                } else {
                    if (county) {
                        if ($query) $query.val(city + ", " + county);
                    } else if ($query) $query.val(city + ", " + longName);
                }
            } else {
                if ($query) $query.val(result.formatted_address);
            }

            if ($line1) $line1.val(firstLine);
            if ($line2) $line2.val("");

            $container.data('result', null);
        };

        var geocode = function (address, callback) {
            var arguments = { address: address };

            geocoder.geocode(arguments, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    callback(results);
                }
            });
        };

        var reverseGeocode = function (coordinates, callback) {
            var arguments = { location: coordinates };

            geocoder.geocode(arguments, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    callback(results);
                }
            });
        };

        var hidden = $("ul > li:hidden").length;
        if (hidden < 1) {
            $('.location-select-example').hide();
        }

        var locate = function (result) {
            $mapContainer.css("display", "block");
            $('.wizard-location-select-info-text').hide();
            $('.location-select-example').hide();
            $query.val(result.formatted_address);
            $container.data('result', result);

            if (map) {
                google.maps.event.trigger(map, 'resize');
                map.fitBounds(result.geometry.viewport);
                marker.setPosition(result.geometry.location);
            }

            updateAddress(result);
            if (result.geometry.location) {
                if ($latitude) $latitude.val(result.geometry.location.lat());
                if ($longitude) $longitude.val(result.geometry.location.lng());
            }
            updateBounds(result.geometry.bounds);

            updateStaticMap();
        };

        if (map) {
            google.maps.event.addListener(marker, 'dragend', function (event) {
                updateLocation(this.getPosition());
            });
        }

        (function () {
            var timeout = null;

            $query.on('keyup', function (e) {
                if (e.which < 32 && e.which != 8) return;

                $container.data('result', null);

                if (timeout) clearTimeout(timeout);

                timeout = setTimeout(function () {
                    var address = $query.val();

                    if (!address) {
                        $suggestions.empty();
                        $suggestions.hide();
                        $latitude.val("");
                        $longitude.val("");
                        return;
                    }

                    geocode(address, function (results) {
                        $suggestions.empty();
                        $suggestions.hide();
                        $latitude.val("");
                        $longitude.val("");

                        for (var i in results) {
                            var result = results[i];
                            var $link = $('<a>', { href: "javascript:void(0)", text: result.formatted_address });

                            $link.data('result', result);

                            $link.on('click', function () {
                                locate($(this).data('result'));
                                $suggestions.empty();
                                $suggestions.hide();
                                $('#Location').removeClass('red-border');
                            });

                            $suggestions.append($('<div/>').html($link));
                        }

                        if (results.length > 0) {
                            $suggestions.show();
                        }
                    });
                }, 750);
            });
        })();

        if ($acceptButton.length > 0) {
            $($acceptButton).on('click', function (e) {
                e.preventDefault();

                if ($query.val() && $container.data('result') == null) {
                    if ($suggestions.is(":visible")) {
                        var link = $suggestions.find("div a").first();

                        if (link) {
                            link.click();
                            $('#Location').removeClass('red-border');
                            return;
                        }
                    }
                    else {
                        geocode($query.val(), function (results) {
                            if (results.length > 0) {
                                locate(results[0]);
                                return;
                            }
                        });
                    }
                }

                var location = marker.getPosition();

                reverseGeocode(location, function (results) {
                    if (results.length == 0) return;
                    updateAddress(results[0]);
                });
            });
        }

        function searchLocation(callback) {
            if ($suggestions.is(":visible")) {
                $suggestions.find("div a").first().click();
            } else {
                geocode($query.val(), function (results) {
                    if (results.length > 0) locate(results[0]);
                    if (callback) callback();
                });
            }
        }

        if (options.autocode) {
            $query.on('blur', function () {
                setTimeout(
                    function () {
                        if ($container.data('result')) return;
                        searchLocation();
                    },
                    500
                );
            });
            
            $submitButton.on('click', function (e) {
                e.preventDefault();

                if ($('#Location').val() == '') {
                    flashBorder();
                }
                else {
                    if ($query.val() && $container.data('result') == null) {
                        var _this = $(this);
                        searchLocation();
                        _this.closest("form").submit();
                        //searchLocation(function () {
                        //    _this.closest("form").submit();
                        //});
                    }
                }
            });

            function flashBorder() {
                $('#Location').removeClass('red-border');
                var numberOfFlashes = 0;
                var interval = setInterval(
                    function () {
                        $('#Location').toggleClass('red-border');
                        numberOfFlashes++;
                        if (numberOfFlashes === 5) {
                            clearInterval(interval);
                        }
                    }, 100)

            };

            $query.on('keypress', function (e) {
                if (e.which == 13 && $query.is(':focus')) {
                    e.preventDefault();
                    if ($query.val() && $container.data('result') == null) {
                        var _this = $(this);
                        searchLocation(function () {
                            _this.closest("form").submit();
                        });
                    }
                }
            });
        }

        if (options.profilesignup) {

            //$query.on('blur', function () {
            //    setTimeout(
            //        function () {
            //            if ($container.data('result')) return;
            //            searchLocation();
            //        },
            //        500
            //    );
            //});
            
            $submitButton.on('click', function (e) {
                e.preventDefault();

                if ($('#Location').val() == '') {
                    flashBorder();
                }
                else {
                    if ($query.val() && $container.data('result') == null) {
                        var _this = $(this);
                        searchLocation();
                        _this.closest("form").submit();
                        //searchLocation(function () {
                        //    _this.closest("form").submit();
                        //});
                    }
                }
            });

            function flashBorder() {
                $('#Location').removeClass('red-border');
                var numberOfFlashes = 0;
                var interval = setInterval(
                    function () {
                        $('#Location').toggleClass('red-border');
                        numberOfFlashes++;
                        if (numberOfFlashes === 5) {
                            clearInterval(interval);
                        }
                    }, 100)

            };

            $query.on('keypress', function (e) {
                if (e.which == 13 && $query.is(':focus')) {
                    e.preventDefault();
                    if ($query.val() && $container.data('result') == null) {
                        var _this = $(this);
                        searchLocation(function () {
                            _this.closest("form").submit();
                        });
                    }
                }
            });
        }
    };
})(jQuery);
