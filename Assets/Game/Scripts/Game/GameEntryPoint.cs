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
    private PagesService _pagesService;

    [SerializeField]
    private PlayerService _playerService;

    private GameStateService _gameStateService;

    private async void Awake()
    {
        _playerService.Initialize(_mapsService);

        await _pagesService.Initialize(_mapsService);

        _objectiveText.Initialize(_pagesService);

         _gameStateService = new GameStateService(_pagesService, _playerService);

        _slenderService.Initialize(_mapsService, _pagesService, _playerService);

        _mapsService.LoadMap(_gameParameters.DefaultMapPrefabPath);
    }
}