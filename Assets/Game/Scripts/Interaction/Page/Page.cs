using System;
using UnityEngine;

public class Page : MonoBehaviour
{
    private Action<Page> _destroyed;
    private Action<Page> _collected;

    private void OnDestroy()
    {
        _destroyed?.Invoke(this);
    }

    public void Initialize(Action<Page> destroyed, Action<Page> collected)
    {
        _destroyed = destroyed;

        _collected = collected;
    }

    public void Collect()
    {
        _collected?.Invoke(this);

        Destroy(gameObject);
    }
}
