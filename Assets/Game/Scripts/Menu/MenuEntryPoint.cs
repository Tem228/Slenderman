using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    private MenuNavigationPanel _navigationPanel;
    [SerializeField]
    private MenuSettingsPanel _settingsPanel;

    private void Awake()
    {
        _navigationPanel.Initialize();

        _settingsPanel.Initialize();

        _navigationPanel.SetVisible(true);
    }
}
