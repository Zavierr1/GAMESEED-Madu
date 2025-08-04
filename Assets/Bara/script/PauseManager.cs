using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject DailyUI;
    public FadeController fadeController; // Assign di Inspector
    public string mainMenuSceneName = "Main Scene"; // Ganti sesuai nama scene utama kamu
    public MonoBehaviour movementScript; // Drag script MovementController ke sini via Inspector
    public static bool isGamePaused = false; // Tambahkan di atas di PauseManager

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Debug.Log("game is resumed");
                ResumeGame();
            }
            else
            {
                Debug.Log("game is paused");
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (DailyUI != null)
        {
            DailyUI.SetActive(false); // Sembunyikan DailyUI jika ada
        }
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        if (movementScript != null) movementScript.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        PauseManager.isGamePaused = true;
        
        // Pause background music
        BackgroundMusicManager musicManager = FindObjectOfType<BackgroundMusicManager>();
        if (musicManager != null)
        {
            musicManager.OnGamePaused();
        }
    }

    public void ResumeGame()
    {
        if (DailyUI != null)
        {
            DailyUI.SetActive(true); // Tampilkan kembali DailyUI jika ada
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (movementScript != null) movementScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        PauseManager.isGamePaused = false;
        
        // Resume background music
        BackgroundMusicManager musicManager = FindObjectOfType<BackgroundMusicManager>();
        if (musicManager != null)
        {
            musicManager.OnGameResumed();
        }
    }

    public void GoToMainMenu()
    {
        // Reset pause state
        isPaused = false;
        PauseManager.isGamePaused = false;
        
        Time.timeScale = 1f;

        // Aktifkan cursor agar bisa klik UI saat kembali ke Main Menu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Nonaktifkan movement (optional safety)
        if (movementScript != null) movementScript.enabled = false;

        // Ganti scene langsung tanpa fade
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
