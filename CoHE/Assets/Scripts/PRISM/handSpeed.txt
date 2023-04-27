using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handSpeed : MonoBehaviour
{
    public Quaternion leftLastRot, rotationPreQua, delatQua,rotationQua;
    public Vector3 rightLastVec;
    public GameObject leftHand;
    public GameObject RightHand;
    float time = 0;
    public float LeftSpeed;
    public float rightSpeed;
    public float LeftSpeedAngleQua;
    public System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    private GainFactor gainFactorSpcrit;
    // Use this for initialization
    void Start()
    {
        RightHand = GameObject.Find("Controller (right)");
        leftHand  = GameObject.Find("Controller (left)");

        gainFactorSpcrit = GameObject.Find("Gain").GetComponent<GainFactor>();
        leftLastRot = leftHand.gameObject.transform.rotation;
        rightLastVec= RightHand.gameObject.transform.localPosition;
    }
     
    // Update is called once per frame
    void Update()
    {
        //虚拟时间
        time += 0.016f;
        
        //左手旋转速度
        rotationQua = leftHand.gameObject.transform.rotation;
        delatQua = rotationQua * Quaternion.Inverse(rotationPreQua);
        rightSpeed = Vector3.Distance(RightHand.gameObject.transform.localPosition, rightLastVec);
        float temp = Mathf.Min(Mathf.Max(delatQua.w, -1.0f), 1.0f);//将w控制在-1.0-1.0之间。
        LeftSpeedAngleQua = 2 * Mathf.Acos(temp);//旋转了多少角度
        if (gainFactorSpcrit.speedMethod)
        {
            LeftSpeedAngleQua = LeftSpeedAngleQua * 10;

        }
        //计算时间运行了一帧
        if (Time.deltaTime / 60 < time)
        {
            //时间归零
            time = 0;

            //上一帧位置重新规划
            rotationPreQua = leftHand.gameObject.transform.rotation;
            rightLastVec = RightHand.gameObject.transform.localPosition;
        }

    }
}

