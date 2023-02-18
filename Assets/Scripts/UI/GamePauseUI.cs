using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverContent;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        Hide();

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenuScene));
        else Debug.LogError($"{gameObject} | Main Button | Is Null");

        if (optionsButton != null)
            optionsButton.onClick.AddListener(() =>
            {
                if (OptionsUI.Instance != null)
                    OptionsUI.Instance.Show();
            });
        else Debug.LogError($"{gameObject} | Options Button | Is Null");
    }

    private void Start()
    {
        if (KitchenGameManager.Instance != null)
        {
            KitchenGameManager.Instance.OnGamePausedStateChanged += Instance_OnGamePausedStateChanged;

            if (resumeButton != null)
                resumeButton.onClick.AddListener(() => KitchenGameManager.Instance.PauseGame());
            else Debug.LogError($"{gameObject} | Resume Button | Is Null");
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