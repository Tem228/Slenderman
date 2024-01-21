using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SlenderAttackState : SlenderStateBase
{
    [Header("Parameters")]

    [SerializeField]
    private float _maxNoiseIntensity;

    [SerializeField]
    private float _noiseActivateDuration;

    [SerializeField]
    private float _noiseDeactivateDuration;

    [SerializeField]
    private float _maxChromaticAberrationIntesity;

    [SerializeField]
    private float _minChromaticAberrationIntesity;

    [SerializeField]
    [Range(0, 1)]
    private float _buffPercent;

    [SerializeField]
    [Range(0, 1)]
    private float _debuffPercent;

    [Header("Components")]
    [SerializeField]
    private AudioSource _audioSource;

    private bool _isActive;

    private bool _subscribedToEvents;

    private Sequence _sequence;

    private Player _target;

    private Slender _slender;

    private PagesService _pagesService;

    private SlenderStateMachine _stateMachine;

    public override SlenderStateType Type => SlenderStateType.Attack;

    private void OnValidate()
    {
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnDestroy()
    {
        UnSubscribeFromEvents();
    }

    private void Update()
    {
        if (!_isActive
        || _slender.InPlayerSight() == true)
        {
            return;
        }

        _stateMachine.ChangeState(SlenderStateType.Chase);
    }

    public void Initialize(Player target, Slender slender, PagesService pagesService, SlenderStateMachine stateMachine)
    {
        _target = target;

        _slender = slender;

        _pagesService = pagesService;

        _stateMachine = stateMachine;

        SubscribeToEvents();
    }

    public override void Activate()
    {
        _isActive = true;

        if (_sequence != null)
        {
            _sequence.Kill();
        }

        _audioSource.Play();

        _sequence = DOTween.Sequence();

        _sequence
            .Append(DOTween.To(OnNoiseEffectIntensityChanged, _target.NoiseEffect.grainIntensityMax, _maxNoiseIntensity, _noiseActivateDuration))
            .Join(DOTween.To(OnAudioSourceVolumeChanged, _audioSource.volume, 1, _noiseActivateDuration))
            .OnComplete(() =>
            {
               _target.HealthSystem.HP -= PlayerHealth.MAX_HP;
            });
    }

    public override void Deactivate()
    {
        _isActive = false;

        if (_sequence != null)
        {
            _sequence.Kill();
        }

        _sequence = DOTween.Sequence();

        _sequence
            .Append(DOTween.To(OnNoiseEffectIntensityChanged, _target.NoiseEffect.grainIntensityMax, 0, _noiseDeactivateDuration))
            .Join(DOTween.To(OnAudioSourceVolumeChanged, _audioSource.volume, 0, _noiseDeactivateDuration))
            .OnComplete(() =>
            {
                _audioSource.Stop();
            });
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

    private void UnSubscribeFromEvents()
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
        _noiseActivateDuration -= _noiseActivateDuration * _buffPercent;
        _noiseDeactivateDuration += _noiseDeactivateDuration * _debuffPercent;
    }

    private void OnNoiseEffectIntensityChanged(float newIntensity)
    {
        _target.NoiseEffect.grainIntensityMax = newIntensity;

        _target.ChromaticAbberationEffect.chromaticAberration = Random.Range(_minChromaticAberrationIntesity, _maxChromaticAberrationIntesity) * newIntensity;
    }

    private void OnAudioSourceVolumeChanged(float newVolume)
    {
        _audioSource.volume = newVolume;
    }

    #endregion
}
