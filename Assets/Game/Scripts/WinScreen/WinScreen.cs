using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button _toMenuButton;

    private bool _subscribedToEvents;

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnSubscribeFromEvents();
    }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if (_subscribedToEvents)
        {
            return;
        }

        _toMenuButton.onClick.AddListener(OnToMenuButtonClick);

        _subscribedToEvents = true;
    }

    private void UnSubscribeFromEvents()
    {
        if (!_subscribedToEvents)
        {
            return;
        }

        _toMenuButton.onClick.RemoveListener(OnToMenuButtonClick);

        _subscribedToEvents = true;
    }

    private async void OnToMenuButtonClick()
    {
        await SceneManager.LoadSceneAsync("Menu").ToUniTask();
    }

    #endregion
}
