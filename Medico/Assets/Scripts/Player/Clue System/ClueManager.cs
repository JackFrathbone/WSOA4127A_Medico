using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _cluePrefab;
    [SerializeField] GameObject _journalClueParent;
    private List<Clue> cluesFound = new List<Clue>();

    private void Start()
    {
        GameManager.Instance.clueManager = this;
    }

    public void AddClue(Clue clue)
    {
        cluesFound.Add(clue);

        GameObject newClueText = Instantiate(_cluePrefab, _journalClueParent.transform);
        newClueText.GetComponent<TextMeshProUGUI>().text = clue.GetClueText();
    }
}
