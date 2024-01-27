using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField]
    private GameParameters _gameParameters;

    [SerializeField]
    private ObjectiveText _objectiveText;

    [SerializeField]
    private SlenderService _slenderService;

    [SerializeField]
    private MapsService _mapsService;

    [SerializeField]
    private PauseService _pauseService;

    [SerializeField]
    private PagesService _pagesService;

    [SerializeField]
    private PlayerService _playerService;

    private GameStateService _gameStateService;

    private async void Awake()
    {
        _pauseService.Initialize();

        _playerService.Initialize(_mapsService, _pauseService);

        await _pagesService.Initialize(_mapsService);

        _objectiveText.Initialize(_pagesService);

         _gameStateService = new GameStateService(_pagesService, _playerService);

        _slenderService.Initialize(_mapsService, _pagesService, _playerService);

        _mapsService.LoadMap(_gameParameters.DefaultMapPrefabPath);
    }
}