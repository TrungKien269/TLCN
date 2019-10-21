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

    $("input.form-control").on("change", function (e) {
        var index = $("input.form-control").index(this);
        var quantity = $(this).val();
        var id = $(".table tr:eq(" + (parseInt(index) + 1).toString() + ") td:eq(0)").text();
        var currentPrice = $(".table tr:eq(" + (parseInt(index) + 1).toString() + ") td:eq(2)").text().replace(",", "");
        var subtotal = parseInt(quantity) * parseInt(currentPrice);
        $(".table tr:eq(" + (parseInt(index) + 1).toString() + ") td:eq(4)").text(formatPrice(subtotal) + " VND");
        $.post('/EditQuantityCart',
            { id: id, quantity: quantity },
            function (data) {
                console.log(data);
            });
    });

    function formatPrice(num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
    }
});