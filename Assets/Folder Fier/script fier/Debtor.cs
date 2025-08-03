using UnityEngine;

[CreateAssetMenu(fileName = "New Debtor", menuName = "Debt Collector/Debtor")]
public class Debtor : ScriptableObject
{
    [Header("Basic Info")]
    public string debtorName;
    public string occupation;
    public PersonalityType personality;
    public float debtAmount;
    
    [Header("First Dialogue Round")]
    public string introDialogue;
    
    [TextArea(3, 5)]
    public string firstIntimidateOption;
    
    [TextArea(3, 5)]
    public string firstPersuadeOption;
    
    [TextArea(3, 5)]
    public string firstNeutralOption;
    
    [Header("Responses to First Dialogue")]
    [TextArea(3, 5)]
    public string responseToIntimidate;
    
    [TextArea(3, 5)]
    public string responseToPersuade;
    
    [TextArea(3, 5)]
    public string responseToNeutral;
    
    [Header("Second Dialogue Round")]
    [TextArea(3, 5)]
    public string secondIntimidateOption;
    
    [TextArea(3, 5)]
    public string secondPersuadeOption;
    
    [TextArea(3, 5)]
    public string secondNeutralOption;
    
    [Header("Final Outcomes")]
    public string successDialogue;
    public string failureDialogue;
}

public enum PersonalityType
{
    Arrogant,      // Andri - Karyawan Swasta
    Gentle,        // Bu Wati/Bu Siti - Ibu Rumah Tangga
    Cunning,       // Pak Riko - Orang Kaya
    Aggressive,    // Yusuf - Penjudi
    Humble,        // Bu Rini - Penjual Sayur
    Stubborn       // Rizwan - Remaja Gacha Addict
}