using UnityEngine;

[RequireComponent(typeof(SlenderAttackState), typeof(SlenderChaseState))]
public class Slender : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField]
    private float _visibleZoneAngle;

    [SerializeField]
    private float _visibleZoneDistance;

    [Header("States")]

    [SerializeField]
    private SlenderAttackState _attackState;

    [SerializeField]
    private SlenderChaseState _chaseState;

    [Header("Components")]

    [SerializeField]
    private SlenderStateMachine _stateMachine;

    private Player _target;

    private void OnDestroy()
    {
        if(_attackState == null)
        {
            _attackState = GetComponent<SlenderAttackState>();
        }

        if(_chaseState == null)
        {
            _chaseState = GetComponent<SlenderChaseState>();
        }
    }

    public void Initialize(Player target, Terrain terrain, PagesService pagesService)
    {
        _target = target;

        _stateMachine = new SlenderStateMachine(new SlenderStateBase[]
        {
           _attackState,
           _chaseState,
        });

        _attackState.Initialize(target, this, pagesService, _stateMachine);

        _chaseState.Initialize(target, terrain, this, pagesService, _stateMachine);

        _stateMachine.ChangeState(SlenderStateType.Chase);
    }

    public bool InPlayerSight()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        float angle = Vector3.Angle(_target.transform.forward, transform.position - _target.transform.position);

        return angle < _visibleZoneAngle && distance < _visibleZoneDistance;
    }
}
