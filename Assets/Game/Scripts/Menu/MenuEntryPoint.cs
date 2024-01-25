using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    [Header("Services")]
    [SerializeField]
    private MenuTabsService _tabsService;

    private void Awake()
    {
        _tabsService.Initialize();
    }
}
