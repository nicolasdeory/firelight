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
    });

    $("#tab-games").click(function ()
    {
        if ($(this).hasClass("selected"))
            return;
        $(".tab").removeClass("selected");
        $(this).addClass("selected");
    });

    $("#tab-settings").click(function ()
    {
        if ($(this).hasClass("selected"))
            return;
        $(".tab").removeClass("selected");
        $(this).addClass("selected");
    });

    let i = 0;
    setInterval(() => {
        setKeyboardColor(i % 109, "#fff");
        i++;
    }, 500);

});