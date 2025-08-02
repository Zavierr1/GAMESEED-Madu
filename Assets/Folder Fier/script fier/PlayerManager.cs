using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    [Header("Player Respawn Settings")]
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private bool setSpawnPointOnStart = true;
    
    private Transform playerTransform;
    private Vector3 originalSpawnPosition;
    private Quaternion originalSpawnRotation;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    private void Start()
    {
        // Find the player GameObject
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            
            if (setSpawnPointOnStart)
            {
                // Set current player position as spawn point if no spawn point is assigned
                if (playerSpawnPoint == null)
                {
                    originalSpawnPosition = playerTransform.position;
                    originalSpawnRotation = playerTransform.rotation;
                }
                else
                {
                    // Use the assigned spawn point
                    originalSpawnPosition = playerSpawnPoint.position;
                    originalSpawnRotation = playerSpawnPoint.rotation;
                }
            }
        }
        else
        {
            Debug.LogError("Player GameObject with 'Player' tag not found!");
        }
    }
    
    /// <summary>
    /// Respawns the player to the original spawn position
    /// </summary>
    public void RespawnPlayer()
    {
        if (playerTransform != null)
        {
            // Get the CharacterController component (if using SimpleFPController)
            CharacterController characterController = playerTransform.GetComponent<CharacterController>();
            
            if (characterController != null)
            {
                // Disable CharacterController temporarily to allow position change
                characterController.enabled = false;
                playerTransform.position = originalSpawnPosition;
                playerTransform.rotation = originalSpawnRotation;
                characterController.enabled = true;
            }
            else
            {
                // If no CharacterController, just set position directly
                playerTransform.position = originalSpawnPosition;
                playerTransform.rotation = originalSpawnRotation;
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
    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        if (newSpawnPoint != null)
        {
            originalSpawnPosition = newSpawnPoint.position;
            originalSpawnRotation = newSpawnPoint.rotation;
            Debug.Log("Player spawn point updated");
        }
    }
    
    /// <summary>
    /// Sets a new spawn point using position and rotation
    /// </summary>
    /// <param name="position">New spawn position</param>
    /// <param name="rotation">New spawn rotation</param>
    public void SetSpawnPoint(Vector3 position, Quaternion rotation)
    {
        originalSpawnPosition = position;
        originalSpawnRotation = rotation;
        Debug.Log("Player spawn point updated");
    }
    
    /// <summary>
    /// Gets the current spawn position
    /// </summary>
    /// <returns>The current spawn position</returns>
    public Vector3 GetSpawnPosition()
    {
        return originalSpawnPosition;
    }
    
    /// <summary>
    /// Gets the current spawn rotation
    /// </summary>
    /// <returns>The current spawn rotation</returns>
    public Quaternion GetSpawnRotation()
    {
        return originalSpawnRotation;
    }
}
