﻿using LabFusion.Marrow;
using LabFusion.Network;
using LabFusion.Player;
using LabFusion.Preferences.Server;
using LabFusion.Representation;
using LabFusion.Scene;
using LabFusion.SDK.Gamemodes;
using LabFusion.Senders;

using System.Text.Json.Serialization;

namespace LabFusion.Data;

[Serializable]
public class LobbyInfo
{
    public static readonly LobbyInfo Empty = new();

    // Info
    [JsonPropertyName("lobbyId")]
    public ulong LobbyId { get; set; }

    [JsonPropertyName("lobbyCode")]
    public string LobbyCode { get; set; }

    [JsonPropertyName("lobbyName")]
    public string LobbyName { get; set; }

    [JsonPropertyName("lobbyDescription")]
    public string LobbyDescription { get; set; }

    [JsonPropertyName("lobbyVersion")]
    public Version LobbyVersion { get; set; }

    [JsonPropertyName("lobbyHostName")]
    public string LobbyHostName { get; set; }

    [JsonPropertyName("playerCount")]
    public int PlayerCount { get; set; }

    [JsonPropertyName("playerList")]
    public PlayerList PlayerList { get; set; }

    // Location
    [JsonPropertyName("levelTitle")]
    public string LevelTitle { get; set; }

    [JsonPropertyName("levelBarcode")]
    public string LevelBarcode { get; set; }

    [JsonPropertyName("levelModId")]
    public int LevelModId { get; set; } = -1;

    // Gamemode
    [JsonPropertyName("gamemodeTitle")]
    public string GamemodeTitle { get; set; }

    [JsonPropertyName("gamemodeBarcode")]
    public string GamemodeBarcode { get; set; }

    [JsonPropertyName("timeBetweenGamemodeRounds")]
    public int TimeBetweenGamemodeRounds { get; set; } = 30;

    // Settings
    [JsonPropertyName("nameTags")]
    public bool NameTags { get; set; }

    [JsonPropertyName("privacy")]
    public ServerPrivacy Privacy { get; set; }

    [JsonPropertyName("slowMoMode")]
    public TimeScaleMode SlowMoMode { get; set; }

    [JsonPropertyName("maxPlayers")]
    public int MaxPlayers { get; set; }

    [JsonPropertyName("voiceChat")]
    public bool VoiceChat { get; set; }

    [JsonPropertyName("playerConstraining")]
    public bool PlayerConstraining { get; set; }

    [JsonPropertyName("mortality")]
    public bool Mortality { get; set; }

    [JsonPropertyName("friendlyFire")]
    public bool FriendlyFire { get; set; }

    [JsonPropertyName("knockout")]
    public bool Knockout { get; set; }

    [JsonPropertyName("knockoutLength")]
    public int KnockoutLength { get; set; }

    [JsonPropertyName("maxAvatarHeight")]
    public float MaxAvatarHeight { get; set; }

    // Permissions
    [JsonPropertyName("devTools")]
    public PermissionLevel DevTools { get; set; }

    [JsonPropertyName("constrainer")]
    public PermissionLevel Constrainer { get; set; }

    [JsonPropertyName("customAvatars")]
    public PermissionLevel CustomAvatars { get; set; }

    [JsonPropertyName("kicking")]
    public PermissionLevel Kicking { get; set; }

    [JsonPropertyName("banning")]
    public PermissionLevel Banning { get; set; }

    [JsonPropertyName("teleportation")]
    public PermissionLevel Teleportation { get; set; }

    public static DateTime ServerStartTime = DateTime.UtcNow;

    public void WriteLobby()
    {
        var uptime = DateTime.UtcNow - ServerStartTime;

        // Info
        LobbyId = PlayerIDManager.LocalPlatformID;
        LobbyCode = NetworkHelper.GetServerCode();
        LobbyName = $"[{uptime:hh}h:{uptime:mm}m:{uptime:ss}s] " + "Dedicated Server Test";
        LobbyDescription = "This is a test using a modified version of FusionLab. Join the dedicated server development discord: https://discord.gg/MV3PDGaqfx\nThis server cleans every 5 minutes.\n- HowNiceOfYou";
        LobbyVersion = FusionMod.Version;
        LobbyHostName = FusionMod.ServerName;

        PlayerCount = PlayerIDManager.PlayerCount;

        var playerList = new PlayerList();
        playerList.WritePlayers();
        PlayerList = playerList;

        // Location
        LevelTitle = FusionSceneManager.Title;
        LevelBarcode = FusionSceneManager.Barcode;

        LevelModId = CrateFilterer.GetModID(FusionSceneManager.Level.Pallet);

        // Gamemode
        GamemodeTitle = string.Empty;
        GamemodeBarcode = string.Empty;

        if (GamemodeManager.ActiveGamemode != null)
        {
            GamemodeTitle = GamemodeManager.ActiveGamemode.Title;
            GamemodeBarcode = GamemodeManager.ActiveGamemode.Barcode;
        }

        TimeBetweenGamemodeRounds = GamemodeRoundManager.Settings.TimeBetweenRounds;

        // Settings
        NameTags = true; // SavedServerSettings.NameTags.Value;
        Privacy = SavedServerSettings.Privacy.Value;
        SlowMoMode = SavedServerSettings.SlowMoMode.Value;
        MaxPlayers = SavedServerSettings.MaxPlayers.Value;
        VoiceChat = true; // SavedServerSettings.VoiceChat.Value;
        PlayerConstraining = SavedServerSettings.PlayerConstraining.Value;
        Mortality = SavedServerSettings.Mortality.Value;
        FriendlyFire = true; // SavedServerSettings.FriendlyFire.Value;
        Knockout = false; // SavedServerSettings.Knockout.Value;
        KnockoutLength = SavedServerSettings.KnockoutLength.Value;
        MaxAvatarHeight = SavedServerSettings.MaxAvatarHeight.Value;

        // Permissions
        DevTools = SavedServerSettings.DevTools.Value;
        Constrainer = PermissionLevel.OWNER; // SavedServerSettings.Constrainer.Value;
        CustomAvatars = PermissionLevel.DEFAULT; // SavedServerSettings.CustomAvatars.Value;
        Kicking = PermissionLevel.OWNER; // SavedServerSettings.Kicking.Value;
        Banning = PermissionLevel.OWNER; // SavedServerSettings.Banning.Value;
        Teleportation = PermissionLevel.OWNER; // SavedServerSettings.Teleportation.Value;
    }
}