using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ShortcutExtension;
using Valve.VR;
public class GainFactor : MonoBehaviour
{
    public float translateGain, rotationGain, scaleGain;
    public float leftSpeedAngule, rightSpeed;
    public float S_target, S_overlap,S_object;
    float distance;
    float l_main, l_top, l_left;
    public float k,sita;
    private Depth allDepthScript, tarDepthScript, objDepthScript;
    private CalculAreaUsingDepth areaScript;
    private CalculCompleteAreaUsingDepth completeAreaScript;
    private GlobalVariable globalVarScript;    // Start is called before the first frame update
    private Bounds boundsObj, boundTar;
    private Vector3 centerObj,centerTar;
    private Vector3 extObj,extTar;
    public  double xAngle, yAngle, zAngle;
    private FollowRot rotationGainsrcipt;
    //trans 

    public float MaxGainTrans=200.0f;
    public float MinGainTrans=1.0f;

    public float MaxGainTransLitte = 1.0f;
    public float MinGainTransLittle = 0.01f;
   // scale
    float MaxGainScale = 50.0f;
    float MinGainScale = 1.0f;
    
    float MinGainScaleLittle = 0.01f;
    float MaxGainScaleLitte = 1.0f;

    //rot
    float MaxGainRot = 20.0f;
    float MinGainRot = 1.0f;

    float MaxGainRotLitte = 1.0f;
    float MinGainRotLittle = 0.05f;

    public string userName;

    public bool ourMethod=false;
    public bool speedMethod=false;
    public bool traditionMethod = false;
    public bool baseLine=false;
    public bool isMaxTrans = false;
    public bool isMaxTransLitte = false;
    
    private bool isMaxScale = false;
    public bool isMinScale = false;

    public bool isMaxRot = false;
    public bool isMinRot = false;

    private float gain;
    private float rotGain;
    public SteamVR_Action_Boolean separatedGreater;
    public SteamVR_Action_Boolean separatedSmaller;
    public SteamVR_Action_Boolean reset;//重置位置以及



    public AudioClip clipSmaller;
    public AudioClip clipGreater;
    public AudioSource sourceSmaller;
    public AudioSource sourceGreater;

    public GameObject objectBunney;
    public GameObject targetBunney;

    float templeV,beforeMax,beforeMin;

    float d_OT,transX,TransY,TransZ;

    private Vector3 PreRandPos, preRandScale,initScale;
    private float   preRandRot;
    Quaternion initRotObj;

    public float transRec, scaleRec, rotRec;

    public float overlapRate;
    float SC_trans = 0.001f, MinS_trans = 0.0001f, MaxS_trans = 0.25f,
       SC_rot = 1f, MinS_rot = 0.01f, MaxS_rot = 35f;//速度的方法的两个阈值


    void Start()
    {
        
        objectBunney = GameObject.Find("stanford-bunny");
        targetBunney = GameObject.Find("StanfordBunny-Target");
        allDepthScript = GameObject.Find("RenderAllCamera").GetComponent<Depth>();
        allDepthScript = GameObject.Find("RenderAllCamera").GetComponent<Depth>();
        tarDepthScript = GameObject.Find("RenderTarCamera").GetComponent<Depth>();
        objDepthScript = GameObject.Find("RenderObjCamera").GetComponent<Depth>();
        areaScript = GameObject.Find("Area").GetComponent<CalculAreaUsingDepth>();
        completeAreaScript = GameObject.Find("Area").GetComponent<CalculCompleteAreaUsingDepth>();
        globalVarScript = GameObject.Find("GlobalVar").GetComponent<GlobalVariable>();
        rotationGainsrcipt = GameObject.Find("RotateIndicator").GetComponent<FollowRot>();
        //d_OT = Vector3.Distance(objectBunney.transform.position, targetBunney.transform.position);
        if (isMaxTrans)// 测量最大值
        {
            transX = targetBunney.transform.position.x + UnityEngine.Random.Range(0.5f, 5f);
            TransY = targetBunney.transform.position.y + UnityEngine.Random.Range(0f, 1f);
            TransZ = targetBunney.transform.position.z + UnityEngine.Random.Range(0.5f, 5f);

            Vector3 randomPos = new Vector3(transX, TransY, TransZ);
            PreRandPos = randomPos;
            objectBunney.transform.position = randomPos;

        }
        if (isMaxTransLitte || isMinRot || isMinScale)//测量最小值
        {
            

            if (isMaxTransLitte)
            {
                transX = targetBunney.transform.position.x + UnityEngine.Random.Range(0f, 0.1f);
                TransY = targetBunney.transform.position.y + UnityEngine.Random.Range(0f, 0.1f);
                TransZ = targetBunney.transform.position.z + UnityEngine.Random.Range(0f, 0.1f);
                Vector3 randomPos = new Vector3(transX, TransY, TransZ);
                PreRandPos = randomPos;
                objectBunney.transform.position = randomPos;
            }
            if (isMinScale)
            {
                transX = targetBunney.transform.position.x + UnityEngine.Random.Range(0f, 0.1f);
                TransY = targetBunney.transform.position.y + UnityEngine.Random.Range(0f, 0.1f);
                TransZ = targetBunney.transform.position.z + UnityEngine.Random.Range(0f, 0.1f);
                //同步成target
                objectBunney.transform.position = targetBunney.transform.position;
                objectBunney.transform.rotation = targetBunney.transform.rotation;

                initScale = objectBunney.transform.localScale;
                float transXS = initScale.x + UnityEngine.Random.Range(0f, 0.5f);
                Vector3 randScale = new Vector3(transXS, transXS, transXS);
                preRandScale = randScale;
                objectBunney.transform.localScale = randScale;

            }
            if (isMinRot)
            {
                //同步
                objectBunney.transform.position = targetBunney.transform.position;
                //objectBunney.transform.localScale = targetBunney.transform.localScale;
                //objectBunney.transform.rotation = targetBunney.transform.rotation;

                initRotObj = objectBunney.transform.rotation;//记录初始位置
                float degree = UnityEngine.Random.Range(1f, 10f);
                preRandRot = degree;
                objectBunney.transform.rotation = Quaternion.AngleAxis(degree, Vector3.up);
            }
        }
       
        //gain = MaxGainTransLitte;
        if (isMaxTrans|| isMaxTransLitte)
        {
            rotationGainsrcipt.R_gain = 1.0f;
            scaleGain = 1.0f;
            MaxGainTrans = 300.0f;
            
            if (isMaxTrans)
            {
                gain = (MaxGainTrans + MinGainTrans) / 2;
            }
            if (isMaxTransLitte)
            {
                gain = (MaxGainTransLitte + MinGainTransLittle) / 2;
            }
             
        }
        if (isMaxScale || isMinScale)
        {
            rotationGainsrcipt.R_gain = 1.0f;
           
            translateGain = 1.0f;
            if (isMaxScale)
            {
               
                gain = (MaxGainScale + MinGainScale) / 2;

            }
            if (isMinScale)
            {
                gain = (MaxGainScaleLitte + MinGainScaleLittle) / 2;

            }



        }
        if (isMaxRot || isMinRot)
        {
            scaleGain = 1.0f;
            translateGain = 1.0f;
            gain = 1.0f;

            if (isMaxRot)
            {
                rotationGainsrcipt.R_gain = (MaxGainRot + MinGainRot) / 2;
            }
            if (isMinRot)
            {
                rotationGainsrcipt.R_gain = (MaxGainRotLitte + MinGainRotLittle) / 2;
            }
        }

        // 测试 第二个user study

    }

    // Update is called once per frame
    void Update()
    {

        if (!globalVarScript.obj)
            return;
        
 
        if (speedMethod)
        {
            rightSpeed = globalVarScript.handSpeedScript.rightSpeed;
            leftSpeedAngule = globalVarScript.handSpeedScript.LeftSpeedAngleQua;

            //globalVarScript.fileIOScript.Writef(translateSpeed.ToString(), true, "./reightspeed.csv");

            //globalVarScript.fileIOScript.Writef(rotSpeed.ToString(), true, "./leftSpeedAngule.csv");
            //PRISM方法
            if (rightSpeed >= SC_trans)
            {
                translateGain = 1.0f;
            }
            else if (rightSpeed > MinS_trans && rightSpeed < SC_trans)
            {
                translateGain = rightSpeed / SC_trans;
            }
            else
            {
                translateGain = 0;
            }
            //rot
            Debug.Log("anglespeed:" + leftSpeedAngule);
            if (leftSpeedAngule >= SC_rot)
            {
                rotationGainsrcipt.R_gain = 1.0f;
            }
            else if (leftSpeedAngule > MinS_rot && leftSpeedAngule < SC_rot)
            {
                rotationGainsrcipt.R_gain = leftSpeedAngule / SC_rot;
            }
            else
            {
                rotationGainsrcipt.R_gain = 0;
            }
        }

        
     

        //translateGain =25.0f;//30.0f,15.0f,10.0f;   0.01f,0.02f,0.05f;
        //rotationGain = 10.0f;//15.0f,20.0f,9.0f,8.0f; 0.1f,0.05f,0.2f,0.01f;
        //scaleGain = 5.0f;//5.0f,7.0f,3.0f; 0.02f,0.01f,0.05f,0.1f

       
    }

    float CosVal(Vector3 v1, Vector3 v2)
    {
        float v1_len = (float)Math.Sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z);
        float v2_len = (float)Math.Sqrt(v2.x * v2.x + v2.y * v2.y + v2.z * v2.z);
        return Vector3.Dot(v1, v2) / (v1_len * v2_len);
    }

    void playaudio(string audioFile)
    {
        AudioClip audioclip = Resources.Load(audioFile) as AudioClip;

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = audioclip;
        audioSource.loop = false; //是否循环播放
        audioSource.Play();
    }
}
