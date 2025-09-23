using Il2CppInterop.Runtime.InteropTypes.Arrays;
using LabFusion.Audio;
using LabFusion.Player;
using LabFusion.Preferences.Client;
using LabFusion.Utilities;
using UnityEngine;

namespace LabFusion.Voice.Unity;

using System;

public sealed class UnityVoiceReceiver : IVoiceReceiver
{
    private static readonly float[] SampleBuffer = new float[AudioInfo.OutputSampleRate];

    private byte[] _encodedData = null;

    private bool _hasVoiceActivity = false;

    private AudioClip _voiceClip = null;

    private int _lastSample = 0;

    private float _amplitude = 0f;

    private float _lastTalkTime = 0f;

    private bool _loopedData = false;

    public float GetVoiceAmplitude()
    {
        return _amplitude;
    }

    public byte[] GetEncodedData()
    {
        return _encodedData;
    }

    public bool HasVoiceActivity()
    {
        return _hasVoiceActivity;
    }

    private void ClearData()
    {
        _encodedData = null;
        _hasVoiceActivity = false;
        _amplitude = 0f;
    }

    public void UpdateVoice(bool enabled)
    {
        ClearData();
        return;
    }

    public void Enable()
    {
    }

    public void Disable()
    {
        _encodedData = null;
        _hasVoiceActivity = false;
    }
}