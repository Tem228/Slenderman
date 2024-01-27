using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerService : MonoBehaviour
{
    [Header("Prefab")]

    [SerializeField]
    private string _prefabPath;

    [SerializeField]
    private Transform _playerParent;

    private bool _subscribedToEvents;

    private MapsService _mapsService;

    private PauseService _pauseService;

    public Player Player { get; private set; }

    public event Action<Player> PlayerCreated;

    public event Action<Player> PlayerDestroyed;

    private void OnDestroy()
    {
        UnSubscribeToEvents();
    }

    public void Initialize(MapsService mapsService, PauseService pauseService)
    {
        _mapsService = mapsService;

        _pauseService = pauseService;

        SubscribeToEvents();
    }

    private async void CreatePlayer()
    {
        Transform spawnPoint = _mapsService.CurrentMap.PlayerSpawnPoints.DefaultPoint;

        GameObject playerObject = await Addressables.InstantiateAsync(_prefabPath, spawnPoint.position, Quaternion.identity, _playerParent).Task.AsUniTask();

        Player = playerObject.GetComponent<Player>();

        if(Player == null)
        {
            throw new Exception($"У обьекта {_prefabPath} отсутствует скрипт Player");
        }

        Player.Initialize(_pauseService);

        PlayerCreated?.Invoke(Player);
    }

    private void DestroyPlayer()
    {
        PlayerDestroyed?.Invoke(Player);

        Addressables.ReleaseInstance(Player.gameObject);
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
