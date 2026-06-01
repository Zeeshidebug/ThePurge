using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager
: MonoBehaviour
{
    public static
    VictoryManager
    Instance;

    [SerializeField]
    private GameObject
    victoryPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        victoryPanel
        .SetActive(
            false
        );
    }

    public void ShowVictory()
    {
        victoryPanel
        .SetActive(
            true
        );

        GameStateManager
        .Instance
        .SetState(
            GameState.UI
        );

        Debug.Log(
            "VICTORY 😭🔥👑"
        );
    }

    public void ReturnToMenu()
    {
        SceneManager
        .LoadScene(
            0
        );
    }
}