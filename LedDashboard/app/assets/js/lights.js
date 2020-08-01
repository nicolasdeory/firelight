function setKeyboardColor(index, hex) {
    if (hex == "000000")
        hex = "rgba(255,255,255,0.05)";
    else
        hex = "#" + hex;
    if (index == 46) {
        $("#k46").attr("fill", hex);
        $("#k46-1").attr("fill", hex);
    } else {
        $("#k" + index).attr("fill", hex);
    }
}

function setLightColors(lightArr) {
    for (let i = 0; i < lightArr[0].length; i++) {
        setKeyboardColor(i, lightArr[0][i])
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
            respObject = JSON.parse(response);
            setLightColors(respObject.Data);
        }, onFailure: function (err, msg) {
            console.log(err, msg);
        }
    });
}


$(document).ready(() => {

    setInterval(getLightFrame, 30);

});