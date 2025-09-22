﻿using LabFusion;
using MelonLoader;
using System.Reflection;

[assembly: AssemblyTitle(FusionMod.ModName)]
[assembly: AssemblyVersion(FusionVersion.VersionString)]
[assembly: AssemblyFileVersion(FusionVersion.VersionString)]

[assembly: MelonInfo(typeof(FusionMod), FusionMod.ModName, FusionVersion.VersionString, FusionMod.ModAuthor)]
[assembly: MelonGame(FusionMod.GameDeveloper, FusionMod.GameName)]
[assembly: MelonPriority(-10000)]
[assembly: MelonOptionalDependencies("Il2CppFacepunch.Steamworks.Win64")]