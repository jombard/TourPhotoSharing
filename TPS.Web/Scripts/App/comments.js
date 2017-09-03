var tourId = 0; // TODO get tour id from page

function addComment(comment) {
    var commenter = $("<h4 />").addClass("media-heading").text(comment.Commenter.FullName);
    var dateDiv = $("<div />").addClass("small").text("on " + moment(comment.CommentDate).format("dddd, MMMM Do YYYY, h:mm a"));
    var p = $("<p />").addClass("lead").text(comment.CommentValue);

    var mediaLeft = $("<div />").addClass("media-left").append("<a href=#><img alt='64x64' class='media-object' src='/Uploads/avatar.jpg'></a>");
    var mediaBody = $("<div />").addClass("media-body").append(commenter).append(dateDiv).append(p);
    var li = $("<li />").addClass("media").append(mediaLeft).append(mediaBody);

    $(".media-list").append(li);
}

$(document).ready(function () {
    $.ajax({
        url: "/api/Comments/" + tourId,
        method: "GET"
    }).done(function (comments) {
        console.log("get comments result", comments);

        $.each(comments, function (i, comment) {
            addComment(comment);
        });
    }).fail(function (error) {
        console.log("get comments error", error);
    });
});

$(".comment-entry").on("click", "button", function (e) {
    e.preventDefault();

    var commentTextarea = $(".comment-entry").find("textarea");

    if (commentTextarea.val() === "") {
        return;
    }

    var commentDto = {
        CommentValue: commentTextarea.val(),
        TourId: tourId
    };

    $.ajax({
        url: "/api/Comments",
        method: "POST",
        data: commentDto
    }).done(function (comment) {
        console.log("post comment result", comment);
        commentTextarea.val("");

        addComment(comment);
    }).fail(function (error) {
        console.log("post comment error", error);
    });
});