using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuPanel;

    private bool isPaused = false;

    void Update()
    {
        if (
            Input.GetKeyDown(
                KeyCode.Escape
            )
        )
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else if (
                GameStateManager
                .Instance
                .IsGameplay()
            )
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(
            false
        );

        GameStateManager
        .Instance
        .SetState(
            GameState.Gameplay
        );

        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(
            true
        );

        GameStateManager
        .Instance
        .SetState(
            GameState.UI
        );

        isPaused = true;
    }

    public void LoadMainMenu()
    {
        SaveManager
        .Instance
        .SaveGame();

        SceneManager
        .LoadScene(
            0
        );
    }
}