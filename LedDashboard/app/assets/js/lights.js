/**
 * A linear interpolator for hexadecimal colors
 * @param {String} a
 * @param {String} b
 * @param {Number} amount
 * @example
 * // returns #7F7F7F
 * lerpColor('#000000', '#ffffff', 0.5)
 * @returns {String}
 */
function lerpColor(a, b, amount) {

    var ah = parseInt(a.replace(/#/g, ''), 16),
        ar = ah >> 16, ag = ah >> 8 & 0xff, ab = ah & 0xff,
        bh = parseInt(b.replace(/#/g, ''), 16),
        br = bh >> 16, bg = bh >> 8 & 0xff, bb = bh & 0xff,
        rr = ar + amount * (br - ar),
        rg = ag + amount * (bg - ag),
        rb = ab + amount * (bb - ab);

    return '#' + ((1 << 24) + (rr << 16) + (rg << 8) + rb | 0).toString(16).slice(1);
}

function hexToHSL(H) {
    // Convert hex to RGB first
    let r = 0, g = 0, b = 0;
    if (H.length == 4) {
        r = "0x" + H[1] + H[1];
        g = "0x" + H[2] + H[2];
        b = "0x" + H[3] + H[3];
    } else if (H.length == 7) {
        r = "0x" + H[1] + H[2];
        g = "0x" + H[3] + H[4];
        b = "0x" + H[5] + H[6];
    }
    // Then to HSL
    r /= 255;
    g /= 255;
    b /= 255;
    let cmin = Math.min(r, g, b),
        cmax = Math.max(r, g, b),
        delta = cmax - cmin,
        h = 0,
        s = 0,
        l = 0;

    if (delta == 0)
        h = 0;
    else if (cmax == r)
        h = ((g - b) / delta) % 6;
    else if (cmax == g)
        h = (b - r) / delta + 2;
    else
        h = (r - g) / delta + 4;

    h = Math.round(h * 60);

    if (h < 0)
        h += 360;

    l = (cmax + cmin) / 2;
    s = delta == 0 ? 0 : delta / (1 - Math.abs(2 * l - 1));
    s = +(s * 100).toFixed(1);
    l = +(l * 100).toFixed(1);

    return [h, s, l];
}

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
    hex = hex.toLowerCase();
    var hsl = hexToHSL("#" + hex);
    var luminosity = hsl[2] / 100;
    if (luminosity < 0.2)
        hex = lerpColor("#2D2D2D", hex, luminosity);
    else
        hex = "#" + hex;
    return hex;
}

function clearLedStripCanvas()
{
    ledStripContext.clearRect(0, 0, $("#ledstrip-canvas").width(), $("#ledstrip-canvas").height());
}

function setLedStripColor(index, hex) {
    ledStripContext.fillStyle = hex;
    ledStripContext.fillRect(index * 5, 0, 5, 5);
}

function setLightColors(lightArr) {
    // Keyboard
    for (let i = 0; i < lightArr[0].length; i++) {
        let hex = sanitizeHex(lightArr[0][i])
        setKeyboardColor(i, hex);
    }
    // Strip
    clearLedStripCanvas();
    for (let i = 0; i < lightArr[1].length; i++) {
        let hex = sanitizeHex(lightArr[1][i])
        setLedStripColor(i, hex);
    }
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
        "url": "/lights/lastframe/get",
        "parameters": null,
        "postData": null
    };

    //let timeout = setTimeout(onTimedOut, 2000);

    window.cefQuery({
        request: JSON.stringify(request),
        onSuccess: function (response) {
            //clearTimeout(timeout);
            respObject = JSON.parse(response);
            setLightColors(respObject.Data);
        }, onFailure: function (err, msg) {
            console.log(err, msg);
        }
    });
}

function onTimedOut()
{
    alert("Something wrong happened. Please try opening the app again.");
    window.close();
}

var currentlyDisplayedError = null;

function handleErrors(errors) {
    if (errors.length == 0) {
        dismissError();
    } else {
        let mostRecentError = errors[errors.length - 1];
        if (currentlyDisplayedError && currentlyDisplayedError.Id == mostRecentError.Id)
            return;
        if (!currentlyDisplayedError)
            showError();
        currentlyDisplayedError = mostRecentError;
        $("#error-title").text(currentlyDisplayedError.Title);
        $("#error-cta").text(currentlyDisplayedError.CtaText);
    }

    
}

function dismissError() {
    $("#error-container").removeClass("visible");
}

function showError() {
    $("#error-container").addClass("visible");
}

function getErrors() {
    var request = {
        "method": "GET",
        "url": "/global/errors/get",
        "parameters": null,
        "postData": null
    };
    window.cefQuery({
        request: JSON.stringify(request),
        onSuccess: function (response) {
            respObject = JSON.parse(response);
            handleErrors(respObject.Data);
        }, onFailure: function (err, msg) {
            console.log(err, msg);
        }
    });
}

var ledStripContext = null;
$(document).ready(() => {

    ledStripContext = $("#ledstrip-canvas")[0].getContext("2d");

    setInterval(getLightFrame, 30);

    setInterval(getErrors, 200)

});