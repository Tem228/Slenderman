using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
     [field : SerializeField]
     public MapsService MapsService { get; private set; }

    public static GameEntryPoint Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        MapsService.Initialize();
    }
}
