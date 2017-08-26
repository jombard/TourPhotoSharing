//var pswpElement = document.querySelectorAll(".pswp")[0];

$(document).on("click", ".image", function (e) {
    e.preventDefault();

    //var index = 0;
    //var thisImg = this;

    //var items = $("li:visible").find("img").map(function (i, pic) {
    //    if ($(pic).hasClass("pswp__img")) {
    //        return null;
    //    }

    //    if (thisImg.src.split("?")[0] === pic.src.split("?")[0]) {
    //        index = i;
    //    }

    //    var authorName = $(pic).data("author");

    //    return {
    //        src: pic.src.split("?")[0] + "?maxwidth=1024&maxheight=768&mode=crop",
    //        title: pic.alt + (authorName ? "<br />Photo by: " + authorName : ""),
    //        w: 1024,
    //        h: 768,
    //        msrc: pic.src
    //    }
    //});

    //var options = {
    //    index: index
    //};

    //var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
    //gallery.init();
});