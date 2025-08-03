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

public enum DialogueStage
{
    ShowingIntro,           // Showing debtor's intro dialogue
    FirstDialogue,          // Showing first choice options
    ShowingPlayerChoice1,   // Showing what player said (first choice)
    ShowingResponse,        // Showing debtor's response to first choice
    SecondDialogue,         // Showing second choice options
    ShowingPlayerChoice2,   // Showing what player said (second choice)
    ShowingOutcome,         // Showing final success/failure dialogue
    Completed
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
    private DialogueStage currentStage;
    private SimpleDialogueChoice firstChoice;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        dialoguePanel.SetActive(false);
        
        intimidateButton.onClick.AddListener(() => OnDialogueChoice(SimpleDialogueChoice.Intimidate));
        persuadeButton.onClick.AddListener(() => OnDialogueChoice(SimpleDialogueChoice.Persuade));
        neutralButton.onClick.AddListener(() => OnDialogueChoice(SimpleDialogueChoice.Neutral));
    }
    
    private void Update()
    {
        // Check for left mouse click to continue when in certain stages
        if (Input.GetMouseButtonDown(0) && dialoguePanel.activeInHierarchy)
        {
            if (currentStage == DialogueStage.ShowingIntro)
            {
                ShowFirstDialogueChoices();
            }
            else if (currentStage == DialogueStage.ShowingPlayerChoice1)
            {
                ShowDebtorResponse();
            }
            else if (currentStage == DialogueStage.ShowingResponse)
            {
                ShowSecondDialogueChoices();
            }
            else if (currentStage == DialogueStage.ShowingPlayerChoice2)
            {
                ShowFinalOutcome();
            }
            else if (currentStage == DialogueStage.ShowingOutcome)
            {
                CompleteCurrentMission();
            }
        }
    }
    
    public void StartDialogue(Debtor debtor)
    {
        currentDebtor = debtor;
        currentStage = DialogueStage.ShowingIntro;
        dialoguePanel.SetActive(true);
        
        debtorNameText.text = debtor.debtorName;
        debtAmountText.text = $"Debt: ${debtor.debtAmount:F0}";
        dialogueText.text = debtor.introDialogue;
        
        // Hide choice buttons
        SetChoiceButtonsActive(false);
    }
    
    private void ShowFirstDialogueChoices()
    {
        currentStage = DialogueStage.FirstDialogue;
        
        // Keep button text as simple labels, not the actual dialogue content
        intimidateButton.GetComponentInChildren<TextMeshProUGUI>().text = "Intimidate";
        persuadeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Persuade";
        neutralButton.GetComponentInChildren<TextMeshProUGUI>().text = "Neutral";
        
        SetChoiceButtonsActive(true);
    }
    
    private void ShowSecondDialogueChoices()
    {
        currentStage = DialogueStage.SecondDialogue;
        
        // Keep button text as simple labels, not the actual dialogue content
        intimidateButton.GetComponentInChildren<TextMeshProUGUI>().text = "Intimidate";
        persuadeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Persuade";
        neutralButton.GetComponentInChildren<TextMeshProUGUI>().text = "Neutral";
        
        SetChoiceButtonsActive(true);
    }
    
    private void OnDialogueChoice(SimpleDialogueChoice choice)
    {
        if (currentStage == DialogueStage.FirstDialogue)
        {
            HandleFirstDialogueChoice(choice);
        }
        else if (currentStage == DialogueStage.SecondDialogue)
        {
            HandleSecondDialogueChoice(choice);
        }
    }
    
    private void HandleFirstDialogueChoice(SimpleDialogueChoice choice)
    {
        firstChoice = choice;
        
        // Show what the player said
        string playerText = "";
        switch (choice)
        {
            case SimpleDialogueChoice.Intimidate:
                playerText = currentDebtor.firstIntimidateOption;
                break;
            case SimpleDialogueChoice.Persuade:
                playerText = currentDebtor.firstPersuadeOption;
                break;
            case SimpleDialogueChoice.Neutral:
                playerText = currentDebtor.firstNeutralOption;
                break;
        }
        
        dialogueText.text = playerText;
        
        // Move to showing player choice stage (wait for click to continue)
        currentStage = DialogueStage.ShowingPlayerChoice1;
        SetChoiceButtonsActive(false);
    }
    
    private void ShowDebtorResponse()
    {
        // Show debtor's response based on first choice
        string response = "";
        switch (firstChoice)
        {
            case SimpleDialogueChoice.Intimidate:
                response = currentDebtor.responseToIntimidate;
                break;
            case SimpleDialogueChoice.Persuade:
                response = currentDebtor.responseToPersuade;
                break;
            case SimpleDialogueChoice.Neutral:
                response = currentDebtor.responseToNeutral;
                break;
        }
        
        dialogueText.text = response;
        
        // Check if first choice immediately fails
        if (ShouldImmediatelyFail(firstChoice))
        {
            currentOutcome = new SimpleMissionOutcome 
            { 
                success = false, 
                moneyCollected = 0, 
                message = "Mission failed!" 
            };
            
            // Show failure dialogue immediately
            dialogueText.text = currentDebtor.failureDialogue;
            currentStage = DialogueStage.ShowingOutcome;
            SetChoiceButtonsActive(false);
        }
        else
        {
            // Move to showing response stage (wait for click to continue)
            currentStage = DialogueStage.ShowingResponse;
            SetChoiceButtonsActive(false);
        }
    }
    
    private void HandleSecondDialogueChoice(SimpleDialogueChoice choice)
    {
        // Show what the player said
        string playerText = "";
        switch (choice)
        {
            case SimpleDialogueChoice.Intimidate:
                playerText = currentDebtor.secondIntimidateOption;
                break;
            case SimpleDialogueChoice.Persuade:
                playerText = currentDebtor.secondPersuadeOption;
                break;
            case SimpleDialogueChoice.Neutral:
                playerText = currentDebtor.secondNeutralOption;
                break;
        }
        
        dialogueText.text = playerText;
        
        // Store the second choice for outcome determination
        currentOutcome = DetermineOutcome(firstChoice, choice);
        
        // Move to showing player choice stage (wait for click to continue)
        currentStage = DialogueStage.ShowingPlayerChoice2;
        SetChoiceButtonsActive(false);
    }
    
    private void ShowFinalOutcome()
    {
        // Show final success or failure dialogue
        dialogueText.text = currentOutcome.success ? currentDebtor.successDialogue : currentDebtor.failureDialogue;
        
        if (currentOutcome.moneyCollected > 0)
        {
            Debug.Log($"Money collected: ${currentOutcome.moneyCollected}");
            if (MoneyManager.Instance != null)
            {
                MoneyManager.Instance.AddMoney(currentOutcome.moneyCollected);
            }
        }
        
        currentStage = DialogueStage.ShowingOutcome;
        SetChoiceButtonsActive(false);
    }
    
    private bool ShouldImmediatelyFail(SimpleDialogueChoice choice)
    {
        // Check if first choice immediately triggers failure
        switch (currentDebtor.personality)
        {
            case PersonalityType.Gentle: // Bu Siti/Bu Wati
                return choice == SimpleDialogueChoice.Intimidate;
            case PersonalityType.Aggressive: // Yusuf
                return choice == SimpleDialogueChoice.Intimidate;
            case PersonalityType.Arrogant: // Andri  
                return choice == SimpleDialogueChoice.Intimidate;
            default:
                return false;
        }
    }
    
    private SimpleMissionOutcome DetermineOutcome(SimpleDialogueChoice firstChoice, SimpleDialogueChoice secondChoice)
    {
        SimpleMissionOutcome outcome = new SimpleMissionOutcome();
        
        // Determine outcome based on personality and both choices
        switch (currentDebtor.personality)
        {
            case PersonalityType.Arrogant: // Andri
                outcome = DetermineAndriOutcome(firstChoice, secondChoice);
                break;
                
            case PersonalityType.Gentle: // Bu Siti/Bu Wati
                outcome = DetermineGentleOutcome(firstChoice, secondChoice);
                break;
                
            case PersonalityType.Cunning: // Pak Riko
                outcome = DetermineCunningOutcome(firstChoice, secondChoice);
                break;
                
            case PersonalityType.Aggressive: // Yusuf
                outcome = DetermineAggressiveOutcome(firstChoice, secondChoice);
                break;
                
            case PersonalityType.Humble: // Bu Rini
                outcome = DetermineHumbleOutcome(firstChoice, secondChoice);
                break;
                
            case PersonalityType.Stubborn: // Rizwan
                outcome = DetermineStubbornOutcome(firstChoice, secondChoice);
                break;
        }
        
        return outcome;
    }
    
    private SimpleMissionOutcome DetermineAndriOutcome(SimpleDialogueChoice first, SimpleDialogueChoice second)
    {
        SimpleMissionOutcome outcome = new SimpleMissionOutcome();
        
        // Success scenarios for Andri
        if ((first == SimpleDialogueChoice.Persuade && second == SimpleDialogueChoice.Neutral) ||
            (first == SimpleDialogueChoice.Neutral && second == SimpleDialogueChoice.Neutral))
        {
            outcome.success = true;
            outcome.moneyCollected = currentDebtor.debtAmount;
            outcome.message = "Payment collected successfully!";
        }
        else
        {
            // All other combinations fail
            outcome.success = false;
            outcome.moneyCollected = 0;
            outcome.message = "Failed to collect payment.";
        }
        
        return outcome;
    }
    
    private SimpleMissionOutcome DetermineGentleOutcome(SimpleDialogueChoice first, SimpleDialogueChoice second)
    {
        SimpleMissionOutcome outcome = new SimpleMissionOutcome();
        
        // Success scenarios for gentle personalities
        if ((first == SimpleDialogueChoice.Persuade && second == SimpleDialogueChoice.Neutral) ||
            (first == SimpleDialogueChoice.Neutral && second == SimpleDialogueChoice.Persuade))
        {
            outcome.success = true;
            outcome.moneyCollected = currentDebtor.debtAmount;
            outcome.message = "Payment collected successfully!";
        }
        else
        {
            outcome.success = false;
            outcome.moneyCollected = 0;
            outcome.message = "Failed to collect payment.";
        }
        
        return outcome;
    }
    
    private SimpleMissionOutcome DetermineCunningOutcome(SimpleDialogueChoice first, SimpleDialogueChoice second)
    {
        SimpleMissionOutcome outcome = new SimpleMissionOutcome();
        
        // Success scenarios for cunning personality (Pak Riko)
        if ((first == SimpleDialogueChoice.Persuade && second == SimpleDialogueChoice.Neutral) ||
            (first == SimpleDialogueChoice.Neutral && second == SimpleDialogueChoice.Neutral))
        {
            outcome.success = true;
            outcome.moneyCollected = currentDebtor.debtAmount;
            outcome.message = "Payment collected successfully!";
        }
        else
        {
            outcome.success = false;
            outcome.moneyCollected = 0;
            outcome.message = "Failed to collect payment.";
        }
        
        return outcome;
    }
    
    private SimpleMissionOutcome DetermineAggressiveOutcome(SimpleDialogueChoice first, SimpleDialogueChoice second)
    {
        SimpleMissionOutcome outcome = new SimpleMissionOutcome();
        
        // Success scenarios for aggressive personality (Yusuf)
        if ((first == SimpleDialogueChoice.Persuade && second == SimpleDialogueChoice.Persuade) ||
            (first == SimpleDialogueChoice.Persuade && second == SimpleDialogueChoice.Neutral))
        {
            outcome.success = true;
            outcome.moneyCollected = currentDebtor.debtAmount;
            outcome.message = "Payment collected successfully!";
        }
        else
        {
            outcome.success = false;
            outcome.moneyCollected = 0;
            outcome.message = "Failed to collect payment.";
        }
        
        return outcome;
    }
    
    private SimpleMissionOutcome DetermineHumbleOutcome(SimpleDialogueChoice first, SimpleDialogueChoice second)
    {
        SimpleMissionOutcome outcome = new SimpleMissionOutcome();
        
        // Success scenarios for humble personality (Bu Rini)
        if ((first == SimpleDialogueChoice.Persuade && second == SimpleDialogueChoice.Neutral) ||
            (first == SimpleDialogueChoice.Neutral && second == SimpleDialogueChoice.Neutral) ||
            (first == SimpleDialogueChoice.Neutral && second == SimpleDialogueChoice.Persuade))
        {
            outcome.success = true;
            outcome.moneyCollected = currentDebtor.debtAmount;
            outcome.message = "Payment collected successfully!";
        }
        else
        {
            outcome.success = false;
            outcome.moneyCollected = 0;
            outcome.message = "Failed to collect payment.";
        }
        
        return outcome;
    }
    
    private SimpleMissionOutcome DetermineStubbornOutcome(SimpleDialogueChoice first, SimpleDialogueChoice second)
    {
        SimpleMissionOutcome outcome = new SimpleMissionOutcome();
        
        // Success scenarios for stubborn personality (Rizwan) - harder to succeed
        if ((first == SimpleDialogueChoice.Neutral && second == SimpleDialogueChoice.Persuade))
        {
            outcome.success = true;
            outcome.moneyCollected = currentDebtor.debtAmount;
            outcome.message = "Payment collected successfully!";
        }
        else
        {
            outcome.success = false;
            outcome.moneyCollected = 0;
            outcome.message = "Failed to collect payment.";
        }
        
        return outcome;
    }
    
    private void SetChoiceButtonsActive(bool active)
    {
        intimidateButton.gameObject.SetActive(active);
        persuadeButton.gameObject.SetActive(active);
        neutralButton.gameObject.SetActive(active);
    }
    
    private void CompleteCurrentMission()
    {
        dialoguePanel.SetActive(false);
        
        // Reset dialogue state
        currentStage = DialogueStage.ShowingIntro;
        SetChoiceButtonsActive(true);
        
        // Notify mission manager
        MissionManager.Instance.CompleteMission(currentOutcome.success);
        
        // Refresh UI to show updated progress
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.RefreshUI();
        }
    }
}