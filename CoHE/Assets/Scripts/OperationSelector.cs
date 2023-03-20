using Unity.VisualScripting;
using UnityEngine;

class OperationSelector : MonoBehaviour
{
    [Serialize] public SystemStateMachine systemStateMachine;
    [Serialize] public GameObject rotation;
    [Serialize] public GameObject translation;
    [Serialize] public GameObject rescaling;

    private void Start()
    {
        MakeVisible(false);
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