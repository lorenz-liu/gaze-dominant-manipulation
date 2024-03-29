﻿using System.Collections;
using UnityEngine;

internal class Raycast : MonoBehaviour
{
    [SerializeField]
    public Camera playerCamera;
    public Transform raycastOrigin;
    public float range;
    public float renderDuration;
    public EyeTracker eyeTracker;
    public InfoPanel infoPanel;
    public SystemStateMachine systemStateMachine;
    public Configuration configuration;
    
    private bool _winking;
    private LineRenderer _lineRenderer;
    private GameObject _currentGazingObject;
    private GameObject _currentSelectedObject;

    private void Awake()
    {
        _winking = configuration.useWinkingAsConfirmation;
    }

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        switch (systemStateMachine.GetCurrentState())
        {
            case State.Idle:
                if (_currentGazingObject != null && _currentGazingObject != _currentSelectedObject)
                {
                    if (_currentGazingObject.GetComponent<Outline>() != null)
                        _currentGazingObject.GetComponent<Outline>().enabled = false;
                    _currentGazingObject = null;
                }
        
                _lineRenderer.SetPosition(0, raycastOrigin.position);
                var rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out var hit, range))
                {
                    _lineRenderer.SetPosition(1, hit.point);
                    _currentGazingObject = hit.transform.gameObject;
                    
                    if (_currentGazingObject.GetComponent<Interactable>() == null)
                        break;
                    
                    if (_currentGazingObject.GetComponent<Outline>() != null)
                    {
                        _currentGazingObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        var outline = _currentGazingObject.AddComponent<Outline>();
                        _currentGazingObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                        _currentGazingObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                        _currentGazingObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAndSilhouette;
                        outline.enabled = true;
                    }
                }
                else
                {
                    _lineRenderer.SetPosition(1, rayOrigin + (playerCamera.transform.forward * range));
                }
        
                if (_winking ? !eyeTracker.GetWinking() : !eyeTracker.GetDoubleBlinking()) return;
                LogHelper.Success("Raycast tries to select. ");

                if (_currentGazingObject != null)
                {
                    _currentSelectedObject = _currentGazingObject;

                    if (!infoPanel.GetStarted())
                    {
                        infoPanel.SetStart();    
                    }
                    
                    var component = _currentGazingObject.GetComponent<Rigidbody>();
                    if (component != null)
                    {
                        component.useGravity = false;
                        component.drag = Mathf.Infinity;
                        component.freezeRotation = true;
                    }
                    
                    systemStateMachine.TransitStateTo(State.ObjectSelected);
                }
        
                StartCoroutine(RenderRaycast());
                break;
            case State.ObjectSelected:
            case State.ObjectTranslating:
            case State.ObjectRotating:
            case State.ObjectRescaling:
            default:
                return;
        }
    }

    private IEnumerator RenderRaycast()
    {
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(renderDuration);
        // ReSharper disable once Unity.InefficientPropertyAccess
        _lineRenderer.enabled = false;
    }

    public GameObject GetSelectedGameObject()
    {
        return _currentSelectedObject;
    }

    public void ResetSelectedObject()
    {
        var component = _currentGazingObject.GetComponent<Rigidbody>();
        if (component != null)
        {
            component.useGravity = true;
            component.drag = 0f;
            component.freezeRotation = false;
        }
        _currentGazingObject.GetComponent<Outline>().enabled = false;
        _currentSelectedObject.GetComponent<Outline>().enabled = false;
        _currentGazingObject = null;
        _currentSelectedObject = null;
    }

    public bool GetWinking()
    {
        return _winking;
    }
}