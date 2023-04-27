using UnityEngine;

internal class AuxiliaryLine : MonoBehaviour
{
    public SystemStateMachine systemStateMachine;
    public Raycast raycast;
    public LineRenderer lineX;
    public LineRenderer lineY;
    public LineRenderer lineZ;

    private void Update()
    {
        if (systemStateMachine.GetCurrentState() != State.ObjectTranslating)
        {
            lineX.enabled = false;
            lineY.enabled = false;
            lineZ.enabled = false;
            return;
        }
        
        var selected = raycast.GetSelectedGameObject();
        if (selected == null) return;
        var originPosition = selected.transform.position;
        
        var x1 = new Vector3(originPosition.x + 100, originPosition.y, originPosition.z);
        var x2 = new Vector3(originPosition.x - 100, originPosition.y, originPosition.z);
        lineX.SetPositions(new []{x1, x2});
            
        var y1 = new Vector3(originPosition.x, originPosition.y + 100, originPosition.z);
        var y2 = new Vector3(originPosition.x, originPosition.y - 100, originPosition.z);
        lineY.SetPositions(new []{y1, y2});
            
        var z1 = new Vector3(originPosition.x, originPosition.y, originPosition.z + 100);
        var z2 = new Vector3(originPosition.x, originPosition.y, originPosition.z - 100);
        lineZ.SetPositions(new []{z1, z2});
        
        lineX.enabled = true;
        lineY.enabled = true;
        lineZ.enabled = true;
    }
}