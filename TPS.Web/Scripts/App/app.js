﻿var pswpElement = document.querySelectorAll(".pswp")[0];

var options = {
    index: 0
};

$(document).on("click", ".image", function (e) {
    e.preventDefault();

    var items = $("img").map(function (i, pic) {
        return {
            src: pic.src.split("?")[0],
            title: pic.alt,
            w: 1024,
            h: 768,
            author: pic.dataset.author
        }
    });

    var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
    gallery.init();
});