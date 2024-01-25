using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavigationTab : MenuTabBase
{
    [Header("Buttons")]
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Button _exitButton;

    private bool _subscribedToEvents;

    public override MenuTabType Type => MenuTabType.Navigation;

    private void OnDestroy()
    {
        UnSubscibeFromEvents();
    }
    public override void Initialize(MenuTabsService tabsService)
    {
        base.Initialize(tabsService);

        SubscribeToEvents();
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if (_subscribedToEvents)
        {
            return;
        }

        _startButton.onClick.AddListener(OnStartButtonClick);

        _settingsButton.onClick.AddListener(OnSettingsClick);

        _exitButton.onClick.AddListener(OnExitButtonClick);

        _subscribedToEvents = true;
    }

    private void UnSubscibeFromEvents()
    {
        if (!_subscribedToEvents)
        {
            return;
        }

        _startButton.onClick.RemoveListener(OnStartButtonClick);

        _settingsButton.onClick.RemoveListener(OnSettingsClick);

        _exitButton.onClick.RemoveListener(OnExitButtonClick);

        _subscribedToEvents = false;
    }

    private async void OnStartButtonClick()
    {
        await SceneManager.LoadSceneAsync("Game").ToUniTask();
    }

    private void OnSettingsClick()
    {
        TabsService.ShowTab(MenuTabType.Settings);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }


    #endregion
}
