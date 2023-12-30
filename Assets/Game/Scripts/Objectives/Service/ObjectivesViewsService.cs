using System.Collections.Generic;
using UnityEngine;

public class ObjectivesViewsService : MonoBehaviour
{
    [Header("Views")]
    [SerializeField]
    private ObjectiveView _viewPrefab;

    [SerializeField]
    private Transform _viewPrefabParent;

    private List<ObjectiveView> _views;

    public void Initialize(ObjectiveInfo[] objectives)
    {
        _views = new List<ObjectiveView>();

        CreateViews(objectives);
    }

    private void CreateViews(ObjectiveInfo[] objectives)
    {
        for(int i = 0; i < objectives.Length; i++)
        {
            CreateView(objectives[i]);
        }
    }

    private void CreateView(ObjectiveInfo objective)
    {
        ObjectiveView view = Instantiate(_viewPrefab, _viewPrefabParent);

        view.Initialize(objective);

        _views.Add(view);
    }

    private void DestroyViews()
    {
        for(int i = 0; i < _views.Count; i++)
        {
            Destroy(_views[i].gameObject);
        }

        _views.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
