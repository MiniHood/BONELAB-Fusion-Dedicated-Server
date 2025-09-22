using LabFusion.Marrow;
using LabFusion.Marrow.Pool;

using UnityEngine;

namespace LabFusion.SDK.Points;

public static class PointShopHelper
{
    public static void SpawnBitMart(Vector3 position, Quaternion rotation)
    {
        var spawnable = LocalAssetSpawner.CreateSpawnable(FusionSpawnableReferences.BitMartReference);

        LocalAssetSpawner.Register(spawnable);

        LocalAssetSpawner.Spawn(spawnable, position, rotation);
    }
}