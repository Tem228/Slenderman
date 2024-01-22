using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ObjectiveText : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField]
    private float _showEffectDuration;

    [SerializeField]
    private float _hideEffectDuration;

    [SerializeField]
    private KeyCode _showTextKey;

    [Header("Components")]
    [SerializeField]
    private TMP_Text _text;

    private Sequence _effectSequence;
    private PagesService _pagesService;

    private bool _subscribedToEvents;

    private void OnValidate()
    {
        if(_text == null)
        {
            _text = GetComponent<TMP_Text>();
        }
    }

    private void OnDestroy()
    {
        UnSubscribedFromEvents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_showTextKey))
        {
            ShowText();
        }
    }

    public void Initialize(PagesService pagesService)
    {
        _pagesService = pagesService;

        UpdateText();

        SubscribeToEvents();
    }

    private void UpdateText()
    {
        _text.text = $"Собрано {_pagesService.CollectedPagesAmount} из {_pagesService.NeedCollectPagesAmount} записок";
    }

    private void ShowText()
    {
        if (_effectSequence == null)
        {
            _effectSequence = DOTween.Sequence();

            _effectSequence
                .Append(_text.DOFade(1, _showEffectDuration))
                .Append(_text.DOFade(0, _hideEffectDuration))
                .SetAutoKill(false);
        }
        else
        {
            _effectSequence.Restart();
        }
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if (_subscribedToEvents)
        {
            return;
        }

        _pagesService.PageCollected += OnPageCollected;

        _subscribedToEvents = true;
    }

    private void UnSubscribedFromEvents()
    {
        if (!_subscribedToEvents)
        {
            return;
        }

        _pagesService.PageCollected -= OnPageCollected;

        _subscribedToEvents = false;
    }

    private void OnPageCollected(Page page)
    {
        UpdateText();

        ShowText();
    }

    #endregion
}
