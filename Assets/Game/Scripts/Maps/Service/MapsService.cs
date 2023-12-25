using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapsService : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceGameObject _defaultMapPrefab;

    [SerializeField]
    private Transform _mapParent;

    public Map CurrentMap { get; private set; }

    private void OnValidate() 
    {
        if(_defaultMapPrefab != null
        && _defaultMapPrefab.editorAsset.GetComponent<Map>() == null)
        {
            _defaultMapPrefab = null;

            throw new System.Exception($"Укажите ссылку на обьект у которого есть скрипт Map");
        }
    }

    public void Initialize()
    {
       LoadMap();
    }

    private async void LoadMap()
    {
        DestroyMap();

        GameObject mapObject = await _defaultMapPrefab.InstantiateAsync(_mapParent).Task.AsUniTask();

        CurrentMap = mapObject.GetComponent<Map>();
    }

    private void DestroyMap()
    {
        if (CurrentMap == null)
        {
            return;
        }

        _defaultMapPrefab.ReleaseInstance(CurrentMap.gameObject);
    }
}
