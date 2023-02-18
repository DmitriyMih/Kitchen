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
        Debug.Log("Hello Orange" %Colorize.Red);
        Debug.Log("Hello Orange" %Colorize.Red %FontFormat.Bold);

        Debug.LogError("Hello Orange" %Colorize.Orange);
        Debug.LogError("Hello Orange" %Colorize.DarkOrange %FontFormat.Bold);

        Debug.Log($"Hello Orange {Colorize.Yellow} Man");
        Debug.LogError($"Hello Orange {+ %Colorize.Blue} Man");

        Hide();

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenuScene));
        else Debug.LogError($"{gameObject.name} | Main Button | Is Null");

        if (optionsButton != null)
            optionsButton.onClick.AddListener(() =>
            {
                if (OptionsUI.Instance != null)
                    OptionsUI.Instance.Show();
            });
        else Debug.LogError($"{gameObject.name} | Options Button | Is Null");
    }

    private void Start()
    {
        if (KitchenGameManager.Instance != null)
        {
            KitchenGameManager.Instance.OnGamePausedStateChanged += Instance_OnGamePausedStateChanged;

            if (resumeButton != null)
                resumeButton.onClick.AddListener(() => KitchenGameManager.Instance.PauseGame());
            else Debug.LogError($"{gameObject.name} | Resume Button | Is Null");
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
        else Debug.Log(string.Format($"{gameObject.name} | Game Over Content | Is Null ", Color.green));
    }

    private void Hide()
    {
        if (gameOverContent != null)
            gameOverContent.SetActive(false);
        else Debug.Log(string.Format($"{gameObject.name} | Game Over Content | Is Null ", Color.green));
    }
}