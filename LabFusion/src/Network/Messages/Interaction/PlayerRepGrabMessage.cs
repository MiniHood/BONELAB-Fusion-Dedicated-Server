using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Interaction;
using LabFusion.Data;
using LabFusion.Entities;
using LabFusion.Grabbables;
using LabFusion.Network.Serialization;
using LabFusion.Player;

namespace LabFusion.Network;

public class PlayerRepGrabData : INetSerializable
{
    public const int Size = sizeof(byte) * 3;

    public byte SmallID;
    public Handedness Handedness;
    public GrabGroup Group;
    public SerializedGrab SerializedGrab;

    public void Serialize(INetSerializer serializer)
    {
        serializer.SerializeValue(ref SmallID);
        serializer.SerializeValue(ref Handedness, Precision.OneByte);
        serializer.SerializeValue(ref Group, Precision.OneByte);

        GrabGroupHandler.SerializeGrab(ref SerializedGrab, serializer, Group);
    }

    public Grip GetGrip()
    {
        return SerializedGrab.GetGrip();
    }

    public NetworkPlayer GetPlayer()
    {
        if (NetworkPlayerManager.TryGetPlayer(SmallID, out var player))
        {
            return player;
        }

        return null;
    }

    public static PlayerRepGrabData Create(byte smallId, Handedness handedness, GrabGroup group, SerializedGrab serializedGrab)
    {
        return new PlayerRepGrabData()
        {
            SmallID = smallId,
            Handedness = handedness,
            Group = group,
            SerializedGrab = serializedGrab
        };
    }
}

[Net.SkipHandleWhileLoading]
public class PlayerRepGrabMessage : NativeMessageHandler
{
    public override byte Tag => NativeMessageTag.PlayerRepGrab;

    protected override void OnHandleMessage(ReceivedMessage received)
    {
        var data = received.ReadData<PlayerRepGrabData>();

        // Make sure this isn't us
        if (data.SmallID == PlayerIDManager.LocalSmallID)
        {
            return;
        }

        // Apply grab
        ApplyGrip(data);
    }

    private static void ApplyGrip(PlayerRepGrabData data)
    {
        var player = data.GetPlayer();

        if (player == null)
        {
#if DEBUG
            FusionLogger.Warn("Grab message requested a player to grab that doesn't exist?");
#endif
            return;
        }

        var grip = data.GetGrip();

        if (grip == null)
        {
            return;
        }

        data.SerializedGrab.RequestGrab(player, data.Handedness, grip);
    }
}