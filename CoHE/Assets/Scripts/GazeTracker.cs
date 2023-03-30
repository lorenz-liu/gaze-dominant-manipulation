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
    private const float InteractionThreshold = 0.15f;
    private bool _specInit;
    private State _lastState;
    private Quaternion _initSpec;
    private float _gazeX;
    private float _gazeY;
    
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

        if (!_specInit)
        {
            _initSpec = playerCamera.transform.rotation;
            _specInit = true;
        }
        
        _lastState = systemStateMachine.GetCurrentState();
        switch (_lastState)
        {
            case State.ObjectTranslating:
                if (eyeTracker.GetDoubleBlinking())
                    systemStateMachine.TransitStateTo(State.ObjectSelected);
                
                var curSpc = playerCamera.transform.rotation;

                var xm = Math.Abs(_gazeX) > InteractionThreshold;
                var ym = Math.Abs(_gazeY) > InteractionThreshold;
                var zm = Math.Abs(curSpc.z - _initSpec.z) > InteractionThreshold;

                var xn = _gazeX > 0 ? 1 : -1;
                var yn = _gazeY > 0 ? 1 : -1;
                var zn = curSpc.z - _initSpec.z > 0 ? -1 : 1;
                
                _selectedObject.transform.position += new Vector3(
                    translationCoefficient * (xm ? xn * Activate(Math.Abs(_gazeX) - InteractionThreshold) : 0), 
                    translationCoefficient * (ym ? yn * Activate(Math.Abs(_gazeY) - InteractionThreshold) : 0), 
                    translationCoefficient * (zm ? zn * Activate(Math.Abs(curSpc.z - _initSpec.z) - InteractionThreshold) : 0));

                break;
            case State.ObjectRotating:
                if (eyeTracker.GetDoubleBlinking())
                    systemStateMachine.TransitStateTo(State.ObjectSelected);
                
                xm = Math.Abs(_gazeX) > InteractionThreshold;
                ym = Math.Abs(_gazeY) > InteractionThreshold;
                
                xn = _gazeX > 0 ? -1 : 1;
                yn = _gazeY > 0 ? -1 : 1;

                _selectedObject.transform.Rotate(Vector3.up, rotationCoefficient * (xm ? xn * Activate(Math.Abs(_gazeX) - InteractionThreshold) : 0));
                _selectedObject.transform.Rotate(Vector3.right, rotationCoefficient * (ym ? yn * Activate(Math.Abs(_gazeY) - InteractionThreshold) : 0));

                break;
            case State.ObjectRescaling:
                if (eyeTracker.GetDoubleBlinking())
                    systemStateMachine.TransitStateTo(State.ObjectSelected);
                
                var localScale = _selectedObject.transform.localScale;
                var originScale = localScale;
                var ix = originScale.x;
                var iy = originScale.y;
                var iz = originScale.z;
                var sum = ix + iy + iz;
                
                xm = Math.Abs(_gazeX) > InteractionThreshold;
                
                xn = _gazeX > 0 ? 1 : -1;

                localScale += new Vector3(
                    ix / sum * (xm ? xn * Activate(Math.Abs(_gazeX) - InteractionThreshold) : 0) * rescalingCoefficient, 
                    iy / sum * (xm ? xn * Activate(Math.Abs(_gazeX) - InteractionThreshold) : 0) * rescalingCoefficient, 
                    iz / sum * (xm ? xn * Activate(Math.Abs(_gazeX) - InteractionThreshold) : 0) * rescalingCoefficient);
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

    private float Activate(float d)
    {
        return d * d * d;
    }

    private void ListenGaze()
    {
        var rayInitPos = gazeRayOrigin.transform.position;
        const int rayLength = 40;
        var cameraTransform = playerCamera.transform;
        var forward = cameraTransform.forward * rayLength;

        _gazeX = _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_IN_HTC] -
                 _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_OUT_HTC];
        _gazeY = _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_UP_HTC] -
                 _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_DOWN_HTC];

        var endingX = _gazeX * rayLength + forward.x;
        var endingY = _gazeY * rayLength + forward.y + cameraTransform.position.y;

        var endingPoint = new Vector3(endingX, endingY, forward.z);
        gazeRay.SetPositions(new []{rayInitPos, endingPoint});
    }

    public float GetGazeX()
    {
        return _gazeX;
    }

    public float GetGazeY()
    {
        return _gazeY;
    }
}