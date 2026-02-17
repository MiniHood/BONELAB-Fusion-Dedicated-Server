using Il2CppSystem.Buffers;
using LabFusion.Player;
using LabFusion.UI.Popups;
using LabFusion.Utilities;
using LabFusion.Representation;
using Il2CppSLZ.Marrow.SceneStreaming;
using Il2CppSLZ.Marrow.Warehouse;

using UnityEngine;

using LabFusion.Network.Serialization;

namespace LabFusion.Network;

public class LevelRequestData : INetSerializable
{
    public string Barcode;
    public string Title;

    public void Serialize(INetSerializer serializer)
    {
        serializer.SerializeValue(ref Barcode);
        serializer.SerializeValue(ref Title);
    }
}

public class LevelRequestMessage : NativeMessageHandler
{
    private const float _requestCooldown = 10f;
    private static float _timeOfRequest = -1000f;

    public override byte Tag => NativeMessageTag.LevelRequest;

    public override ExpectedReceiverType ExpectedReceiver => ExpectedReceiverType.ServerOnly;

    protected override void OnHandleMessage(ReceivedMessage received)
    {
        // Prevent request spamming
        if (TimeUtilities.TimeSinceStartup - _timeOfRequest <= _requestCooldown)
        {
            return;
        }

        var sender = received.Sender;

        if (!sender.HasValue)
        {
            return;
        }

        _timeOfRequest = TimeUtilities.TimeSinceStartup;

        var data = received.ReadData<LevelRequestData>();

        // Get player and their username
        var id = PlayerIDManager.GetPlayerID(sender.Value);

        FusionPermissions.FetchPermissionLevel(id.PlatformID, out var level, out _);
        if (id != null && level == PermissionLevel.OWNER)
        {

            SceneStreamer.Load(new Barcode(data.Barcode));
        }
        else {
            FusionLogger.Error($"Fusion Server User: {id.PlatformID} Level: {level} is trying to request {data.Barcode} ");
        }
        if (id == null) { FusionLogger.Error($"Fusion Server ID was Null User: {id.PlatformID} Level: {level} is trying to request {data.Barcode} "); }
        if (level == PermissionLevel.OWNER) { FusionLogger.Error($"Fusion Server Permission was owner User: {id.PlatformID} Level: {level} is trying to request {data.Barcode} "); }
        /* if (id != null && id.TryGetDisplayName(out var name))
        {
            Notifier.Send(new Notification()
            {
                Title = $"{data.Title} Load Request",
                Message = new NotificationText($"{name} has requested to load {data.Title}.", Color.yellow),

                SaveToMenu = true,
                ShowPopup = true,
                OnAccepted = () =>
                {
                    SceneStreamer.Load(new Barcode(data.Barcode));
                },
            });
        } */
    }
}