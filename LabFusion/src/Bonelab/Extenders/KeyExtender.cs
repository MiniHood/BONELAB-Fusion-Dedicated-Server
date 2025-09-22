using Il2CppSLZ.Interaction;
using LabFusion.Entities;
using LabFusion.Utilities;

namespace LabFusion.Bonelab.Extenders;

public class KeyExtender : EntityComponentExtender<Key>
{
    public static readonly FusionComponentCache<Key, NetworkEntity> Cache = new();

    protected override void OnRegister(NetworkEntity entity, Key component)
    {
        Cache.Add(component, entity);
    }

    protected override void OnUnregister(NetworkEntity entity, Key component)
    {
        Cache.Remove(component);
    }
}