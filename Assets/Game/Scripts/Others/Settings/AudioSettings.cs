using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

public class AudioSettings 
{
    private float _musicVolume;

    private float _effectsVolume;

    private AudioMixer _audioMixer;

    public event Action<float> MusicVolumeChanged;

    public event Action<float> EffectsVolumeChanged;

    private const float DEFAULT_MUSIC_VOLUME = 1;

    private const float DEFAULT_EFFECTS_VOLUME = 1;

    private const string SAVE_KEY_MUSIC_VOLUME = "music_volume";

    private const string SAVE_KEY_EFFECTS_VOLUME = "effects_volume";

    private const string AUDIOMIXER_MUSIC_VOLUME_PARAMETER_NAME = "MusicVolume";

    private const string AUDIOMIXER_EFFECTS_VOLUME_PARAMETER_NAME = "EffectsVolume";

    public float MusicVolume
    {
        get => _musicVolume;

        set
        {
            if (value <= 0)
            {
                value = 0.0001f;
            }

            _musicVolume = value;

            _audioMixer.SetFloat(AUDIOMIXER_MUSIC_VOLUME_PARAMETER_NAME, Mathf.Log(_musicVolume) * 20);

            PlayerPrefs.SetFloat(SAVE_KEY_MUSIC_VOLUME, _musicVolume);

            MusicVolumeChanged?.Invoke(_musicVolume);
        }
    }

    public float EffectsVolume
    {
        get => _effectsVolume;

        set
        {
            if (value <= 0)
            {
                value = 0.0001f;
            }

            _effectsVolume = value;

            _audioMixer.SetFloat(AUDIOMIXER_EFFECTS_VOLUME_PARAMETER_NAME, Mathf.Log(_effectsVolume) * 20);

            PlayerPrefs.SetFloat(SAVE_KEY_EFFECTS_VOLUME, _effectsVolume);

            EffectsVolumeChanged?.Invoke(_effectsVolume);
        }
    }

    public AudioSettings(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;

        InitializeMusicVolume();

        InitializeEffectsVolume();
    }

    private void InitializeMusicVolume()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY_MUSIC_VOLUME))
        {
            MusicVolume = DEFAULT_MUSIC_VOLUME;

            return;
        }

        MusicVolume = PlayerPrefs.GetFloat(SAVE_KEY_MUSIC_VOLUME);
    }

    private void InitializeEffectsVolume()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY_EFFECTS_VOLUME))
        {
            EffectsVolume = DEFAULT_EFFECTS_VOLUME;

            return;
        }

        EffectsVolume = PlayerPrefs.GetFloat(SAVE_KEY_EFFECTS_VOLUME);
    }
}
