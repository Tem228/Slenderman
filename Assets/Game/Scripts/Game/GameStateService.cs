using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateService
{
    private bool _subscribedToEvents;

    private bool _subscribedToPlayerEvents;

    private PagesService _pagesService;

    private PlayerService _playerService;

    private void OnDestroy()
    {
        UnSubscribeFromEvents();

        UnSubscribeFromPlayerEvents();
    }

    public GameStateService(PagesService pagesService, PlayerService playerService)
    {
        _pagesService = pagesService;

        _playerService = playerService;

        SubscribeToEvents();
    }

    ~GameStateService()
    {
        UnSubscribeFromEvents();

        UnSubscribeFromPlayerEvents();
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if (_subscribedToEvents)
        {
            return;
        }

        _playerService.PlayerCreated += OnPlayerCreated;

        _playerService.PlayerDestroyed += OnPlayerDestroyed;

        _pagesService.AllPagesCollected += OnAllPagesCollected;

        _subscribedToEvents = true;
    }

    private void UnSubscribeFromEvents()
    {
        if (!_subscribedToEvents)
        {
            return;
        }

        _playerService.PlayerCreated -= OnPlayerCreated;

        _playerService.PlayerDestroyed -= OnPlayerDestroyed;

        _pagesService.AllPagesCollected -= OnAllPagesCollected;

        _subscribedToEvents = false;
    }

    private async void OnAllPagesCollected()
    {
        await SceneManager.LoadSceneAsync("WinScreen").ToUniTask();
    }

    private void OnPlayerCreated(Player player)
    {
        SubscribeToPlayerEvents();
    }

    private void OnPlayerDestroyed(Player player)
    {
        UnSubscribeFromEvents();
    }

    private void SubscribeToPlayerEvents()
    {
        if (_subscribedToPlayerEvents)
        {
            return;
        }

        _playerService.Player.HealthSystem.Died += OnPlayerDied;

        _subscribedToPlayerEvents = true;
    }

    private void UnSubscribeFromPlayerEvents()
    {
        if (!_subscribedToPlayerEvents)
        {
            return;
        }

        _playerService.Player.HealthSystem.Died -= OnPlayerDied;

        _subscribedToPlayerEvents = false;
    }

    private async void OnPlayerDied()
    {
        await SceneManager.LoadSceneAsync("DiedScreen").ToUniTask();
    }

    #endregion
}
