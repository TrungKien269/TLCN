$("#button").click(function () {
    $("#show").slideToggle("slow");
});

$('#search').keyup(function () {
    if ($(this).val()) {
        $('#search-result').show("slow");
    }
    else {
        $('#search-result').hide("slow");
    }
})
