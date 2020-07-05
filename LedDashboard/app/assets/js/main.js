function setKeyboardColor(index, hex) {
    if (index == 46) {
        $("#k46").attr("fill", hex);
        $("#k46-1").attr("fill", hex);
    } else {
        $("#k" + index).attr("fill", hex);
    }
}


$(document).ready(() =>
{

    $("#tab-lights").click(function ()
    {
        if ($(this).hasClass("selected"))
            return;
        $(".tab").removeClass("selected");
        $(this).addClass("selected");
        window.location.href = "lights.html";
    });

    $("#tab-games").click(function ()
    {
        if ($(this).hasClass("selected"))
            return;
        $(".tab").removeClass("selected");
        $(this).addClass("selected");
        window.location.href = "games.html";
    });

    $("#tab-settings").click(function ()
    {
        if ($(this).hasClass("selected"))
            return;
        $(".tab").removeClass("selected");
        $(this).addClass("selected");
        window.location.href = "settings.html";
    });

});