using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CurrentState = GameState.Gameplay;
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;

        Debug.Log("State: " + newState);
    }

    public bool IsGameplay()
    {
        return CurrentState == GameState.Gameplay;
    }
}