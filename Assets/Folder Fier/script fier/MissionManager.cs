using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    
    [Header("Daily Mission Settings")]
    [SerializeField] private GameObject debtorCharacterPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int totalMissions = 3;
    [SerializeField] private int successfulMissions = 0;
    [SerializeField] private int completedMissions = 0;
    
    [Header("Player Respawn Settings")]
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private bool respawnPlayerOnNewDay = true;
    
    private List<Debtor> todayDebtors = new List<Debtor>();
    private Transform playerTransform;
    private Vector3 originalPlayerPosition;
    private Quaternion originalPlayerRotation;
    
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
        // Find and store player reference
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            
            // Store original player position (or use spawn point if assigned)
            if (playerSpawnPoint != null)
            {
                originalPlayerPosition = playerSpawnPoint.position;
                originalPlayerRotation = playerSpawnPoint.rotation;
            }
            else
            {
                originalPlayerPosition = playerTransform.position;
                originalPlayerRotation = playerTransform.rotation;
            }
        }
        else
        {
            Debug.LogError("Player GameObject with 'Player' tag not found!");
        }
        
        SetupDailyMissions();
    }
    
    public void SetupDailyMissions()
    {
        // Respawn player to starting position if enabled
        if (respawnPlayerOnNewDay && playerTransform != null)
        {
            RespawnPlayer();
        }
        
        // Clean up previous day's debtor characters
        DebtorCharacter[] existingDebtors = FindObjectsOfType<DebtorCharacter>();
        foreach (DebtorCharacter debtor in existingDebtors)
        {
            Destroy(debtor.gameObject);
        }
        
        todayDebtors.Clear();
        completedMissions = 0;
        successfulMissions = 0;
        
        Debug.Log("Mission progress reset: 0/3 missions completed, 0 successful missions");
        
        // --- IMPROVED LOGIC FOR RANDOM LOCATIONS ---

        // 1. Create a temporary list of all spawn points that we can modify.
        // This is like putting all the location papers back in the hat for the new day.
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        // Determine how many missions to set up based on the smaller value:
        // either the total missions desired or the number of available spawn points.
        int missionsToSetup = Mathf.Min(totalMissions, spawnPoints.Length);

        for (int i = 0; i < missionsToSetup; i++)
        {
            // Safety check: if we've somehow run out of spawn points, stop.
            if (availableSpawnPoints.Count == 0) break;

            // Safety check: ensure DebtorPool exists
            if (DebtorPool.Instance == null)
            {
                Debug.LogError("DebtorPool.Instance is null! Make sure DebtorPool is in the scene.");
                break;
            }

            Debtor newDebtor = DebtorPool.Instance.GetRandomDebtor();
            if (newDebtor != null)
            {
                todayDebtors.Add(newDebtor);

                // 2. Pick a random location from the list of REMAINING spawn points.
                int randomIndex = Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[randomIndex];
            
                // 3. Spawn the character at that randomly selected point.
                GameObject debtorGO = Instantiate(debtorCharacterPrefab, spawnPoint.position, spawnPoint.rotation);
                DebtorCharacter debtorCharacter = debtorGO.GetComponent<DebtorCharacter>();
                if (debtorCharacter != null)
                {
                    debtorCharacter.SetDebtorData(newDebtor);
                }
                else
                {
                    Debug.LogError("DebtorCharacter component not found on prefab!");
                }
            
                // 4. Remove the used location from the temporary list for this day
                // to ensure no two debtors spawn in the same place.
                availableSpawnPoints.RemoveAt(randomIndex);
            }
        }
        
        Debug.Log($"Daily missions setup: {todayDebtors.Count} debtors spawned at random locations.");
        
        // Refresh UI to show reset mission progress (0/3, Success: 0)
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.RefreshUI();
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
        
        // Check if all missions are completed for the day.
        if (completedMissions >= todayDebtors.Count)
        {
            Debug.Log($"All missions completed! Final result: {successfulMissions}/{totalMissions}");
            
            // Respawn player when all missions are finished
            if (respawnPlayerOnNewDay && playerTransform != null)
            {
                RespawnPlayer();
            }
            
            onAllMissionsCompleted.Invoke();
        }
    }
    
    public List<Debtor> GetTodayDebtors()
    {
        return todayDebtors;
    }
    
    // This function is called by DayManager to start a new day.
    public void StartNewDay()
    {
        SetupDailyMissions();
    }
    
    /// <summary>
    /// Respawns the player to the original spawn position
    /// </summary>
    private void RespawnPlayer()
    {
        if (playerTransform != null)
        {
            // Get the CharacterController component (if using SimpleFPController)
            CharacterController characterController = playerTransform.GetComponent<CharacterController>();
            
            if (characterController != null)
            {
                // Disable CharacterController temporarily to allow position change
                characterController.enabled = false;
                playerTransform.position = originalPlayerPosition;
                playerTransform.rotation = originalPlayerRotation;
                characterController.enabled = true;
            }
            else
            {
                // If no CharacterController, just set position directly
                playerTransform.position = originalPlayerPosition;
                playerTransform.rotation = originalPlayerRotation;
            }
            
            Debug.Log("Player respawned to starting position");
        }
        else
        {
            Debug.LogError("Player transform not found! Cannot respawn player.");
        }
    }
    
    /// <summary>
    /// Sets a new spawn point for the player
    /// </summary>
    /// <param name="newSpawnPoint">The new spawn point transform</param>
    public void SetPlayerSpawnPoint(Transform newSpawnPoint)
    {
        if (newSpawnPoint != null)
        {
            originalPlayerPosition = newSpawnPoint.position;
            originalPlayerRotation = newSpawnPoint.rotation;
            Debug.Log("Player spawn point updated");
        }
    }
}