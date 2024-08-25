using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MediumStopWatch : MonoBehaviour
{
    private bool _timerActive;
    private float _currentTime;
    [SerializeField] private int _startMinutes;
    [SerializeField] private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _startMinutes * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerActive)
        {
            _currentTime = _currentTime - Time.deltaTime;
            //_currentTime += Time.deltaTime;
            if (_currentTime <= 0)
            {
                _timerActive = false;
                Start();
            }
        }

        /*   TimeSpan time = TimeSpan.FromSeconds(_currentTime);
           _text.text = time.Minutes.ToString();*/

        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        _text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void StartTimer()
    {
        _timerActive = true;
    }

    public void StopTimer()
    {
        _timerActive = false;
    }
}
