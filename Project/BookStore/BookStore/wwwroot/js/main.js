



jQuery(document).ready(function ($) {
  jQuery(".owl-carousel-product").length && jQuery('.owl-carousel-product').owlCarousel({
    nav: true
    , dots: false
    , items: 4
    , margin: 30
    , responsive: {
      0: {
        items: 1
      }
      , 320: {
        items: 2
      }
      , 768: {
        items: 4
      }
      , 992: {
        items: 5
      }
      , 1200: {
        items: 6
      }
    }
    , navElement:'div',   
  });

});



function myFunction() {
  var x = document.getElementById("comment__form");
  if (x.style.display === "block") {
    x.style.display = "none";
  } else {
    x.style.display = "block";
  }
}
function showFunction() {
  var x = document.getElementById("searchBar");
  if (x.style.height === "0px" && x.style.transform === "scale(0)") {
    x.style.height = "6rem";
    x.style.transform = "scale(1)";
  } else {
    x.style.height = "0px";
    x.style.transform = "scale(0)";
  }
}
function showFunction1() {
  var x = document.getElementById("comment");
  var i = 0;

  if (x.style.height === "0px" && x.style.transform === "scale(0)") {
    x.style.height = "45rem";
    x.style.transform = "scale(1)";
  } else {
    x.style.height = "0px";
    x.style.transform = "scale(0)";
  }
}
// var prevScrollpos = window.pageYOffset;
// window.onscroll = function() {
//   var currentScrollPos = window.pageYOffset;
//   if (prevScrollpos > currentScrollPos) {
//     document.getElementById("navbar").style.top = "0";
//   } else {
//     document.getElementById("navbar").style.top = "-12rem";
//   }
//   prevScrollpos = currentScrollPos;
// }

function wcqib_refresh_quantity_increments() {
  jQuery("div.quantity:not(.buttons_added), td.quantity:not(.buttons_added)").each(function (a, b) {
    var c = jQuery(b);
    c.addClass("buttons_added"), c.children().first().before('<input type="button" value="-" class="minus" />'), c.children().last().after('<input type="button" value="+" class="plus" />')
  })
}
String.prototype.getDecimals || (String.prototype.getDecimals = function () {
  var a = this,
    b = ("" + a).match(/(?:\.(\d+))?(?:[eE]([+-]?\d+))?$/);
  return b ? Math.max(0, (b[1] ? b[1].length : 0) - (b[2] ? +b[2] : 0)) : 0
}), jQuery(document).ready(function () {
  wcqib_refresh_quantity_increments()
}), jQuery(document).on("updated_wc_div", function () {
  wcqib_refresh_quantity_increments()
}), jQuery(document).on("click", ".plus, .minus", function () {
  var a = jQuery(this).closest(".quantity").find(".qty"),
    b = parseFloat(a.val()),
    c = parseFloat(a.attr("max")),
    d = parseFloat(a.attr("min")),
    e = a.attr("step");
  b && "" !== b && "NaN" !== b || (b = 0), "" !== c && "NaN" !== c || (c = ""), "" !== d && "NaN" !== d || (d = 0), "any" !== e && "" !== e && void 0 !== e && "NaN" !== parseFloat(e) || (e = 1), jQuery(this).is(".plus") ? c && b >= c ? a.val(c) : a.val((b + parseFloat(e)).toFixed(e.getDecimals())) : d && b <= d ? a.val(d) : b > 0 && a.val((b - parseFloat(e)).toFixed(e.getDecimals())), a.trigger("change")
});



const container = $(".star__rating");
container.each(function (index_container) {
  const stars = $(this).find("label.full");
  var value_star = $(this).data("value-star");
  getcolorStar(value_star, stars);
});

function getcolorStar(value_star, element) {
  for (i = 0; i <= value_star - 1; i++) {
    $(element[i]).css({
      "color": "#fec600",
    });
  }
}
//star chay nguoc


$('[data-toggle="zoom"]').each(function () {
  $(this).zoom({
    url: $(this).attr('data-image'),
    on: 'click',
    duration: 0
  });
});






