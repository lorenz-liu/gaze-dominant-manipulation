using UnityEngine;
using UnityEngine.UI;

internal class InfoPanel : MonoBehaviour
{
    private bool _started;
    private bool _isRunning;
    private float _elapsedTime;

    public Text text;

    private void Awake()
    {
        _started = false;
        text.text = "00:00.000";
    }

    private void Update()
    {
        if (!_started) return;
        if (!_isRunning) return;
        
        _elapsedTime += Time.deltaTime;
        text.text = FormatTime(_elapsedTime);
    }
    
    private static string FormatTime(float time)
    {
        var minutes = (int)(time / 60.0f);
        var seconds = (int)(time % 60.0f);
        var milliseconds = (int)((time - minutes * 60 - seconds) * 1000.0f);

        return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }

    public void SetStart()
    {
        _started = true;
        _isRunning = true;
    }

    public bool GetStarted()
    {
        return _started;
    }
}