using HarmonyLib;
using Il2CppSLZ.Marrow;
using LabFusion.Data;
using LabFusion.Extensions;
using LabFusion.Network;
using LabFusion.Player;
using LabFusion.Preferences;
using LabFusion.SDK.Gamemodes;
using LabFusion.Senders;
using LabFusion.Utilities;

namespace LabFusion.Patching;

[HarmonyPatch(typeof(HeadSFX))]
public static class HeadSFXPatches
{
    [HarmonyPatch(nameof(HeadSFX.RecoveryVocal))]
    [HarmonyPrefix]
    public static void RecoveryVocal(HeadSFX __instance)
    {
        // Is this our player?
        var rm = __instance._physRig.manager;

        if (NetworkInfo.HasServer && rm.IsLocalPlayer())
        {
            // Notify the server about the recovery
            PlayerSender.SendPlayerAction(PlayerActionType.RECOVERY);
        }
    }
}