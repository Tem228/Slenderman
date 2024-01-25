using System.Collections.Generic;
using UnityEngine;

public class MenuTabsService : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField]
    private MenuTabBase _defaultTab;
    [SerializeField]
    private MenuTabBase[] _tabs;

    private MenuTabBase _currentTab;

    private Dictionary<MenuTabType, MenuTabBase> _tabsDictionary;

    public void Initialize()
    {
        InitializeTabsDictionary();

        ShowTab(_defaultTab.Type);
    }

    private void InitializeTabsDictionary()
    {
        _tabsDictionary = new Dictionary<MenuTabType, MenuTabBase>();

        for(int i = 0; i < _tabs.Length; i++)
        {
            MenuTabBase tab = _tabs[i];

            tab.Initialize(this);

            _tabsDictionary.Add(tab.Type, tab);
        }

        _tabs = null;
    }

    public void ShowTab(MenuTabType tabType)
    {
        if (!_tabsDictionary.ContainsKey(tabType))
        {
            throw new System.Exception($"¬кладки с типом {tabType} не существует");
        }

        HideCurrentTab();

        _currentTab =_tabsDictionary[tabType];

        _currentTab.SetVisible(true);
    }

    public void HideCurrentTab()
    {
        if (_currentTab != null
        && _currentTab.IsOpen)
        {
            _currentTab.SetVisible(false);
        }
    }
}