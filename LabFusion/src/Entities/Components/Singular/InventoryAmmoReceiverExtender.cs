using Il2CppSLZ.Marrow;
using LabFusion.Utilities;

namespace LabFusion.Entities;

public class InventoryAmmoReceiverExtender : EntityComponentExtender<InventoryAmmoReceiver>
{
    public static readonly FusionComponentCache<InventoryAmmoReceiver, NetworkEntity> Cache = new();

    protected override void OnRegister(NetworkEntity entity, InventoryAmmoReceiver component)
    {
        Cache.Add(component, entity);
    }

    protected override void OnUnregister(NetworkEntity entity, InventoryAmmoReceiver component)
    {
        Cache.Remove(component);
    }
}