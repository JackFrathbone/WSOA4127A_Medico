using UnityEngine;

[CreateAssetMenu(fileName = "Injury", menuName = "ScriptableObjects/Injury", order = 1)]
public class Injury : ScriptableObject
{
    [Header("Data")]
    public string injuryName;
    //Time in minutes
    public float injurySeverityMin;
    public float injurySeverityMax;
}
