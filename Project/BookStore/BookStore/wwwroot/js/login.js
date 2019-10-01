$(document).ready(function () {

    $("form").submit(function (e) {
        e.preventDefault();
        var username = "rletty0";
        var password = "HY5KFJLu";
        var form = $(this);
        var url = form.attr('action');

        $.ajax({
            type: "POST",
            //url: "/Login/",
            url: url,
            data: form.serialize(),
            success: function (data) {
                if (data.status === true) {
                    window.location = "/";
                }
                else {
                    alert(data.message);
                }
            },
            error: function (err) {
                alert("Error");
                console.log(err);
            }
        });
    });

});