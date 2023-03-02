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
        File.CreateText(creatingPath);
        LogHelper.Success("Create log file at " + creatingPath);
    }

    private void Update()
    {
        _facialManager.GetWeightings(out _eyeDataMap);

        foreach (var eyeShape in _eyeDataMap.Keys)
        {
            
        }
    }
}