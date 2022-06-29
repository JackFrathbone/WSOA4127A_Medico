using UnityEngine;

[CreateAssetMenu(fileName = "Clue", menuName = "ScriptableObjects/Clue", order = 1)]
public class Clue : ScriptableObject
{
    [Header("Data")]
    [TextArea(0,3)]
    [SerializeField] string _clueText;

    public string GetClueText()
    {
        return _clueText;
    }
}
