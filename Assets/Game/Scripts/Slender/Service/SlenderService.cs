using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SlenderService : MonoBehaviour
{
    [Header("Prefab")]

    [SerializeField]
    private string _prefabPath;

    [SerializeField]
    private Transform _slenderParent;

    private bool _subscribedToEvents;

    private Slender _slender;

    private MapsService _mapsService;

    private PagesService _pagesService;

    private PlayerService _playerService;

    private void OnDestroy()
    {
        UnSubscribeFromEvents();
    }

    public void Initialize(MapsService mapService, PagesService pagesService, PlayerService playerService)
    {
        _mapsService = mapService;

        _pagesService = pagesService;

        _playerService = playerService;

        SubscribeToEvents();
    }

    private async void CreateSlender()
    {
        Transform spawnPoint = _mapsService.CurrentMap.SlenderSpawnPoints.DefaultPoint;

        GameObject slenderObject = await Addressables.InstantiateAsync(_prefabPath, spawnPoint.position, Quaternion.identity, _slenderParent).Task.AsUniTask();

        _slender = slenderObject.GetComponent<Slender>();

        if (_slender == null)
        {
            throw new System.Exception($"У обьекта {_prefabPath} отсутствует скрипт Slender");
        }

        _slender.Initialize(_playerService.Player, _mapsService.CurrentMap.Terrain, _pagesService);
    }

    private void DestroySlender()
    {
        Addressables.ReleaseInstance(_slender.gameObject);
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

        _subscribedToEvents = false;
    }

    private void OnPlayerCreated(Player player)
    {
        CreateSlender();
    }

    private void OnPlayerDestroyed(Player player)
    {
        DestroySlender();
    }

    #endregion
}
