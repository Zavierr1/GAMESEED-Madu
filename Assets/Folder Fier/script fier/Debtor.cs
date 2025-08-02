using UnityEngine;

[CreateAssetMenu(fileName = "New Debtor", menuName = "Debt Collector/Debtor")]
public class Debtor : ScriptableObject
{
    [Header("Basic Info")]
    public string debtorName;
    public string occupation;
    public PersonalityType personality;
    public float debtAmount;
    
    [Header("Dialogues")]
    public string introDialogue;
    public string successDialogue;
    public string failureDialogue;
    
    [TextArea(3, 10)]
    public string intimidateOption;
    
    [TextArea(3, 10)]
    public string persuadeOption;
    
    [TextArea(3, 10)]
    public string neutralOption;
    
    [Header("Personality Traits")]
    [Range(0, 100)]
    public int intimidationResistance = 50;
    [Range(0, 100)]
    public int persuasionSusceptibility = 50;
    [Range(0, 100)]
    public int empathySusceptibility = 50;
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