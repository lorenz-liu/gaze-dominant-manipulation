using System;
using UnityEngine;
using UnityEngine.Serialization;

class OperationSelector : MonoBehaviour
{
    public SystemStateMachine systemStateMachine;
    public GameObject rotation;
    public GameObject translation;
    public GameObject rescaling;
    public GameObject cancel;
    public GazeTracker gazeTracker;
    [FormerlySerializedAs("confirmCircle1")] public ConfirmCircle confirmCircle;

    private const float InteractionThreshold = 0.5f;
    private int _selectingMode;
    
    private void Start()
    {
        MakeVisible(true);
    }

    private void Update()
    {
        if (systemStateMachine.GetCurrentState() == State.ObjectSelected)
        {
            MakeVisible(true);

            var gazeX = gazeTracker.GetGazeX();
            var gazeY = gazeTracker.GetGazeY();
            
            ProcessGaze(gazeX, gazeY);
        }
        else
        {
            MakeVisible(false);
        }
    }

    private void MakeVisible(bool v)
    {
        rotation.SetActive(v);
        translation.SetActive(v);
        rescaling.SetActive(v);
        cancel.SetActive(v);
    }

    private void ProcessGaze(float x, float y)
    {
        if (x <= -InteractionThreshold && Math.Abs(y) < InteractionThreshold)
        {
            if (_selectingMode != 1)
            {
                confirmCircle.Deactivate();
                _selectingMode = 1;
                confirmCircle.Activate();
            }
            if (confirmCircle.Confirmed())
                systemStateMachine.TransitStateTo(State.ObjectTranslating);
        } else if (Math.Abs(x) < InteractionThreshold && Math.Abs(y) >= InteractionThreshold)
        {
            if (_selectingMode != 2)
            {
                confirmCircle.Deactivate();
                _selectingMode = 2;
                confirmCircle.Activate();
            }
            if (confirmCircle.Confirmed())
                systemStateMachine.TransitStateTo(State.ObjectRotating);
        } else if (x >= InteractionThreshold && Math.Abs(y) < InteractionThreshold)
        {
            if (_selectingMode != 3)
            {
                confirmCircle.Deactivate();
                _selectingMode = 3;
                confirmCircle.Activate();
            }
            if (confirmCircle.Confirmed())
                systemStateMachine.TransitStateTo(State.ObjectRescaling);
        } else if (Math.Abs(x) < InteractionThreshold && Math.Abs(y) <= -InteractionThreshold)
        {
            if (_selectingMode != 4)
            {
                confirmCircle.Deactivate();
                _selectingMode = 4;
                confirmCircle.Activate();
            }
            if (confirmCircle.Confirmed())
                systemStateMachine.TransitStateTo(State.Idle);
        }
    }
}