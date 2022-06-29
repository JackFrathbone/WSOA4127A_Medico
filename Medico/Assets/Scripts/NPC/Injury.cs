using UnityEngine;

[CreateAssetMenu(fileName = "Injury", menuName = "ScriptableObjects/Injury", order = 1)]
public class Injury : ScriptableObject
{
    [Header("Data")]
    public string _injuryName;
    //Time in minutes
    public float _injurySeverityMin;
    public float _injurySeverityMax;
}
