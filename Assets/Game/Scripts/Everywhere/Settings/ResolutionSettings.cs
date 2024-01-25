using System;
using UnityEngine;

public class ResolutionSettings
{
    private int _resolutionIndex;

    public event Action<int> ResoultionIndexChanged;

    private const string SAVE_KEY_RESOLUTION_INDEX = "resolution_index";

    public int ResoultionIndex
    {
        get => _resolutionIndex;

        set
        {
            int resolutionsAmount = Screen.resolutions.Length;

            if (value > resolutionsAmount)
            {
                throw new System.Exception($"Индекс разрешения экрана не может быть больше чем {resolutionsAmount}");
            }

            _resolutionIndex = value;

            Resolution currentResoultion = Screen.resolutions[_resolutionIndex];

            Screen.SetResolution(currentResoultion.width, currentResoultion.height, Screen.fullScreen);

            PlayerPrefs.SetInt(SAVE_KEY_RESOLUTION_INDEX, _resolutionIndex);

            ResoultionIndexChanged?.Invoke(_resolutionIndex);
        }
    }

    public ResolutionSettings()
    {
        InitializeResolutionIndex();
    }

    private void InitializeResolutionIndex()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY_RESOLUTION_INDEX))
        {
            ResoultionIndex = GetIndexOfCurrentResolution();

            return;
        }

        ResoultionIndex = PlayerPrefs.GetInt(SAVE_KEY_RESOLUTION_INDEX);
    }

    private int GetIndexOfCurrentResolution()
    {
        Resolution currentResolution = Screen.currentResolution;

        Resolution[] availableResolutions = Screen.resolutions;

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            Resolution resolution = availableResolutions[i];

            if (resolution.width == currentResolution.width
            && resolution.height == currentResolution.height)
            {
                return i;
            }
        }

        return -1;
    }
}
