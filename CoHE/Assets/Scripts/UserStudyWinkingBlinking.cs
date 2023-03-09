using System;
using UnityEngine;

class UserStudyWinkingBlinking : MonoBehaviour
{
    private GameObject _target1;
    private GameObject _target2;
    private GameObject _target3;
    private GameObject _target4;
    private GameObject _target5;
    private GameObject _target6;

    private const float Radius = 4f;
    private const float Distance = 15f;

    private void Start()
    {
        _target1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _target2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _target3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _target4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _target5 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _target6 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        _target1.transform.position = new Vector3(0, Radius, Distance);
        _target2.transform.position = new Vector3(Radius / 2 * (float)Math.Sqrt(3), Radius / 2, Distance);
        _target3.transform.position = new Vector3(Radius / 2 * (float)Math.Sqrt(3), -Radius / 2, Distance);
        _target4.transform.position = new Vector3(0, -Radius, Distance);
        _target5.transform.position = new Vector3(-Radius / 2 * (float)Math.Sqrt(3), -Radius / 2, Distance);
        _target6.transform.position = new Vector3(-Radius / 2 * (float)Math.Sqrt(3), Radius / 2, Distance);
    }
}