using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler<OnGameStateChangedEventArgs> OnGameStateChanged;

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public class OnGameStateChangedEventArgs : EventArgs
    {
        public GameState gameState;
    }
    public enum GameState
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }
    public GameState gameState;
    [SerializeField] private float waitingToStartTimerMax = 3f;
    [SerializeField] private float countdownToStartTimerMax = 3f;
    [SerializeField] private float gamePlayingTimerMax = 30f;
    private float waitingToStartTimer = 0f;
    private float countdownToStartTimer = 0f;
    private float gamePlayingTimer = 0f;
    private bool isGamePaused = false;

    void Awake()
    {
        countdownToStartTimer = countdownToStartTimerMax;
        Instance = this;
        gameState = GameState.WaitingToStart;
        OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs
        {
            gameState = GameState.WaitingToStart
        });
    }

    void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseActionPerformed;
    }

    private void GameInput_OnPauseActionPerformed(object sender, EventArgs e)
    {
        TooglePauseGame();
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.WaitingToStart:
                waitingToStartTimer += Time.deltaTime;
                if (waitingToStartTimer > waitingToStartTimerMax)
                {
                    OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs
                    {
                        gameState = GameState.CountdownToStart
                    });
                    gameState = GameState.CountdownToStart;
                    waitingToStartTimer = 0f;
                }
                break;
            case GameState.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0)
                {
                    OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs
                    {
                        gameState = GameState.GamePlaying
                    });
                    gameState = GameState.GamePlaying;
                    countdownToStartTimer = countdownToStartTimerMax;
                }
                break;
            case GameState.GamePlaying:
                gamePlayingTimer += Time.deltaTime;
                if (gamePlayingTimer > gamePlayingTimerMax)
                {
                    OnGameStateChanged?.Invoke(this, new OnGameStateChangedEventArgs
                    {
                        gameState = GameState.GameOver
                    });
                    gameState = GameState.GameOver;
                    gamePlayingTimer = 0f;
                }
                break;
            case GameState.GameOver:
                break;
        }
        Debug.Log(gameState);
    }

    public bool IsGamePlaying()
    {
        return gameState == GameState.GamePlaying;
    }

    public float GetCountdown()
    {
        return countdownToStartTimer;
    }
    public float GetPlayingTimerNormalized()
    {
        return gamePlayingTimer / gamePlayingTimerMax;
    }

    public void TooglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            Debug.Log("her");
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            return;
        }
        Debug.Log("test");
        Time.timeScale = 1f;
        OnGameUnpaused?.Invoke(this, EventArgs.Empty);
    }
}
