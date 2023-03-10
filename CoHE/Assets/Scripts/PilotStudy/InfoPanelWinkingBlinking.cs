using UnityEngine;

namespace PilotStudy
{
    class InfoPanelWinkingBlinking : MonoBehaviour
    {
        [SerializeField] 
        public RaycastWinkingBlinking raycast;

        public PilotStudyWinkingBlinking study;
        
        private void Update()
        {
            GetComponent<UnityEngine.UI.Text>().text = GenerateInfo();
        }

        private string GenerateInfo()
        {
            return $"{"Selection Signal: ",20}{(raycast.winking ? "Winking" : "Double Blinking"),10}\n" +
                   $"{"Destroyed Spheres: ",20}{study.DestroyedCount, 10}";
        }
    }
}