using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
    private TextMeshProUGUI countdownText;

    private void Awake()
    {
        countdownText = GetComponentInChildren<TextMeshProUGUI>();
        Hide();
    }

    private void Start()
    {
        if (KitchenGameManager.Instance != null)
            KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    }

    private void Update()
    {
        if (KitchenGameManager.Instance != null && countdownText != null)
            countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
            Show();
        else
            Hide();
    }

    private void Show()
    {
        if (countdownText != null)
            countdownText.gameObject.SetActive(true);
    }

    private void Hide()
    {
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }
}