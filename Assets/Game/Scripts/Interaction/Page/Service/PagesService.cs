using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

using Random = UnityEngine.Random;

public class PagesService : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField]
    private float _minDistanceBetweenPages;

    [SerializeField]
    [Range(1, 10)]
    private int _createPagesAmount;

    [Header("Prefab")]

    [SerializeField]
    private string _prefabPath;

    [SerializeField]
    private Transform _pagesParent;  

    private bool _subscribedToEvents;

    private Page _prefab;

    private List<Page> _pages;

    private MapsService _mapsService;

    public int CollectedPagesAmount { get; private set; }

    public int NeedCollectPagesAmount => _createPagesAmount; 

    public event Action<Page> PageCollected;

    private void OnDestroy()
    {
        Addressables.ReleaseInstance(_prefab.gameObject);

        UnSubscribeFromEvents();
    }

    public void Initialize(MapsService mapsService)
    {
        _pages = new List<Page>();

        _mapsService = mapsService;

        SubscribeToEvents();
    }

    private async void CreatePages()
    {
        CollectedPagesAmount = 0;

        GameObject pageObject = await Addressables.LoadAssetAsync<GameObject>(_prefabPath).Task.AsUniTask();

        _prefab = pageObject.GetComponent<Page>();

        if (_prefab == null)
        {
            throw new Exception($"У обьекта {_prefabPath} отсутствует скрипт Page");
        }

        for (int i = 0; i < _createPagesAmount; i++)
        {
            CreatePage();
        }
    }

    private void CreatePage()
    {
        Transform randomPoint = GetPageRandomPoint(_mapsService.CurrentMap.PagesSpawnPoints.Points.ToList());

        Page page = Instantiate(_prefab, randomPoint.position, randomPoint.rotation, _pagesParent);

        page.Initialize(OnPageCollected, OnPageCollected);

        _pages.Add(page);
    }

    private void DestroyPages()
    {
        for (int i = 0; i < _pages.Count; i++)
        {
            Destroy(_pages[i].gameObject);
        }
    }

    private Transform GetPageRandomPoint(List<Transform> points)
    {
        if(points.Count == 0)
        {
            throw new Exception($"Количество созданных записок: {_pages.Count}. Не хватает точек. Добавьте больше");
        }

        Transform result = points[Random.Range(0, points.Count)];

        for (int i = 0; i < _pages.Count; i++)
        {
            if (Vector2.Distance(_pages[i].transform.position, result.position) < _minDistanceBetweenPages)
            {
                points.Remove(result);

                return GetPageRandomPoint(points);
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

        CollectedPagesAmount += 1;
    }

    #endregion
}