$(document).ready(function () {
    $("#checkoutForm").submit(function(e) {
        e.preventDefault();
        var form = $(this);
        var url = form.attr('action');
        var proceedType = $("select#proceed-type").val();

        if (checkFormValidateOrNot(form) === true) {
            Swal.fire({
                title: 'Do you want to proceed order?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes!'
            }).then((result) => {
                if (result.value) {
                    if (proceedType === "0") {
                        $.ajax({
                            type: "POST",
                            url: url,
                            data: form.serialize(),
                            success: function (data) {
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
                    else {
                        var total = $("div.title-wrapper div.product-price").text();
                        total = total.substring(0, total.length - 4).replace(/,/g, '');
                        $.ajax({
                            type: "POST",
                            url: url,
                            data: form.serialize(),
                            success: function (data) {
                                if (data.status === true) {
                                    Swal.fire({
                                        title: "Notification",
                                        text: "Prepare to redirect to validation form",
                                        icon: 'info'
                                    }).then(function () {
                                        $.post("/OnlineProceed",
                                            {
                                                fullname: $("input#name").val(),
                                                mobile: $("input#number").val(),
                                                email: $("input#address").val(),
                                                total: total
                                            },
                                            function (data) {
                                                window.location.href = data;
                                            });
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