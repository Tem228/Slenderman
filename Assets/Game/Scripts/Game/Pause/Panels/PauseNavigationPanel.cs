using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseNavigationPanel : Panel
{
    [Header("Buttons")]

    [SerializeField]
    private Button _continueButton;
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Button _backToMenuButton;
    [SerializeField]
    private Button _exitButton;

    [Header("Settings")]
    [SerializeField]
    private PauseSettingsPanel _settingsPanel;

    private PauseService _pauseService;

    private bool _subscribedToEvents;

    private void OnDestroy()
    {
        UnSubscribeFromEvents();
    }

    public void Initialize(PauseService pauseService)
    {
        _pauseService = pauseService;

        SubscribeToEvents();
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if (_subscribedToEvents)
        {
            return;
        }

        _continueButton.onClick.AddListener(OnContinueButtonClick);

        _settingsButton.onClick.AddListener(OnSettingsButtonClick);

        _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);

        _exitButton.onClick.AddListener(OnExitButtonClick);

        _subscribedToEvents = true;
    }

    private void UnSubscribeFromEvents()
    {
        if (!_subscribedToEvents)
        {
            return;
        }

        _continueButton.onClick.RemoveListener(OnContinueButtonClick);

        _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);

        _backToMenuButton.onClick.RemoveListener(OnBackToMenuButtonClick);

        _exitButton.onClick.RemoveListener(OnExitButtonClick);

        _subscribedToEvents = false;
    }

    private void OnContinueButtonClick()
    {
        _pauseService.PauseState = false;
    }

    private void OnSettingsButtonClick()
    {
        SetVisible(false);

        _settingsPanel.SetVisible(true);
    }

    private async void OnBackToMenuButtonClick()
    {
        await SceneManager.LoadSceneAsync("Menu").ToUniTask();
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    #endregion
}
