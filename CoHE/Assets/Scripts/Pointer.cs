using UnityEngine;

internal class Pointer : MonoBehaviour
{
    public SystemStateMachine systemStateMachine;
    private void Update()
    {
        switch (systemStateMachine.GetCurrentState())
        {
            case State.Idle:
                GetComponent<MeshRenderer>().enabled = true;
                break;
            case State.ObjectRescaling:
            case State.ObjectRotating:
            case State.ObjectSelected:
            case State.ObjectTranslating:
                GetComponent<MeshRenderer>().enabled = false;
                break;
            default:
                LogHelper.Failure("Unexpected case encountered in Pointer.cs!");
                break;
        }
    }
}