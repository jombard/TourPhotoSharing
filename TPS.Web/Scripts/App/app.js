var $modal = $("#imageModal");
$(document).on("click", ".image", function (e) {
    e.preventDefault();
    var $this = $(this);
    $modal.show();
    $modal.find("#modal-img").attr("src", $this.attr("src").split("?")[0]);
});

$modal.on("click", ".carousel-control", function (e) {
    e.preventDefault();
});