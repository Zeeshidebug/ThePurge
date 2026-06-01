using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager
: MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager
        .LoadScene(
            1
        );
    }

    public void ContinueGame()
    {
        // Implement continue game nanti kalo udah punya save system
        Debug.Log(
            "CONTINUE 😭🔥"
        );
    }

    public void Weaponry()
    {
        // Implement weaponry nanti kalo udah punya weaponry UI
        Debug.Log(
            "WEAPONRY 😭🔥"
        );
    }

    public void Settings()
    {
        // Implement settings nanti kalo udah punya settings UI
        Debug.Log(
            "SETTINGS 😭🔥"
        );
    }

    public void QuitGame()
    {
        Application
        .Quit();

        Debug.Log(
            "QUIT 😭🔥"
        );
    }
}