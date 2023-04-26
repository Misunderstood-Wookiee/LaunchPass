# LaunchPass
LaunchPass is a Frontend and fork of [RetroPass](https://github.com/retropassdev/RetroPass)
it is a highly customisable Frontend for Xbox One/Series Consoles for use with UWP Retro Gaming Emulators.
![Video](https://github.com/Misunderstood-Wookiee/LaunchPass/blob/e55c515e3d5e9385093a306142e1ab50302d9f97/Docs/LaunchPass.webp)
[![Showcase](https://github.com/Misunderstood-Wookiee/LaunchPass/blob/173fc46d795b28ca138c553bbb393205f00be7d8/Docs/Screenshot%202023-04-22%20021815.png)](https://www.youtube.com/watch?v=Ox-JfUBo9as)


This is made specifically for Xbox Consoles and hopefully will give users enough customisation options to make it feel their own. 
## Usage
[Check out our Wiki for setup and usage intructions](https://github.com/Misunderstood-Wookiee/LaunchPass/wiki)


## Limitations

 - Xbox Only
 - Optimized for Gamepad Only
 - Zipped content supported only if RetroArch, Retrix Gold or other supported cores/emulators support compressed archive format.
 - No Automatic Scrapper, you must use [LaunchBox](https://www.launchbox-app.com). for Setup (More Info Below)
 - EmulationStation support is discontinued sorry!
 - Video Background can cause lag in some-instances due to UWP limitations, we recommend 1080p resolutions and use a Image for the Game Details to reduce how many video play at the same time. (Series X may fair better in this regard)
  
## Installation
 1. [Download](../../releases/) latest LaunchPass.
 2. Connect to Xbox through Xbox Device portal and install:
	- LaunchPass_x.y.z.0_x64.appxbundle
	- The 3 appx dependencies in Dependencies.zip or Dependencies.7z.
3. Installation is finished. Proceed to and Follow [Setup](https://github.com/Misunderstood-Wookiee/LaunchPass/wiki/Basic-Usage) Guide!


## Build from source

1. Install Visual Studio 2019
2. Get the latest source code from Main/Dev branch or [release](../../releases/)
3. Open **RetroPass.sln**
4. Under **Package.appxmanifest** -> **Packing**, create and choose a different Certificate if needed.
5. Set **Configuration** to **Release**
6. Set **Platform** to **x64**
7. **Project** -> **Publish** -> **Create App Packages...**
8. Choose **Sideloading**, turn off **Enable automatic updates**
9. **Yes, select a certificate** or **Yes, use the current certificate**
10. Under **Architecture** check only **x64**
11. Package is built and ready to install.
12. Optionally, for smaller package size, it is safe to delete:
	- Add-AppDevPackage.resources
	- Dependencies\arm
	- Dependencies\arm64
	- Dependencies\x86
	- TelemetryDependencies
	- Add-AppDevPackage.ps1
	- Install.ps1

Feel free to make PR`s or even fork the repository and develop the app to your liking, but please keep branding, give credit where it`s due & keep in line with the licence. 

