using UnityEngine;

// Pause menu behaviour
public class PauseMenu : GameManager
{
    [Header("Pause Menu Variables")]
    public static bool gameIsPaused;
    public GameObject pausePanel;

    // Get pause menu components
    void Start()
    {
        gameIsPaused = false;
    }

    // Check if Player pauses game
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame();
        }
    }

    // Pause game
    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}