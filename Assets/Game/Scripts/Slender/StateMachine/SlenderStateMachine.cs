using System.Collections.Generic;

public class SlenderStateMachine
{
    private SlenderStateBase _currentState;

    private Dictionary<SlenderStateType, SlenderStateBase> _states;

    public SlenderStateMachine(SlenderStateBase[] states)
    {
        _states = new Dictionary<SlenderStateType, SlenderStateBase>();

        for (int i = 0; i < states.Length; i++)
        {
            SlenderStateBase state = states[i];

            _states.Add(state.Type, state);
        }
    }

    public void ChangeState(SlenderStateType stateType)
    {
        if(_currentState != null)
        {
            _currentState.Deactivate();
        }

        _currentState = GetStateByType(stateType);

        if(_currentState == null)
        {
            throw new System.Exception($"Состояния с типом {stateType} не найдено");
        }

        _currentState.Activate();
    }

    private SlenderStateBase GetStateByType(SlenderStateType stateType)
    {
        if (!_states.ContainsKey(stateType))
        {
            return null;
        }

        return _states[stateType];
    }
}
