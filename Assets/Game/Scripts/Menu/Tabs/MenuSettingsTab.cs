using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettingsTab : MenuTabBase
{
    [Header("Buttons")]
    [SerializeField]
    private Button _backButton;

    [Header("Sliders")]
    [SerializeField]
    private Slider _musicVolumeSlider;
    [SerializeField]
    private Slider _effectsVolumeSlider;

    [Header("Dropdowns")]
    [SerializeField]
    private TMP_Dropdown _graphicsDropdown;
    [SerializeField]
    private TMP_Dropdown _resolutionsDropdown;

    private bool _subscribedToEvents;

    private Settings _settings;

    public override MenuTabType Type => MenuTabType.Settings;

    private void OnDestroy()
    {
        UnSubscribeFromEvents();
    }

    public override void Initialize(MenuTabsService tabsService)
    {
        base.Initialize(tabsService);

        _settings = Settings.Instance;

        InitializeMusicVolumeSlider();

        InitializeEffectsVolumeSlider();

        InitializeGraphicsDropdown();

        InitializeResolutionsDropdown();

        SubscribeToEvents();
    }

    private void InitializeMusicVolumeSlider()
    {
        _musicVolumeSlider.value = _settings.Audio.MusicVolume;
    }

    private void InitializeEffectsVolumeSlider()
    {
        _effectsVolumeSlider.value = _settings.Audio.EffectsVolume;
    }

    private void InitializeGraphicsDropdown()
    {
        if (_graphicsDropdown.options.Count > 0)
        {
            return;
        }

        string[] qualityLevels = QualitySettings.names;

        for (int i = 0; i < qualityLevels.Length; i++)
        {
            _graphicsDropdown.options.Add(new TMP_Dropdown.OptionData(qualityLevels[i]));
        }

        _graphicsDropdown.value = _settings.Graphics.GraphicsLevel;
    }

    private void InitializeResolutionsDropdown()
    {
        if (_resolutionsDropdown.options.Count > 0)
        {
            return;
        }

        Resolution[] availableResolutions = Screen.resolutions;

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            Resolution resolution = availableResolutions[i];

            string resolutionName = $"{resolution.width}x{resolution.height} hz {resolution.refreshRateRatio}";

            _resolutionsDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionName));
        }

        _resolutionsDropdown.value = _settings.Resolution.ResoultionIndex;
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if (_subscribedToEvents)
        {
            return;
        }

        _backButton.onClick.AddListener(OnBackButtonClicked);

        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeValueChanged);

        _effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeValueChanged);

        _graphicsDropdown.onValueChanged.AddListener(OnGraphicsDropdownValueChanged);

        _resolutionsDropdown.onValueChanged.AddListener(OnResolutionsDropdownValueChanged);

        _subscribedToEvents = true;
    }

    private void UnSubscribeFromEvents()
    {
        if (!_subscribedToEvents)
        {
            return;
        }

        _backButton.onClick.RemoveListener(OnBackButtonClicked);

        _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeValueChanged);

        _effectsVolumeSlider.onValueChanged.RemoveListener(OnEffectsVolumeValueChanged);

        _graphicsDropdown.onValueChanged.RemoveListener(OnGraphicsDropdownValueChanged);

        _resolutionsDropdown.onValueChanged.RemoveListener(OnResolutionsDropdownValueChanged);

        _subscribedToEvents = false;
    }

    private void OnBackButtonClicked()
    {
        TabsService.ShowTab(MenuTabType.Navigation);
    }

    private void OnMusicVolumeValueChanged(float value)
    {
        _settings.Audio.MusicVolume = value;
    }

    private void OnEffectsVolumeValueChanged(float value)
    {
        _settings.Audio.EffectsVolume = value;
    }

    private void OnGraphicsDropdownValueChanged(int value)
    {
        _settings.Graphics.GraphicsLevel = value;     
    }

    private void OnResolutionsDropdownValueChanged(int value)
    {
        _settings.Resolution.ResoultionIndex = value;
    }

    #endregion
}
