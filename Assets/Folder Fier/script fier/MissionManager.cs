using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    
    [SerializeField] private int minDailyMissions = 2;
    [SerializeField] private int maxDailyMissions = 3;
    
    private List<Debtor> todayDebtors = new List<Debtor>();
    private int completedMissions = 0;
    
    public UnityEvent onAllMissionsCompleted;
    
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
        
        // Generate random number of missions for the day
        int missionCount = UnityEngine.Random.Range(minDailyMissions, maxDailyMissions + 1);
        
        // Get debtors from the pool (would be replaced with actual debtor database)
        for (int i = 0; i < missionCount; i++)
        {
            Debtor newDebtor = DebtorPool.Instance.GetRandomDebtor();
            todayDebtors.Add(newDebtor);
        }
        
        // Start first mission
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
        DialogueManager.Instance.StartDialogue(currentDebtor);
    }
    
    public void CompleteMission(bool success)
    {
        completedMissions++;
        
        // Check if all missions are completed
        if (completedMissions >= todayDebtors.Count)
        {
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
}