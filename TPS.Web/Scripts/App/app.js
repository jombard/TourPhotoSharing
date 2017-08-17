var $modal = $("#imageModal");
$(document).on("click", ".image", function (e) {
    e.preventDefault();
    var $this = $(this);
    $modal.show();
    $modal.find("#modal-img").attr("src", $this.attr("src").split("?")[0]);

    var caption = $this.attr("alt");
    console.log($this, caption);
    $modal.find(".caption").text(caption);
});

$modal.on("click", ".carousel-control", function (e) {
    e.preventDefault();
});