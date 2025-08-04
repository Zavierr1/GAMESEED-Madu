using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("Navigation")]
    [SerializeField] private Button nextDayButton;
    
    [Header("Mission Progress UI Elements")]
    [SerializeField] private TextMeshProUGUI missionProgressText;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    private void Start()
    {
        // Hide next day button initially
        if (nextDayButton != null)
        {
            nextDayButton.gameObject.SetActive(false);
            // Add button listener
            nextDayButton.onClick.AddListener(OnNextDayClicked);
        }
        
        // Subscribe to mission completion event
        if (MissionManager.Instance != null)
        {
            MissionManager.Instance.onAllMissionsCompleted.AddListener(OnAllMissionsCompleted);
        }
        
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
        }
    }
    
    public void OnAllMissionsCompleted()
    {
        // Update UI one final time
        UpdateMissionProgressUI();
        
        // Show cursor for clicking the next day button
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        // Disable player movement since they need to click the button
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            // Find any movement script and disable it
            MonoBehaviour[] scripts = playerObj.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script.GetType().Name.Contains("Movement") || 
                    script.GetType().Name.Contains("Controller") ||
                    script.GetType().Name.Contains("FP"))
                {
                    script.enabled = false;
                }
            }
        }
        
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
        // Re-enable player movement and hide cursor
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            // Find any movement script and re-enable it
            MonoBehaviour[] scripts = playerObj.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script.GetType().Name.Contains("Movement") || 
                    script.GetType().Name.Contains("Controller") ||
                    script.GetType().Name.Contains("FP"))
                {
                    script.enabled = true;
                }
            }
        }
        
        // Hide cursor and lock it back for gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        // Hide the button
        if (nextDayButton != null)
        {
            nextDayButton.gameObject.SetActive(false);
        }
        
        // Advance to next day
        if (DayManager.Instance != null)
        {
            DayManager.Instance.AdvanceDay();
        }
    }
}