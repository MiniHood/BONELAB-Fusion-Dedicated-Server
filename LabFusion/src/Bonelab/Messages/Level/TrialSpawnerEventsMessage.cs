﻿using LabFusion.Bonelab.Patching;
using LabFusion.Data;
using LabFusion.Network;
using LabFusion.Network.Serialization;
using LabFusion.SDK.Modules;

namespace LabFusion.Bonelab.Messages;

public class TrialSpawnerEventsData : INetSerializable
{
    public ComponentHashData HashData;

    public void Serialize(INetSerializer serializer)
    {
        serializer.SerializeValue(ref HashData);
    }
}

[Net.DelayWhileTargetLoading]
public class TrialSpawnerEventsMessage : ModuleMessageHandler
{
    public override ExpectedReceiverType ExpectedReceiver => ExpectedReceiverType.ClientsOnly;

    protected override void OnHandleMessage(ReceivedMessage received)
    {
        var data = received.ReadData<TrialSpawnerEventsData>();

        var trialSpawnerEvents = Trial_SpawnerEventsPatches.HashTable.GetComponentFromData(data.HashData);

        if (trialSpawnerEvents == null)
        {
            return;
        }

        Trial_SpawnerEventsPatches.IgnorePatches = true;

        trialSpawnerEvents.OnSpawnerDeath();
    }
}
