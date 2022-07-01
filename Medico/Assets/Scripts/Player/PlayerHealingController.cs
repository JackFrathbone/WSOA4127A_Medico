using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealingController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _healingMenuUI;
    [SerializeField] TextMeshProUGUI _CharacterNameText;
    [SerializeField] TextMeshProUGUI _CharacterStatusText;
    [SerializeField] TextMeshProUGUI _CharacterTimeLeftText;

    [SerializeField] GameObject _injuryButtonPrefab;
    [SerializeField] GameObject _injuryButtonParent;

    private NPCManager _healingTargetNPC;
    private Button _selectedTreatment;
    private Button _selectedInjury;

    //For item amounts
    [SerializeField] TextMeshProUGUI _bandageAmountText;
    [SerializeField] TextMeshProUGUI _GauzeAmountText;
    [SerializeField] TextMeshProUGUI _AntisepticAmountText;
    [SerializeField] TextMeshProUGUI _AntibioticAmountText;
    [SerializeField] TextMeshProUGUI _MedicineAmountText;
    [SerializeField] TextMeshProUGUI _HerbalremedyAmountText;

    [SerializeField] List<Injury> _failureInjuries = new List<Injury>();

    [Header("Bonuses")]
    //Determines if the antiSeptic bonus is applied
    private bool usedAntiSeptic;

    [Header("Descriptions")]
    [SerializeField] TextMeshProUGUI _treatmentDescriptionTitle;
    [SerializeField] TextMeshProUGUI _treatmentDescriptionText;

    [TextArea(0, 4)]
    [SerializeField] string _bandageDescription;
    [TextArea(0, 4)]
    [SerializeField] string _gauzeDescription;
    [TextArea(0, 4)]
    [SerializeField] string _antiSepticDescription;
    [TextArea(0, 4)]
    [SerializeField] string _antiBioticDescription;
    [TextArea(0, 4)]
    [SerializeField] string _medicineDescription;
    [TextArea(0, 4)]
    [SerializeField] string _herbalRemedyDescription;

    private void Start()
    {
        _healingMenuUI.SetActive(false);
        GameManager.Instance.playerHealingController = this;
        UnselectTreatment();
    }

    public void StartHealing(NPCManager targetNPC)
    {
        _healingTargetNPC = targetNPC;

        GameManager.Instance.Pause();
        _healingMenuUI.SetActive(true);

        //Set the static UI
        _CharacterNameText.text = _healingTargetNPC.charName;

        GenerateInjuryButtons();
    }

    public void StopHealing()
    {
        _healingTargetNPC = null;
        GameManager.Instance.UnPause();
        ClearInjuryButtons();
        _healingMenuUI.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_healingTargetNPC != null)
        {
            _CharacterStatusText.text = _healingTargetNPC.npcState.ToString();
            _CharacterTimeLeftText.text = _healingTargetNPC.GetTimeLeftToLive();

            _bandageAmountText.text = GameManager.Instance.playerInventory.GetTreatmentAmount("bandage").ToString();
            _GauzeAmountText.text = GameManager.Instance.playerInventory.GetTreatmentAmount("gauze").ToString();
            _AntisepticAmountText.text = GameManager.Instance.playerInventory.GetTreatmentAmount("antiSeptic").ToString();
            _AntibioticAmountText.text = GameManager.Instance.playerInventory.GetTreatmentAmount("antiBiotic").ToString();
            _MedicineAmountText.text = GameManager.Instance.playerInventory.GetTreatmentAmount("medicine").ToString();
            _HerbalremedyAmountText.text = GameManager.Instance.playerInventory.GetTreatmentAmount("herbalRemedy").ToString();
        }
    }

    public void SelectTreatment(Button button)
    {
        _selectedInjury = null;
        _selectedTreatment = button;

        switch (_selectedTreatment.GetComponentInChildren<TextMeshProUGUI>().text)
        {
            case "Bandage":
                _treatmentDescriptionTitle.text = "Bandage";
                _treatmentDescriptionText.text = _bandageDescription;
                break;
            case "Gauze":
                _treatmentDescriptionTitle.text = "Gauze";
                _treatmentDescriptionText.text = _gauzeDescription;
                break;
            case "Anti-Septic":
                _treatmentDescriptionTitle.text = "Anti-Septic";
                _treatmentDescriptionText.text = _antiSepticDescription;
                break;
            case "Anti-Biotic":
                _treatmentDescriptionTitle.text = "Anti-Biotic";
                _treatmentDescriptionText.text = _antiBioticDescription;
                break;
            case "Medicine":
                _treatmentDescriptionTitle.text = "Medicine";
                _treatmentDescriptionText.text = _medicineDescription;
                break;
            case "Herbal Remedy":
                _treatmentDescriptionTitle.text = "Herbal Remedy";
                _treatmentDescriptionText.text = _herbalRemedyDescription;
                break;
        }

    }

    private void SelectInjury(Button button)
    {
        _selectedInjury = button;

        //This is where the treatment takes place
        if (_selectedTreatment != null)
        {
            string _selectedTreatmentText = _selectedTreatment.GetComponentInChildren<TextMeshProUGUI>().text;
            string _selectedInjuryText = _selectedInjury.GetComponentInChildren<TextMeshProUGUI>().text;

            switch (_selectedTreatmentText)
            {
                case "Bandage":
                    if (GameManager.Instance.playerInventory.GetTreatmentAmount("bandage") > 0 && GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText) != 0)
                    {
                        GameManager.Instance.playerInventory.UseTreatment("bandage", 1);

                        if (CheckTreatmentPass(GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText)))
                        {
                            _healingTargetNPC.RemoveInjury(_selectedInjuryText, true);
                        }
                        else
                        {
                            _healingTargetNPC.AddInjury(_failureInjuries[3], true);
                        }

                        usedAntiSeptic = false;
                    }
                    break;
                case "Gauze":
                    if (GameManager.Instance.playerInventory.GetTreatmentAmount("gauze") > 0 && GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText) != 0)
                    {
                        GameManager.Instance.playerInventory.UseTreatment("gauze", 1);
                        if (CheckTreatmentPass(GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText)))
                        {
                            _healingTargetNPC.RemoveInjury(_selectedInjuryText, true);
                        }
                        else
                        {
                            _healingTargetNPC.AddInjury(_failureInjuries[3], true);
                        }

                        usedAntiSeptic = false;
                    }
                    break;
                case "Anti-Septic":
                    if (GameManager.Instance.playerInventory.GetTreatmentAmount("antiSeptic") > 0)
                    {
                        GameManager.Instance.playerInventory.UseTreatment("antiSeptic", 1);
                        usedAntiSeptic = true;
                    }
                    break;
                case "Anti-Biotic":
                    if (GameManager.Instance.playerInventory.GetTreatmentAmount("antiBiotic") > 0 && GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText) != 0)
                    {
                        GameManager.Instance.playerInventory.UseTreatment("antiBiotic", 1);
                        if (CheckTreatmentPass(GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText)))
                        {
                            _healingTargetNPC.RemoveInjury(_selectedInjuryText, true);
                        }
                        else
                        {
                            _healingTargetNPC.AddInjury(_failureInjuries[4], true);
                        }
                    }
                    break;
                case "Medicine":
                    if (GameManager.Instance.playerInventory.GetTreatmentAmount("medicine") > 0 && GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText) != 0)
                    {
                        GameManager.Instance.playerInventory.UseTreatment("medicine", 1);
                        if (CheckTreatmentPass(GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText)))
                        {
                            _healingTargetNPC.RemoveInjury(_selectedInjuryText, true);
                        }
                    }
                    break;
                case "Herbal Remedy":
                    if (GameManager.Instance.playerInventory.GetTreatmentAmount("herbalRemedy") > 0 && GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText) != 0)
                    {
                        GameManager.Instance.playerInventory.UseTreatment("herbalRemedy", 1);
                        if (CheckTreatmentPass(GetTreatmentPassPercentage(_selectedTreatmentText, _selectedInjuryText)))
                        {
                            _healingTargetNPC.RemoveInjury(_selectedInjuryText, true);
                        }
                        else
                        {
                            _healingTargetNPC.AddInjury(_failureInjuries[4], true);
                        }
                    }
                    break;
            }

            ClearInjuryButtons();
            GenerateInjuryButtons();
            UnselectTreatment();
        }
    }

    private void UnselectTreatment()
    {
        _selectedInjury = null;
        _selectedTreatment = null;
        _treatmentDescriptionTitle.text = "Doctors Notes:";
        _treatmentDescriptionText.text = "Select any treatment to read more about it, then select an injury to treat it with the currently highlighted treatment.";
    }

    private void GenerateInjuryButtons()
    {
        foreach (Injury injury in _healingTargetNPC.currentInjuries)
        {
            Button button = Instantiate(_injuryButtonPrefab, _injuryButtonParent.transform).GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = injury.injuryName;
            button.onClick.AddListener(delegate { SelectInjury(button); });
        }
    }


    private void ClearInjuryButtons()
    {
        foreach (Transform child in _injuryButtonParent.transform)
        {
            if (child.GetComponent<Button>() != null)
            {
                Destroy(child.gameObject, 0.01f);
            }
        }
    }

    private int GetTreatmentPassPercentage(string selectedTreatment, string selectedInjury)
    {
        int antiSepticBonus = 0;

        if (usedAntiSeptic)
        {
            antiSepticBonus = 10;
        }


        switch (selectedTreatment)
        {
            case "Bandage":
                if (selectedInjury == "Bleeding")
                {
                    return 90 + antiSepticBonus;
                }
                else if (selectedInjury == "Flesh Wound")
                {
                    return 80 + antiSepticBonus;
                }
                else if (selectedInjury == "Burn")
                {
                    return 65 + antiSepticBonus;
                }
                else
                {
                    return 0;
                }
            case "Gauze":
                if (selectedInjury == "Burn")
                {
                    return 90 + antiSepticBonus;
                }
                else if (selectedInjury == "Flesh Wound")
                {
                    return 80 + antiSepticBonus;
                }
                else if (selectedInjury == "Bleeding")
                {
                    return 65 + antiSepticBonus;
                }
                else
                {
                    return 0;
                }
            case "Anti-Septic":
                return 100;
            case "Anti-Biotic":
                if (selectedInjury == "Infection")
                {
                    return 90;
                }
                else if (selectedInjury == "Sickness")
                {
                    return 75;
                }
                else if (selectedInjury == "Exotic Sickness")
                {
                    return 75;
                }
                else
                {
                    return 0;
                }
            case "Medicine":
                if (selectedInjury == "Sickness")
                {
                    return 90;
                }
                else if (selectedInjury == "Exotic Sickness")
                {
                    return 65;
                }
                else
                {
                    return 0;
                }
            case "Herbal Remedy":
                if (selectedInjury == "Exotic Sickness")
                {
                    return 90;
                }
                else if (selectedInjury == "Sickness")
                {
                    return 65;
                }
                else if (selectedInjury == "Infection")
                {
                    return 65;
                }
                else
                {
                    return 0;
                }
        }

        return 0;
    }

    private bool CheckTreatmentPass(int passPercentage)
    {
        int roll = Random.Range(0, 101);

        if (roll >= passPercentage)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
