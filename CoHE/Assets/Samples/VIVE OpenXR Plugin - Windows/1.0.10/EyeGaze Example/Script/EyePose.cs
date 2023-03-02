using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using VIVE.EyeGaze;
public class EyePose : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_ActionReferencePose;
    public InputActionReference actionReferencePose { get => m_ActionReferencePose; set => m_ActionReferencePose = value; }
    Type lastActiveType_Pose = null;
    public LineRenderer rayRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (actionReferencePose != null && actionReferencePose.action != null
            && actionReferencePose.action.enabled && actionReferencePose.action.controls.Count > 0)
        {
            // Pose
            Type typeToUse_Pose = null;
            if (actionReferencePose.action.activeControl != null)
            {
                typeToUse_Pose = actionReferencePose.action.activeControl.valueType;
            }
            else
            {
                typeToUse_Pose = lastActiveType_Pose;
            }

            if (typeToUse_Pose == typeof(UnityEngine.XR.OpenXR.Input.Pose))
            {
                lastActiveType_Pose = typeof(UnityEngine.XR.OpenXR.Input.Pose);
                UnityEngine.XR.OpenXR.Input.Pose value = actionReferencePose.action.ReadValue<UnityEngine.XR.OpenXR.Input.Pose>();
                if (value.isTracked)
                {
                    Vector3 gazePosition = value.position;
                    Quaternion gazeRotation = value.rotation;
                    Quaternion orientation = new Quaternion(
                        1 * (gazeRotation.x),
                        1 * (gazeRotation.y),
                        1 * gazeRotation.z,
                        1 * gazeRotation.w);
                    Vector3 DirectionLocal = orientation * Vector3.forward;
                    DirectionLocal.x = -1 * DirectionLocal.x;
                    DirectionLocal.y = 1 * DirectionLocal.y;
                    DirectionLocal.z = -1 * DirectionLocal.z;
                    Vector3 DirectionGlobal = Camera.main.transform.TransformDirection(DirectionLocal);
                    Ray rayGlobal = new Ray(Camera.main.transform.position, DirectionGlobal);
                    RaycastHit hit;
                    int dart_board_layer_id = LayerMask.NameToLayer("NoReflection");
                    bool valid = Physics.SphereCast(rayGlobal, 0, out hit, 20, (1 << dart_board_layer_id));
                    if (valid == true)
                    {
                        DartBoard dartBoard = hit.transform.GetComponent<DartBoard>();
                        if (dartBoard != null) dartBoard.Focus(hit.point);
                    }

                    rayRenderer.SetPosition(0, Camera.main.transform.position - Camera.main.transform.up * 0.05f);
                    rayRenderer.SetPosition(1, Camera.main.transform.position+DirectionGlobal * 25);
                }
                else
                {
                    return;
                }

            }
        }
    }
}
