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
    
    private static readonly Dictionary <XrEyeShapeHTC, EyeAction> ShapeMap = new()
    {
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_BLINK_HTC, EyeAction.EyeLeftBlink},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_WIDE_HTC, EyeAction.EyeLeftWide},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_BLINK_HTC, EyeAction.EyeRightBlink},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_WIDE_HTC, EyeAction.EyeRightWide},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_SQUEEZE_HTC, EyeAction.EyeLeftSqueeze},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_SQUEEZE_HTC,EyeAction.EyeRightSqueeze},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_DOWN_HTC, EyeAction.EyeLeftDown},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_DOWN_HTC,EyeAction.EyeRightDown},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_OUT_HTC,EyeAction.EyeLeftLeft},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_IN_HTC,EyeAction.EyeRightLeft}, 
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_IN_HTC, EyeAction.EyeLeftRight},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_OUT_HTC, EyeAction.EyeRightRight},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_UP_HTC, EyeAction.EyeLeftUp},
        {XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_UP_HTC ,EyeAction.EyeRightUp}
    };
    
    private FacialManager _facialManager;
    private Dictionary<XrEyeShapeHTC,float> _eyeWeightings = new();

    private void Start()
    {
        _facialManager.StartFramework(XrFacialTrackingTypeHTC.XR_FACIAL_TRACKING_TYPE_EYE_DEFAULT_HTC);
    }

    private void Update()
    {
        _facialManager.GetWeightings(out _eyeWeightings);

        for (var i = XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_BLINK_HTC;
             i < XrEyeShapeHTC.XR_EYE_EXPRESSION_MAX_ENUM_HTC;
             ++i)
        {
            Debug.Log("Eye Tracker Feedback: [" + ShapeMap[i] + "] " + _eyeWeightings[i]);
        }
    }

    private void OnDestroy()
    {
        _facialManager.StopFramework(XrFacialTrackingTypeHTC.XR_FACIAL_TRACKING_TYPE_EYE_DEFAULT_HTC);
    }
}