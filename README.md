# BONELAB Fusion Dedicated Server
This is a fork of BONELAB Fusion for headless dedicated server hosting.
A multiplayer mod for BONELAB featuring support for all platforms.

![](https://i.imgur.com/1ZpMfei.png)

## Setup
### If any issues occur, please discuss this in https://discord.gg/jSw8Qrkmwn and not the main bonelab fusion discord :)

You must have the following
- [Steam](https://steamcommunity.com/)
- [Sandboxie Plus](https://sandboxie-plus.com/downloads/)
- [Bonelab](https://store.steampowered.com/app/1592190/BONELAB/)
- [Flatplayer](https://thunderstore.io/c/bonelab/p/LlamasHere/FlatPlayer/)


------------------------------------------------------------------------------------------------------------------------------------------------------


This is a brief guide on how to setup a server.
- Create a new bonelab directory with bonelab inside of it just for this. This will be the directory for hosting your server. Set it up as you would normal bonelab. I didn't like the hassle so I used a pirated copy of bonelab for the server.
<img width="942" height="585" alt="image" src="https://github.com/user-attachments/assets/6ed2271c-d183-41ac-b4cc-2c1470a51f34" />

------------------------------------------------------------------------------------------------------------------------------------------------------


- Run steam in a sandboxie and login. This will be the account hosting your server, it can host multiple servers.
<img width="197" height="320" alt="image" src="https://github.com/user-attachments/assets/383e13fc-5a0e-4ec4-a0e8-eaf409c5ecb0" />
<img width="598" height="429" alt="image" src="https://github.com/user-attachments/assets/e98de366-fe84-4ede-92d3-2ae6a9b9b5e0" />
<img width="602" height="426" alt="image" src="https://github.com/user-attachments/assets/89ddee36-8ba9-454c-b908-a83687a0f811" />
<img width="460" height="134" alt="image" src="https://github.com/user-attachments/assets/cbc232e2-2bba-4ab1-9ff7-07da4b2576ff" />
<img width="1130" height="728" alt="image" src="https://github.com/user-attachments/assets/1e5fd6ab-4c48-42ab-843c-d729b92ffcc3" />


------------------------------------------------------------------------------------------------------------------------------------------------------


- Build the bonelab fusion dedicated server's source code.
<img width="848" height="81" alt="image" src="https://github.com/user-attachments/assets/3d704407-88db-4a07-9ec8-fc9bb7c9613e" />


------------------------------------------------------------------------------------------------------------------------------------------------------


- Place LabFusion.dll & FlatPlayer.dll (you must get this elsewhere) in your new bonelabs mods.
<img width="649" height="241" alt="image" src="https://github.com/user-attachments/assets/c313e182-47a9-4e4b-8272-a99784c39c0f" />


------------------------------------------------------------------------------------------------------------------------------------------------------


- Place the .bat file below into your main directory, beside BONELAB.exe.
<img width="619" height="419" alt="image" src="https://github.com/user-attachments/assets/7515a245-7f36-462d-9e84-313417c0d6af" />
<img width="448" height="132" alt="image" src="https://github.com/user-attachments/assets/8258c811-9a6e-4fe8-a65c-c3a76b976697" />


------------------------------------------------------------------------------------------------------------------------------------------------------


- Run the .bat file from the same sandboxie group you're running steam inside.
<img width="622" height="192" alt="image" src="https://github.com/user-attachments/assets/b6164e81-28e7-45ae-9d96-e9f835b5ed0c" />


------------------------------------------------------------------------------------------------------------------------------------------------------


- Your server should be started

```bat
@echo off
BONELAB_Steam_Windows64.exe -batchmode -nographics
```

## Credits
- BoneLib AutoUpdater: https://github.com/yowchap/BoneLib
- Testing/Development Credits In Game
- Lakatrazz (Developer of Fusion): https://github.com/Lakatrazz/BONELAB-Fusion
- 
## Licensing
- The source code of [Facepunch.Steamworks](https://github.com/Facepunch/Facepunch.Steamworks) is used partially under the MIT License. The full license can be found [here](https://github.com/Facepunch/Facepunch.Steamworks/blob/master/LICENSE).
- The source code of [Steamworks.NET](https://github.com/rlabrecque/Steamworks.NET) is used partially under the MIT License. The full license can be found [here](https://github.com/rlabrecque/Steamworks.NET/blob/master/LICENSE.txt).
- The source code of [LiteNetLib](https://github.com/RevenantX/LiteNetLib) is included under the MIT License. The full license can be found [here](https://github.com/RevenantX/LiteNetLib/blob/master/LICENSE.txt).

## Disclaimer

#### THIS PROJECT IS NOT AFFILIATED WITH ANY OTHER MULTIPLAYER MODS OR STRESS LEVEL ZERO! This is its own standalone mod and shares no code with others, any similarities are coincidental!
