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
                alert(data.results.formatted_address);
            })
            .fail(function() {

            });
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