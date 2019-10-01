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
                success: function(data) {
                    //console.log(data);
                    //for (var i = 0; i < data.length; i++) {
                    //    console.log(data[i]);
                    //}
                    $("<ol></ol>").insertAfter(currentCate);
                    data.forEach(function(item) {
                        $("ol").prepend(
                            "<li class = 'SubCategory'>" + item.name + "</li>"
                        );
                    });
                },
                error: function(err) {
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
});