using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private Button nextDayButton;
    
    [Header("Mission Progress UI Elements")]
    [SerializeField] private TextMeshProUGUI missionProgressText;
    [SerializeField] private TextMeshProUGUI successRateText;
    
    private void Start()
    {
        // Hide next day button initially
        nextDayButton.gameObject.SetActive(false);
        
        // Subscribe to mission completion event
        MissionManager.Instance.onAllMissionsCompleted.AddListener(OnAllMissionsCompleted);
        
        // Add button listener
        nextDayButton.onClick.AddListener(OnNextDayClicked);
        
        // Initial UI update
        UpdateMissionProgressUI();
    }
    
    private void UpdateMissionProgressUI()
    {
        if (MissionManager.Instance != null)
        {
            int successCount = MissionManager.Instance.GetSuccessCount();
            int totalMissions = MissionManager.Instance.GetTotalMissions();
            int completedCount = MissionManager.Instance.GetCompletedCount();
            
            if (missionProgressText != null)
            {
                missionProgressText.text = $"Missions: {completedCount}/{totalMissions} (Success: {successCount})";
            }
            
            if (successRateText != null)
            {
                float percentage = totalMissions > 0 ? (float)successCount / totalMissions * 100f : 0f;
                successRateText.text = $"Success Rate: {percentage:F0}%";
            }
        }
    }
    
    private void OnAllMissionsCompleted()
    {
        // Update UI one final time
        UpdateMissionProgressUI();
        
        // Show next day button
        nextDayButton.gameObject.SetActive(true);
    }
    
    // Public method to refresh UI (call this after mission completion)
    public void RefreshUI()
    {
        UpdateMissionProgressUI();
    }
    
    private void OnNextDayClicked()
    {
        // Hide the button
        nextDayButton.gameObject.SetActive(false);
        
        // Advance to next day
        DayManager.Instance.AdvanceDay();
    }
}