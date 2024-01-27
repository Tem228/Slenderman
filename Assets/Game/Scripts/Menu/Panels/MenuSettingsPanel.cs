using UnityEngine;

public class MenuSettingsPanel : SettingsPanel
{
    [Header("Panels")]
    [SerializeField]
    private MenuNavigationPanel _navigationPanel;

    protected override void OnBackButtonClicked()
    {
        SetVisible(false);

        _navigationPanel.SetVisible(true); 
    }
}