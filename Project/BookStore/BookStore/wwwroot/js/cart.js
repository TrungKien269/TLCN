$(document).ready(function () {
    $(".btn-danger").click(function(e) {
        var index = $("button").index(this);
        var id = $(".table tr:eq(" + (parseInt(index) + 1).toString() + ") td:eq(0)").text();
        $.post('/RemoveBookCart',
            { id: id },
            function(data) {
                console.log(data);
            });
        $(".table tr:eq(" + (parseInt(index) + 1).toString() + ")").remove();
    });
});