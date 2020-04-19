
<p align="center">
  <img  src="icon.jpg">
</p>

# What is League of Legends Bot

  * League of legends bot is a pixel bot, written in C# .NET using System.Interop, PVInvoke and NLua. 
  * It's a good way to raise your account level 30  quickly without having to play. Or to win blue essences for free

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
   * Type 'startAram' or 'startCoop'
	 
# Contact

   Join the discord server : https://discord.gg/cB8qtcE

# Authors

   * **Skinz** - *Initial work* [Skinz3](https://github.com/Skinz3)
   * **Forerunner**  [Glenndilen](https://github.com/glenndilen)

# API References

| Function | Return Type | Description |
| :--- | :--- | :--- |
| `keyUp` | `void` |  `(string key)` Release a keyboard key |
| `keyDown` | `void` | `(string key)`  Sink a keyboard key |
| `pressKey` | `void` | `(string key)`  Press a keyboard key |
| `moveMouse` | `void` |  `(int x,int y)` Mouve mouse |
| `rightClick` | `void` |  `(int x,int y)` Mouse right click |
| `leftClick` | `void` |  `(int x,int y)` Mouse left click |
| `getColor` | `String` |  `(int x,int y)` Get Hexadecimal color |
| `waitForColor` | `void` |  `(int x,int y,String colorHex)` Stop bot until hex color exists at position |
| `talk` | `void` |  `(String message)` Talk in ally chat channel |
| `waitUntilProcessBounds` | `void` |  `(String processName,int width,int height)` Pause bot until process rect bounds is width * heigth |
| `isProcessOpen` | `bool` |  `(String processName)` Return state of the process |
| `centerProcess` | `void` |  `(String processName)` Center process windows relatively to screen size |
| `bringProcessToFront` | `void` |  `(String processName)` Bring process window to front |
| `waitProcessOpen` | `void` |  `(String processName)` Pause bot until the process open |
| `centerProcess` | `void` |  `(String processName)` Center process windows relatively to screen size |
| `executePattern` | `void` |  `(String patternName)` Execute pattern **pattern name** |
| `log` | `void` |  `(String message)` Log **message** in the console |
| `wait` | `void` |  `(int duration)` Wait (**duration** is in milliseconds) |
| `waitForImage` | `void` |  `(string image)` Wait util the **image** is displayed on screen (**image** must exists in `/Binaries/Images/`) |
| `leftClickImage` | `void` |  `(string image)` Right click the first **image** founded on screen (**image** must exists in `/Binaries/Images/`) |