$(document).ready(() => {

    var currentGameId = null;

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
                data.forEach(game => {
                    $(`<div class="sidebar-button" game="${game.Id}">${game.Name}</div>`)
                        .appendTo("#game-list")
                        .click(onTabSelected);
                });
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
            } else {
                console.log("I dont know what to do with " + key)
            }
        }
    }

    populateTabs();
    /*var interval = setInterval(() => {
        if ($("#game-list").children().length > 0) {
            clearInterval(interval);
        }
        else {
            populateTabs();
        }
        console.log('aaa');
    }, 100);*/

});