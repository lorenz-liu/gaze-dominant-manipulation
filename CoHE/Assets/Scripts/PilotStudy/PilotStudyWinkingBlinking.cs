using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Random = System.Random;

namespace PilotStudy
{
    class PilotStudyWinkingBlinking : MonoBehaviour
    {
        private GameObject _target;
        private const float Radius = 4f;
        private const float Distance = 15f;
        private Random _random;
        private int _interval;
        private int _countInterval;
        [NonSerialized] public int DestroyedCount;
        private Stopwatch _stopwatch;
        private int _selectingActionCount;
        private const int TestTimes = 5;

        private void Start()
        {
            _random = new Random();
            _interval = _random.Next(300, 500);
        }

        private void CreateRandomTarget()
        {
            _stopwatch ??= Stopwatch.StartNew();

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
            if (DestroyedCount >= TestTimes)
            {
                _stopwatch.Stop();
                var timeSpan = _stopwatch.Elapsed;
                const string logFolder = @"PilotStudy\WinkingVSBlinking\";
                var creatingPath = logFolder + 
                                   DateTime.Now.Year + "-" + 
                                   DateTime.Now.Month + "-" + 
                                   DateTime.Now.Day + "-" + 
                                   DateTime.Now.Hour + "-" + 
                                   DateTime.Now.Minute + "-" + 
                                   DateTime.Now.Second + "-" +
                                   DateTime.Now.Millisecond + 
                                   ".txt";
                var fs = new FileStream(creatingPath, FileMode.Create);
                fs.Dispose();
                LogHelper.Success("Create pilot study file at " + creatingPath);
                var logWriter = new StreamWriter(creatingPath);
                logWriter.WriteLine("{0,30}{1,30}{2,30}", "Time Spent", "Destroyed Count", "Selecting Action Count");
                logWriter.WriteLine("{0,30}{1,30}{2,30}", timeSpan.Hours * 60 * 60 * 1000 + timeSpan.Minutes * 60 * 1000 + timeSpan.Seconds * 1000 + timeSpan.Milliseconds, TestTimes, _selectingActionCount);

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                LogHelper.Failure("Should Quit!");
            }

            if (Input.GetKeyDown(KeyCode.Space))
                ++_selectingActionCount;
            
            if (++_countInterval <= _interval) return;
            CreateRandomTarget();
            _countInterval = 0;
            _interval = _random.Next(300, 500);
        }
    }
}