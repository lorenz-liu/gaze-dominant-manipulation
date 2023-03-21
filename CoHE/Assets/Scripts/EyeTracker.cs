using System;
using UnityEngine;
using VIVE.FacialTracking;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using VIVE;

class EyeTracker : MonoBehaviour
{
    private readonly FacialManager _facialManager = new();
    private Dictionary<XrEyeShapeHTC, float> _eyeDataMap = new();
    private const string LOGFolder = @"Logs\EyeData\";
    private StreamWriter _logWriter;
    private Watcher _watcher;
    private bool _doubleBlinking;
    private bool _winking;
    
    [Serialize] public GameObject avatar;
    [Serialize] public bool enableSelectionTest;
    
    
    private void Awake()
    {
        _facialManager.StartFramework(XrFacialTrackingTypeHTC.XR_FACIAL_TRACKING_TYPE_EYE_DEFAULT_HTC);

        var creatingPath = LOGFolder + 
                           DateTime.Now.Year + "-" + 
                           DateTime.Now.Month + "-" + 
                           DateTime.Now.Day + "-" + 
                           DateTime.Now.Hour + "-" + 
                           DateTime.Now.Minute + "-" + 
                           DateTime.Now.Second + "-" +
                           DateTime.Now.Millisecond + 
                           ".txt";
        try
        {
            var fs = new FileStream(creatingPath, FileMode.Create);
            fs.Dispose();
            LogHelper.Success("Create log file at " + creatingPath);
            _logWriter = new StreamWriter(creatingPath);
            _logWriter.AutoFlush = true;
            _logWriter.WriteLine("{0,20}{1,20}{2,20}{3,20}{4,20}{5,20}{6,20}{7,20}{8,20}{9,20}{10,20}{11,20}{12,20}{13,20}", "LEFT_BLINK", "LEFT_WIDE", "RIGHT_BLINK", "RIGHT_WIDE", "LEFT_SQUEEZE", "RIGHT_SQUEEZE", "LEFT_DOWN", "RIGHT_DOWN", "LEFT_OUT", "RIGHT_IN", "LEFT_IN", "RIGHT_OUT", "LEFT_UP", "RIGHT_UP");
        }
        catch (DirectoryNotFoundException)
        {
            LogHelper.Failure("Cannot find this directory! Please check if there are ambiguous symbols in the path. ");
        }

        _watcher = new Watcher();
    }

    private void FixedUpdate()
    {
        _facialManager.GetWeightings(out _eyeDataMap);
        WriteLog();

        if (_watcher.DoubleBlinkingOccurs(_eyeDataMap))
        {
            _doubleBlinking = true;
            if (!enableSelectionTest) return;
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var sourceCor = avatar.transform.position;
            cube.transform.localScale *= 0.1f;
            cube.transform.position = new Vector3(sourceCor.x, sourceCor.y, sourceCor.z);
            var cubeRigidBody = cube.AddComponent<Rigidbody>();
            cubeRigidBody.useGravity = true;
        }
        else
        {
            _doubleBlinking = false;
        }
        
        if (_watcher.WinkingOccurs(_eyeDataMap))
        {
            _winking = true;
            if (!enableSelectionTest) return;
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var sourceCor = avatar.transform.position;
            cube.transform.localScale *= 0.1f;
            cube.transform.position = new Vector3(sourceCor.x, sourceCor.y, sourceCor.z);
            var cubeRigidBody = cube.AddComponent<Rigidbody>();
            cubeRigidBody.useGravity = true;
        }
        else
        {
            _winking = false;
        }
    }

    private void WriteLog()
    {
        if (_logWriter == null)
            return;
        
        _logWriter.WriteLine("{0,20:0.00000}{1,20:0.00000}{2,20:0.00000}{3,20:0.00000}{4,20:0.00000}{5,20:0.00000}{6,20:0.00000}{7,20:0.00000}{8,20:0.00000}{9,20:0.00000}{10,20:0.00000}{11,20:0.00000}{12,20:0.00000}{13,20:0.00000}", _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_BLINK_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_WIDE_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_BLINK_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_WIDE_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_SQUEEZE_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_SQUEEZE_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_DOWN_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_DOWN_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_OUT_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_IN_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_IN_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_OUT_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_UP_HTC], _eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_UP_HTC]);
            
        // XR_EYE_EXPRESSION_LEFT_BLINK_HTC
        // XR_EYE_EXPRESSION_LEFT_WIDE_HTC
        // XR_EYE_EXPRESSION_RIGHT_BLINK_HTC
        // XR_EYE_EXPRESSION_RIGHT_WIDE_HTC
        // XR_EYE_EXPRESSION_LEFT_SQUEEZE_HTC
        // XR_EYE_EXPRESSION_RIGHT_SQUEEZE_HTC
        // XR_EYE_EXPRESSION_LEFT_DOWN_HTC
        // XR_EYE_EXPRESSION_RIGHT_DOWN_HTC
        // XR_EYE_EXPRESSION_LEFT_OUT_HTC
        // XR_EYE_EXPRESSION_RIGHT_IN_HTC
        // XR_EYE_EXPRESSION_LEFT_IN_HTC
        // XR_EYE_EXPRESSION_RIGHT_OUT_HTC
        // XR_EYE_EXPRESSION_LEFT_UP_HTC
        // XR_EYE_EXPRESSION_RIGHT_UP_HTC
    }

    public bool GetDoubleBlinking()
    {
        return _doubleBlinking;
    }

    public bool GetWinking()
    {
        return _winking;
    }

    public Dictionary<XrEyeShapeHTC, float> GetEyeDataMap()
    {
        return _eyeDataMap;
    }
}