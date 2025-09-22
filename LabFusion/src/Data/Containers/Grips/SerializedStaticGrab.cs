using Il2CppSLZ.Marrow;
using LabFusion.Grabbables;
using LabFusion.Network.Serialization;
using LabFusion.Patching;

namespace LabFusion.Data;

public class StaticGrabGroupHandler : GrabGroupHandler<SerializedStaticGrab>
{
    public override GrabGroup? Group => GrabGroup.STATIC;
}

public class SerializedStaticGrab : SerializedGrab
{
    public new const int Size = SerializedGrab.Size + ComponentHashData.Size;

    public ComponentHashData gripHash = null;

    public SerializedStaticGrab() { }

    public SerializedStaticGrab(ComponentHashData gripHash)
    {
        this.gripHash = gripHash;
    }

    public override int GetSize()
    {
        return Size;
    }

    public override void Serialize(INetSerializer serializer)
    {
        base.Serialize(serializer);

        serializer.SerializeValue(ref gripHash);
    }

    public override Grip GetGrip()
    {
        var grip = GripPatches.HashTable.GetComponentFromData(gripHash);

        return grip;
    }
}