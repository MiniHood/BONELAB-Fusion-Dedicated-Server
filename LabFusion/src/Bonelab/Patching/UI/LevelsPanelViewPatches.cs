using HarmonyLib;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow.Warehouse;
using LabFusion.Network;
using LabFusion.Senders;
using LabFusion.UI.Popups;

namespace LabFusion.Bonelab.Patching;

[HarmonyPatch(typeof(LevelsPanelView))]
public class LevelsPanelViewPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(LevelsPanelView.SelectItem))]
    public static bool SelectItemPrefix(LevelsPanelView __instance, int idx)
    {
        try
        {
            // Prevent the menu from loading a different level if we aren't the host
            if (NetworkInfo.HasServer && !NetworkInfo.IsHost)
            {
                // Send level request
                LevelCrate crate = __instance._levelCrates[idx + (__instance._currentPage * __instance.items.Count)];
                LoadSender.SendLevelRequest(crate);

                // Notify the user they've requested a level
                Notifier.Send(new Notification()
                {
                    Title = "Requested Level",
                    Message = $"Sent a level request for {crate.Title}!",
                    SaveToMenu = false,
                    ShowPopup = true,
                });

                return false;
            }
        }
        catch (Exception e)
        {
#if DEBUG
            FusionLogger.LogException("executing patch LevelsPanelView.SelectItem", e);
#endif
        }

        return true;
    }
}
