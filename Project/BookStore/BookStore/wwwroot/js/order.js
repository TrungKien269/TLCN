$(document).ready(function () {
    $(".btn").click(function (e) {
        var id = $(this).text();
        $("div.listOrders").css("display", "none");
        $("div#" + id).css("display", "block");
    });

    $("#txtSearch").keypress(function (e) {
        if (e.which === 13) {
            window.location.href = "/Search/value=" + $(this).val();
        }
    });

    $("button.btn-danger").click(function (e) {
        var index = $(this).index("button");
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, cancel!'
        }).then((result) => {
            if (result.value) {
                $.post("/CancelOrder",
                    {
                        id: $(this).attr("id")
                    },
                    function (data) {
                        if (data.status === true) {
                            Swal.fire({
                                title: 'Complete',
                                text: "Cancel successfully",
                                icon: 'success'
                            }).then(function () {
                                $("div#canceled").prepend($("div.order-block:eq(" + index + ")"));
                                $("div#canceled button.btn-danger").remove();
                                $("div#processing div.order-block:eq(" + index + ")").remove();
                            });
                        } else {
                            popupFail("Fail", data.message);
                        }
                    });
            }
        });
        //$.post("/CancelOrder",
        //    {
        //        id: $(this).attr("id")
        //    },
        //    function (data) {
        //        if (data.status === true) {
        //            $("div#canceled").prepend($("div.order-block:eq(" + index + ")"));
        //            $("div#canceled button.btn-danger").remove();
        //            $("div#processing div.order-block:eq(" + index + ")").remove();
        //        } else {
        //            alert(data.message);
        //        }
        //    });
    });
});