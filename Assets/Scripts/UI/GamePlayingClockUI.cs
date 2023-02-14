using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerFillImage;
    [SerializeField] private GameObject gameTimerContent;

    private void Start()
    {
        if (KitchenGameManager.Instance != null)
        {
            KitchenGameManager.Instance.OnGamePlayingTime += KitchenGameManager_OnGamePlayingTime;
            KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        }
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
            Show();
        else
            Hide();
    }

    private void KitchenGameManager_OnGamePlayingTime(float value)
    {
        timerFillImage.fillAmount = value;
    }

    private void Show()
    {
        if (gameTimerContent != null)
            gameTimerContent.SetActive(true);
    }

    private void Hide()
    {
        if (gameTimerContent != null)
            gameTimerContent.SetActive(false);
    }
}