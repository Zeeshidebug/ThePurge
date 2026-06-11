using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text lostSoulText;

    private int lostSouls;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (PlayerStats.Instance != null)
        {
            int currentSouls = PlayerStats.Instance.GetSoulFragments();
            lostSouls = Mathf.FloorToInt(currentSouls * 0.25f);
            PlayerStats.Instance.SpendSoulFragments(lostSouls);

            if (lostSoulText != null)
            {
                lostSoulText.text = "You Lost " + lostSouls + " Soul Fragments";
            }
        }

        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.SetState(GameState.UI);
        }
    }

    public void RespawnPlayer()
    {
        Time.timeScale = 1f;

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    public void Respawn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveAndQuit()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveGame();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}