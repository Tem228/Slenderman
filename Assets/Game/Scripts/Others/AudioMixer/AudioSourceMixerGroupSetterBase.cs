using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioSourceMixerGroupSetterBase : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private AudioSource _audioSource;

    private void OnValidate()
    {
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>(); 
        }

        if (_audioSource.outputAudioMixerGroup != null)
        {
            _audioSource.outputAudioMixerGroup = null;

            throw new System.Exception($"У AudioSource не должно быть ссылки на группу AudioMixer чтобы избежать клонирование AudioMixer");
        }
    }

    private void Awake()
    {
        _audioSource.outputAudioMixerGroup = GetMixerGroup();
    }

    protected abstract AudioMixerGroup GetMixerGroup();
}
