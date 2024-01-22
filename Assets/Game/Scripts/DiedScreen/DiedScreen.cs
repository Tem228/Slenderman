using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiedScreen : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button _retryButton;
    [SerializeField]
    private Button _toMenuButton;

    private bool _subscribedToEvents;

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnSubscribeFromEvents();
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if (_subscribedToEvents)
        {
            return;
        }

        _retryButton.onClick.AddListener(OnRetryButtonClick);

        _toMenuButton.onClick.AddListener(OnToMenuButtonClick);

        _subscribedToEvents = true;
    }

    private void UnSubscribeFromEvents()
    {
        if (!_subscribedToEvents)
        {
            return;
        }

        _retryButton.onClick.RemoveListener(OnRetryButtonClick);

        _toMenuButton.onClick.RemoveListener(OnToMenuButtonClick);

        _subscribedToEvents = true;
    }

    private async void OnRetryButtonClick()
    {
        await SceneManager.LoadSceneAsync("Game").ToUniTask();
    }

    private async void OnToMenuButtonClick()
    {
        await SceneManager.LoadSceneAsync("Menu").ToUniTask();
    }

    #endregion
}
