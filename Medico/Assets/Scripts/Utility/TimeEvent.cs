using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeEvent : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] List<Injury> _injuriesToAdd;

    [Header("Events to Run on Pass")]
    public UnityEvent timePassEvent;

    [Header("Settings")]
    [System.NonSerialized] public bool eventPassed;
    [SerializeField] int _eventDay;
    [SerializeField] int _eventHour;
    private TimeSpan _eventTime;

    private void Start()
    {
        GameManager.instance.timeKeeper.AddTimeEvent(this);

        _eventTime = TimeSpan.FromDays(_eventDay) + TimeSpan.FromHours(_eventHour);
    }

    private void OnDisable()
    {
        if (!this.gameObject.scene.isLoaded) return;
        GameManager.instance.timeKeeper.RemoveTimeEvent(this);
    }

    public void CheckTimePass(TimeSpan trackedTime)
    {
        if (eventPassed)
        {
            return;
        }

        if(trackedTime >= _eventTime)
        {
            eventPassed = true;
            RunPassEvents();
        }
        else
        {
            eventPassed = false;
        }
    }

    private void RunPassEvents()
    {
        timePassEvent.Invoke();

        NPCManager npcManager = GetComponent<NPCManager>();

        if(npcManager != null)
        {
            foreach(Injury injury in _injuriesToAdd)
            {
                npcManager.AddInjury(injury);
            }
        }
    }
}
