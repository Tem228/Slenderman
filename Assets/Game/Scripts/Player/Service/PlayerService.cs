using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerService : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceGameObject _playerPrefab;

    [SerializeField]
    private Transform _playerParent;

    private MapsService _mapsService;

    public Player Player { get; private set; }

    private bool _subscribedToEvents;

    private void OnValidate() 
    {
        if (_playerPrefab != null
        && _playerPrefab.editorAsset.GetComponent<Player>() == null)
        {
            _playerPrefab = null;

            throw new System.Exception($"Укажите ссылку на обьект у которого есть скрипт Player");
        }
    }

    private void OnDestroy()
    {
        UnSubscribeToEvents();
    }

    public void Initialize(MapsService mapsService)
    {
        _mapsService = mapsService;

        SubscribeToEvents();
    }

    private async void CreatePlayer()
    {
        Transform spawnPoint = _mapsService.CurrentMap.PlayerSpawnPoints.DefaultPoint;

        GameObject playerObject = await _playerPrefab.InstantiateAsync(spawnPoint.position, Quaternion.identity, _playerParent).Task.AsUniTask();

        Player = playerObject.GetComponent<Player>();
    }

    private void DestroyPlayer()
    {
        _playerPrefab.ReleaseInstance(Player.gameObject);
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if(_subscribedToEvents)
        {
            return;
        }

        _mapsService.MapCreated += OnMapCreated;
        _mapsService.MapDestroyed += OnMapDestroy;
    }

    private void UnSubscribeToEvents()
    {
        if(!_subscribedToEvents)
        {
            return;
        }

        _mapsService.MapCreated -= OnMapCreated;
        _mapsService.MapDestroyed -= OnMapDestroy;
    }

    private void OnMapCreated()
    {
        CreatePlayer();
    }

    private void OnMapDestroy()
    {
       DestroyPlayer(); 
    }

    #endregion
}
