using System;
using UnityEngine;
using VIVE.FacialTracking;
using System.Collections.Generic;
using System.IO;
using VIVE;

class EyeTracker : MonoBehaviour
{
    private readonly FacialManager _facialManager = new();
    private static Dictionary<XrEyeShapeHTC, float> _eyeDataMap = new();
    private readonly string _logFolder = @"Logs\EyeData\";
    private StreamWriter _logWriter;

    private void Start()
    {
        _facialManager.StartFramework(XrFacialTrackingTypeHTC.XR_FACIAL_TRACKING_TYPE_EYE_DEFAULT_HTC);

        var creatingPath = _logFolder + 
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
            _logWriter.WriteLine("{0,20}{1,20}{2,20}{3,20}{4,20}{5,20}{6,20}{7,20}{8,20}{9,20}{10,20}{11,20}{12,20}{13,20}", "LEFT_BLINK", "LEFT_WIDE", "RIGHT_BLINK", "RIGHT_WIDE", "LEFT_SQUEEZE", "RIGHT_SQUEEZE", "LEFT_DOWN", "RIGHT_DOWN", "LEFT_OUT", "RIGHT_IN", "LEFT_IN", "RIGHT_OUT", "LEFT_UP", "RIGHT_UP");
        }
        catch (DirectoryNotFoundException)
        {
            LogHelper.Failure("Cannot find this directory! Please check if there are ambiguous symbols in the path. ");
        }
    }

    private void Update()
    {
        _facialManager.GetWeightings(out _eyeDataMap);
        WriteLog();
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
}