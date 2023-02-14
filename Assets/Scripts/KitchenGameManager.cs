using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }
    [SerializeField] private State state;
    
    private float waitingToStartTimer;
    [SerializeField] private float waitingToStartTimerMax = 1f;

    private float countdownToStartTimer;
    [SerializeField] private float countdownToStartTimerMax = 3f;

    private float gamePlayingTimer;
    [SerializeField] private float gamePlayingTimerMax = 10f;

    public event EventHandler OnStateChanged;
    public event Action<float> OnGamePlayingTime;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        waitingToStartTimer = waitingToStartTimerMax;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;

                if (waitingToStartTimer < 0f)
                {
                    countdownToStartTimer = countdownToStartTimerMax;

                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;

                if (countdownToStartTimer < 0f)
                {
                    gamePlayingTimer = gamePlayingTimerMax;

                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                OnGamePlayingTime?.Invoke(GetGamePlayingTimerNormalized());

                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:

                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    private float GetGamePlayingTimerNormalized()
    {
        return gamePlayingTimer / gamePlayingTimerMax;
    }
}