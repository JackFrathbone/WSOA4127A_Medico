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

    [Header("Dialogue")]
    [SerializeField] List<NPCDialogue> dialogues = new List<NPCDialogue>();

    [Header("Settings")]
    //dead, injured, alive
    public NPCState npcState;

    public List<NPCDialogue> GetDialogues()
    {
        return dialogues;
    }
}
