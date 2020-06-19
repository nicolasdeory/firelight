# leagueoflegends-led
![dotnet-ci](https://github.com/nicolasdeory/leagueoflegends-led/workflows/dotnet-ci/badge.svg)

A modular League of Legends integration for Razer Chroma and RGB LED devices.

![](repo/video-gif.gif)

You can join our [Discord server](https://discord.gg/KtTRPZt) to follow the latest updates.

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
- Azir
- Jax

## Usage
- Download the [latest release](https://github.com/nicolasdeory/leagueoflegends-led/releases)
- Setup a LED strip that listens to color data via sACN. If you don't have a LED strip, you will be able to see the simulated LED display in your screen. Alternatively, if you have a Razer Chroma Keyboard, the program should work out of the box!
- Open the program (`LedDashboard.exe`). It will default to Razer Chroma mode. If you want to use it with an LED strip, click on "Use LED strip".
- Load into a LoL game
- Enjoy the lights!

## How to Run 
1.	Once the Download is finished. Locate the file V0.4.1 [Version will change keep that in mind.]
2.	Extract the File to your Desire location.
3.	Open the Folder and continue to open folders until you pas win-x64

  ![](repo/File%20path.jpg)
  
4.	 In this folder scroll down until you find LedDashboard

  ![](repo/LED%20location.png)
  
5.	A screen should Pop up like the one Below.
  
    ![](repo/prg%20screen.png)
    
6. Open LOL and Load into a game! The program should work. 
 
Notice: if you want to confirm it connected to Choma connect it should look like this. 

 ![](repo/razer%20connect.png)
 
 It is common to have this with no text or icon show up [This will be updated to provide visual information].

## Roadmap / Planned Features
- Animation sets for each champion and active ability item
- Effects for summoner spells
- Add support for different keyboard brands and more RGB devices

## Known Issues
- Window focus isn't taken into account
- Game chat isn't taken into account (keypresses are treated as ability casts)

## Contributing
Check out the [contributing guide](CONTRIBUTING.md). Help is greatly appreciated with anything, really.

### Reporting Issues
If you found that something isn't working as expected, please post an issue detailing the problem and the steps to reproduce it. If you get an "Unhandled Exception" error, please paste the text that appears inside the box. Thank you!
