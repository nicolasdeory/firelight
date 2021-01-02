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

    $("input[type='text']").focus(function () {
        $(this).parents().eq(1).children().find("path").attr("fill", "#FFE14D");
    });

    $("input[type='text']").blur(function () {
        $(this).parents().eq(1).children().find("path").attr("fill", "#767676");
    });
    
});