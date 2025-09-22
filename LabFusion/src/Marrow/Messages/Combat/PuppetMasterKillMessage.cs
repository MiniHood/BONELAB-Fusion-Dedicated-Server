using LabFusion.Entities;
using LabFusion.Marrow.Extenders;
using LabFusion.Marrow.Patching;
using LabFusion.Network;
using LabFusion.SDK.Modules;
using LabFusion.Utilities;

namespace LabFusion.Marrow.Messages;

[Net.SkipHandleWhileLoading]
public class PuppetMasterKillMessage : ModuleMessageHandler
{
    protected override void OnHandleMessage(ReceivedMessage received)
    {
        var data = received.ReadData<NetworkEntityReference>();

        if (!data.TryGetEntity(out var entity))
        {
            return;
        }

        var extender = entity.GetExtender<PuppetMasterExtender>();

        if (extender == null)
        {
            return;
        }

        // Save the most recent killed NPC
        PuppetMasterExtender.LastKilled = entity;

        // Kill the puppet
        PuppetMasterPatches.IgnorePatches = true;

        try
        {
            extender.Component.Kill();
        }
        catch (Exception e)
        {
            FusionLogger.LogException("executing PuppetMaster.Kill", e);
        }

        PuppetMasterPatches.IgnorePatches = false;
    }
}