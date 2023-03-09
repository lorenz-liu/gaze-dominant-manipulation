using System;
using UnityEngine;
using Random = System.Random;

class UserStudyWinkingBlinking : MonoBehaviour
{
    private GameObject _target;

    private const float Radius = 4f;
    private const float Distance = 15f;

    private Random _random;
    private int _interval;
    private int _countInterval;

    private void Start()
    {
        _random = new Random();
        _interval = _random.Next(300, 500);
    }

    private void CreateRandomTarget()
    {
        switch (_random.Next(1, 7))
        {
            case 1:
                _target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _target.transform.position = new Vector3(0, Radius, Distance);
                break;
            case 2:
                _target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _target.transform.position = new Vector3(Radius / 2 * (float)Math.Sqrt(3), Radius / 2, Distance);
                break;    
            case 3:
                _target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _target.transform.position = new Vector3(Radius / 2 * (float)Math.Sqrt(3), -Radius / 2, Distance);
                break;    
            case 4:
                _target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _target.transform.position = new Vector3(0, -Radius, Distance);
                break;    
            case 5:
                _target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _target.transform.position = new Vector3(-Radius / 2 * (float)Math.Sqrt(3), -Radius / 2, Distance);
                break;    
            case 6:
                _target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _target.transform.position = new Vector3(-Radius / 2 * (float)Math.Sqrt(3), Radius / 2, Distance);
                break;
                
        }
    }

    private void Update()
    {
        if (++_countInterval <= _interval) return;
        CreateRandomTarget();
        _countInterval = 0;
        _interval = _random.Next(300, 500);
    }
}