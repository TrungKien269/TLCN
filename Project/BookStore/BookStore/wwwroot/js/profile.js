$(document).ready(function () {
    $("#updateForm").submit(function(e) {
        e.preventDefault();
        var form = $(this);
        var url = form.attr('action');

        if (checkFormValidateOrNot(form) === true) {
            $.ajax({
                type: "POST",
                url: url,
                data: form.serialize(),
                success: function (data) {
                    if (data.status === true) {
                        alert(data.message);
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
        }
        
    });

    $("#changeForm").submit(function(e) {
        e.preventDefault();
        var form = $(this);
        var url = form.attr('action');

        if ($("input#new").val().length < 8) {
            alert("Password must have at least 8 characters!");
        }

        else if ($("input#new").val() === $("input#confirm").val()) {
            $.ajax({
                type: "POST",
                url: "/CheckPassword",
                data: form.serialize(),
                success: function(data) {
                    if (data.status === true) {
                        $.ajax({
                            type: "POST",
                            url: url,
                            data: form.serialize(),
                            success: function(data) {
                                alert(data.message);
                            },
                            error: function(err) {
                                alert("Error");
                                console.log(err);
                            }
                        });
                    } else {
                        alert(data.message);
                    }
                },
                error: function(err) {
                    alert("Error");
                    console.log(err);
                }
            });
        }
        else {
            alert("Password has to be confirm right!");
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