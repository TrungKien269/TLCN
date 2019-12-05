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
                                $("div#processing a.btn--blue:eq(" + index + ")").text("Confirm order");
                                $("div#delivering").append($("div#processing div.order-block:eq(" + index + ")"));

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
                                                            $("div#delivered").prepend($("div#delivering div.order-block:eq(" + index + ")"));
                                                            $("div#delivered a.btn--blue").remove();
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
                                $("div#delivered").append($("div#delivering div.order-block:eq(" + index + ")"));
                                $("div#delivered a").remove($("div#delivered a.btn--blue"));
                                $("div#delivered a.btn--blue").remove();
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

    $("button#btnAuthors").click(function (e) {
        e.stopPropagation();
        var author = $("input#txtAuthor").val();
        $("textarea#authors").val($("textarea#authors").val() + author + "\n");
        return false;
    });

    $("button#btnImages").click(function (e) {
        e.stopPropagation();
        var image = $("input#txtImage").val();
        $("textarea#images").val($("textarea#images").val() + image + "\n");
        return false;
    });

    $("form#show").submit(function(e) {
        e.preventDefault();

        var book = {
            //9781447225584
            "id": $("input#id").val(),
            "name": $("input#name").val(),
            "originalPrice": parseInt($("input#o-price").val()),
            "currentPrice": parseInt($("input#c-price").val()),
            "releaseYear": parseInt($("input#releaseyear").val()),
            "weight": parseFloat($("input#weight").val()),
            "numOfPage": parseInt($("input#numofpage").val()),
            "image": $("input#thumb").val(),
            "summary": $("textarea#description").val()
        };
        var cateID = $("select#category").val();
        var formID = $("select#form").val();
        var publisherID = $("select#publisher").val();
        var supplierID = $("select#supplier").val();
        var authors = [];
        var images = []; 

        var authorArr = $("textarea#authors").val().split("\n");
        $.each(authorArr, function (index, item) {
            if (item !== undefined && item !== null && item.length > 0) {
                var author = { "name": item };
                authors.push(author);
            }
        });

        var imageArr = $("textarea#images").val().split("\n");
        $.each(imageArr, function (index, item) {
            if (item !== undefined && item !== null && item.length > 0) {
                images.push(item);
            }
        });

        if (ValidateForm(authors).status === true) {
            Swal.fire({
                title: 'Do you want to insert or update this book?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, cancel!'
            }).then((result) => {
                if (result.value) {
                    $.post("/InsertBook",
                        {
                            book: book,
                            authors: authors,
                            images: images,
                            cateID: cateID,
                            formID: formID,
                            publisherID: publisherID,
                            supplierID: supplierID
                        },
                        function (data) {
                            if (data.status === true) {
                                Swal.fire({
                                    title: "Complete",
                                    text: "Insert book successfully!",
                                    icon: "success"
                                });
                            }
                            else {
                                Swal.fire({
                                    title: "Fail",
                                    text: data.message,
                                    icon: "error"
                                });
                            }
                        });
                }
            });
        }
        else {
            Swal.fire({
                title: "Fail",
                text: ValidateForm(authors).message,
                icon: "error"
            });
        }

    });

    $("input#search").keydown(function (e) {
        if (e.which === 13) {
            var value = $("input#search").val();
            $.post("/SearchAdmin", { value: value }, function (data) {
                console.log(data);
                SearchBook(data.obj);
                $('#search-result').show("slow");
            });
        }
        if ($(this).val() === "" || $(this).val() === " ") {
            $('#search-result').hide("slow");
            $("div#search-result div.row div.col-md-2").remove();
        }
    });

    $("input#search").change(function(e) {
        if ($(this).val() === "" || $(this).val() === " ") {
            $('#search-result').hide("slow");
            $("div#search-result div.row div.col-md-2").remove();
        }
    });

    function ValidateForm(authors) {
        if ($("input#id").val() === "" || $("input#id").val() === " " || $("input#id").val() === null) {
            return { status: false, message: "ID Field must be filled correctly!" };
        }
        else if ($("input#name").val() === "" || $("input#name").val() === " " || $("input#name").val() === null) {
            return { status: false, message: "Name Field must be filled correctly!" };
        }
        else if ($("input#thumb").val() === "" || $("input#thumb").val() === " " || $("input#thumb").val() === null) {
            return { status: false, message: "Thumbnail Field must be filled correctly!" };
        }
        else if (authors.length === 0) {
            return { status: false, message: "Author Field must be filled correctly!" };
        }
        else {
            return { status: true, message: "Success" };
        }
    }

    function SearchBook(bookArr) {
        for (var i = 0; i < bookArr.length; i++) {
            $("div#search-result div.row").append(
                '<div class="col-md-2">' +
                '<div class="card display-on-hover">' + 
                '<a href="' + bookArr[i].id + '">' +
                '<img class="card-img-top img-cover img-cover-25" src="' + bookArr[i].image + '" alt="Card image cap">' +
                '<div class="card-body">' +
                '<h5 class="card__book-title">' + bookArr[i].name + '</h5>' +
                '<p class="card__book-price">' + bookArr[i].currentPrice + '</p>' +
                '</div>' +
                '<div class="badge badge__utilities item-display">' +
                '<a href="#" class="badge__utilities-blue" id="remove" data="' + bookArr[i].id + '">' +
                '<i class="fas fa-trash-alt"></i>' +
                '</a>' +
                '<a href="#" class="badge__utilities-blue" id="update" data="' + bookArr[i].id + '">' +
                '<i class="fas fa-pen"></i>' +
                '</a>' +
                '</div>' +
                '</a>' +
                '</div>' +
                '</div>'
            );
            
        }
        $("div.card a#remove").click(function (e) {
            e.preventDefault();
            Swal.fire({
                title: 'Do you want to remove this book?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, cancel!'
            }).then((result) => {
                if (result.value) {
                    var id = $(this).attr("data");
                    $.post("Remove", { id: id }, function (data) {
                        if (data.status === true) {
                            Swal.fire({
                                title: "Complete",
                                text: "Remove book successfully!",
                                icon: "success"
                            });
                        }
                        else {
                            Swal.fire({
                                title: "Fail",
                                text: data.message,
                                icon: "error"
                            });
                        }
                    });
                }
            });
        });

        $("div.card a#update").click(function (e) {
            e.preventDefault();
            var index = parseInt($("div.card a#update").index($(this)));
            
            $("input#id").val(bookArr[index].id);
            $("input#name").val(bookArr[index].name);
            $("input#o-price").val(bookArr[index].originalPrice);
            $("input#c-price").val(bookArr[index].currentPrice);
            $("input#releaseyear").val(bookArr[index].releaseYear);
            $("input#weight").val(bookArr[index].weight);
            $("input#numofpage").val(bookArr[index].numOfPage);
            $("input#thumb").val(bookArr[index].image);
            $("textarea#description").val(bookArr[index].summary);

            $("select#category").val(bookArr[index].bookCategory[0].cate.id);
            $("select#form").val(bookArr[index].formBook[0].form.id);
            $("select#publisher").val(bookArr[index].publisherBook[0].publisher.id);
            $("select#supplier").val(bookArr[index].supplierBook[0].supplier.id);

            var authors = "";
            $.each(bookArr[index].authorBook, function (i, item) {
                authors += item.author.name;
                authors += "\n";
            });
            $("textarea#authors").val(authors);

            var images = "";
            $.each(bookArr[index].imageBook, function (i, item) {
                images += item.path;
                images += "\n";
            });
            $("textarea#images").val(images);

            $("#show").show("slow");
        });
    }

    $("#button").click(function (e) {
        if ($("form#show").css("display") === "none") {
            $("form#show").show("slow");
        }
        else {
            if ($("input#id").val().length === 0) {
                $("form#show").hide("slow");
                ResetFields();
            }
            else {
                ResetFields();    
            }
        }
    });

    function ResetFields() {
        $("input#id").val("");
        $("input#name").val("");
        $("input#o-price").val(50000);
        $("input#c-price").val(50000);
        $("input#releaseyear").val(2000);
        $("input#weight").val(1.0);
        $("input#numofpage").val(1);
        $("input#thumb").val("");
        $("textarea#description").val("");
        $("textarea#authors").val("");
        $("textarea#images").val("");
    }
});