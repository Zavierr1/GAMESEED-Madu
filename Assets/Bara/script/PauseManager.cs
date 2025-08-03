using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public FadeController fadeController; // Assign di Inspector
    public string mainMenuSceneName = "MainMenu"; // Ganti sesuai nama scene utama kamu

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
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // reset sebelum load
        fadeController.nextSceneName = mainMenuSceneName;
        fadeController.StartGameWithFade(); // pakai fade out
    }
}
