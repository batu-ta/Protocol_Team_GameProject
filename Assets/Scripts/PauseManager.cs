using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişi için lazım

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Inspector'dan PausePanel'i buraya sürükleyeceğiz
    private bool isPaused = false;

    void Update()
    {
        // ESC tuşuna basıldığında
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true); // Menüyü göster
        Time.timeScale = 0f;       // Zamanı durdur
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false); // Menüyü gizle
        Time.timeScale = 1f;        // Zamanı akıt
        isPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; // Zamanı düzeltmeyi unutma
        SceneManager.LoadScene("Menu"); // Ana menü sahnesi
    }
}