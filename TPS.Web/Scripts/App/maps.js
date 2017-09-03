var google = google || {};

function enableMaps() {
    $(".js-view-map").removeClass("hidden");
}

function mapInit(mapDivId, mapLocations) {

    var $mapDiv = $("#" + mapDivId);

    if (!mapLocations.length) {
        var noMapData = $("<div />").addClass("alert alert-danger").text("Unfortunately there is no location data for the selected photo's.");
        $($mapDiv).append(noMapData);
        return;
    }

    var bounds = new google.maps.LatLngBounds();
    var infowindow = new google.maps.InfoWindow();

    var map = new google.maps.Map(document.getElementById(mapDivId));
    $.each(mapLocations, function (i, loc) {
        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(loc.lat, loc.lng),
            map: map,
            icon: loc.src + "?w=50&h=50&mode=crop",
            maxZoom: 5
    });
        bounds.extend(marker.position);

        google.maps.event.addListener(map, "zoom_changed", function() {
            var zoomChangeBoundsListener = google.maps.event.addListener(map, "bounds_changed", function() {
                if (this.getZoom() > 15 && this.initialZoom === true) {
                    this.setZoom(15);
                    this.initialZoom = false;
                }
                google.maps.event.removeListener(zoomChangeBoundsListener);
            });
        });

        map.initialZoom = true;

        google.maps.event.addListener(marker,
            "click",
            (function (marker) {
                return function () {
                    infowindow.setContent(loc.alt);
                    infowindow.open(map, marker);
                }
            })(marker, i));
    });

    map.fitBounds(bounds);
}

