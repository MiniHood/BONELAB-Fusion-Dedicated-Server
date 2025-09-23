using LabFusion.Network;
using LabFusion.Preferences.Server;
using LabFusion.Representation;
using LabFusion.Senders;

public class ChangeSettingCommand : IFusionCommand
{
    public string Name => "changesetting";

    private readonly Dictionary<string, Func<string, string>> _settingHandlers;

    public ChangeSettingCommand()
    {
        _settingHandlers = new Dictionary<string, Func<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            // General
            ["maxplayers"] = value =>
            {
                if (int.TryParse(value, out int max))
                {
                    SavedServerSettings.MaxPlayers.Value = max;
                    return $"MaxPlayers set to {max}";
                }
                return "Invalid value for MaxPlayers";
            },
            ["privacy"] = value =>
            {
                if (Enum.TryParse<ServerPrivacy>(value, true, out var privacy))
                {
                    SavedServerSettings.Privacy.Value = privacy;
                    return $"Privacy set to {privacy}";
                }
                return "Invalid value for Privacy";
            },
            ["nametags"] = value =>
            {
                if (bool.TryParse(value, out var tags))
                {
                    SavedServerSettings.NameTags.Value = tags;
                    return $"NameTags set to {tags}";
                }
                return "Invalid value for NameTags";
            },
            ["voicechat"] = value =>
            {
                if (bool.TryParse(value, out var vc))
                {
                    SavedServerSettings.VoiceChat.Value = vc;
                    return $"VoiceChat set to {vc}";
                }
                return "Invalid value for VoiceChat";
            },
            ["playerconstraining"] = value =>
            {
                if (bool.TryParse(value, out var pc))
                {
                    SavedServerSettings.PlayerConstraining.Value = pc;
                    return $"PlayerConstraining set to {pc}";
                }
                return "Invalid value for PlayerConstraining";
            },
            ["slowmomode"] = value =>
            {
                if (Enum.TryParse<TimeScaleMode>(value, true, out var mode))
                {
                    SavedServerSettings.SlowMoMode.Value = mode;
                    return $"SlowMoMode set to {mode}";
                }
                return "Invalid value for SlowMoMode";
            },

            // Visual
            ["servername"] = value =>
            {
                SavedServerSettings.ServerName.Value = value;
                return $"ServerName set to {value}";
            },
            ["serverdescription"] = value =>
            {
                SavedServerSettings.ServerDescription.Value = value;
                return $"ServerDescription set to {value}";
            },

            // Combat
            ["mortality"] = value =>
            {
                if (bool.TryParse(value, out var m))
                {
                    SavedServerSettings.Mortality.Value = m;
                    return $"Mortality set to {m}";
                }
                return "Invalid value for Mortality";
            },
            ["friendlyfire"] = value =>
            {
                if (bool.TryParse(value, out var ff))
                {
                    SavedServerSettings.FriendlyFire.Value = ff;
                    return $"FriendlyFire set to {ff}";
                }
                return "Invalid value for FriendlyFire";
            },
            ["knockout"] = value =>
            {
                if (bool.TryParse(value, out var ko))
                {
                    SavedServerSettings.Knockout.Value = ko;
                    return $"Knockout set to {ko}";
                }
                return "Invalid value for Knockout";
            },
            ["knockoutlength"] = value =>
            {
                if (int.TryParse(value, out var kl))
                {
                    SavedServerSettings.KnockoutLength.Value = kl;
                    return $"KnockoutLength set to {kl}";
                }
                return "Invalid value for KnockoutLength";
            },
            ["maxavatarheight"] = value =>
            {
                if (float.TryParse(value, out var height))
                {
                    SavedServerSettings.MaxAvatarHeight.Value = height;
                    return $"MaxAvatarHeight set to {height}";
                }
                return "Invalid value for MaxAvatarHeight";
            },

            // Permissions
            ["devtools"] = value =>
            {
                if (Enum.TryParse<PermissionLevel>(value, true, out var p))
                {
                    SavedServerSettings.DevTools.Value = p;
                    return $"DevTools permission set to {p}";
                }
                return "Invalid value for DevTools";
            },
            ["constrainer"] = value =>
            {
                if (Enum.TryParse<PermissionLevel>(value, true, out var p))
                {
                    SavedServerSettings.Constrainer.Value = p;
                    return $"Constrainer permission set to {p}";
                }
                return "Invalid value for Constrainer";
            },
            ["customavatars"] = value =>
            {
                if (Enum.TryParse<PermissionLevel>(value, true, out var p))
                {
                    SavedServerSettings.CustomAvatars.Value = p;
                    return $"CustomAvatars permission set to {p}";
                }
                return "Invalid value for CustomAvatars";
            },
            ["kicking"] = value =>
            {
                if (Enum.TryParse<PermissionLevel>(value, true, out var p))
                {
                    SavedServerSettings.Kicking.Value = p;
                    return $"Kicking permission set to {p}";
                }
                return "Invalid value for Kicking";
            },
            ["banning"] = value =>
            {
                if (Enum.TryParse<PermissionLevel>(value, true, out var p))
                {
                    SavedServerSettings.Banning.Value = p;
                    return $"Banning permission set to {p}";
                }
                return "Invalid value for Banning";
            },
            ["teleportation"] = value =>
            {
                if (Enum.TryParse<PermissionLevel>(value, true, out var p))
                {
                    SavedServerSettings.Teleportation.Value = p;
                    return $"Teleportation permission set to {p}";
                }
                return "Invalid value for Teleportation";
            }
        };
    }


    public async Task<object> ExecuteAsync(List<string> args)
    {
        if (args.Count < 2)
            return "Usage: changeSetting <settingName> <value>";

        string key = args[0];
        string value = args[1];

        if (_settingHandlers.TryGetValue(key, out var handler))
            return await Task.FromResult(handler(value));

        return $"Unknown setting: {key}";
    }
}
