function processSettings(settings) {

    let champions = Array.from(new Set(Object.keys(settings).filter(x => x.indexOf("champion") == 0).map(x => /champion-(\w+)-/g.exec(x)[1])));
    console.log(champions);
    new autoComplete({
        data: {
            src: champions
        },
        placeholder: "Type your champion",
        selector: "#champion-search",
        searchEngine: "strict",
        onSelection: feedback => {
            onChampionSelected(feedback.selection.value);
        },
        highlight:
        {
            render: true
        }
    });

    $("#champion-search").on('keyup', function (e) {
        if (e.keyCode === 13) {
            let champion = $("#autoComplete_result_0").text();
            if (champion)
                onChampionSelected(champion);           
        }
    });
}

let currentSelectedChampion = 'Ahri';

async function onChampionSelected(champion) {
    const settings = await getGameSettings('leagueoflegends');
    $("#champion-search").val("");
    $("#autoComplete_list").remove();
    $("#champion-name").text(champion);
    let prefix = `champion-${currentSelectedChampion}`;
    let keys = [`${prefix}-q-cast-mode`, `${prefix}-w-cast-mode`, `${prefix}-e-cast-mode`, `${prefix}-r-cast-mode`];
    let newPrefix = `champion-${champion}`;
    let newKeys = [`${newPrefix}-q-cast-mode`, `${newPrefix}-w-cast-mode`, `${newPrefix}-e-cast-mode`, `${newPrefix}-r-cast-mode`];
    console.log(key);
    for (let i = 0; i < keys.length; i++) {
        $(`input[name="${keys[i]}"]`).prop('name', newKeys[i]);
        let val = settings[newKeys[i]];
        $(`input[name="${newKeys[i]}"]`).val([val]);
    }
    currentSelectedChampion = champion;

}

getGameSettings('leagueoflegends').then(settings => processSettings(settings));

