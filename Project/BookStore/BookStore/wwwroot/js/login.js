$(document).ready(function () {

    $("#loginForm").submit(function (e) {
        e.preventDefault();
        var username = "rletty0";
        var password = "HY5KFJLu";
        var form = $(this);
        var url = form.attr('action');


        if (checkFormValidateOrNot(form) === true) {
            $.ajax({
                type: "POST",
                url: url,
                data: form.serialize(),
                success: function (data) {
                    if (data.status === true) {
                        if (data.previousState === null) {
                            if (data.obj.username === "admin") {
                                Swal.fire({
                                    title: "Success",
                                    text: "Login completely",
                                    icon: 'success'
                                }).then(function () {
                                    window.location.href = "/Admin";
                                });  
                            }
                            else {
                                Swal.fire({
                                    title: "Success",
                                    text: "Login completely",
                                    icon: 'success'
                                }).then(function () {
                                    window.location.href = "/";
                                });    
                            }
                        }
                        else {
                            if (data.obj.username === "admin") {
                                Swal.fire({
                                    title: "Success",
                                    text: "Login completely",
                                    icon: 'success'
                                }).then(function() {
                                    window.location.href = "/Admin";
                                });
                            }
                            else {
                                Swal.fire({
                                    title: "Success",
                                    text: "Login completely",
                                    icon: 'success'
                                }).then(function () {
                                    window.location.href = "/" + data.previousState;
                                });    
                            }
                        }
                    }
                    else {
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

    $("#signupForm").submit(function (e) {
        e.preventDefault();
        var form = $(this);
        var url = form.attr('action');

        if (checkFormValidateOrNot(form) === true) {
            $.post(url,
                form.serialize(),
                function (data) {
                    if (data.status === true) {
                        if (data.previousState === null) {
                            window.location.href = "/";
                        }
                        else {
                            window.location.href = "/" + data.previousState;
                        }
                    }
                    else {
                        alert(data.message);
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