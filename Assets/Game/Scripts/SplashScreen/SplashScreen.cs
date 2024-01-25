using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private async void Start()
    {
        await SceneManager.LoadSceneAsync("Menu").ToUniTask();
    }
}
