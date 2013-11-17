#Nitrogen - Halo Content API

##about;
Nitrogen is a .NET library with the ultimate goal of making it super duper easy to modify user-generated files in Halo. The current focus is on Halo 4 gametypes and map variants, but there are plans to extend Nitrogen to support other games and content types.

####Supported Content Types:
#####Halo 4
+ Map Info
+ Map Variants (Partial)

#####Halo: Reach
_none yet_

#####Halo 3
_none yet_

##disclaimer;
Unauthorized modifications to data files violate the Xbox LIVE ToS. We are not responsible for anything that happens to your console, account, and your dignity. This API is intended for educational use only.

Nitrogen is licensed under GPLv3. The full license text is available in the LICENSE file.

##requirements;
If you have a JTAG, RGH, or a dev kit, you will need to use Xbox Neighborhood to send gametypes to your Xbox. Navigate to the *cache0:/autosave/* directory to manipulate the temporary history and avoid dealing with STFS packages.

Otherwise, you'll have to resort to using a flash drive. Make sure that your flash drive was formatted by your Xbox. Nitrogen is currently unable to work with STFS container (CON) files, so you will need to extract/inject the variant BLF file. We recommend you use [Velocity](https://github.com/hetelek/Velocity).

##how do I use this?
_to be implemented_

##subz n viewz;
####developers;
+ Matt ([synth92](http://github.com/synth92))
+ Aaron ([amd7](http://github.com/amd7))

####architecture support;
+ kornman00 ([Kornman00](http://github.com/Kornman00))

####lead researcher;
+ Lord Zedd ([Lord-Zedd](http://github.com/Lord-Zedd))

####contributors;
+ Alex ([Xerax](http://github.com/Xerax))
+ TJ ([ThunderWaffle](http://github.com/ThunderWaffle))
+ Collin ([OrangeMohawk](http://github.com/OrangeMohawk))
+ The [XboxChaos](http://xboxchaos.com) Community
