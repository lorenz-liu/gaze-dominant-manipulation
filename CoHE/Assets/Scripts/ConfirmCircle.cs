using UnityEngine;

internal class ConfirmCircle : MonoBehaviour
{
    public GazeTracker gazeTracker;
    public Camera playerCamera;
    public GameObject fullCircle;
    public GameObject loadingCircle;
    public Configuration configuration;

    private float _progress;
    private int _round;
    private bool _activated;
    private bool _confirmed;
    private const float OriginScale = 0.2f;

    private void Awake()
    {
        fullCircle.SetActive(false);
        loadingCircle.SetActive(false);
        _progress = 0;
        _round = 0;
        _activated = false;
        _confirmed = false;
    }

    private void Update()
    {
        if (!_activated) return;

        var forward = playerCamera.transform.forward;
        var pos = new Vector3(
            10 * (gazeTracker.GetGazeX() + forward.x),
            10 * (gazeTracker.GetGazeY() + forward.y), 
            10 * forward.z);
        transform.position = pos;

        _progress += ++_round * 0.0001f * configuration.progressBarSpeed;
        loadingCircle.transform.localScale = new Vector3(OriginScale * _progress, OriginScale * _progress, loadingCircle.transform.localScale.z);

        if (OriginScale * _progress >= OriginScale) 
            _confirmed = true;
    }

    public bool Confirmed()
    {
        return _confirmed;
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
        _confirmed = false;
    }
}