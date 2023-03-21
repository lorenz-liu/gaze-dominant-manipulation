using System;
using System.Collections.Generic;
using UnityEngine;
using VIVE;

class GazeTracker : MonoBehaviour
{
    private GameObject _selectedObject;
    public SystemStateMachine systemStateMachine;
    public Raycast raycast;
    public Camera playerCamera;
    public LineRenderer gazeRay;
    public GameObject gazeRayOrigin;
    public EyeTracker eyeTracker;
    public float rotationCoefficient;
    public float translationCoefficient;
    public float rescalingCoefficient;
    public bool showGazeRay;

    private Dictionary<XrEyeShapeHTC, float> _eyeDataMap;

    private const float TranslationThreshold = 0.1f;
    
    private bool _specInit;
    private State _lastState;

    private Quaternion _initSpec;
    
    private void Start()
    {
        _specInit = false;
        _lastState = State.Idle;

        gazeRay.gameObject.SetActive(showGazeRay);
    }

    private void Update()
    {
        _eyeDataMap = eyeTracker.GetEyeDataMap();
        
        _selectedObject = raycast.GetSelectedGameObject();

        if (_lastState != systemStateMachine.GetCurrentState())
        {
            _specInit = false;
        }
        
        ListenGaze();

        _lastState = systemStateMachine.GetCurrentState();
        switch (_lastState)
        {
            case State.ObjectTranslating:
                if (!_specInit)
                {
                    _initSpec = playerCamera.transform.rotation;
                    _specInit = true;
                }

                var curSpc = playerCamera.transform.rotation;

                var xm = Math.Abs(curSpc.x - _initSpec.x) > TranslationThreshold;
                var ym = Math.Abs(curSpc.y - _initSpec.y) > TranslationThreshold;
                var zm = Math.Abs(curSpc.z - _initSpec.z) > TranslationThreshold;

                var xn = curSpc.x - _initSpec.x > 0 ? -1 : 1;
                var yn = curSpc.y - _initSpec.y > 0 ? 1 : -1;
                var zn = curSpc.z - _initSpec.z > 0 ? -1 : 1;
                
                _selectedObject.transform.position += new Vector3(
                    translationCoefficient * (ym ? yn * 0.1f * (Math.Abs(curSpc.y - _initSpec.y) - TranslationThreshold) : 0), 
                    translationCoefficient * (xm ? xn * 0.1f * (Math.Abs(curSpc.x - _initSpec.x) - TranslationThreshold) : 0), 
                    translationCoefficient * (zm ? zn * 0.1f * (Math.Abs(curSpc.z - _initSpec.z) - TranslationThreshold) : 0));

                break;
            case State.ObjectRotating:
                if (!_specInit)
                {
                    _initSpec = playerCamera.transform.rotation;
                    _specInit = true;
                }

                curSpc = playerCamera.transform.rotation;
                _selectedObject.transform.Rotate(
                    rotationCoefficient * (curSpc.x - _initSpec.x), 
                    rotationCoefficient * (curSpc.y - _initSpec.y), 
                    rotationCoefficient * (curSpc.z - _initSpec.z));

                break;
            case State.ObjectRescaling:
                if (!_specInit)
                {
                    _initSpec = playerCamera.transform.rotation;
                    _specInit = true;
                }

                curSpc = playerCamera.transform.rotation;

                var localScale = _selectedObject.transform.localScale;
                var originScale = localScale;
                var ix = originScale.x;
                var iy = originScale.y;
                var iz = originScale.z;
                var sum = ix + iy + iz;
                var diff = curSpc.y - _initSpec.y;

                localScale += new Vector3(
                    ix / sum * diff * rescalingCoefficient, 
                    iy / sum * diff * rescalingCoefficient, 
                    iz / sum * diff * rescalingCoefficient);
                _selectedObject.transform.localScale = localScale;

                break;
            case State.Idle:
                break;
            case State.ObjectSelected:
                break;
            default:
                return;
        }
    }

    private void ListenGaze()
    {
        var rayInitPos = gazeRayOrigin.transform.position;
        const int rayLength = 40;
        var cameraTransform = playerCamera.transform;
        var forward = cameraTransform.forward * rayLength;
        
        var endingX = (_eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_IN_HTC] -
                       _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_OUT_HTC]) * rayLength;
        var endingY = (_eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_UP_HTC] -
                       _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_DOWN_HTC]) * rayLength;

        endingX += forward.x;
        endingY += forward.y + cameraTransform.position.y;

        var endingPoint = new Vector3(endingX, endingY, forward.z);
        gazeRay.SetPositions(new []{rayInitPos, endingPoint});
    }
}