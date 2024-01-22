using System.Collections;
using UnityEngine;

public class SlenderChaseState : SlenderStateBase
{
    [Header("Parameters")]

    [SerializeField]
    private float _minTeleportRange;

    [SerializeField]
    private float _maxTeleportRange;

    [SerializeField]
    private float _teleportDelay;

    [SerializeField]
    [Range(0, 1)]
    private float _buffPercent;

    [SerializeField]
    [Range(0, 1)]
    private float _debuffPercent;

    private bool _isActive;

    private bool _subscribedToEvents;

    private Coroutine _teleportCoroutine;

    private Player _target;

    private Slender _slender;

    private Terrain _terrain;

    private PagesService _pagesService;

    private SlenderStateMachine _stateMachine;

    public override SlenderStateType Type => SlenderStateType.Chase;

    private void OnDestroy()
    {
        UnSubscribeFromEvents();
    }

    private void Update()
    {
        if (!_isActive
        || _slender.InPlayerSight() == false)
        {
            return;
        }

        _stateMachine.ChangeState(SlenderStateType.Attack);
    }

    public void Initialize(Player target, Terrain terrain, Slender slender, PagesService pagesService, SlenderStateMachine stateMachine)
    {
        _target = target;

        _terrain = terrain;

        _slender = slender;

        _pagesService = pagesService;

        _stateMachine = stateMachine;

        SubscribeToEvents();
    }

    public override void Activate()
    {
        _isActive = true;

        _teleportCoroutine = StartCoroutine(Chase());
    }

    public override void Deactivate()
    {
        _isActive = false;

        StopCoroutine(_teleportCoroutine);
    }

    private IEnumerator Chase()
    {
        while (true)
        {
            yield return new WaitForSeconds(_teleportDelay);

            TeleportToTarget();

            LookAtTarget();
        }
    }

    private void TeleportToTarget()
    {
        Vector3 teleportDirection = Random.insideUnitSphere;

        Vector3 teleportPosition = _target.transform.position + (teleportDirection * Random.Range(_minTeleportRange, _maxTeleportRange));

        _slender.transform.position = new Vector3(teleportPosition.x, _terrain.SampleHeight(teleportPosition) + 0.25f, teleportPosition.z);
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = _target.transform.position - _slender.transform.position;

        lookPos.y = 90;

        _slender.transform.rotation = Quaternion.LookRotation(lookPos);
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
        _minTeleportRange -= _minTeleportRange * _debuffPercent;
        _maxTeleportRange -= _maxTeleportRange * _debuffPercent;

        _teleportDelay -= _teleportDelay * _debuffPercent;
    }

    #endregion
}
