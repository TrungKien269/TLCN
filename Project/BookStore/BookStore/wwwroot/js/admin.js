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

    $("div#book button#btnTest").click(function(e) {
        var book = {
            //9781447225584
            "id": "123",
            "name": "FIDDLEHEAD: A CLOCKWORK CENTURY NOVEL",
            "originalPrice": 168000,
            "currentPrice": 143000,
            "releaseYear": 2013,
            "weight": 264,
            "numOfPage": 400,
            "image": "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_1_2018_10_27_11_36_34.jpg",
            "summary": "Ex-spy 'Belle Boyd' is retired - more or less. " +
                "Retired from spying on the Confederacy anyway. Her short-lived marriage to a Union navy boy cast " +
                "suspicion on those Southern loyalties, so her mid-forties found her unemployed, widowed and disgraced. " +
                "Until her life-changing job offer from the staunchly Union Pinkerton Detective Agency. " +
                "When she's required to assist Abraham Lincoln himself, she has to put any old loyalties firmly aside - " +
                "for a man she spied against twenty years ago. Lincoln's friend Gideon Bardsley, colleague and ex-slave, " +
                "is targeted for assassination after the young inventor made a breakthrough. " +
                "Fiddlehead, Bardsley's calculating engine, has proved the world is facing an extraordinary threat. " +
                "Meaning it's not the time for civil war. Now Bardsley and Fiddlehead are in great danger as forces " +
                "conspire to keep this potentially unifying secret, the war moving and the money flowing. " +
                "With spies from both camps gunning for her, can even the notorious Belle Boyd hold the war-hawks at bay? " +
                "Cherie Priest has been hailed as 'the High Priestess of Steampunk' - and you can see why in this latest " +
                "wonderfully written adventure."
        };
        var authors = [{ "name": "Cherie Priest" }];
        var images = [
            "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_2_2018_10_27_11_36_34.png",
            "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_3_2018_10_27_11_36_34.png",
            "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_4_2018_10_27_11_36_34.png",
            "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_5_2018_10_27_11_36_34.png",
            "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_6_2018_10_27_11_36_34.png",
            "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_7_2018_10_27_11_36_34.png",
            "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_8_2018_10_27_11_36_34.png",
            "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_9_2018_10_27_11_36_34.png",
            "https://cdn0.fahasa.com/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/f/i/fiddlehead_a_clockwork_century_novel_10_2018_10_27_11_36_34.png"
        ];
        var cateID = 4;
        var formID = 4;
        var publisherID = 550;
        var supplierID = 31;

        $.post("/Test",
            {
                book: book,
                authors: authors,
                images: images,
                cateID: cateID,
                formID: formID,
                publisherID: publisherID,
                supplierID: supplierID
            },
            function(data) {
                console.log(data);
            });
    });
});