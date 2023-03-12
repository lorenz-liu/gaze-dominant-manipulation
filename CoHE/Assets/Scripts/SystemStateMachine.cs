using System.Collections;
using System.Linq;
using UnityEngine;

enum State
{
    Idle, 
    ObjectSelected,
    ObjectTranslating,
    ObjectRotating,
    ObjectRescaling
}

class StateMachine
{
    private readonly State _current;
    private readonly ArrayList _next;

    public StateMachine(State currentState)
    {
        _current = currentState;
        _next = new ArrayList();
    }

    public void AttachState(StateMachine next)
    {
        _next.Add(next);
    }

    public State GetCurrentState()
    {
        return _current;
    }

    public bool CanTransitTo(State state)
    {
        return _next.Cast<StateMachine>().Any(machine => machine.GetCurrentState() == state);
    }

    public StateMachine TransitTo(State state)
    {
        foreach (StateMachine sm in _next)
        {
            if (sm.GetCurrentState() == state)
                return sm;
        }
        
        LogHelper.Failure("Something went wrong when transiting state. ");

        return null;
    }
}

class SystemStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    
    private void Awake()
    {
        _stateMachine = new StateMachine(State.Idle);

        var objectTranslatingSm = new StateMachine(State.ObjectTranslating);
        var objectRotatingSm = new StateMachine(State.ObjectRotating);
        var objectRescalingSm = new StateMachine(State.ObjectRescaling);
        
        objectTranslatingSm.AttachState(_stateMachine);
        objectRotatingSm.AttachState(_stateMachine);
        objectRescalingSm.AttachState(_stateMachine);

        var objectSelectedSm = new StateMachine(State.ObjectSelected);
        objectSelectedSm.AttachState(objectTranslatingSm);
        objectSelectedSm.AttachState(objectRotatingSm);
        objectSelectedSm.AttachState(objectRescalingSm);
        
        _stateMachine.AttachState(objectSelectedSm);
    }

    public void TransitStateTo(State nextState)
    {
        if (_stateMachine.CanTransitTo(nextState))
        {
            _stateMachine = _stateMachine.TransitTo(nextState);
            LogHelper.Success("Transited to " + nextState);
        }
        else
        {
            LogHelper.Failure("The state transition is invalid! ");
        }
    }

    public State GetCurrentState()
    {
        return _stateMachine.GetCurrentState();
    }
}