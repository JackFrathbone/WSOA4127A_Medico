using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class NPCDialogue
{
    //Temporary solution, stops this from saving between plays
    [System.NonSerialized] public bool hasRun = false;
    public string topic;
    [TextArea(3, 10)]
    public string[] sentences;

    [Header("Conditionals")]
    public List<WorldEvent> eventsToCheck;

    [Header("Outcomes")]
    public Clue clue;
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
  