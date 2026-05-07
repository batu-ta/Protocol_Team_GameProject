using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçiþi için lazým

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Inspector'dan PausePanel'i buraya sürükleyeceðiz
    private bool isPaused = false;

    void Update()
    {
        // ESC tuþuna basýldýðýnda
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true); // Menüyü göster
        Time.timeScale = 0f;       // Zamaný durdur
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false); // Menüyü gizle
        Time.timeScale = 1f;        // Zamaný akýt
        isPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; // Zamaný düzeltmeyi unutma
        SceneManager.LoadScene("MainMenuScene"); // Sahne adýný buraya yaz
    }
}