using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenEntryPoint : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Settings _settings;

    private async void Awake()
    {
        await _settings.Initialize();

        await SceneManager.LoadSceneAsync("Menu").ToUniTask();
    }
}
