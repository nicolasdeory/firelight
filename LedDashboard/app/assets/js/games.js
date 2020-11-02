$(document).ready(() => {

    var currentGameId = null;

    function populateGameSettings(gameId) {
        $.get(`/gamesettings/${gameId}.html`)
            .then(html => {
                currentGameId = gameId;
                $("#game-content").html(html);
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

                })
                onTabSelected.bind($("#game-list > :first-child"))();
            }, onFailure: function (err, msg) {
                console.log(err, msg);
            }
        });
    }

    populateTabs();

});