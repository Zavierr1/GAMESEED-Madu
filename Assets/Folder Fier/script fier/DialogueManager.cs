using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI debtorNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button intimidateButton;
    [SerializeField] private Button persuadeButton;
    [SerializeField] private Button neutralButton;
    
    private Debtor currentDebtor;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        // Hide dialogue panel initially
        dialoguePanel.SetActive(false);
        
        // Add button listeners
        intimidateButton.onClick.AddListener(() => OnDialogueChoice(DialogueChoice.Intimidate));
        persuadeButton.onClick.AddListener(() => OnDialogueChoice(DialogueChoice.Persuade));
        neutralButton.onClick.AddListener(() => OnDialogueChoice(DialogueChoice.Neutral));
    }
    
    public void StartDialogue(Debtor debtor)
    {
        currentDebtor = debtor;
        dialoguePanel.SetActive(true);
        
        // Set debtor name
        debtorNameText.text = debtor.debtorName;
        
        // Set intro dialogue
        dialogueText.text = debtor.introDialogue;
        
        // Set button text
        intimidateButton.GetComponentInChildren<TextMeshProUGUI>().text = debtor.intimidateOption;
        persuadeButton.GetComponentInChildren<TextMeshProUGUI>().text = debtor.persuadeOption;
        neutralButton.GetComponentInChildren<TextMeshProUGUI>().text = debtor.neutralOption;
    }
    
    private void OnDialogueChoice(DialogueChoice choice)
    {
        // Determine outcome based on debtor personality and player choice
        bool success = DetermineOutcome(choice);
        
        // Show outcome dialogue
        dialogueText.text = success ? currentDebtor.successDialogue : currentDebtor.failureDialogue;
        
        // Disable choice buttons
        intimidateButton.interactable = false;
        persuadeButton.interactable = false;
        neutralButton.interactable = false;
        
        // After a delay, complete the mission
        Invoke(nameof(CompleteCurrentMission), 2f);
    }
    
    private bool DetermineOutcome(DialogueChoice choice)
    {
        // Simple logic - in a full game, this would be more complex
        switch (currentDebtor.personality)
        {
            case PersonalityType.Aggressive:
                return choice == DialogueChoice.Intimidate;
            case PersonalityType.Passive:
                return choice == DialogueChoice.Persuade;
            case PersonalityType.Indecisive:
                return choice == DialogueChoice.Neutral;
            case PersonalityType.Calculating:
                return choice == DialogueChoice.Persuade;
            default:
                return Random.value > 0.5f;
        }
    }
    
    private void CompleteCurrentMission()
    {
        dialoguePanel.SetActive(false);
        
        // Reset buttons
        intimidateButton.interactable = true;
        persuadeButton.interactable = true;
        neutralButton.interactable = true;
        
        // Notify mission manager
        MissionManager.Instance.CompleteMission(true);
    }
}

public enum DialogueChoice
{
    Intimidate,
    Persuade,
    Neutral
}