$(document).ready(function() {
    $(".btn").click(function (e) {
        var id = $(this).text();
        $("div.listOrders").css("display", "none");
        $("div#" + id).css("display", "block");
    });
});