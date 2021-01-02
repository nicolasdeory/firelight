const keyCodeToChar = { 8: "Backspace", 9: "Tab", 13: "Enter", 16: "ShiftLeft", 17: "CtrlLeft", 18: "AltLeft", 19: "Pause", 20: "CapsLock", 27: "Esc", 32: "Space", 33: "PgUp", 34: "PgDown", 35: "End", 36: "Home", 37: "Left", 38: "Up", 39: "Right", 40: "Down", 45: "Insert", 46: "Delete", 48: "0", 49: "1", 50: "2", 51: "3", 52: "4", 53: "5", 54: "6", 55: "7", 56: "8", 57: "9", 65: "A", 66: "B", 67: "C", 68: "D", 69: "E", 70: "F", 71: "G", 72: "H", 73: "I", 74: "J", 75: "K", 76: "L", 77: "M", 78: "N", 79: "O", 80: "P", 81: "Q", 82: "R", 83: "S", 84: "T", 85: "U", 86: "V", 87: "W", 88: "X", 89: "Y", 90: "Z", 91: "WinKey", 93: "ContextMenu", 96: "Numpad 0", 97: "Numpad 1", 98: "Numpad 2", 99: "Numpad 3", 100: "Numpad 4", 101: "Numpad 5", 102: "Numpad 6", 103: "Numpad 7", 104: "Numpad 8", 105: "Numpad 9", 106: "Numpad *", 107: "Numpad +", 109: "Numpad -", 110: "Numpad .", 111: "Numpad /", 112: "F1", 113: "F2", 114: "F3", 115: "F4", 116: "F5", 117: "F6", 118: "F7", 119: "F8", 120: "F9", 121: "F10", 122: "F11", 123: "F12", 144: "NumLock", 145: "ScrLock", 182: "MyComputer", 183: "MyCalculator", 186: ";", 187: "=", 188: ",", 189: "-", 190: ".", 191: "/", 192: "`", 219: "[", 220: "\\", 221: "]", 222: "'" };
function mouseCodeToString(btn) {
    switch (btn) {
        case 0:
            return "LMB";
        case 1:
            return "Scroll";
        case 2:
            return "RMB";
        case 3:
            return "Mouse 4";
        case 4:
            return "Mouse 5";
        default:
            return "Mouse ?";
    }
}
$(document).ready(() => {

    var currentGameId = null;
    var currentSelectedBindingName = null;

    function populateGameSettings(gameId) {
        $.get(`/gamesettings/${gameId}.html`)
            .then(html => {
                getGameSettings(gameId).then((settings) => {
                    currentGameId = gameId;
                    $("#game-content").html(html);
                    fillSettingsValues(settings);
                    attachEvents();
                }).catch((err) => {
                    console.error(err);
                });
            });
    }

    function getGameSettings(gameId) {

        var request = {
            "method": "GET",
            "url": `/games/${gameId}`
        };

        return new Promise((resolve, reject) => {
            window.cefQuery({
                request: JSON.stringify(request),
                onSuccess: function (response) {
                    data = JSON.parse(response).Data;
                    resolve(data);
                }, onFailure: function (err, msg) {
                    console.log(err, msg);
                    reject();
                }
            });
        });
        
    }

    function onTabSelected() {
        if ($(this).hasClass("selected"))
            return;
        $(this).addClass("selected");
        var game = $(this).attr("game");
        populateGameSettings(game);
    }

    function populateTabs() {
        var request = {
            "method": "GET",
            "url": "/games"
        };
        window.cefQuery({
            request: JSON.stringify(request),
            onSuccess: function (response) {
                data = JSON.parse(response).Data;
                for (let i = 0; i < data.length; i++) {
                    const game = data[i];
                    $(`<div class="sidebar-button" game="${game.Id}">${game.Name}</div>`)
                        .appendTo("#game-list")
                        .click(onTabSelected);
                }
                onTabSelected.bind($("#game-list > :first-child"))();
            }, onFailure: function (err, msg) {
                console.log(err, msg);
            }
        });
    }

    function post(url, data) {
        return new Promise((resolve, reject) => {
            var request = {
                "method": "POST",
                "url": url,
                "postData": data
            };
            window.cefQuery({
                request: JSON.stringify(request),
                onSuccess: () => resolve(),
                onFailure: () => reject()
            });
        })
        
    }

    function onSwitchChanged() {
        let value = Boolean($(this).prop("checked"));
        let updateObj = {};
        updateObj[$(this).attr('name')] = value;
        post(`/games/${currentGameId}`, updateObj);
    }

    function onMultichoiceChanged() {
        let value = $(this).val();
        let updateObj = {};
        updateObj[$(this).attr('name')] = value;
        post(`/games/${currentGameId}`, updateObj);
    }

    function onBindingClicked() {
        const isSelectedAlready = $(this).hasClass("selected");
        $(".binding-box").removeClass("selected")
        if (!isSelectedAlready) {
            $(this).addClass("selected");
            currentSelectedBindingName = $(this).attr("name");
        } 
    }

    function unselectCurrentBinding() {
        $(`.binding-box[name='${currentSelectedBindingName}']`).removeClass("selected");
        currentSelectedBindingName = null;
    } 

    function updateCurrentBinding(text) {
        updateBinding(currentSelectedBindingName, text);
        unselectCurrentBinding();
    }

    function updateBinding(name, text) {
        const bindingbox = $(`.binding-box[name='${name}']`);
        $(bindingbox).text(text);
        if (text.length > 1) {
            $(bindingbox).addClass("smaller");
        } else {
            $(bindingbox).removeClass("smaller");
        }
    }

    function onKeyPress(e) {
        if (currentSelectedBindingName == null)
            return;
        e.preventDefault();
        const kc = e.which;
        let keyname = e.key;
        if (keyname == "Escape") {
            unselectCurrentBinding();
            return;
        }
        keyname = keyCodeToChar[kc];
        const finalValue = "k" + kc;
        let updateObj = {};
        updateObj[currentSelectedBindingName] = finalValue;
        post(`/games/${currentGameId}`, updateObj);
        updateCurrentBinding(keyname);
    }

    function onClick(e) {
        if (currentSelectedBindingName == null)
            return;
        e.preventDefault();
        const btn = e.button;
        let buttonName = mouseCodeToString(btn);
        const finalValue = "m" + (btn+1);
        let updateObj = {};
        updateObj[currentSelectedBindingName] = finalValue;
        post(`/games/${currentGameId}`, updateObj);
        updateCurrentBinding(buttonName);
    }

// TODO collapsible tabs in content
    function attachEvents() {
        $(".switch input").change(onSwitchChanged);
        $(".multi-choice input").change(onMultichoiceChanged);
        $(".binding .binding-box").click(onBindingClicked);
        //$(".setting-section-restore-default button").click(onRestoreDefaultsClicked);
    }

    function fillSettingsValues(settings) {
        for (key in settings) {
            const val = settings[key];
            if (val == "true" || val == "false") {
                // checkbox
                let ch = val === 'true';
                $(`input[name="${key}"]`).prop("checked", ch);
            } else if (val == "1" || val == "2" || val == "3") {
                // radio
                $(`input[name="${key}"]`).val([val]);
            } else if (val.indexOf("k") == 0) {
                // key binding
                let code = parseInt(val.replace(/[k]/, ""))
                $(`.binding-box[name="${key}"]`).text(keyCodeToChar[code]);
            } else if (val.indexOf("m") == 0) {
                // mouse binding
                let code = parseInt(val.replace(/[m]/, ""))
                updateBinding(key, mouseCodeToString(code-1));
            }
            else {
                console.log("I dont know what to do with " + key);
            }
        }
    }

    $(document).on('click contextmenu', (e) => {
        if (currentSelectedBindingName) {
            e.preventDefault();
            e.stopImmediatePropagation();
        }
    });

    $(document).keydown(onKeyPress);
    $(document).mousedown(onClick);


    populateTabs();

});