using UnityEngine;

[CreateAssetMenu(fileName = "New Debtor", menuName = "Debt Collector/Debtor")]
public class Debtor : ScriptableObject
{
    public string debtorName;
    public PersonalityType personality;
    public float debtAmount;
    public string introDialogue;
    public string successDialogue;
    public string failureDialogue;
    
    [TextArea(3, 10)]
    public string intimidateOption;
    
    [TextArea(3, 10)]
    public string persuadeOption;
    
    [TextArea(3, 10)]
    public string neutralOption;
}

public enum PersonalityType
{
    Aggressive,
    Passive,
    Indecisive,
    Calculating
}