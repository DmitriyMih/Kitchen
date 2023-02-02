using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
    }

    private void PlayerOnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        if (visualGameObject == null) { Debug.LogError("Visual object is null"); return; }

        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        if (visualGameObject == null) { Debug.LogError("Visual object is null"); return; }

        visualGameObject.SetActive(false);
    }
}