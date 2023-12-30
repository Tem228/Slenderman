using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField]
    private GameParameters _gameParameters;

    [field: SerializeField]
    public MapsService MapsService { get; private set; }


    [field: SerializeField]
    public PagesService PagesService { get; private set; }

    [field: SerializeField]
    public PlayerService PlayerService { get; private set; }

    public static GameEntryPoint Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        PagesService.Initialize(MapsService);

        PlayerService.Initialize(MapsService);

        MapsService.LoadMap(_gameParameters.DefaultMapPrefab);
    }
}