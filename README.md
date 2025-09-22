# BONELAB Fusion Dedicated Server
This is a fork of BONELAB Fusion for headless dedicated server hosting.
A multiplayer mod for BONELAB featuring support for all platforms.

![](https://i.imgur.com/1ZpMfei.png)

## Setup
You must have the following
- Steam
- Sandboxie Plus
- Bonelab
- Flatplayer

This is a brief guide on how to setup a server.
- Create a new bonelab directory with bonelab inside of it just for this. This will be the directory for hosting your server. Set it up as you would normal bonelab. I didn't like the hassle so I used a pirated copy of bonelab for the server.
- Run steam in a sandboxie and login. This will be the account hosting your server, it can host multiple servers.
- Build the bonelab fusion dedicated server's source code.
- Place LabFusion.dll & FlatPlayer.dll (you must get this elsewhere) in your new bonelabs mods.
- Place the .bat file below into your main directory, beside BONELAB.exe.
- Run the .bat file from the same sandboxie group you're running steam inside.
- Your server should be started

```bat
@echo off
BONELAB_Steam_Windows64.exe -batchmode -nographics
```

If any issues occur, please discuss this in https://discord.gg/jSw8Qrkmwn and not the main bonelab fusion discord :)

## Credits
- BoneLib AutoUpdater: https://github.com/yowchap/BoneLib
- Testing/Development Credits In Game

## Licensing
- The source code of [Facepunch.Steamworks](https://github.com/Facepunch/Facepunch.Steamworks) is used partially under the MIT License. The full license can be found [here](https://github.com/Facepunch/Facepunch.Steamworks/blob/master/LICENSE).
- The source code of [Steamworks.NET](https://github.com/rlabrecque/Steamworks.NET) is used partially under the MIT License. The full license can be found [here](https://github.com/rlabrecque/Steamworks.NET/blob/master/LICENSE.txt).
- The source code of [LiteNetLib](https://github.com/RevenantX/LiteNetLib) is included under the MIT License. The full license can be found [here](https://github.com/RevenantX/LiteNetLib/blob/master/LICENSE.txt).

## Disclaimer

#### THIS PROJECT IS NOT AFFILIATED WITH ANY OTHER MULTIPLAYER MODS OR STRESS LEVEL ZERO! This is its own standalone mod and shares no code with others, any similarities are coincidental!
