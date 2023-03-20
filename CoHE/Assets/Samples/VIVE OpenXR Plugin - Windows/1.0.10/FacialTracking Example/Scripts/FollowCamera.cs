using UnityEngine;

namespace Samples.VIVE_OpenXR_Plugin___Windows._1._0._10.FacialTracking_Example.Scripts
{
    public class FollowCamera : MonoBehaviour
    {
        // Start is called before the first frame update
        public new Transform camera;
        private Vector3 _offset;
        void Start()
        {
            var position = camera.position;
            transform.position = new Vector3(position.x, position.y - 0.2f, position.z + 2f);
        }

        // Update is called once per frame
        void Update()
        {
            var position = camera.position;
            transform.position = new Vector3(position.x, position.y - 0.2f, position.z + 2f);
        }
    }
}

