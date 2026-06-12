$(document).ready(function () {
    resize_main_content();

    //canvas loader
    var cl = new CanvasLoader('canvasloader-container');
    cl.setColor('#443c41'); // default is '#000000'
    cl.setShape('square'); // default is 'oval'
    cl.setDiameter(80); // default is 40
    cl.setDensity(100); // default is 40
    cl.setRange(0.9); // default is 1.3
    cl.setFPS(58); // default is 24
    cl.show(); // Hidden by default


    // This bit is only for positioning - not necessary
    var loaderObj = document.getElementById("canvasLoader");
    loaderObj.style.position = "absolute";
    loaderObj.style["top"] = cl.getDiameter() * -0.5 + "px";
    loaderObj.style["left"] = cl.getDiameter() * -0.5 + "px";

    $("label.checkbox").each(function () {
        if ($(this).find("input").is(":checked")) $(this).addClass("checked");
        else $(this).removeClass("checked");
    });
});
$(window).resize(function () {
    resize_main_content();
});
function resize_main_content() {
    new_height = $(window).height() - $(".bg-inner").outerHeight(true) - $(".user-menu").outerHeight(true) - $("footer").outerHeight(true);
    $(".main-content").css("min-height", new_height + "px");
}

//mobile check
var isMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function () {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i);
    },
    any: function () {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};

//if not mobile => Select2
if (!isMobile.any()) $(".selectboxit").select2();
else $(".selectboxit").parent().append('<span class="select-style"><span class="select-arrow"></span></span>');

//Bootstrap tooltips init:
$('*[data-toggle="tooltip"]').tooltip();

//connected datepickers
$("#from").datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: "dd.mm.yy",
    onClose: function (selectedDate) {
        $("#to").datepicker("option", "minDate", selectedDate);
    }
});
$("#to").datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: "dd.mm.yy",
    onClose: function (selectedDate) {
        $("#from").datepicker("option", "maxDate", selectedDate);
    }
});

$(".date").keydown(function () {
    var _self = $(this);
    if (_self.val().length > 10) {
        $(this).val(_self.val().substring(0, 9));
    }
    else {
        for (var i = 0; i < _self.val().length; i++) {
            if (i === 2 || i === 5) {
                if (_self.val()[i] !== '.') {
                    $(this).val(_self.val().substring(0, i));
                    break;
                }
            } else {
                if (!IsNumeric(_self.val()[i])) {
                    $(this).val(_self.val().substring(0, i));
                    break;
                }
            }
        }
    }

    function IsNumeric(input) {
        var RE = /^-{0,1}\d*\.{0,1}\d+$/;
        return (RE.test(input));
    }
});
$(".date").change(function () {
    //var RE = /^(\d{1,2}).(\d{1,2}).(\d{4})$/;
    var RE = /(^((([0][1-9]|1[0-9]|2[0-8])\.(0[1-9]|1[012]))|((29|30|31)\.(0[13578]|1[02]))|((29|30)\.(0[4,6,9]|11)))\.(19|[2-9][0-9])\d\d$)|(^29\.02\.(19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)/;
    if (!RE.test($(this).val())) {
        $(this).val('');
    }
});

//show/hide advanced search fields
$("span.advanced-search span").click(function () {
    if (!$(".search-panel").hasClass("advanced")) {
        $(".search-panel").addClass("advanced");
        $("span.advanced-search .advanced").hide();
        $("span.advanced-search .short").show();
        $("input[name='IsAdvanced']").val('True');
    }
    else {
        $(".search-panel").removeClass("advanced");
        $("span.advanced-search .short").hide();
        $("span.advanced-search .advanced").show();
        $("input[name='IsAdvanced']").val('False');
    }
    if ($("header a img").width() > 80) {
        $(".search-panel").hide();
        $(".search-panel").fadeIn(400);
    }
    if ($.isFunction(calc_list_margin_top)) {
        calc_list_margin_top();
    }

});

//checkbox style
$("label.checkbox").click(function () {
    if ($(this).find("input").is(":checked")) $(this).addClass("checked");
    else $(this).removeClass("checked");
});

//scroll to top:
$(".scroll-to-top").click(function () {
    $('html, body').animate({ scrollTop: 0 }, 1400, 'easeOutQuint');
});

$("html, body").bind("scroll mousedown DOMMouseScroll mousewheel keyup", function () {
    $('html, body').stop();
});

$('.please-wait').click(function (e) {
    if (!$(this).is("a")
        || (e.which != 2 && !(e.shiftKey || e.altKey || e.metaKey || e.ctrlKey))) {
        $('.loader').show();
    }
});