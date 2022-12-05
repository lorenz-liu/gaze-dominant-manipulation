using UnityEngine;
using VIVE.FacialTracking;
using System.Collections.Generic;
using VIVE;

public class EyeTracker : MonoBehaviour
{
    private enum EyeAction
    {
        EyeLeftBlink, 
        EyeLeftWide,
        EyeLeftRight,
        EyeLeftLeft,
        EyeLeftUp,
        EyeLeftDown,
        EyeLeftSqueeze,
        EyeRightBlink, 
        EyeRightWide,
        EyeRightRight,
        EyeRightLeft,
        EyeRightUp,
        EyeRightDown,
        EyeRightSqueeze
    }
    
    private static Dictionary <XrEyeShapeHTC, EyeAction> _shapeMap;
    private FacialManager _facialManager;
    private Dictionary<XrEyeShapeHTC,float> _eyeWeightings = new();

    private void Start()
    {
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_BLINK_HTC, EyeAction.EyeLeftBlink);
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_WIDE_HTC, EyeAction.EyeLeftWide);
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_BLINK_HTC, EyeAction.EyeRightBlink);
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_WIDE_HTC, EyeAction.EyeRightWide );
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_SQUEEZE_HTC, EyeAction.EyeLeftSqueeze );
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_SQUEEZE_HTC,EyeAction.EyeRightSqueeze );
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_DOWN_HTC, EyeAction.EyeLeftDown);
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_DOWN_HTC,EyeAction.EyeRightDown);
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_OUT_HTC,EyeAction.EyeLeftLeft );
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_IN_HTC,EyeAction.EyeRightLeft ); 
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_IN_HTC, EyeAction.EyeLeftRight );
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_OUT_HTC, EyeAction.EyeRightRight );
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_UP_HTC, EyeAction.EyeLeftUp );
        _shapeMap.Add(XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_UP_HTC ,EyeAction.EyeRightUp );
        
        _facialManager.StartFramework(XrFacialTrackingTypeHTC.XR_FACIAL_TRACKING_TYPE_EYE_DEFAULT_HTC);
    }

    private void Update()
    {
        _facialManager.GetWeightings(out _eyeWeightings);

        for (var i = XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_BLINK_HTC;
             i < XrEyeShapeHTC.XR_EYE_EXPRESSION_MAX_ENUM_HTC;
             ++i)
        {
            Debug.Log("Eye Tracker Feedback: [" + _shapeMap[i] + "] " + _eyeWeightings[i]);
        }
    }

    private void OnDestroy()
    {
        _facialManager.StopFramework(XrFacialTrackingTypeHTC.XR_FACIAL_TRACKING_TYPE_EYE_DEFAULT_HTC);
    }
}