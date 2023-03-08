using System;
using UnityEngine;

class Raycast : MonoBehaviour
{
    [SerializeField]
    public Camera playerCamera;
    public Transform raycastOrigin;
    public float range;
    public float renderDurations;
    public EyeTracker eyeTracker;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (eyeTracker.doubleBlinking)
        {
            LogHelper.Success("RAYCAST");
        }
    }
}