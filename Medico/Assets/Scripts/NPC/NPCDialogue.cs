using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class NPCDialogue : ScriptableObject
{
    //Temporary solution, stops this from saving between plays
    [System.NonSerialized] public bool hasRun = false;
    [TextArea(3, 10)]
    public string[] sentences;

    [Header("Conditionals")]
    public List<WorldEvent> eventsToCheck;

    [Header("Events to Run on Finish")]
    public UnityEvent dialogueEvent;

    //Check if all relevant events have passed
    public bool CheckPass()
    {
        if (hasRun)
        {
            return false;
        }
        else if (eventsToCheck.Count == 0 && !hasRun)
        {
            return true;
        }
        else
        {
            bool tempCheck = true;

            foreach (WorldEvent worldEvent in eventsToCheck)
            {
                if (!worldEvent.eventPassed)
                {
                    tempCheck = false;
                }
            }

            return tempCheck;
        }
    }
}