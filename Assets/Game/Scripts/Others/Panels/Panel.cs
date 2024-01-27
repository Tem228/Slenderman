using UnityEngine;

public class Panel : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private GameObject _panel;

    public bool IsOpen => _panel.activeSelf;

    public void SetVisible(bool isVisible)
    {
        if (isVisible)
        {
            OnOpen();
        }
        else
        {
            OnClose();
        }

        _panel.SetActive(isVisible);
    }

    protected virtual void OnOpen() { }

    protected virtual void OnClose() { }
}
