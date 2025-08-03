using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class CharacterMapping
{
    public string characterName;
    public GameObject prefab;
    public PersonalityType personality;
    
    [System.NonSerialized]
    public Debtor debtorData;
}

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    [Header("Daily Mission Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int totalMissions = 3;
    [SerializeField] private int successfulMissions = 0;
    [SerializeField] private int completedMissions = 0;
    
    [Header("Character Mappings")]
    [SerializeField] private CharacterMapping[] characterMappings = new CharacterMapping[6];
    
    [Header("Player Respawn Settings")]
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private bool respawnPlayerOnNewDay = true;
    
    private List<Debtor> todayDebtors = new List<Debtor>();
    private List<CharacterMapping> availableCharacters = new List<CharacterMapping>();
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
        // Initialize character mappings with debtor data
        InitializeCharacterMappings();
        
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
                Debug.Log($"Player spawn point set to: {originalPlayerPosition}");
            }
            else
            {
                originalPlayerPosition = playerTransform.position;
                originalPlayerRotation = playerTransform.rotation;
                Debug.Log($"Player spawn point set to current position: {originalPlayerPosition}");
            }
        }
        else
        {
            Debug.LogError("Player GameObject with 'Player' tag not found!");
        }
        
        SetupDailyMissions();
    }
    
    private void InitializeCharacterMappings()
    {
        // Initialize all character mappings with their debtor data
        for (int i = 0; i < characterMappings.Length; i++)
        {
            if (characterMappings[i] == null)
                characterMappings[i] = new CharacterMapping();
                
            // Create ScriptableObject instances for each character
            Debtor debtor = ScriptableObject.CreateInstance<Debtor>();
            
            switch (i)
            {
                case 0: // Andri - Arrogant
                    characterMappings[i].characterName = "Andri";
                    characterMappings[i].personality = PersonalityType.Arrogant;
                    DebtorDataHelper.ConfigureAndriData(debtor);
                    break;
                case 1: // Bu Wati - Gentle
                    characterMappings[i].characterName = "Bu Wati";
                    characterMappings[i].personality = PersonalityType.Gentle;
                    DebtorDataHelper.ConfigureBuWatiData(debtor);
                    break;
                case 2: // Pak Riko - Cunning
                    characterMappings[i].characterName = "Pak Riko";
                    characterMappings[i].personality = PersonalityType.Cunning;
                    DebtorDataHelper.ConfigurePakRikoData(debtor);
                    break;
                case 3: // Yusuf - Aggressive
                    characterMappings[i].characterName = "Yusuf";
                    characterMappings[i].personality = PersonalityType.Aggressive;
                    DebtorDataHelper.ConfigureYusufData(debtor);
                    break;
                case 4: // Bu Rini - Humble
                    characterMappings[i].characterName = "Bu Rini";
                    characterMappings[i].personality = PersonalityType.Humble;
                    DebtorDataHelper.ConfigureBuRiniData(debtor);
                    break;
                case 5: // Rizwan - Stubborn
                    characterMappings[i].characterName = "Rizwan";
                    characterMappings[i].personality = PersonalityType.Stubborn;
                    DebtorDataHelper.ConfigureRizwanData(debtor);
                    break;
            }
            
            characterMappings[i].debtorData = debtor;
        }
    }

    public void SetupDailyMissions()
    {
        // Clear previous day's data
        DebtorCharacter[] existingDebtors = FindObjectsOfType<DebtorCharacter>();
        foreach (DebtorCharacter debtor in existingDebtors)
        {
            Destroy(debtor.gameObject);
        }

        todayDebtors.Clear();
        completedMissions = 0;
        successfulMissions = 0;

        Debug.Log("Mission progress reset: 0/3 missions completed, 0 successful missions");

        // Create a copy of available characters for this day (no duplicates)
        availableCharacters = new List<CharacterMapping>(characterMappings);
        
        // Randomize available spawn points
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        int missionsToSetup = Mathf.Min(totalMissions, Mathf.Min(availableSpawnPoints.Count, availableCharacters.Count));

        for (int i = 0; i < missionsToSetup; i++)
        {
            if (availableSpawnPoints.Count == 0 || availableCharacters.Count == 0)
            {
                Debug.LogWarning("Spawn points or characters exhausted before completing setup.");
                break;
            }

            // Select random character (ensures no duplicates in same day)
            int randomCharacterIndex = Random.Range(0, availableCharacters.Count);
            CharacterMapping selectedCharacter = availableCharacters[randomCharacterIndex];
            availableCharacters.RemoveAt(randomCharacterIndex);

            // Select random spawn point
            int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];
            availableSpawnPoints.RemoveAt(randomSpawnIndex);

            // Validate character mapping
            if (selectedCharacter.prefab == null)
            {
                Debug.LogError($"No prefab assigned for character: {selectedCharacter.characterName}");
                continue;
            }

            if (selectedCharacter.debtorData == null)
            {
                Debug.LogError($"No debtor data found for character: {selectedCharacter.characterName}");
                continue;
            }

            // Add to today's debtors
            todayDebtors.Add(selectedCharacter.debtorData);

            // Spawn the character
            GameObject debtorGO = Instantiate(selectedCharacter.prefab, spawnPoint.position, spawnPoint.rotation);
            DebtorCharacter debtorCharacter = debtorGO.GetComponent<DebtorCharacter>();
            
            if (debtorCharacter != null)
            {
                debtorCharacter.SetDebtorData(selectedCharacter.debtorData);
                Debug.Log($"Spawned {selectedCharacter.characterName} ({selectedCharacter.personality}) at {spawnPoint.name}");
            }
            else
            {
                Debug.LogError($"DebtorCharacter component not found on prefab for {selectedCharacter.characterName}!");
            }
        }

        Debug.Log($"Daily missions setup: {todayDebtors.Count} unique characters spawned at random locations.");

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
        
        // Check if all missions are completed for the day
        if (completedMissions >= todayDebtors.Count)
        {
            Debug.Log($"All missions completed! Final result: {successfulMissions}/{totalMissions}");
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
        Debug.Log("Starting new day...");
        
        // Respawn player when starting new day (called by Next Day button)
        if (respawnPlayerOnNewDay && playerTransform != null)
        {
            RespawnPlayer();
        }
        
        // Set up new missions for the day
        SetupDailyMissions();
    }
    
    /// <summary>
    /// Respawns the player to the original spawn position
    /// </summary>
    private void RespawnPlayer()
    {
        if (playerTransform != null)
        {
            Debug.Log($"Respawning player from {playerTransform.position} to {originalPlayerPosition}");
            
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
            
            Debug.Log("Player successfully respawned to starting position");
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