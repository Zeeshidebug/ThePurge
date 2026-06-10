using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager
: MonoBehaviour
{

    public static
    GameOverManager
    Instance;

    [SerializeField]
    private GameObject
    gameOverPanel;

    [SerializeField]
    private TMP_Text
    lostSoulText;
    private int lostSouls;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameOverPanel
        .SetActive(
            false
        );
    }

    public void ShowGameOver()
    {
        gameOverPanel
        .SetActive(
            true
        );

        int currentSouls =
        PlayerStats
        .Instance
        .GetSoulFragments();

        lostSouls =
        Mathf.FloorToInt(
            currentSouls * 0.25f
        );

        PlayerStats
        .Instance
        .SpendSoulFragments(
            lostSouls
        );

        lostSoulText.text =
        "You Lost "
        + lostSouls
        + " Soul Fragments";

        GameStateManager
        .Instance
        .SetState(
            GameState.UI
        );
    }

    public void Respawn()
    {

        SceneManager
        .LoadScene(
            SceneManager
            .GetActiveScene()
            .buildIndex
        );
    }

    public void SaveAndQuit()
    {
        SaveManager
        .Instance
        .SaveGame();

        SceneManager
        .LoadScene(0);
    }
}