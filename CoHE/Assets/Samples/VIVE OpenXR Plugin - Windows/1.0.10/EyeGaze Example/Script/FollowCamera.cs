using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VIVE.EyeGaze
{
    public class FollowCamera : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform camera;
        private Vector3 offset;
        void Start()
        {
            this.transform.position = new Vector3(camera.position.x, camera.position.y, camera.position.z + 1);
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = new Vector3(this.transform.position.x, camera.position.y, this.transform.position.z);
        }
    }

}
