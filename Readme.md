
<p align="center">
  <img  src="icon.jpg">
</p>

# What is League of Legends Bot

  * League of legends bot is a pixel bot, written in C# .NET using System.Interop, PVInvoke and NLua. 
  * It's a good way to raise your level 30 account quickly without having to play. Or to win blue essences for free
  * This bot is undetectable because it is visual (a pixel-bot)

  > Version: League of Legends 10.8

## Installation

   Requirements : League Of Legends (a League of legends account), a computer with a 1920 * 1080 Resolution. 
   .NET 4.7 framework.
  
## How to make it work?

   * First step go to league of legends client and set windows size to 1600 * 900
   ![alt text](https://puu.sh/FyhQs/e8a84b1ad9.png)
   * Now go to this screen: 
   ![alt text](https://puu.sh/FyhP1/9c3a9c8aac.png)
   * Start \Binaries\LeagueBot.exe as Administrator.
   * Type 'startAram'
	 
# Contact

   Join the discord server : https://discord.gg/cB8qtcE

# Authors

   * **Skinz** - *Initial work* [Skinz3](https://github.com/Skinz3)
   * **Forerunner**  [Glenndilen](https://github.com/glenndilen)

# API References

| Function | Return Type | Description |
| :--- | :--- | :--- |
| `keyUp` | `void` |  Release a keyboard key |
| `keyDown` | `void` |  Sink a keyboard key |
| `pressKey` | `void` |  Press a keyboard key |
| `moveMouse` | `void` |  ** `(int x,int y)` **  Mouve mouse |
| `rightClick` | `void` | **  `(int x,int y)`**  Mouse right click |
| `leftClick` | `void` | **  `(int x,int y)`**  Mouse left click |
| `getColor` | `String` |  ** `(int x,int y)`**  Get Hexadecimal color |
| `waitForColor` | `void` |  ** `(int x,int y,String colorHex)`**  Stop bot until hex color exists at position |
| `talk` | `void` |  ** `(String message)`**  Talk in ally chat channel |
| `waitUntilProcessBounds` | `void` |  ** `(String processName,int width,int height)`**  Wait until process rect bounds is width * heigth |
| `isProcessOpen` | `bool` |  ** `(String processName)`**  Return state of the process |