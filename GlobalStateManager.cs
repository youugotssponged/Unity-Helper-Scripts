using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalStateManager : MonoBehaviour
{
    public enum GameState 
    {
        START = 0,
        LOADING,
        WIN,
        LOSE,
    }

    protected GameState CurrentGameState;
    public GameState GetCurrentGameState() => CurrentGameState;

    public static event Action<GameState> OnGameStateChanged;
    private static readonly Lazy<GlobalStateManager> _lazyLoadedStateManager = new Lazy<GlobalStateManager>(() => new GlobalStateManager());
    protected GlobalStateManager(){}
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static GlobalStateManager Instance
    {
        get
        {
            return _lazyLoadedStateManager.Value;
        }
    }

    // Update for later key mapping for demoing states if needed
    private void Update(){}

    public void UpdateGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;

        // <<< Handle Internal Logic here >>>
        // For now this just pings subscribers the new state that was changed

        OnGameStateChanged?.Invoke(newGameState);
    }
}
