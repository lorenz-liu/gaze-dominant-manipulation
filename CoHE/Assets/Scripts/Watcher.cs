using System;
using System.Collections.Generic;
using VIVE;

public class Watcher
{
    private bool _firstOfDoubleBlinkingOccurs;
    private const float Tolerance = 0.25f;
    private const int FrameCycle = 150;
    private int _frameCount;
    private bool _waitForAnotherBlinking = true;

    public bool DoubleBlinkingOccurs(Dictionary<XrEyeShapeHTC, float> eyeDataMap)
    {
        if (++_frameCount >= FrameCycle)
        {
            _frameCount = 0;
            _firstOfDoubleBlinkingOccurs = false;
            _waitForAnotherBlinking = false;
        }

        switch (_firstOfDoubleBlinkingOccurs)
        {
            case true when _waitForAnotherBlinking &&
                           Math.Abs(eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_BLINK_HTC] - 1) < Tolerance &&
                           Math.Abs(eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_BLINK_HTC] - 1) < Tolerance:
                _frameCount = 0;
                _firstOfDoubleBlinkingOccurs = false;
                _waitForAnotherBlinking = false;
            
                return true;
            case true when !_waitForAnotherBlinking &&
                           Math.Abs(eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_BLINK_HTC]) < Tolerance &&
                           Math.Abs(eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_BLINK_HTC]) < Tolerance:
                _waitForAnotherBlinking = true;

                return false;
            case false when !_waitForAnotherBlinking &&
                            Math.Abs(eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_BLINK_HTC]) < Tolerance &&
                            Math.Abs(eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_BLINK_HTC]) < Tolerance:
                _waitForAnotherBlinking = true;

                return false;
            case false when _waitForAnotherBlinking &&
                            Math.Abs(eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_LEFT_BLINK_HTC] - 1) < Tolerance &&
                            Math.Abs(eyeDataMap[XrEyeShapeHTC.XR_EYE_EXPRESSION_RIGHT_BLINK_HTC] - 1) < Tolerance:
                _frameCount = 0;
                _firstOfDoubleBlinkingOccurs = true;
                _waitForAnotherBlinking = false;
            
                return false;
            default:
                return false;
        }
    }
}