using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // assign di Inspector
    public MonoBehaviour movementScript; // drag script movement-mu di sini

    void Start()
    {
        // Reset TimeScale
        Time.timeScale = 1f;

        // Pastikan pause menu tidak aktif
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);

        // Aktifkan movement
        if (movementScript != null) movementScript.enabled = true;

        // Lock cursor untuk gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
