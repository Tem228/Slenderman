using System;
using UnityEngine;

public class PauseService : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private KeyCode _pauseGameKey;

    [Header("Panels")]
    [SerializeField]
    private PauseNavigationPanel _navigationPanel;
    [SerializeField]
    private PauseSettingsPanel _settingsPanel;

    private bool _pauseState;

    public event Action<bool> PauseStateChanged;

    public bool PauseState
    {
        get => _pauseState;

        set
        {
            if (_settingsPanel.IsOpen)
            {
                return;
            }

            _pauseState = value;

            _navigationPanel.SetVisible(_pauseState);

            Time.timeScale = _pauseState ? 0 : 1;

            Cursor.visible = _pauseState;

            Cursor.lockState = _pauseState ? CursorLockMode.None : CursorLockMode.Locked;

            PauseStateChanged?.Invoke(_pauseState);
        }
    }

    public void Initialize()
    {
        _navigationPanel.Initialize(this);

        _settingsPanel.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_pauseGameKey))
        {
            PauseState = !PauseState;
        }
    }
}
