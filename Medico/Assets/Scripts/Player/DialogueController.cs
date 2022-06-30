using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [Header("Settings")]
    private int _onDialogue;
    private bool _dialogueEnded;


    [Header("References")]
    [SerializeField] GameObject _dialogueUI;
    [SerializeField] TextMeshProUGUI _dialogueCharacterName;
    [SerializeField] TextMeshProUGUI _dialogueBodyText;
    [SerializeField] TextMeshProUGUI _dialogueButtonText;

    private NPCManager _currentTargetNPC;
    private NPCDialogue _currentTargetDialogue;

 
    public void StartDialogue(NPCManager npcManager)
    {
        _currentTargetNPC = npcManager;
        GetDialogue();
        _onDialogue = 0;
        _dialogueEnded = false;

        if (_currentTargetDialogue != null)
        {
            CheckDialogueEnded();

            _dialogueUI.SetActive(true);
            _dialogueCharacterName.text = _currentTargetNPC.charName;
            _dialogueBodyText.text = _currentTargetDialogue.sentences[_onDialogue];

            _currentTargetDialogue.hasRun = true;
            _currentTargetDialogue.dialogueEvent.Invoke();

            if(_currentTargetDialogue.clue != null)
            {
                GameManager.Instance.clueManager.AddClue(_currentTargetDialogue.clue);
            }
        }
        else
        {
            _dialogueUI.SetActive(true);
            _dialogueCharacterName.text = _currentTargetNPC.charName;
            _dialogueBodyText.text = "This person has no more to say to you";
            _dialogueEnded = true;
            _dialogueButtonText.text = "Exit";
        }

    }

    public void ActivateButton()
    {
        if (!_dialogueEnded)
        {
            _onDialogue++;
            CheckDialogueEnded();
            _dialogueBodyText.text = _currentTargetDialogue.sentences[_onDialogue];
        }
        else 
        {
            _dialogueUI.SetActive(false);
            GameManager.Instance.UnPause();
            GameManager.Instance.StartClock();
        }

    }

    private void GetDialogue()
    {
        _currentTargetDialogue = null;

        foreach (NPCDialogue dialogue in _currentTargetNPC.GetDialogues())
        {
            if (dialogue.CheckPass())
            {
                _currentTargetDialogue = dialogue;
            }
        }
    }

    private void CheckDialogueEnded()
    {
        if(_onDialogue + 1 >= _currentTargetDialogue.sentences.Length)
        {
            _dialogueEnded = true;
            _dialogueButtonText.text = "Exit";
        }
        else
        {
            _dialogueButtonText.text = "Next";
        }
    }
}
