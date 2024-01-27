using System;
using UnityEngine;

public class GraphicsSettings
{
    private int _graphicsLevel;

    public event Action<int> GraphicsLevelChanged;

    private const int DEFAULT_GRAPHICS_LEVEL = 0;

    private const string SAVE_KEY_GRAPHICS_LEVEL = "graphics_level";

    public int GraphicsLevel
    {
        get => _graphicsLevel;

        set
        {
            if(value > QualitySettings.count)
            {
                throw new Exception($"Уровень графики не может быть больше чем {QualitySettings.count}");
            }

            _graphicsLevel = value;

            PlayerPrefs.SetInt(SAVE_KEY_GRAPHICS_LEVEL, _graphicsLevel);

            QualitySettings.SetQualityLevel(_graphicsLevel);

            GraphicsLevelChanged?.Invoke(_graphicsLevel);
        }
    }

    public GraphicsSettings()
    {
        InitializeGraphicsLevel();
    }

    private void InitializeGraphicsLevel()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY_GRAPHICS_LEVEL))
        {
            GraphicsLevel = DEFAULT_GRAPHICS_LEVEL;

            return;
        }

        GraphicsLevel = PlayerPrefs.GetInt(SAVE_KEY_GRAPHICS_LEVEL);
    }
}
