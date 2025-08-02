using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    
    [Header("Money Settings")]
    [SerializeField] private float currentMoney = 0f;
    [SerializeField] private TextMeshProUGUI moneyDisplayText;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    private void Start()
    {
        UpdateMoneyDisplay();
    }
    
    public void AddMoney(float amount)
    {
        currentMoney += amount;
        UpdateMoneyDisplay();
        Debug.Log($"Money added: {amount}. Total: {currentMoney}");
    }
    
    public void RemoveMoney(float amount)
    {
        currentMoney -= amount;
        if (currentMoney < 0) currentMoney = 0;
        UpdateMoneyDisplay();
        Debug.Log($"Money lost: {amount}. Total: {currentMoney}");
    }
    
    public void OnDialogueSuccess(float debtAmount)
    {
        AddMoney(debtAmount);
    }
    
    private void UpdateMoneyDisplay()
    {
        if (moneyDisplayText != null)
        {
            moneyDisplayText.text = $"Money: ${currentMoney:F0}";
        }
    }
    
    public float GetCurrentMoney()
    {
        return currentMoney;
    }
}
