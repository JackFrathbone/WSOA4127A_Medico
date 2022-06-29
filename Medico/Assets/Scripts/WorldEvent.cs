using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event", order = 1)]
public class WorldEvent : ScriptableObject
{
    [Header("Settings")]
    [System.NonSerialized] public bool eventPassed;
    [TextArea(0, 3)]
    [SerializeField] string eventDescription;

    public void PassEvent()
    {
        eventPassed = true;
    }
}
