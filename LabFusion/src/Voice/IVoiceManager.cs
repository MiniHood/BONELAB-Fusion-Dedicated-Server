﻿using LabFusion.Network;
using LabFusion.Player;
using LabFusion.Senders;

namespace LabFusion.Voice;

public interface IVoiceManager
{
    List<IVoiceSpeaker> VoiceSpeakers { get; }

    bool CanTalk { get; }
    bool CanHear { get; }

    string[] InputDevices { get; }

    IVoiceSpeaker GetSpeaker(PlayerID id);
    void RemoveSpeaker(PlayerID id);

    IVoiceReceiver GetReceiver();

    void Enable();
    void Disable();

    void UpdateManager();
    void ClearManager();
}

public abstract class VoiceManager : IVoiceManager
{
    protected List<IVoiceSpeaker> _voiceSpeakers = new();
    public List<IVoiceSpeaker> VoiceSpeakers => _voiceSpeakers;

    public virtual bool CanTalk => true;
    public virtual bool CanHear => true;

    public virtual string[] InputDevices => Array.Empty<string>();

    private IVoiceReceiver _receiver = null;

    public void Enable()
    {
        _receiver = OnCreateReceiverOrDefault();

        _receiver?.Enable();
    }

    public void Disable()
    {
        if (_receiver != null)
        {
            _receiver.Disable();

            _receiver = null;
        }

        ClearManager();
    }

    protected bool TryGetSpeaker(PlayerID id, out IVoiceSpeaker speaker)
    {
        speaker = null;

        for (var i = 0; i < VoiceSpeakers.Count; i++)
        {
            var result = VoiceSpeakers[i];

            if (result.ID == id)
            {
                speaker = result;
                return true;
            }
        }

        return false;
    }

    protected abstract IVoiceSpeaker OnCreateSpeaker(PlayerID id);

    protected abstract IVoiceReceiver OnCreateReceiverOrDefault();

    public IVoiceSpeaker GetSpeaker(PlayerID id)
    {
        if (TryGetSpeaker(id, out var handler))
            return handler;

        var newSpeaker = OnCreateSpeaker(id);
        VoiceSpeakers.Add(newSpeaker);

        return newSpeaker;
    }

    public IVoiceReceiver GetReceiver()
    {
        return _receiver;
    }

    public void UpdateManager()
    {
        UpdateSpeakers();
        UpdateReceiver();
    }

    private void UpdateSpeakers()
    {
        for (var i = 0; i < VoiceSpeakers.Count; i++)
        {
            VoiceSpeakers[i].Update();
        }
    }

    private bool _hasDisabledVoice = true;

    private void UpdateReceiver()
    {
        if (_receiver == null || !CanTalk)
        {
            return;
        }

        _receiver.UpdateVoice(true);
        _hasDisabledVoice = false;

        if (_receiver.HasVoiceActivity())
        {
            PlayerSender.SendPlayerVoiceChat(_receiver.GetEncodedData());
        }
    }

    public void RemoveSpeaker(PlayerID id)
    {
        IVoiceSpeaker playerHandler = null;

        foreach (var handler in VoiceSpeakers)
        {
            if (handler.ID == id)
            {
                playerHandler = handler;
                break;
            }
        }

        if (playerHandler != null)
        {
            playerHandler.Cleanup();
            _voiceSpeakers.Remove(playerHandler);
        }
    }

    public void ClearManager()
    {
        foreach (var handler in VoiceSpeakers)
        {
            handler.Cleanup();
        }

        _voiceSpeakers.Clear();
    }
}