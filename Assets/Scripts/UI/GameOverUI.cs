using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private GameObject gameOverContent;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        if (KitchenGameManager.Instance != null)
            KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
            Show();
        else
            Hide();
    }

    private void Show()
    {
        if (gameOverContent != null)
            gameOverContent.SetActive(true);

        if (recipesDeliveredText != null && DeliveryManager.Instance != null)
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
    }

    private void Hide()
    {
        if (gameOverContent != null)
            gameOverContent.SetActive(false);
    }
}