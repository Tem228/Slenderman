using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapsService : MonoBehaviour
{
    [SerializeField]
    private Transform _mapParent;

    public Map CurrentMap { get; private set; }

    public event Action MapCreated;

    public event Action MapDestroyed;

    public async void LoadMap(AssetReferenceGameObject mapPrefab)
    {
        DestroyMap();

        GameObject mapObject = await  Addressables.InstantiateAsync(mapPrefab, _mapParent).Task.AsUniTask();

        CurrentMap = mapObject.GetComponent<Map>();

        CurrentMap.Initialize();

        MapCreated?.Invoke();
    }

    private void DestroyMap()
    {
        if (CurrentMap == null)
        {
            return;
        }

        Addressables.ReleaseInstance(CurrentMap.gameObject);

        MapDestroyed?.Invoke();
    }
}
