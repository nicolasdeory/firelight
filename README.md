# leagueoflegends-led
A modular League of Legends integration for Razer Chroma and RGB LED devices.

![](repo/video-gif.gif)


## Features
- Health Bar
- Custom spellcast animations for several champions and items
- Trinket notifications - lights up when you have a ward available
- Gold notification - lights up when you should back to buy
- Keeps track of cooldowns and mana
- Kill Lights
- Death Lights
- Compatible with E1.31 devices (LED strips)
- Razer Chroma support

### Supported champions
If you play with one of these champions, you will see animations for their abilities.
- Ahri
- Ezreal
- Twisted Fate
- Udyr
- Vel'Koz
- Xerath

![Death light](repo/img-death.png)
![Out of Mana light](repo/img-oom.png)

## Usage
- Download the [latest release](https://github.com/nicolasdeory/leagueoflegends-led/releases)
- Setup a LED strip that listens to color data via sACN. If you don't have a LED strip, you will be able to see the simulated LED display in your screen. Alternatively, if you have a Razer Chroma Keyboard, the program should work out of the box!
- Open the program (`LedDashboard.exe`). It will default to Razer Chroma mode. If you want to use it with an LED strip, click on "Use LED strip".
- Load into a LoL game
- Enjoy the lights!

## Roadmap / Planned Features
- Animation sets for each champion and active ability item
- Effects for summoner spells
- Add support for different keyboard brands and more RGB devices

You can join our [Discord](https://discord.gg/EyBuGv) to follow the latest updates.

## Known Issues
- Window focus isn't taken into account
- Game chat isn't taken into account (keypresses are treated as ability casts)

## Contributing
Check out the [contributing guide](CONTRIBUTING.md). Help is greatly appreciated with anything, really.

### Reporting Issues
If you found that something isn't working as expected, please post an issue detailing the problem and the steps to reproduce it. If you get an "Unhandled Exception" error, please paste the text that appears inside the box. Thank you!
