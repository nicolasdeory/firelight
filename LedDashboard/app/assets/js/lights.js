function setKeyboardColor(index, hex) {
    if (index == 46) {
        $("#k46").attr("fill", hex);
        $("#k46-1").attr("fill", hex);
    } else {
        $("#k" + index).attr("fill", hex);
    }
}

function setLedColor(prefix, index, hex) {
    $("#" + prefix + index).attr("fill", hex);
}


function sanitizeHex(hex) {
    if (hex == "000000")
        hex = "rgba(255,255,255,0.05)";
    else
        hex = "#" + hex;
    return hex;
}

function setLightColors(lightArr) {
    // Keyboard
    for (let i = 0; i < lightArr[0].length; i++) {
        let hex = sanitizeHex(lightArr[0][i])
        setKeyboardColor(i, hex);
    }
    // Strip
   /* for (let i = 0; i < lightArr[1].length; i++) {
        let hex = sanitizeHex(lightArr[0][i])
        setKeyboardColor(i, hex);
    }*/
    // Mouse
    for (let i = 0; i < lightArr[2].length; i++) {
        let hex = sanitizeHex(lightArr[2][i])
        setLedColor('m', i, hex);
    }
    // Mousepad
    for (let i = 0; i < lightArr[3].length; i++) {
        let hex = sanitizeHex(lightArr[3][i])
        setLedColor('mp', i, hex);
    }
    // Headset
    for (let i = 0; i < lightArr[4].length; i++) {
        let hex = sanitizeHex(lightArr[4][i])
        setLedColor('h', i, hex);
    }
    // Keypad
    for (let i = 0; i < lightArr[5].length; i++) {
        let hex = sanitizeHex(lightArr[5][i])
        setLedColor('kp', i, hex);
    }
    // General
    for (let i = 0; i < lightArr[6].length; i++) {
        let hex = sanitizeHex(lightArr[6][i])
        setLedColor('g', i, hex);
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