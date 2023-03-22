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
        
        var selectedSm = new StateMachine(State.ObjectSelected);
        var translatingSm = new StateMachine(State.ObjectTranslating);
        var rotatingSm = new StateMachine(State.ObjectRotating);
        var rescalingSm = new StateMachine(State.ObjectRescaling);
        
        translatingSm.AttachState(selectedSm);
        rotatingSm.AttachState(selectedSm);
        rescalingSm.AttachState(selectedSm);
        
        selectedSm.AttachState(translatingSm);
        selectedSm.AttachState(rotatingSm);
        selectedSm.AttachState(rescalingSm);
        selectedSm.AttachState(_stateMachine);
        
        _stateMachine.AttachState(selectedSm);
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