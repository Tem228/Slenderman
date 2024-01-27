using UnityEngine;

public class PauseSettingsPanel : SettingsPanel
{
    [Header("Panels")]
    [SerializeField]
    private PauseNavigationPanel _navigationPanel;

    protected override void OnBackButtonClicked()
    {
        SetVisible(false);

        _navigationPanel.SetVisible(true);
    }
}
