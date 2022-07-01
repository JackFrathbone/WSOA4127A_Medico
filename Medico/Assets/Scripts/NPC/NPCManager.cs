using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public enum NPCState
    {
        alive,
        injured,
        dead
    }

    [Header("States")]
    public string charName;
    public List<Injury> currentInjuries = new List<Injury>();
    //dead, injured, alive
    public NPCState npcState;

    [Header("Time Left To Live")]
    public int minutesLeftToLive;
    public int hoursLeftToLive;
    private TimeSpan _timeLeftToLive = TimeSpan.Zero;
    private float _timeScale;

    [Header("Dialogue")]
    [SerializeField] List<NPCDialogue> _dialogues = new List<NPCDialogue>();

    [Header("References")]
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _timeLeftToLive = TimeSpan.FromMinutes(minutesLeftToLive) + TimeSpan.FromHours(hoursLeftToLive);
        _timeScale = GameManager.Instance.timeKeeper.GetTimescale();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (currentInjuries.Count != 0 && npcState != NPCState.dead)
        {
            UpdateNPCState(NPCState.injured);
        }
    }

    private void Update()
    {
        if (npcState == NPCState.injured)
        {
            _timeLeftToLive -= TimeSpan.FromSeconds(1 * _timeScale);

            if (_timeLeftToLive <= TimeSpan.Zero)
            {
                UpdateNPCState(NPCState.dead);
            }
        }
    }

    public List<NPCDialogue> GetDialogues()
    {
        return _dialogues;
    }

    public void AddInjury(Injury injury, bool changeTime)
    {
        currentInjuries.Add(injury);
        UpdateNPCState(NPCState.injured);

        if (changeTime)
        {
            RemoveTimeLeft(UnityEngine.Random.Range(injury.injurySeverityMin, injury.injurySeverityMax));
        }
    }

    public void RemoveInjury(string injuryName, bool changeTime)
    {
        foreach (Injury injury in currentInjuries)
        {
            if (injury.injuryName == injuryName)
            {
                currentInjuries.Remove(injury);

                if (CheckHealed() && npcState != NPCState.dead)
                {
                    UpdateNPCState(NPCState.alive);
                }

                if (changeTime)
                {
                    AddTimeLeft(UnityEngine.Random.Range(injury.injurySeverityMin, injury.injurySeverityMax));
                }

                return;
            }
        }
    }

    public string GetTimeLeftToLive()
    {
        string timeLeftText = "";

        if (npcState == NPCState.alive)
        {
            return "healthy";
        }
        else if (npcState == NPCState.dead)
        {
            return "They have passed";
        }

        if (_timeLeftToLive.Days > 2)
        {
            timeLeftText = "A couple of days";
        }
        else if (_timeLeftToLive.Days == 1)
        {
            timeLeftText = "Around a day left";
        }
        else if (_timeLeftToLive.Days == 0)
        {
            if (_timeLeftToLive.Hours >= 12)
            {
                timeLeftText = "Half a day";
            }
            else if (_timeLeftToLive.Hours < 12 && _timeLeftToLive.Hours >= 6)
            {
                timeLeftText = "Quarter of a day";
            }
            else if (_timeLeftToLive.Hours < 6 && _timeLeftToLive.Hours > 1)
            {
                timeLeftText = "A couple of hours";
            }
            else if (_timeLeftToLive.Hours == 1)
            {
                timeLeftText = "Around an hour";
            }
            else if (_timeLeftToLive.Hours == 0)
            {
                if (_timeLeftToLive.Minutes >= 30)
                {
                    timeLeftText = "Less than an hour";
                }
                else if (_timeLeftToLive.Minutes < 30 && _timeLeftToLive.Minutes >= 15)
                {
                    timeLeftText = "Less than half an hour";
                }

                else if (_timeLeftToLive.Minutes < 15 && _timeLeftToLive.Minutes > 0)
                {
                    timeLeftText = "Any minute now";
                }

                else if (_timeLeftToLive.Minutes == 0)
                {
                    timeLeftText = "They have passed";
                }
            }
        }
        return timeLeftText;
    }

    //Looks to see if the character has no more injuries
    private bool CheckHealed()
    {
        if (currentInjuries.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateNPCState(NPCState state)
    {
        npcState = state;

        if (npcState == NPCState.alive)
        {
            _spriteRenderer.color = Color.white;
        }
        else if (npcState == NPCState.injured)
        {
            _spriteRenderer.color = Color.red;
        }
        else if (npcState == NPCState.dead)
        {
            _spriteRenderer.color = Color.gray;
        }
    }

    public void AddTimeLeft(float minutes)
    {
        TimeSpan t = TimeSpan.FromMinutes(minutes);

        _timeLeftToLive += t;
    }

    public void RemoveTimeLeft(float minutes)
    {
        TimeSpan t = TimeSpan.FromMinutes(minutes);

        _timeLeftToLive -= t;
    }

    public void MoveCharacterToLocation(Transform target)
    {
        gameObject.transform.position = target.position;
    }
}
