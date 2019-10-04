$(document).ready(function () {

    $(window).click(function () {
        $("ol").remove();
    });

    $(".Category").click(function (e) {
        e.stopPropagation();
        if ($("ol").length == 0) {
            var name = $(this).text();
            var currentCate = $(this);
            $("ol").remove();
            $.ajax({
                type: "GET",
                url: "/SubLists/" + name,
                success: function (data) {
                    //console.log(data);
                    //for (var i = 0; i < data.length; i++) {
                    //    console.log(data[i]);
                    //}
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

        //$.ajax({
        //    //type: "POST",
        //    //url: "/Login/",
        //    //data: {username: username, password: password},
        //    //success: function (data) {
        //    //    console.log(data.message);
        //    //},
        //    //error: function (err) {
        //    //    console.log(err);
        //    //}
        //});

        //$.post("/Login",
        //    {
        //        username: username,
        //        password: password
        //    }, function (data) {
        //        console.log(data.obj);
        //        $("ul").append(
        //            '<li>' + data.obj.username + '</li>' +
        //            '<li>' + data.obj.password + '</li>' +
        //            '<li>' + data.obj.salt + '</li>'
        //        );
        //    });
    });

    $("#lbLogout").click(function (e) {
        e.preventDefault();
        $.get("/Logout",
            function (data) {
                if (data.status == true) {
                    window.location = "/";
                };
            });
    });

    $("a.nav-link").click(function (e) {
        var id = $(this).attr("id").substring(5);

        $.post("/FamousPublisherBookList", {
            id: id
        }, function (data) {
            for (var i = 0; i, data.obj.length; i++) {
                $(".tab-content div.card:eq(" + (parseInt(i)) + ") img.card-img-top").attr("src", data.obj[i].image);
                $(".tab-content div.card:eq(" + (parseInt(i)) + ") h5.card__book-title").text(data.obj[i].name);
                $(".tab-content div.card:eq(" + (parseInt(i)) + ") p.card__book-price")
                    .text((parseInt(data.obj[i].currentPrice) / 1000).toFixed(3) + " VND");
                $(".tab-content div.card p.card__book-price").css("text-align", "center");
            }
        });
        
    });
});