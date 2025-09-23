using Il2CppSLZ.Marrow.SceneStreaming;
using Il2CppSLZ.Marrow.Warehouse;
using LabFusion.Data;
using LabFusion.Downloading;
using LabFusion.Downloading.ModIO;
using LabFusion.Marrow;
using LabFusion.Marrow.Patching;
using LabFusion.Preferences.Client;
using LabFusion.RPC;
using LabFusion.Utilities;

namespace LabFusion.Scene;

public static class LevelDownloaderManager
{
    public struct LevelDownloadInfo
    {
        public string LevelBarcode;
        public byte LevelHost;

        public Action OnDownloadSucceeded, OnDownloadFailed, OnDownloadCanceled;
    }

    private static LevelDownloadInfo _downloadingInfo;

    public static void OnInitializeMelon()
    {
        MultiplayerHooking.OnDisconnected += OnDisconnect;
    }

    private static void OnDisconnect()
    {
        // Incase the player gets stuck in purgatory, disable it on disconnect
        NetworkSceneManager.Purgatory = false;
    }

    public static void DownloadLevel(LevelDownloadInfo info)
    {
        _downloadingInfo = info;

        // Get the maximum amount of bytes that we download before cancelling, to make sure the level isn't too big
        long maxBytes = DataConversions.ConvertMegabytesToBytes(ClientSettings.Downloading.MaxLevelSize.Value);

        // Request the mod id from the host
        NetworkModRequester.RequestAndInstallMod(new NetworkModRequester.ModInstallInfo()
        {
            Target = info.LevelHost,
            Barcode = info.LevelBarcode,
            BeginDownloadCallback = OnDownloadBegin,
            FinishDownloadCallback = OnDownloadFinished,
            MaxBytes = maxBytes,
            HighPriority = true,
        });
    }

    private static void OnDownloadBegin(NetworkModRequester.ModCallbackInfo info)
    {
        NetworkSceneManager.Purgatory = true;

        LoadWaitingScene();
    }

    private static void OnDownloadFinished(DownloadCallbackInfo info)
    {
        NetworkSceneManager.Purgatory = false;

        if (info.result == ModResult.CANCELED)
        {
            _downloadingInfo.OnDownloadCanceled?.Invoke();
            return;
        }

        if (info.result == ModResult.FAILED)
        {
            _downloadingInfo.OnDownloadFailed?.Invoke();
            return;
        }

        _downloadingInfo.OnDownloadSucceeded?.Invoke();
    }

    private static void LoadWaitingScene()
    {
        SceneStreamerPatches.IgnorePatches = true;

        SceneStreamer.Load(new Barcode(FusionLevelReferences.LoadDownloadingReference.Barcode));

        SceneStreamerPatches.IgnorePatches = false;
    }
}
