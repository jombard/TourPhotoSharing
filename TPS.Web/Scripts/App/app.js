var pswpElement = document.querySelectorAll(".pswp")[0];

$(document).on("click", ".image", function (e) {
    e.preventDefault();
    var index = 0;
    var thisImg = this;

    var items = $("li:visible").find("img.image").map(function (i, pic) {
        if ($(pic).hasClass("pswp__img")) {
            return null;
        }

        if (thisImg.src.split("?")[0] === pic.src.split("?")[0]) {
            index = i;
        }

        var authorName = $(pic).data("author");

        return {
            src: pic.src.split("?")[0] + "?maxwidth=1024&maxheight=768&mode=crop",
            title: pic.alt + (authorName ? "<br />Photo by: " + authorName : ""),
            w: $(pic).data("width"),
            h: $(pic).data("height")
        }
    });

    var options = {
        index: index,
        showHideOpacity: false,
        getThumbBoundsFn: function (x) {
            var thumbnail = $("li:visible").find("img")[x];
            var pageYScroll = window.pageYOffset || document.documentElement.scrollTop;
            var rect = $(thumbnail).offset();
            var width = $(thumbnail).width();
            return { x: rect.left, y: rect.top + pageYScroll, w: width };
        }
    };

    var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
    gallery.init();
});