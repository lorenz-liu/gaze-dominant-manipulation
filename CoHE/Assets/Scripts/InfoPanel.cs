using UnityEngine;

internal class InfoPanel : MonoBehaviour
{
    [SerializeField] public Raycast raycast;

    private void Update()
    {
        GetComponent<UnityEngine.UI.Text>().text = GenerateInfo();
    }

    private string GenerateInfo()
    {
        return $"{"Selection Signal: ",20}{(raycast.GetWinking() ? "Winking" : "Double Blinking"),10}";
    }
}