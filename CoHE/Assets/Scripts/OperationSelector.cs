using UnityEngine;

class OperationSelector : MonoBehaviour
{
    public SystemStateMachine systemStateMachine;
    public GameObject rotation;
    public GameObject translation;
    public GameObject rescaling;
    public GazeTracker gazeTracker;

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
        }
    }

    private void MakeVisible(bool v)
    {
        rotation.SetActive(v);
        translation.SetActive(v);
        rescaling.SetActive(v);
    }
}