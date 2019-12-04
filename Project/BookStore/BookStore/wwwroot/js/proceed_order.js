$(document).ready(function () {
    $("#checkoutForm").submit(function(e) {
        e.preventDefault();
        var form = $(this);
        var url = form.attr('action');

        if (checkFormValidateOrNot(form) === true) {
            $.ajax({
                type: "POST",
                url: url,
                data: form.serialize(),
                success: function(data) {
                    if (data.status === true) {
                        Swal.fire({
                            title: "Complete",
                            text: "Check out successfully",
                            icon: 'success'
                        }).then(function () {
                            window.location.href = "/Orders";
                        });
                    } else {
                        popupFail("Fail", data.message);
                    }
                },
                error: function (err) {
                    popupFail("Error", err);
                    console.log(err);
                }
            });
        }
    });

    $("button#btnOnlineProceed").click(function (e) {
        var total = $("div.title-wrapper div.product-price").text();
        total = total.substring(0, total.length - 4).replace(',', '');
        $.post("/OnlineProceed",
            {
                fullname: $("input#name").val(),
                mobile: $("input#number").val(),
                email: $("input#address").val(),
                total: total
            },
            function(data) {
                window.location.href = data;
            });
    });

    function checkFormValidateOrNot(form) {
        console.log(form.attr("id"));
        if ($("#" + form.attr("id") + " .field-validation-error").length > 0) {
            return false;
        }

        $(".field-control").each(function () {
            if ($(this).attr("data-val") === "true" && $(this).val() === "" &&
                $(this).is("select") === false) {
                return false;
            }
        });

        return true;
    }
});