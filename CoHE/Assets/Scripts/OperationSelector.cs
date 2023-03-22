using System;
using UnityEngine;

class OperationSelector : MonoBehaviour
{
    public SystemStateMachine systemStateMachine;
    public GameObject rotation;
    public GameObject translation;
    public GameObject rescaling;
    public GameObject cancel;
    public GameObject prompts;
    public GazeTracker gazeTracker;

    private const float InteractionThreshold = 0.5f;
    
    private void Start()
    {
        MakeVisible(true);
    }

    private void Update()
    {
        if (systemStateMachine.GetCurrentState() == State.ObjectSelected)
        {
            MakeVisible(true);
            LogHelper.Success("selection panel should start. ");

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
        prompts.SetActive(v);
    }

    private void ProcessGaze(float x, float y)
    {
        if (x <= -InteractionThreshold && Math.Abs(y) < InteractionThreshold)
        {
            systemStateMachine.TransitStateTo(State.ObjectTranslating);
        } else if (Math.Abs(x) < InteractionThreshold && Math.Abs(y) >= InteractionThreshold)
        {
            systemStateMachine.TransitStateTo(State.ObjectRotating);
        } else if (x >= InteractionThreshold && Math.Abs(y) < InteractionThreshold)
        {
            systemStateMachine.TransitStateTo(State.ObjectRescaling);
        } else if (Math.Abs(x) < InteractionThreshold && Math.Abs(y) <= -InteractionThreshold)
        {
            systemStateMachine.TransitStateTo(State.Idle);
        }
    }
}