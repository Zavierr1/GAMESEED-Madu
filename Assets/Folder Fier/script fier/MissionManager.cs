using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    [Header("Daily Mission Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int totalMissions = 3;
    [SerializeField] private int successfulMissions = 0;
    [SerializeField] private int completedMissions = 0;
    
    [Header("Debtor Character Variants")]
    [SerializeField] private List<GameObject> debtorCharacterPrefabs; // Drag all 6 prefabs here via Inspector

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
        // Respawn player ke posisi awal jika diaktifkan
        if (respawnPlayerOnNewDay && playerTransform != null)
        {
            RespawnPlayer();
        }

        // Hapus semua debtor yang masih ada dari hari sebelumnya
        DebtorCharacter[] existingDebtors = FindObjectsOfType<DebtorCharacter>();
        foreach (DebtorCharacter debtor in existingDebtors)
        {
            Destroy(debtor.gameObject);
        }

        todayDebtors.Clear();
        completedMissions = 0;
        successfulMissions = 0;

        Debug.Log("Mission progress reset: 0/3 missions completed, 0 successful missions");

        // --- RANDOMIZED SPAWN POINTS & CHARACTER PREFABS (NO DUPLICATES) ---
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);
        List<GameObject> availableCharacterPrefabs = new List<GameObject>(debtorCharacterPrefabs);

        int missionsToSetup = Mathf.Min(totalMissions, Mathf.Min(availableSpawnPoints.Count, availableCharacterPrefabs.Count));

        for (int i = 0; i < missionsToSetup; i++)
        {
            if (availableSpawnPoints.Count == 0 || availableCharacterPrefabs.Count == 0)
            {
                Debug.LogWarning("Spawn point atau prefab habis sebelum selesai setup semua mission.");
                break;
            }

            // Pastikan DebtorPool ada
            if (DebtorPool.Instance == null)
            {
                Debug.LogError("DebtorPool.Instance is null! Make sure DebtorPool is in the scene.");
                break;
            }

            // Ambil data debtor dari pool
            Debtor newDebtor = DebtorPool.Instance.GetRandomDebtor();
            if (newDebtor != null)
            {
                todayDebtors.Add(newDebtor);

                // Acak spawn point
                int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];
                availableSpawnPoints.RemoveAt(randomSpawnIndex);

                // Acak prefab karakter debtor
                int randomPrefabIndex = Random.Range(0, availableCharacterPrefabs.Count);
                GameObject selectedPrefab = availableCharacterPrefabs[randomPrefabIndex];
                availableCharacterPrefabs.RemoveAt(randomPrefabIndex);

                // Spawn karakter di posisi yang dipilih
                GameObject debtorGO = Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);
                DebtorCharacter debtorCharacter = debtorGO.GetComponent<DebtorCharacter>();
                if (debtorCharacter != null)
                {
                    debtorCharacter.SetDebtorData(newDebtor);
                }
                else
                {
                    Debug.LogError("DebtorCharacter component not found on selected prefab!");
                }
            }
        }

        Debug.Log($"Daily missions setup: {todayDebtors.Count} debtors spawned at random unique locations with unique prefabs.");

        // Refresh UI
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