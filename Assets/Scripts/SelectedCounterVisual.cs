using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Awake()
    {
        baseCounter = GetComponent<BaseCounter>();
    }

    private void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
    }

    private void PlayerOnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        for (int i = 0; i < visualGameObjectArray.Length; i++)
        {
            if (visualGameObjectArray[i] == null)
            {
                Debug.LogError("Visual object is null");
                continue;
            }

            visualGameObjectArray[i].SetActive(true);
        }
    }

    private void Hide()
    {
        for (int i = 0; i < visualGameObjectArray.Length; i++)
        {
            if (visualGameObjectArray[i] == null)
            {
                Debug.LogError("Visual object is null");
                continue;
            }

            visualGameObjectArray[i].SetActive(false);
        }
    }
}