using UnityEngine;

public abstract class MenuTabBase : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private GameObject _tab;

    protected MenuTabsService TabsService { get; private set; }

    public bool IsOpen => _tab.activeSelf;

    public abstract MenuTabType Type { get; } 

    public virtual void Initialize(MenuTabsService tabsService)
    {
        TabsService = tabsService;
    }

    public void SetVisible(bool isVisible)
    {
        _tab.SetActive(isVisible);
    }
}
