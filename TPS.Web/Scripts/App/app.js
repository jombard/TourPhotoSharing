//var $modal = $("#imageModal");
//$(document).on("click", ".image", function (e) {
//    e.preventDefault();
//    var $this = $(this);
//    $modal.show();
//    $modal.find("#modal-img").attr("src", $this.attr("src").split("?")[0]);

//    var caption = $this.attr("alt");
//    $modal.find(".caption").text(caption);
//});

//$modal.on("click", ".carousel-control", function (e) {
//    e.preventDefault();
//});

var pswpElement = document.querySelectorAll('.pswp')[0];

// build items array
//var items = [
//    {
//        src: 'https://placekitten.com/600/400',
//        w: 600,
//        h: 400
//    },
//    {
//        src: 'https://placekitten.com/1200/900',
//        w: 1200,
//        h: 900
//    }
//];

var items = $("img").map(function (i, pic) {
    return {
        src: pic.src.split("?")[0],
        title: pic.alt,
        w: 1024,
        h: 768,
        author: pic.dataset.author
    }
});

// define options (if needed)
var options = {
    // optionName: 'option value'
    // for example:
    index: 0 // start at first slide
};

// Initializes and opens PhotoSwipe

$(document).on("click", ".image", function (e) {
    e.preventDefault();

    var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
    gallery.init();
});