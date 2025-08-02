using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SimpleMissionOutcome
{
    public bool success;
    public float moneyCollected;
    public string message;
}

public enum SimpleDialogueChoice
{
    Intimidate,
    Persuade,
    Neutral
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI debtorNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI debtAmountText;
    [SerializeField] private Button intimidateButton;
    [SerializeField] private Button persuadeButton;
    [SerializeField] private Button neutralButton;
    
    private Debtor currentDebtor;
    private SimpleMissionOutcome currentOutcome;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        dialoguePanel.SetActive(false);
        
        intimidateButton.onClick.AddListener(() => OnDialogueChoice(SimpleDialogueChoice.Intimidate));
        persuadeButton.onClick.AddListener(() => OnDialogueChoice(SimpleDialogueChoice.Persuade));
        neutralButton.onClick.AddListener(() => OnDialogueChoice(SimpleDialogueChoice.Neutral));
    }
    
    public void StartDialogue(Debtor debtor)
    {
        currentDebtor = debtor;
        dialoguePanel.SetActive(true);
        
        debtorNameText.text = debtor.debtorName;
        debtAmountText.text = $"Debt: ${debtor.debtAmount:F0}";
        dialogueText.text = debtor.introDialogue;
        
        intimidateButton.GetComponentInChildren<TextMeshProUGUI>().text = debtor.intimidateOption;
        persuadeButton.GetComponentInChildren<TextMeshProUGUI>().text = debtor.persuadeOption;
        neutralButton.GetComponentInChildren<TextMeshProUGUI>().text = debtor.neutralOption;
    }
    
    private void OnDialogueChoice(SimpleDialogueChoice choice)
    {
        currentOutcome = DetermineOutcome(choice);
        
        dialogueText.text = currentOutcome.success ? currentDebtor.successDialogue : currentDebtor.failureDialogue;
        
        intimidateButton.interactable = false;
        persuadeButton.interactable = false;
        neutralButton.interactable = false;
        
        if (currentOutcome.moneyCollected > 0)
        {
            Debug.Log($"Money collected: ${currentOutcome.moneyCollected}");
        }
        
        Invoke(nameof(CompleteCurrentMission), 2f);
    }
    
    private SimpleMissionOutcome DetermineOutcome(SimpleDialogueChoice choice)
    {
        SimpleMissionOutcome outcome = new SimpleMissionOutcome();
        bool success = false;
        
        // Simple success logic based on personality
        switch (currentDebtor.personality)
        {
            case PersonalityType.Arrogant: // Andri
                success = (choice == SimpleDialogueChoice.Neutral || choice == SimpleDialogueChoice.Persuade);
                break;
                
            case PersonalityType.Gentle: // Bu Siti
                success = (choice == SimpleDialogueChoice.Persuade || choice == SimpleDialogueChoice.Neutral);
                break;
                
            case PersonalityType.Cunning: // Pak Riko
                success = (choice == SimpleDialogueChoice.Neutral || choice == SimpleDialogueChoice.Persuade);
                break;
                
            case PersonalityType.Aggressive: // Yusuf
                success = (choice == SimpleDialogueChoice.Persuade || choice == SimpleDialogueChoice.Neutral);
                break;
                
            case PersonalityType.Humble: // Bu Rini
                success = (choice == SimpleDialogueChoice.Persuade || choice == SimpleDialogueChoice.Neutral);
                break;
                
            case PersonalityType.Stubborn: // Rizwan
                success = (choice == SimpleDialogueChoice.Persuade);
                break;
        }
        
        outcome.success = success;
        outcome.moneyCollected = success ? currentDebtor.debtAmount : 0;
        outcome.message = success ? "Payment collected!" : "Failed - can revisit";
        
        return outcome;
    }
    
    private void CompleteCurrentMission()
    {
        dialoguePanel.SetActive(false);
        
        intimidateButton.interactable = true;
        persuadeButton.interactable = true;
        neutralButton.interactable = true;
        
        // Notify mission manager
        MissionManager.Instance.CompleteMission(currentOutcome.success);
    }
}