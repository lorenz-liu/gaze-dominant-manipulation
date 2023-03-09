using UnityEngine;

namespace PilotStudy
{
    class InfoPanelWinkingBlinking : MonoBehaviour
    {
        [SerializeField] public RaycastWinkingBlinking raycast;

        private void Update()
        {
            GetComponent<UnityEngine.UI.Text>().text = GenerateInfo();
        }

        private string GenerateInfo()
        {
            return $"{"Selection Signal: ",20}{(raycast.winking ? "Winking" : "Double Blinking"),10}";
        }
    }
}