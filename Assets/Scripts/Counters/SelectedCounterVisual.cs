using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] visualGameObjectArray;
    [SerializeField] private BaseCounter baseCounter;

    private void Awake()
    {
        baseCounter = GetComponent<BaseCounter>();
        Hide();
    }

    private void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += PlayerController_OnSelectedCounterChanged;
    }

    private void PlayerController_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
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
            //Debug.Log("Show - " + gameObject);
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