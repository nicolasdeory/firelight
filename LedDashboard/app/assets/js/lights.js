function setKeyboardColor(index, hex) {
    if (index == 46) {
        $("#k46").attr("fill", hex);
        $("#k46-1").attr("fill", hex);
    } else {
        $("#k" + index).attr("fill", hex);
    }
}

function getLightFrame() {
    var request = {
        "method": "GET",
        "url": "/lights/lastframe",
        "parameters": null,
        "postData": null
    };
    window.cefQuery({
        request: JSON.stringify(request),
        onSuccess: function (response) {
            //respObject = JSON.parse(response);
            console.log(response);
        }, onFailure: function (err, msg) {
            console.log(err, msg);
        }
    });
}


$(document).ready(() =>
{

    setInterval(getLightFrame, 30);

});