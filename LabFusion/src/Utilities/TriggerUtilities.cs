using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.AI;
using LabFusion.Data;
using LabFusion.Network;

namespace LabFusion.Utilities;

public static class TriggerUtilities
{
    public static bool IsMatchingRig(TriggerRefProxy proxy, RigManager rig)
    {
        if (!NetworkInfo.HasServer || !RigData.HasPlayer)
        {
            return true;
        }

        RigManager found;

        if (proxy.root && (found = RigManager.Cache.Get(proxy.root)))
        {
            return found == rig;
        }

        return false;
    }
}