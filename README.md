# Nitrogen - Halo 4 Variant API

## about;
Nitrogen is a .NET-based interface to Halo 4 map and game variants.

## requirements;
If you have a JTAG or RGH, you will need to use Xbox Neighborhood to send gametypes to your Xbox. Navigate to the *cache:/autosave/* directory to manipulate the temporary history and to avoid having to deal with STFS packages.

Otherwise, you'll have to resort to a flash drive. Make sure that your flash drive has been formatted by your Xbox. Nitrogen is currently unable to work with STFS container (CON) files, so you will need to extract/inject the variant BLF file. We recommend you use [Velocity](https://github.com/hetelek/Velocity).

In order to compile Nitrogen, you will need the latest version of the .NET Framework and Visual Studio IDE. You will also need the [Code Contracts extension](http://visualstudiogallery.msdn.microsoft.com/1ec7db13-3363-46c9-851f-1ce455f66970) for Visual Studio.

## how do I use this?
Visit the [wiki](https://github.com/ProjectGuiltySpark/nitrogen/wiki) for reference guides, tutorials, and more.

## WumboScript Viewer
We also made a small tool using Nitrogen that will output the Megalo script to a human readable format. Use it to verify that your script is correct. [More information is available on my blog](https://msaville8.wordpress.com/projects/wumboscript-viewer/).
![alt tag](https://msaville8.files.wordpress.com/2014/11/tg9kug5.png?w=1200&h=&crop=1)
![alt tag](https://msaville8.files.wordpress.com/2014/11/skqinpc.png?w=1200&h=&crop=1)

[Download it here](http://www.xboxchaos.com/files/file/153-wumboscript-viewer/) (Registration required)

## license;
Nitrogen is licensed under GPLv3. The full license text is available in the LICENSE file.

## credits;
#### lead developers;
+ Matt ([msaville8](http://github.com/msaville8))
+ Aaron ([amd7](http://github.com/amd7))

#### UI developer;
+ Alex ([Xerax](http://github.com/Xerax))

#### supporting developer;
+ Collin ([OrangeMohawk](http://github.com/OrangeMohawk))

#### architecture support;
+ kornman00 ([Kornman00](http://github.com/Kornman00))

#### lead researcher;
+ Lord Zedd ([Lord-Zedd](http://github.com/Lord-Zedd))

#### other contributors;
+ TJ ([ThunderWaffle](http://github.com/ThunderWaffle))
+ The [XboxChaos](http://xboxchaos.com) Community
+ SK Crisis for the team emblems
