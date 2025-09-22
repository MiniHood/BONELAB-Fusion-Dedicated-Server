﻿using HarmonyLib;

using Il2CppSLZ.Marrow.Zones;
using LabFusion.Extensions;
using LabFusion.Marrow;
using LabFusion.Utilities;

namespace LabFusion.Patching;

[HarmonyPatch(typeof(ZoneCuller))]
public static class ZoneCullerPatches
{
    public static readonly Dictionary<int, ZoneCuller> CullerIdToZone = new();
    public static readonly Dictionary<int, ZoneCuller> HashToZone = new();
    public static readonly Dictionary<ZoneCuller, int> ZoneToHash = new(new UnityComparer());

    [HarmonyPrefix]
    [HarmonyPatch(nameof(ZoneCuller.OnEnable))]
    public static void OnEnable(ZoneCuller __instance)
    {
        // Make sure the zone has its id
        if (!__instance._hasZoneId)
        {
            return;
        }

        var hash = GameObjectHasher.GetFastHash(__instance.gameObject);

        if (HashToZone.TryGetValue(hash, out var conflict))
        {
            FusionLogger.Warn($"Zones {__instance.name} and {conflict.name} have a conflicting hash of {hash}!");
            return;
        }

        CullerIdToZone.Add(__instance._zoneId, __instance);
        HashToZone.Add(hash, __instance);
        ZoneToHash.Add(__instance, hash);
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(ZoneCuller.OnDisable))]
    public static void OnDisable(ZoneCuller __instance)
    {
        CullerIdToZone.Remove(__instance._zoneId);

        if (ZoneToHash.TryGetValue(__instance, out var hash))
        {
            ZoneToHash.Remove(__instance);
            HashToZone.Remove(hash);
        }
    }
}