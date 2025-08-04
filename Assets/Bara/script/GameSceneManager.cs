using UnityEngine;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // assign di Inspector
    public MonoBehaviour movementScript; // drag script movement-mu di sini

    void Start()
    {
        // Reset TimeScale
        Time.timeScale = 1f;
        
        // Reset pause state
        PauseManager.isGamePaused = false;

        // Pastikan pause menu tidak aktif
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);

        // Aktifkan movement
        if (movementScript != null) movementScript.enabled = true;

        // Lock cursor untuk gameplay - give it a small delay to ensure proper initialization
        StartCoroutine(InitializeCursorState());
    }
    
    private System.Collections.IEnumerator InitializeCursorState()
    {
        // Wait a frame to ensure all other scripts have initialized
        yield return null;
        
        // Force proper cursor state for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Debug.Log("Cursor state initialized for gameplay");
    }
}
