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