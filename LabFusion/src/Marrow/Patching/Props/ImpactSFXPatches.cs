using HarmonyLib;
using Il2CppSLZ.Marrow;
using LabFusion.Network;
using UnityEngine;

namespace LabFusion.Marrow.Patching;

[HarmonyPatch(typeof(ImpactSFX))]
public static class ImpactSFXPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(ImpactSFX.ImpactSound))]
    public static bool ImpactSound(ImpactSFX __instance, Collision c)
    {
        if (!NetworkInfo.HasServer)
        {
            return true;
        }

        if (__instance._host == null)
        {
            return true;
        }

        var properties = ImpactProperties.Cache.Get(c.gameObject);

        if (properties)
        {
            return ImpactAttackValidator.ValidateImpact(__instance.gameObject, __instance._host, properties);
        }

        return true;
    }
}
