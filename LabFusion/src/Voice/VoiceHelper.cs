using LabFusion.Player;

namespace LabFusion.Voice;

public static class VoiceHelper
{
    public static void OnVoiceChatUpdate()
    {
        VoiceInfo.VoiceManager?.UpdateManager();
    }

    public static void OnVoiceDataReceived(PlayerID player, byte[] data)
    {
        var speaker = VoiceInfo.VoiceManager?.GetSpeaker(player);
        speaker?.OnVoiceDataReceived(data);
    }
}