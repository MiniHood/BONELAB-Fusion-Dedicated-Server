using LabFusion.Marrow.Combat;
using LabFusion.Marrow.Messages;
using LabFusion.Marrow.Patching;
using LabFusion.Marrow.Scene;
using LabFusion.SDK.Modules;
using LabFusion.Utilities;

namespace LabFusion.Marrow;

public class MarrowModule : Module
{
    public override string Name => "Marrow";
    public override string Author => FusionMod.ModAuthor;
    public override Version Version => FusionMod.Version;

    public override ConsoleColor Color => ConsoleColor.White;

    protected override void OnModuleRegistered()
    {
        ModuleMessageManager.RegisterHandler<ButtonChargeMessage>();
        ModuleMessageManager.RegisterHandler<EventActuatorMessage>();

        ModuleMessageManager.RegisterHandler<GunShotMessage>();
        ModuleMessageManager.RegisterHandler<PuppetMasterKillMessage>();

        ModuleMessageManager.RegisterHandler<InventoryAmmoReceiverDropMessage>();
        ModuleMessageManager.RegisterHandler<InventorySlotDropMessage>();
        ModuleMessageManager.RegisterHandler<InventorySlotInsertMessage>();

        ModuleMessageManager.RegisterHandler<ConstrainerModeMessage>();
        ModuleMessageManager.RegisterHandler<ConstraintCreateMessage>();
        ModuleMessageManager.RegisterHandler<ConstraintDeleteMessage>();
        ModuleMessageManager.RegisterHandler<MagazineClaimMessage>();
        ModuleMessageManager.RegisterHandler<MagazineEjectMessage>();
        ModuleMessageManager.RegisterHandler<MagazineInsertMessage>();
        ModuleMessageManager.RegisterHandler<NimbusGunNoClipMessage>();
        ModuleMessageManager.RegisterHandler<ObjectDestructibleDestroyMessage>();

        ModuleMessageManager.RegisterHandler<CrateSpawnerMessage>();

        MultiplayerHooking.OnMainSceneInitialized += NetworkGunManager.OnMainSceneInitialized;

        LevelEventHandler.OnInitializeMelon();

        if (PlatformHelper.IsAndroid)
        {
            CrateSpawnerAndroidPatches.PatchAll();
        }
        else
        {
            CrateSpawnerPatches.PatchAll();
        }
    }

    protected override void OnModuleUnregistered()
    {

    }
}