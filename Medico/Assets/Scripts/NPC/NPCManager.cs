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
        _timeScale = GameManager.instance.timeKeeper.GetTimescale();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (currentInjuries.Count != 0 && npcState != NPCState.dead)
        {
            UpdateNPCState(NPCState.injured);
        }
    }

    private void Update()
    {
        if(npcState == NPCState.injured)
        {
            _timeLeftToLive -= TimeSpan.FromSeconds(1 * _timeScale);

            if(_timeLeftToLive <= TimeSpan.Zero)
            {
                UpdateNPCState(NPCState.dead);
            }
        }
    }

    public List<NPCDialogue> GetDialogues()
    {
        return _dialogues;
    }

    public void AddInjury(Injury injury)
    {
        currentInjuries.Add(injury);
        UpdateNPCState(NPCState.injured);
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
}
