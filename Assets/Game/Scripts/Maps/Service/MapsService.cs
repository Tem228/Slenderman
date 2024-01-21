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

    public async void LoadMap(string prefabPath)
    {
        DestroyMap();

        GameObject mapObject = await Addressables.InstantiateAsync(prefabPath, _mapParent).Task.AsUniTask();

        CurrentMap = mapObject.GetComponent<Map>();

        if(CurrentMap == null)
        {
            throw new Exception($"У обьекта {prefabPath} отсутсвует скрипт Map");
        }

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
