using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _bandageAmountText;
    [SerializeField] TextMeshProUGUI _GauzeAmountText;
    [SerializeField] TextMeshProUGUI _AntisepticAmountText;
    [SerializeField] TextMeshProUGUI _AntibioticAmountText;
    [SerializeField] TextMeshProUGUI _MedicineAmountText;
    [SerializeField] TextMeshProUGUI _HerbalremedyAmountText;

    [Header("Data")]
    [SerializeField] int _bandageAmount;
    [SerializeField] int _GauzeAmount;
    [SerializeField] int _AntisepticAmount;
    [SerializeField] int _AntibioticAmount;
    [SerializeField] int _MedicineAmount;
    [SerializeField] int _HerbalremedyAmount;


    private void Start()
    {
        GameManager.Instance.playerInventory = this;
        UpdateInventoryText();
    }

    private void UpdateInventoryText()
    {
        _bandageAmountText.text = _bandageAmount.ToString();
        _GauzeAmountText.text = _GauzeAmount.ToString();
        _AntisepticAmountText.text = _AntisepticAmount.ToString();
        _AntibioticAmountText.text = _AntibioticAmount.ToString();
        _MedicineAmountText.text = _MedicineAmount.ToString();
        _HerbalremedyAmountText.text = _HerbalremedyAmount.ToString();
    }

    private void AddTreatments(string name, int amount)
    {
        switch (name)
        {
            case "bandage":
                _bandageAmount += amount;
                break;
            case "gauze":
                _GauzeAmount += amount;
                break;
            case "antiSeptic":
                _AntisepticAmount += amount;
                break;
            case "antiBiotic":
                _AntibioticAmount += amount;
                break;
            case "medicine":
                _MedicineAmount += amount;
                break;
            case "herbalRemedy":
                _HerbalremedyAmount += amount;
                break;
        }

        UpdateInventoryText(); ;
    }

    public void UseTreatment(string name, int amount)
    {
        switch (name)
        {
            case "bandage":
                _bandageAmount -= amount;
                GameManager.Instance.timeKeeper.AddTime(15);
                break;
            case "gauze":
                _GauzeAmount -= amount;
                GameManager.Instance.timeKeeper.AddTime(15);
                break;
            case "antiSeptic":
                _AntisepticAmount -= amount;
                GameManager.Instance.timeKeeper.AddTime(10);
                break;
            case "antiBiotic":
                _AntibioticAmount -= amount;
                GameManager.Instance.timeKeeper.AddTime(30);
                break;
            case "medicine":
                _MedicineAmount -= amount;
                GameManager.Instance.timeKeeper.AddTime(45);
                break;
            case "herbalRemedy":
                _HerbalremedyAmount -= amount;
                GameManager.Instance.timeKeeper.AddTime(60);
                break;
        }

        UpdateInventoryText(); ;
    }

    public int GetTreatmentAmount(string name)
    {
        switch (name)
        {
            case "bandage":
                return _bandageAmount;
            case "gauze":
                return _GauzeAmount;
            case "antiSeptic":
                return _AntisepticAmount;
            case "antiBiotic":
                return _AntibioticAmount;
            case "medicine":
                return _MedicineAmount;
            case "herbalRemedy":
                return _HerbalremedyAmount;
        }

        return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            TreatmentPickup treatmentPickup = collision.GetComponent<TreatmentPickup>();
            AddTreatments(treatmentPickup.pickupType, treatmentPickup.pickupAmount);
            Destroy(collision.gameObject);
        }
    }
}
