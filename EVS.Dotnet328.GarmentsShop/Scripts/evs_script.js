$(".form-control").blur(function () {
    var check = $(this).data("evscheck");
    if (check == 'empty') {
        if ($(this).val().length == 0) {
            $(this).parent().addClass("has-error");
            $(this).parent().removeClass("has-success");
            $(this).next().html("<span class='glyphicon glyphicon-warning-sign'></span>");
        }
        else {
            $(this).parent().addClass("has-success");
            $(this).parent().removeClass("has-error");
            $(this).next().html("<span class='glyphicon glyphicon-check'></span>");
        }
    }
});

$(".img-swap").hover(function () {
    var img1 = $(this).attr("src");
    $(this).attr("src", $(this).data("swapimage"));
    $(this).data("swapimage", img1);
});

$(".panel-heading").click(function () {
    var temp = $(this).parent().data("hide");
    if (temp == 'fade') {
        $(this).next().fadeToggle(500);
    }
    else {
        if (temp == 'slide') {
            $(this).next().slideToggle(500);
        }
    }
});