using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    // Load scene berikutnya berdasarkan build index saat ini
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Pastikan index berikutnya masih dalam batas
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Time.timeScale = 1f; // Pastikan waktu berjalan normal
        }
        else
        {
            Debug.LogWarning("Tidak ada scene berikutnya dalam Build Settings!");
        }
    }

    // Keluar dari game
    public void ExitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Supaya keluar juga saat di editor
#endif
    }
}
