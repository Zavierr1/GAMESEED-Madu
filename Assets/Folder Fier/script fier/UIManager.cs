using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button nextDayButton;
    
    private void Start()
    {
        // Hide next day button initially
        nextDayButton.gameObject.SetActive(false);
        
        // Subscribe to mission completion event
        MissionManager.Instance.onAllMissionsCompleted.AddListener(OnAllMissionsCompleted);
        
        // Add button listener
        nextDayButton.onClick.AddListener(OnNextDayClicked);
    }
    
    private void OnAllMissionsCompleted()
    {
        // Show next day button
        nextDayButton.gameObject.SetActive(true);
    }
    
    private void OnNextDayClicked()
    {
        // Hide the button
        nextDayButton.gameObject.SetActive(false);
        
        // Advance to next day
        DayManager.Instance.AdvanceDay();
    }
}