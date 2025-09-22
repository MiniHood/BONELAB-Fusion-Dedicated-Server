using LabFusion.Entities;
using LabFusion.Marrow.Integration;
using LabFusion.Utilities;

namespace LabFusion.SDK.Extenders;

public class RPCEventExtender : EntityComponentArrayExtender<RPCEvent>
{
    public static readonly FusionComponentCache<RPCEvent, NetworkEntity> Cache = new();

    protected override void OnRegister(NetworkEntity entity, RPCEvent[] components)
    {
        foreach (var component in components)
        {
            Cache.Add(component, entity);
        }
    }

    protected override void OnUnregister(NetworkEntity entity, RPCEvent[] components)
    {
        foreach (var component in components)
        {
            Cache.Remove(component);
        }
    }
}