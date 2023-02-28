using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class EyeDataTracker : MonoBehaviour
{
    private Dictionary<>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
            SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT)
        {
            Debug.Log("NOTE: Eye tracking feature is not supported! If you are using Vive Focus 3, please check if your eye tracking device is correctly attached and whether your HMD OS version is up-to-date. ");
            return;
        }
        
        
    }
}
