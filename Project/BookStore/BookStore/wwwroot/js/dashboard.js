﻿$(document).ready(function () {

    $(window).click(function () {
        $("ol").remove();
    });

    $("h3.lb_category").click(function(e) {
        window.location.href = "/ListBook/" + $(this).text();
    });

    $(".Category").click(function (e) {
        e.stopPropagation();
        if ($("ol").length === 0) {
            var name = $(this).text();
            var currentCate = $(this);
            $("ol").remove();
            $.ajax({
                type: "GET",
                url: "/SubLists/" + name,
                success: function (data) {
                    $("<ol></ol>").insertAfter(currentCate);
                    data.forEach(function (item) {
                        $("ol").prepend(
                            "<li class = 'SubCategory'>" + item.name + "</li>"
                        );
                    });
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
        else {
            $("ol").remove();
        }
    });

    $("#lbLogout").click(function (e) {
        e.preventDefault();
        $.get("/Logout",
            function (data) {
                if (data.status === true) {
                    window.location = "/";
                };
            });
    });

    $("a.nav-link").click(function (e) {
        var id = $(this).attr("id").substring(5);

        $.post("/FamousPublisherBookList", {
            id: id
        }, function (data) {
            console.log(data);
            for (var i = 0; i, data.obj.length; i++) {
                $(".tab-content div.card:eq(" + (parseInt(i)) + ") img.card-img-top").attr("src", data.obj[i].image);
                $(".tab-content div.card:eq(" + (parseInt(i)) + ") h5.card__book-title").text(data.obj[i].name);
                $(".tab-content div.card:eq(" + (parseInt(i)) + ") p.card__book-price")
                    .text((parseInt(data.obj[i].currentPrice) / 1000).toFixed(3) + " VND");
                $(".tab-content div.card p.card__book-price").css("text-align", "center");
                $(".tab-content div.col-md-2:eq(" + (parseInt(i)) + ") a.linkBook")
                    .attr("href", "/Book/" + data.obj[i].id);
                $(".tab-content div.col-md-2:eq(" + (parseInt(i)) + ") a.btn--rounded")
                    .attr("href", data.obj[i].id);
                $(".tab-content div.col-md-2:eq(" + (parseInt(i)) + ") a.badge__utilities-blue:eq(1)")
                    .attr("href", "/Book/" + data.obj[i].id);
                $(".tab-content div.col-md-2:eq(" + (parseInt(i)) + ") a.badge__utilities-blue:eq(1)")
                    .attr("data-object", data.obj[i].id);
            }
        });
    });

    $("a#linkProfile").click(function (e) {
        //e.preventDefault();
        //$.post("/Profile",
        //    function (data) {
        //        console.log(data);
        //    });
    });

    $("div.card a.btn--rounded").click(function (e) {
        e.preventDefault();
        //$.post("/AddToCart",
        //    {
        //        id: $(this).attr("href"),
        //        quantity: 1
        //    },
        //    function (data) {
        //        console.log(data);
        //    });
        $.post("/AddToCart",
            {
                id: $(this).attr("href"),
                quantity: 1
            },
            function (data) {
                if (data.status === true) {
                    Swal.fire({
                        title: 'Complete',
                        text: "Add book to cart successfully",
                        icon: 'success'
                    });
                } else {
                    popupFail("Fail", data.message);
                }
            });
    });

    $("div.card div.badge a.badge__utilities-blue").click(function (e) {
        $.post("/Book",
            { id: $(this).attr("data-object") },
            function(data) {
                if (data.status === true) {
                    $("a.articles__link").attr("href", "/Book/" + data.obj.id);
                    $("p.product__price").text((parseInt(data.obj.currentPrice) / 1000).toFixed(3) + " VND");

                    var authors = "";
                    for (var i = 0; i < data.obj.authorBook.length; i++) {
                        authors += data.obj.authorBook[i].author.name;
                        authors += ", ";
                    }
                    authors = authors.substring(0, authors.length - 2);
                    $("div.special-author").text("Authors: " + authors);

                    $("a.f-3").text(data.obj.name);
                    $("a.f-3").attr("href", "/Book/" + data.obj.id);

                    $("input.qty").val("1");

                    $("div.modal div.carousel-inner").append(
                        '<div class="carousel-item active cursor-zoom" data-image="' +
                        data.obj.image +
                        '" data-toggle="zoom"> ' +
                        '<div class="card">' +
                        '<img src="' + data.obj.image + '" class="card-img-top img-cover" alt="...">' +
                        '</div></div>');
                }
                else {
                    console.log(data);
                }
            });
    });

    $("#modalQuickview").on('hide.bs.modal', function () {
        $("div.modal div.carousel-inner").find("div.carousel-item").remove();
    });

    $("#modalQuickview a.btn--rounded").click(function (e) {
        e.preventDefault();
        
        var id = $("a.f-3").attr("href");
        id = id.substring(6);
        var quantity = $("#modalQuickview input.qty").val();

        $.post("/AddToCart",
            {
                id: id,
                quantity: quantity
            },
            function (data) {
                window.location.href = "/ProceedOrder";
            });
    });

    $("#txtSearch").keypress(function(e) {
        if (e.which === 13) {
            window.location.href = "/Search/value=" + $(this).val();
        }
    });
});