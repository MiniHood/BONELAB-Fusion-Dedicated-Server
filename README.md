<div align="center">

# 🎮 BONELAB Fusion Dedicated Server

**A powerful headless server manager for hosting multiple BONELAB Fusion multiplayer instances**

![BONELAB Fusion Banner](https://i.imgur.com/1ZpMfei.png)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-6.0-512BD4.svg)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D6.svg)](https://www.microsoft.com/windows)

[Features](#-features) • [Quick Start](#-quick-start) • [Installation](#-installation-guide) • [Architecture](#-how-it-works) • [Credits](#-credits)

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Prerequisites](#-prerequisites)
- [Quick Start](#-quick-start)
- [Installation Guide](#-installation-guide)
- [How It Works](#-how-it-works)
- [Building from Source](#-building-from-source)
- [Troubleshooting](#-troubleshooting)
- [Credits](#-credits)
- [License](#-license)
- [Disclaimer](#-disclaimer)

---

## 🎯 Overview

This project is a **dedicated server fork** of [BONELAB Fusion](https://github.com/Lakatrazz/BONELAB-Fusion) designed for headless multiplayer server hosting. It enables you to run multiple BONELAB Fusion server instances efficiently using a centralized management system with features like:

- 🖥️ **Headless Operation**: Run servers without graphics rendering
- 📊 **Multi-Instance Management**: Host and manage multiple server instances from one interface
- 💬 **Interactive CLI**: Control and monitor servers through a live command-line interface
- ⚡ **Resource Optimization**: Automatic memory trimming and performance monitoring
- 🔌 **Named Pipe Communication**: Efficient inter-process communication between manager and game instances

---

## ✨ Features

- **🎮 Full Fusion Support**: All BONELAB Fusion multiplayer features
- **🌐 Cross-Platform Play**: Support for Steam PCVR, Meta PCVR, and Meta Quest
- **🎲 Multiple Gamemodes**: Deathmatch, Team Deathmatch, Hide and Seek, and more
- **🏆 Achievements & Cosmetics**: Full progression system support
- **📡 Live Management**: Real-time server monitoring and command execution
- **🔧 Automated Maintenance**: Built-in memory management and health checks

---

## 📦 Prerequisites

Before setting up your dedicated server, ensure you have:

| Requirement | Description | Link |
|------------|-------------|------|
| **Steam** | Steam client for authentication | [Download](https://steamcommunity.com/) |
| **Sandboxie Plus** | Sandbox environment for running isolated instances | [Download](https://sandboxie-plus.com/downloads/) |
| **BONELAB** | Base game (can be a separate copy for server) | [Steam Store](https://store.steampowered.com/app/1592190/BONELAB/) |
| **FlatPlayer** | Required mod for headless operation | [Download](https://thunderstore.io/c/bonelab/p/LlamasHere/FlatPlayer/) |
| **.NET 6.0 SDK** | For building the server manager | [Download](https://dotnet.microsoft.com/download/dotnet/6.0) |

---

## 🚀 Quick Start

```bash
# 1. Clone this repository
git clone https://github.com/MiniHood/BONELAB-Fusion-Dedicated-Server.git
cd BONELAB-Fusion-Dedicated-Server

# 2. Build the server manager
dotnet build LabFusionManager/LabFusionManager.csproj --configuration Release

# 3. Build the Fusion mod
dotnet build LabFusion/LabFusion.csproj --configuration Release

# 4. Follow the detailed installation guide below to set up your server instances
```

> **⚠️ Support Note**: For issues or questions, please use the [dedicated Discord server](https://dc.gg/plus) rather than the main BONELAB Fusion Discord.

---

## 📖 Installation Guide

<details>
<summary><b>Step 1: Set Up BONELAB Server Directory</b></summary>

<br>

Create a dedicated directory for your server instance(s). This should be a **separate copy** of BONELAB to avoid conflicts with your main installation.

1. Create a new folder (e.g., `C:\BonelabServer`)
2. Install or copy BONELAB to this directory
3. Set up MelonLoader and mods as you would for a normal BONELAB installation

<img width="942" height="585" alt="Server Directory Structure" src="https://github.com/user-attachments/assets/6ed2271c-d183-41ac-b4cc-2c1470a51f34" />

</details>

<details>
<summary><b>Step 2: Configure Sandboxie for Steam</b></summary>

<br>

Run Steam in an isolated Sandboxie environment to allow multiple server instances:

1. Open **Sandboxie Plus** and create a new sandbox
2. Configure Steam to run in the sandbox
3. **Recommended**: Add these launch parameters to reduce resource usage:
   ```
   -silent -nobootstrapupdate -nocrashdialog -noverifyfiles
   ```

**Visual Guide:**

<img width="197" height="320" alt="Sandboxie Configuration 1" src="https://github.com/user-attachments/assets/383e13fc-5a0e-4ec4-a0e8-eaf409c5ecb0" />
<img width="598" height="429" alt="Sandboxie Configuration 2" src="https://github.com/user-attachments/assets/e98de366-fe84-4ede-92d3-2ae6a9b9b5e0" />
<img width="602" height="426" alt="Sandboxie Configuration 3" src="https://github.com/user-attachments/assets/89ddee36-8ba9-454c-b908-a83687a0f811" />
<img width="460" height="134" alt="Sandboxie Configuration 4" src="https://github.com/user-attachments/assets/cbc232e2-2bba-4ab1-9ff7-07da4b2576ff" />
<img width="1130" height="728" alt="Sandboxie Configuration 5" src="https://github.com/user-attachments/assets/1e5fd6ab-4c48-42ab-843c-d729b92ffcc3" />

</details>

<details>
<summary><b>Step 3: Build the Dedicated Server</b></summary>

<br>

Build both the Fusion mod and the server manager:

```bash
# Build LabFusion.dll
dotnet build LabFusion/LabFusion.csproj --configuration Release

# Build the server manager
dotnet build LabFusionManager/LabFusionManager.csproj --configuration Release
```

<img width="848" height="81" alt="Build Output" src="https://github.com/user-attachments/assets/3d704407-88db-4a07-9ec8-fc9bb7c9613e" />

</details>

<details>
<summary><b>Step 4: Install Required Mods</b></summary>

<br>

Copy the required DLLs to your BONELAB server's `Mods` folder:

1. **LabFusion.dll** - Built from this repository
2. **FlatPlayer.dll** - Download from [Thunderstore](https://thunderstore.io/c/bonelab/p/LlamasHere/FlatPlayer/)

Place both files in: `<ServerDirectory>/BONELAB/Mods/`

<img width="649" height="241" alt="Mods Folder" src="https://github.com/user-attachments/assets/c313e182-47a9-4e4b-8272-a99784c39c0f" />

</details>

<details>
<summary><b>Step 5: Create Server Launch Script</b></summary>

<br>

Create a batch file named `StartServer.bat` in your BONELAB directory (next to `BONELAB.exe`):

```bat
@echo off
BONELAB_Steam_Windows64.exe -batchmode -nographics
```

This launches BONELAB in headless mode without rendering.

<img width="619" height="419" alt="Batch File Location" src="https://github.com/user-attachments/assets/7515a245-7f36-462d-9e84-313417c0d6af" />
<img width="448" height="132" alt="Batch File Content" src="https://github.com/user-attachments/assets/8258c811-9a6e-4fe8-a65c-c3a76b976697" />

</details>

<details>
<summary><b>Step 6: Launch Your Server</b></summary>

<br>

1. **Start the Server Manager:**
   ```bash
   dotnet run --project LabFusionManager/LabFusionManager.csproj
   ```

2. **Run the batch file** from the same Sandboxie group as Steam:
   - Right-click `StartServer.bat` → Run in Sandbox

<img width="622" height="192" alt="Running Server" src="https://github.com/user-attachments/assets/b6164e81-28e7-45ae-9d96-e9f835b5ed0c" />

3. Your server should now appear in the manager CLI and be accessible to players!

</details>

---

## 🏗️ How It Works

The dedicated server system consists of two main components:

### 🎯 **LabFusion** (Game Mod)
- Modified BONELAB Fusion that runs in headless mode
- Communicates with the manager via named pipes
- Handles multiplayer game logic and player connections

### 🖥️ **LabFusionManager** (Server Manager)
- .NET console application that manages multiple game instances
- Features:
  - **Registration System**: Tracks active server instances
  - **Health Monitoring**: Pings servers to detect disconnections
  - **Resource Management**: Automatic memory trimming
  - **Interactive CLI**: Navigate between servers and send commands
  - **Named Pipe IPC**: Fast communication with game instances

```
┌─────────────────────────┐
│  LabFusionManager.exe   │
│  (Server Manager)       │
└────────┬────────────────┘
         │ Named Pipes
         ├─────────────┬─────────────┬─────────────┐
         │             │             │             │
    ┌────▼───┐    ┌────▼───┐    ┌────▼───┐    ┌────▼───┐
    │Server 1│    │Server 2│    │Server 3│    │Server N│
    │(BONELAB│    │(BONELAB│    │(BONELAB│    │ ...    │
    │Fusion) │    │Fusion) │    │Fusion) │    │        │
    └────────┘    └────────┘    └────────┘    └────────┘
```

---

## 🔨 Building from Source

### Requirements
- Visual Studio 2022 or .NET 6.0 SDK
- BONELAB game files (for references)

### Build Steps

```bash
# Clone the repository
git clone https://github.com/MiniHood/BONELAB-Fusion-Dedicated-Server.git
cd BONELAB-Fusion-Dedicated-Server

# Restore NuGet packages
dotnet restore

# Build everything
dotnet build --configuration Release

# Or build individual projects
dotnet build LabFusion/LabFusion.csproj --configuration Release
dotnet build LabFusionManager/LabFusionManager.csproj --configuration Release
```

---

## 🔧 Troubleshooting

<details>
<summary><b>Server doesn't appear in the manager</b></summary>

- Ensure the server is running in the same Sandboxie group as Steam
- Check that named pipes aren't blocked by antivirus/firewall
- Verify LabFusion.dll is properly loaded by checking MelonLoader logs

</details>

<details>
<summary><b>High memory usage</b></summary>

- The manager automatically trims memory every second
- Ensure headless mode is active (`-batchmode -nographics`)
- Consider reducing server tick rate if needed

</details>

<details>
<summary><b>Players can't connect</b></summary>

- Verify Steam is logged in within the Sandboxie environment
- Check firewall settings allow BONELAB/Steam connections
- Ensure the server has completed initialization before players join

</details>

<details>
<summary><b>Build errors</b></summary>

- Ensure .NET 6.0 SDK is installed
- Verify all game references are correctly set
- Check that MelonLoader dependencies are available

</details>

---

## 🏆 Credits

This project builds upon the incredible work of many talented developers and communities:

### 👨‍💻 Core Development

| Contributor | Role | Link |
|------------|------|------|
| **Lakatrazz** | Original BONELAB Fusion Developer | [GitHub](https://github.com/Lakatrazz/BONELAB-Fusion) |
| **MiniHood** | Dedicated Server Fork Maintainer | [GitHub](https://github.com/MiniHood) |
| **yowchap** | BoneLib AutoUpdater | [GitHub](https://github.com/yowchap/BoneLib) |

### 🎮 Testing & Community
- In-game credits include all testing and development contributors
- Community support via [Discord](https://dc.gg/plus)

### 📚 Dependencies & Libraries

- **[MelonLoader](https://melonwiki.xyz/)** - Mod loading framework
- **[BoneLib](https://github.com/yowchap/BoneLib)** - BONELAB modding library
- **[FlatPlayer](https://thunderstore.io/c/bonelab/p/LlamasHere/FlatPlayer/)** - Headless player mod by LlamasHere
- **[MoonSharp](https://www.moonsharp.org/)** - Lua scripting support

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

### Third-Party Licenses

This project uses code from the following libraries under the MIT License:

| Library | License | Link |
|---------|---------|------|
| **Facepunch.Steamworks** | MIT License | [View License](https://github.com/Facepunch/Facepunch.Steamworks/blob/master/LICENSE) |
| **Steamworks.NET** | MIT License | [View License](https://github.com/rlabrecque/Steamworks.NET/blob/master/LICENSE.txt) |
| **LiteNetLib** | MIT License | [View License](https://github.com/RevenantX/LiteNetLib/blob/master/LICENSE.txt) |

---

## ⚠️ Disclaimer

> **THIS PROJECT IS NOT AFFILIATED WITH STRESS LEVEL ZERO OR OTHER MULTIPLAYER MODS**
> 
> This is an independent fork specifically for dedicated server hosting. It shares no code with other multiplayer implementations. Any similarities are purely coincidental.
> 
> BONELAB™ is a trademark of Stress Level Zero. This project is a community-created mod and is not officially endorsed by or affiliated with Stress Level Zero.

---

<div align="center">

**Made with ❤️ by the BONELAB modding community**

[Report Bug](https://github.com/MiniHood/BONELAB-Fusion-Dedicated-Server/issues) • [Request Feature](https://github.com/MiniHood/BONELAB-Fusion-Dedicated-Server/issues) • [Discord](https://dc.gg/plus)

</div>
