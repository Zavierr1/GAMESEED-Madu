using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    
    [Header("Daily Mission Settings")]
    [SerializeField] private int totalMissions = 3;
    [SerializeField] private int successfulMissions = 0;
    [SerializeField] private int completedMissions = 0;
    
    private List<Debtor> todayDebtors = new List<Debtor>();
    
    public UnityEvent onAllMissionsCompleted;
    
    // Public getters for UI
    public int GetSuccessCount() { return successfulMissions; }
    public int GetTotalMissions() { return totalMissions; }
    public int GetCompletedCount() { return completedMissions; }
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    private void Start()
    {
        SetupDailyMissions();
    }
    
    public void SetupDailyMissions()
    {
        todayDebtors.Clear();
        completedMissions = 0;
        successfulMissions = 0;
        
        // Always exactly 3 missions per day
        for (int i = 0; i < totalMissions; i++)
        {
            Debtor newDebtor = DebtorPool.Instance.GetRandomDebtor();
            if (newDebtor != null)
            {
                todayDebtors.Add(newDebtor);
            }
        }
        
        Debug.Log($"Daily missions setup: {todayDebtors.Count} debtors assigned");
        
        // Start first mission if available
        if (todayDebtors.Count > 0)
        {
            StartMission(0);
        }
    }
    
    public void StartMission(int debtorIndex)
    {
        if (debtorIndex < 0 || debtorIndex >= todayDebtors.Count)
            return;
            
        Debtor currentDebtor = todayDebtors[debtorIndex];
        
        // Use DialogueManager
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(currentDebtor);
        }
        else
        {
            Debug.LogError("DialogueManager instance not found!");
        }
    }
    
    public void CompleteMission(bool success)
    {
        completedMissions++;
        
        if (success)
        {
            successfulMissions++;
        }
        
        Debug.Log($"Mission {completedMissions}/{totalMissions} completed. Success: {success}");
        Debug.Log($"Success rate: {successfulMissions}/{totalMissions}");
        
        // Check if all missions completed
        if (completedMissions >= totalMissions)
        {
            Debug.Log($"All missions completed! Final result: {successfulMissions}/{totalMissions}");
            onAllMissionsCompleted.Invoke();
        }
        else
        {
            // Start next mission
            StartMission(completedMissions);
        }
    }
    
    public List<Debtor> GetTodayDebtors()
    {
        return todayDebtors;
    }
    
    // Reset for new day
    public void StartNewDay()
    {
        SetupDailyMissions();
    }
}