using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private AssetReferenceT<AudioMixer> _audioMixerAsset;

    public AudioMixer AudioMixer { get; private set; }

    public AudioSettings Audio { get; private set; }

    public GraphicsSettings Graphics { get; private set; }

    public ResolutionSettings Resolution { get; private set; }

    public static Settings Instance { get; private set; }

    private void OnDestroy()
    {
       if(AudioMixer != null)
        {
            Addressables.Release(AudioMixer);
        }
    }

    public async UniTask Initialize()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;

        await UniTask.WaitForSeconds(0.01f);

        AudioMixer = await _audioMixerAsset.LoadAssetAsync().Task.AsUniTask();

        Audio = new AudioSettings(AudioMixer);

        Graphics = new GraphicsSettings();

        Resolution = new ResolutionSettings();

        DontDestroyOnLoad(gameObject);
    }
}
