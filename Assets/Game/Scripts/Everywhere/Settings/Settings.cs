using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private AudioMixer _audioMixer;

    public AudioSettings Audio { get; private set; }

    public GraphicsSettings Graphics { get; private set; }

    public ResolutionSettings Resolution { get; private set; }

    public static Settings Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;

        Audio = new AudioSettings(_audioMixer);

        Graphics = new GraphicsSettings();

        Resolution = new ResolutionSettings();

        DontDestroyOnLoad(gameObject);
    }
}
