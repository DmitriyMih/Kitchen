using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverContent;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        Hide();

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenuScene));
    }

    private void Start()
    {
        if (KitchenGameManager.Instance != null)
        {
            KitchenGameManager.Instance.OnGamePausedStateChanged += Instance_OnGamePausedStateChanged;

            if (resumeButton != null)
                resumeButton.onClick.AddListener(() => KitchenGameManager.Instance.PauseGame());
        }
    }

    private void Instance_OnGamePausedStateChanged(bool pauseState)
    {
        if (pauseState)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        if (gameOverContent != null)
            gameOverContent.SetActive(true);
    }

    private void Hide()
    {
        if (gameOverContent != null)
            gameOverContent.SetActive(false);
    }
}