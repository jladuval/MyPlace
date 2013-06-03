var SelectLocation = (function(google) {
    $(function() {
        initialiseMap();
    });

    function initialiseMap() {
        if (navigator.geolocation) {
            var success = function(position) {
                createMap(position.coords.latitude, position.coords.longitude);
            };
            var error = function() { createMap(12.659493, 79.415412); };

            navigator.geolocation.getCurrentPosition(success, error);
        } else {
            createMap(12.659493, 79.415412);
        }
    }

    var map;

    function createMap(lat, lng) {
        var mapOptions = {
            center: new google.maps.LatLng(lat, lng),
            zoom: 15,
            scrollwheel: true,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);
    }
})(google);