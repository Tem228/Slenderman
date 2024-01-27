using Hertzole.GoldPlayer;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(GoldPlayerController), typeof(PlayerPagesInteraction))]
public class Player : MonoBehaviour
{
    [Header("Components")]

    [SerializeField]
    private PlayerFlashLight _flashLight;

    [SerializeField]
    private PlayerPagesInteraction _pagesInteraction;

    [SerializeField]
    private GoldPlayerController _controller;

    [SerializeField]
    private NoiseAndScratches _noiseEffect;

    [SerializeField]
    private VignetteAndChromaticAberration _chromaticAbberationEffect;

    private bool _subscribedToEvents;

    public NoiseAndScratches NoiseEffect => _noiseEffect;

    public VignetteAndChromaticAberration ChromaticAbberationEffect => _chromaticAbberationEffect;

    public PlayerHealth HealthSystem { get; private set; }

    private PauseService _pauseService;

    private void OnValidate() 
    {
        if(_controller == null)
        {
            _controller = GetComponent<GoldPlayerController>(); 
        }

        if(_pagesInteraction == null)
        {
            _pagesInteraction = GetComponent<PlayerPagesInteraction>();
        }
    }

    private void OnDestroy()
    {
        UnSubscribeFromEvents();
    }

    public void Initialize(PauseService pauseService)
    {
        _pauseService = pauseService;

        HealthSystem = new PlayerHealth();

        SubscribeToEvents();
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        if (_subscribedToEvents)
        {
            return;
        }

        _pauseService.PauseStateChanged += OnPauseStateChanged;

        _subscribedToEvents = true;
    }

    private void UnSubscribeFromEvents()
    {
        if (!_subscribedToEvents)
        {
            return;
        }

        _pauseService.PauseStateChanged -= OnPauseStateChanged;

        _subscribedToEvents = false;
    }

    private void OnPauseStateChanged(bool pauseState)
    {
        bool enableComponents = pauseState == false;

        _controller.enabled = enableComponents;

        _flashLight.enabled = enableComponents;

        _pagesInteraction.enabled = enableComponents;
    }

    #endregion
}