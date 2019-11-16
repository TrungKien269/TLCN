$(document).ready(function () {
    $("button#btnRemove").click(function(e) {
        var index = $("button").index(this);
        var id = $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td:eq(0)").text();
        $.post('/RemoveBookCart',
            { id: id },
            function(data) {
                console.log(data);
            });
        $("table tr:eq(" + (parseInt(index) + 1).toString() + ")").remove();
    });

    $("input.qty").on("change", function (e) {
        if (isNotNumeric(parseInt($(this).val()))) {
            $(this).val("1");
            var index = $("input.qty").index(this);
            var quantity = $(this).val();
            var id = $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td:eq(0)").text();
            var currentPrice = $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td.item-price p").text()
                .replace(",", "").replace(" VND", "");
            var subtotal = parseInt(quantity) * parseInt(currentPrice);
            $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td.item-total p")
                .text(formatPrice(subtotal) + " VND");
            $.post('/EditQuantityCart',
                { id: id, quantity: quantity },
                function (data) {
                    console.log(data);
                });
        }
        else {
            if (parseInt($(this).val()) <= 1) {
                $(this).val("1");
                var index = $("input.qty").index(this);
                var quantity = $(this).val();
                var id = $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td:eq(0)").text();
                var currentPrice = $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td.item-price p").text()
                    .replace(",", "").replace(" VND", "");
                var subtotal = parseInt(quantity) * parseInt(currentPrice);
                $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td.item-total p")
                    .text(formatPrice(subtotal) + " VND");
                $.post('/EditQuantityCart',
                    { id: id, quantity: quantity },
                    function (data) {
                        console.log(data);
                    });
            }
            else {
                var index = $("input.qty").index(this);
                var quantity = $(this).val();
                var id = $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td:eq(0)").text();
                var currentPrice = $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td.item-price p").text()
                    .replace(",", "").replace(" VND", "");
                var subtotal = parseInt(quantity) * parseInt(currentPrice);
                $("table tr:eq(" + (parseInt(index) + 1).toString() + ") td.item-total p")
                    .text(formatPrice(subtotal) + " VND");
                $.post('/EditQuantityCart',
                    { id: id, quantity: quantity },
                    function (data) {
                        console.log(data);
                    });
            }
        }
    });

    $("button#btnOrder").click(function(e) {
        window.location.href = "/ProceedOrder";
    });

    $("#txtSearch").keypress(function (e) {
        if (e.which === 13) {
            window.location.href = "/Search/value=" + $(this).val();
        }
    });

    function formatPrice(num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
    }

    function isNotNumeric(num) {
        return isNaN(num);
    }
});