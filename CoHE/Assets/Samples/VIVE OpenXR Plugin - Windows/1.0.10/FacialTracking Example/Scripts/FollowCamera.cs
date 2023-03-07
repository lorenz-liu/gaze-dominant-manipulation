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
            this.transform.position = new Vector3(position.x + 0.8f, position.y, position.z + 2f);
            this.transform.Rotate(0, 30.0f, 0);
        }

        // Update is called once per frame
        void Update()
        {
            var transform1 = this.transform;
            var position = transform1.position;
            position = new Vector3(position.x, position.y, position.z);
            transform1.position = position;
        }
    }
}

