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
            return $"Selection Signal: {(raycast.winking ? "Winking" : "Double Blinking")}\n" +
                   $"Destroyed Spheres: {study.DestroyedCount}";
        }
    }
}