using System;
using UnityEngine;

class ConfirmCircle : MonoBehaviour
{
    public GameObject fullCircle;
    public GameObject loadingCircle;
    
    private float _progress;
    private int _round;
    private bool _activated;
    
    private const double Tolerance = 0.0001;
    private const float OriginScale = 0.2f;

    private void Awake()
    {
        fullCircle.SetActive(false);
        loadingCircle.SetActive(false);
        _progress = 0;
        _round = 0;
        _activated = false;
    }

    private void Update()
    {
        if (!_activated) return;
        
        _progress += ++_round * 0.01f;
        loadingCircle.transform.localScale = new Vector3(OriginScale * _progress, OriginScale * _progress, loadingCircle.transform.localScale.z);
    }

    public bool Confirmed()
    {
        if (!(Math.Abs(_progress - 1) < Tolerance)) return false;
        
        Deactivate();
        return true;
    }

    public void Activate()
    {
        fullCircle.SetActive(true);
        loadingCircle.SetActive(true);
        _activated = true;
    }

    public void Deactivate()
    {
        loadingCircle.transform.localScale = new Vector3(0, 0, loadingCircle.transform.localScale.z);
        
        fullCircle.SetActive(false);
        loadingCircle.SetActive(false);
        _progress = 0;
        _round = 0;
        _activated = false;
    }
}