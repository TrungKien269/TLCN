$(document).ready(function (e) {
    $("div#processing a.btn--blue").click(function (e) {
        e.preventDefault();
        var index = $(this).index("div#processing a.btn--blue");
        Swal.fire({
            title: 'Do you want to accept?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, accept!'
        }).then((result) => {
            if (result.value) {
                $.post("/ConfirmOrder",
                    {
                        id: $(this).attr("href"),
                        status: "Delivering"
                    },
                    function (data) {
                        if (data.status === true) {
                            Swal.fire({
                                title: "Complete",
                                text: "Success",
                                icon: "success"
                            }).then((result) => {
                                $(this).text("Confirm order");
                                $("div#delivering").append($("div#processing div.order-block:eq(" + index + ")"));
                            });
                        }
                        else {
                            Swal.fire({
                                title: "Fail",
                                text: data.message,
                                icon: "error"
                            });
                        }
                    }
                );
            }
        });
    });

    $("div#delivering a.btn--blue").click(function (e) {
        e.preventDefault();
        var index = $(this).index("div#delivering a.btn--blue");
        var currentButton = $(this);
        Swal.fire({
            title: 'Do you want to confirm?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, confirm!'
        }).then((result) => {
            if (result.value) {
                $.post("/ConfirmOrder",
                    {
                        id: $(this).attr("href"),
                        status: "Delivered"
                    },
                    function (data) {
                        if (data.status === true) {
                            Swal.fire({
                                title: "Complete",
                                text: "Success",
                                icon: "success"
                            }).then((result) => {
                                $(currentButton).remove();
                                $("div#delivered").append($("div#delivering div.order-block:eq(" + index + ")"));
                            });
                        }
                        else {
                            Swal.fire({
                                title: "Fail",
                                text: data.message,
                                icon: "error"
                            });
                        }
                    }
                );
            }
        });
    });
});