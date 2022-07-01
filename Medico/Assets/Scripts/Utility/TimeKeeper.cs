using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _timeScale;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _timeUIText;
    readonly private List<TimeEvent> _timeEvents = new List<TimeEvent>();

    [Header("Data")]
    private TimeSpan _trackedTime = TimeSpan.Zero;
    private TimeSpan _timeTimer = TimeSpan.Zero;

    private void Awake()
    {
        GameManager.Instance.timeKeeper = this;
    }

    private void Start()
    {
        //Debug.Log("Time keeping has started");
        _timeUIText.text = GetTimeAsText();

        _timeTimer = _trackedTime + TimeSpan.FromHours(1);
    }

    private void Update()
    {
        _trackedTime += TimeSpan.FromSeconds(1 * _timeScale);

        if(_timeTimer <= _trackedTime)
        {
            _timeUIText.text = GetTimeAsText();

            if(_timeEvents.Count > 0)
            {
                foreach(TimeEvent timeEvent in _timeEvents)
                {
                    timeEvent.CheckTimePass(_trackedTime);
                }
            }

            _timeTimer = _trackedTime + TimeSpan.FromHours(1);
        }

        //Debug.Log(trackedTime);
    }

    private void OnDisable()
    {
        //Debug.Log("Time keeping has paused");
    }

    private void OnEnable()
    {
        //Debug.Log("Time keeping has continued");
    }

    private string GetTimeAsText()
    {
        string roughTime;

        if (_trackedTime.Hours < 6)
        {
            roughTime = "Early Morning";
        }
        else if (_trackedTime.Hours >= 6 && _trackedTime.Hours < 10)
        {
            roughTime = "Late Morning";
        }
        else if (_trackedTime.Hours >= 10 && _trackedTime.Hours < 14)
        {
            roughTime = "Midday";
        }
        else if (_trackedTime.Hours >= 14 && _trackedTime.Hours < 18)
        {
            roughTime = "Afternoon";
        }
        else if (_trackedTime.Hours >= 18 && _trackedTime.Hours < 20)
        {
            roughTime = "Evening";
        }
        else
        {
            roughTime = "Midnight";
        }

        return "Day " + _trackedTime.Days + " " + roughTime;
    }

    public void AddTimeEvent(TimeEvent timeEvent)
    {
        _timeEvents.Add(timeEvent);
    }

    public void RemoveTimeEvent(TimeEvent timeEvent)
    {
        _timeEvents.Remove(timeEvent);
    }

    public void AddTime(float minutes)
    {
        TimeSpan t = TimeSpan.FromMinutes(minutes);

        _trackedTime += t;
    }

    public float GetTimescale()
    {
        return _timeScale;
    }
}
