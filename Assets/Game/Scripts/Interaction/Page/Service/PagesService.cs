using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

public class PagesService : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float _minDistanceBetweenPages;
    [SerializeField]
    [Range(0, 10)]
    private int _createPagesAmount;

    [Header("Prefab")]
    [SerializeField]
    private AssetReferenceGameObject _pageReference;

    private Page _pagePrefab;

    private List<Page> _pages;

    private MapsService _mapsService;

    public event Action<Page> PageCollected;

    private bool _subscribedToEvents;

    private void OnValidate()
    {
        if (_pageReference != null
        && _pageReference.editorAsset.GetComponent<Page>() == null)
        {
            _pageReference = null;

            throw new Exception($"”кажите ссылку на обьект у которого есть скрипт Page");
        }
    }

    private void OnDestroy()
    {
        _pageReference.ReleaseAsset();

        UnSubscribeFromEvents();
    }

    public async void Initialize(MapsService mapsService)
    {
        _pages = new List<Page>();

        _mapsService = mapsService;

      GameObject pagePrefab = await _pageReference.LoadAssetAsync().ToUniTask();

        _pagePrefab = pagePrefab.GetComponent<Page>(); 

        SubscribeToEvents();
    }

    private void CreatePages()
    {
        for (int i = 0; i < _createPagesAmount; i++)
        {
            CreatePage();
        }
    }

    private void CreatePage()
    {
        MapPagesSpawnPoints pagesSpawnPoints = _mapsService.CurrentMap.PageSpawnPoints;

        Vector3 randomPosition = CalculatePageFuturePosition(pagesSpawnPoints.Points);

        Page page = Instantiate(_pagePrefab, randomPosition, Quaternion.identity, pagesSpawnPoints.Parent);

        page.Initialize(OnPageCollected, OnPageCollected);
    }

    private void DestroyPages()
    {
        for (int i = 0; i < _pages.Count; i++)
        {
            Destroy(_pages[i].gameObject);
        }
    }

    private Vector3 CalculatePageFuturePosition(Vector3[] positions)
    {
        Vector3 result = positions[Random.Range(0, positions.Length)];

        for (int i = 0; i < _pages.Count; i++)
        {
            if (Vector2.Distance(_pages[i].transform.position, result) > _minDistanceBetweenPages)
            {
                return CalculatePageFuturePosition(positions);
            }
        }

        return result;
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if (_subscribedToEvents)
        {
            return;
        }

        _mapsService.MapCreated += OnMapCreated;
        _mapsService.MapDestroyed += OnMapDestroyed;

        _subscribedToEvents = true;
    }
    private void UnSubscribeFromEvents()
    {
        if (!_subscribedToEvents)
        {
            return;
        }

        _mapsService.MapCreated -= OnMapCreated;
        _mapsService.MapDestroyed -= OnMapDestroyed;

        _subscribedToEvents = false;
    }

    private void OnMapDestroyed()
    {
        DestroyPages();
    }

    private void OnMapCreated()
    {
        CreatePages();
    }

    private void OnPageDestroyed(Page page)
    {
        if (!_pages.Contains(page))
        {
            return;
        }

        _pages.Remove(page);
    }

    private void OnPageCollected(Page page)
    {
        PageCollected?.Invoke(page);
    }

    #endregion
}