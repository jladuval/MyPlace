var SelectLocation = (function (google) {
    var map;
    var marker;

    $(function () {
        initialiseMap();
    });

    function initialiseMap() {
        if (navigator.geolocation) {
            var success = function (position) {
                createMap(position.coords.latitude, position.coords.longitude);
            };
            var error = function () { createMap(12.659493, 79.415412); };

            navigator.geolocation.getCurrentPosition(success, error);
        } else {
            createMap(12.659493, 79.415412);
        }
    }

    function createMap(lat, lng) {
        var mapOptions = {
            center: new google.maps.LatLng(lat, lng),
            zoom: 15,
            scrollwheel: true,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            disableDefaultUI: true
        };
        map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);
        setMarker(lat, lng);
        reverseLookup(lat, lng);
        google.maps.event.addListener(map, 'click', function (event) {
            placeMarker(event.latLng);
            reverseLookup(event.latLng.lat(), event.latLng.lng());
        });
    }

    function reverseLookup(lat, lng) {
        $.get("http://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + lng + "&sensor=true")
            .done(function(data) {
                populateAddress(data.results[0].address_components);
            })
            .fail(function() {

            });
    }

    function populateAddress(locationInfo) {
        var length = locationInfo.length;
        $('#address').val(locationInfo[0].short_name + ' ' + locationInfo[1].short_name);
        $('#suburb').val(locationInfo[2].short_name);
        $('#city').val(locationInfo[3].short_name);
        $('#country').val(locationInfo[length-2].short_name);
        $('#postcode').val(locationInfo[length-1].short_name);
    }

    function setMarker(lat, lng) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(lat, lng),
        });

        marker.setMap(map);
    }

    function placeMarker(location) {
        if (marker == undefined) {
            marker = new google.maps.Marker({
                position: location,
                map: map,
                animation: google.maps.Animation.DROP,
            });
        }
        else {
            marker.setPosition(location);
        }
    }


})(google);