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

    private void Awake()
    {

        _slenderService.Initialize(_mapsService, _pagesService, _playerService);

        _pagesService.Initialize(_mapsService);

        _objectiveText.Initialize(_pagesService);

        _playerService.Initialize(_mapsService);

        _mapsService.LoadMap(_gameParameters.DefaultMapPrefabPath);
    }
}