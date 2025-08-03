using UnityEngine;
using TMPro;
using System;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;
    
    [SerializeField] private TextMeshProUGUI dayDisplayText;
    private DayOfWeek currentDay;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        // Start on Monday
        currentDay = DayOfWeek.Monday;
        UpdateDayDisplay();
    }
    
    public void AdvanceDay()
    {
        // Cycle through days of the week
        currentDay = (DayOfWeek)(((int)currentDay + 1) % 7);
        UpdateDayDisplay();
        
        // Start new day which handles player respawn and mission setup
        MissionManager.Instance.StartNewDay();
    }
    
    private void UpdateDayDisplay()
    {
        if (dayDisplayText != null)
        {
            dayDisplayText.text = currentDay.ToString();
        }
    }
    
    public DayOfWeek GetCurrentDay()
    {
        return currentDay;
    }
}