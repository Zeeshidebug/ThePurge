using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager
: MonoBehaviour
{
    [SerializeField]
    private PlayerData
    playerData;

    public void Start()
    {
        Debug.Log(
            "MAIN MENU DATA ID: "
            + playerData.GetInstanceID()
        );
    }

    public void PlayGame()
    {
        SaveManager
        .Instance
        .DeleteSave();

        playerData.soulFragments = 0;

        SceneManager
        .LoadScene(1);

    }

    public void ContinueGame()
    {
        playerData.soulFragments =
            SaveManager
            .Instance
            .LoadSoulFragments();

        SceneManager.LoadScene(1);
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