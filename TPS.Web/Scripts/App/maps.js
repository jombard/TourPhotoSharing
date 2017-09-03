var google = google || {};

function enableMaps() {
    $(".js-view-map").removeClass("hidden");
}

function mapInit(e) {
    e.preventDefault();

    $("#map").toggleClass("hidden");

    if ($("#map").children().length)
        return;

    var mapLocations = $("img").filter(function (i, pic) {
        return pic.dataset.lat && pic.dataset.lng && $(pic).is(":visible");
    }).map(function (i, pic) {
        return {
            lat: parseFloat(pic.dataset.lat),
            lng: parseFloat(pic.dataset.lng),
            alt: "<div><div class='thumbnail'><img src='" + pic.src + "' /></div>" +
            "<p>" + pic.alt + "</p><p class='small'>Photo by: " + pic.dataset.author + "</p></div>",
            src: pic.src.split("?")[0]
        }
    });

    if (!mapLocations.length) {
        var noMapData = $("<div />").addClass("alert alert-danger").text("Unfortunately there is no location data for the selected photo's.");
        $("#map").append(noMapData);
        return;
    }

    var bounds = new google.maps.LatLngBounds();
    var infowindow = new google.maps.InfoWindow();

    var map = new google.maps.Map(document.getElementById('map'));
    $.each(mapLocations, function (i, loc) {
        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(loc.lat, loc.lng),
            map: map,
            icon: loc.src + "?w=50&h=50&mode=crop"
        });
        bounds.extend(marker.position);

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

$(".js-view-map").on("click", mapInit);
