using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] visualGameObjectArray;
    [SerializeField] private KitchenObject kitchenObject;

    private void Awake()
    {
        kitchenObject = GetComponent<KitchenObject>();
    }

    private void Start()
    {
        PlayerController.Instance.OnSelectedKitchenObjectChanged += PlayerController_OnSelectedKitchenObjectChanged;
    }

    private void PlayerController_OnSelectedKitchenObjectChanged(object sender, PlayerController.OnSelectedkitchenObjectChangedEventArgs e)
    {
        if (e.selectedKitchenObject == kitchenObject)
            Show();
        else
            Hide();
    }

    private void OnDisable()
    {
        PlayerController.Instance.OnSelectedKitchenObjectChanged -= PlayerController_OnSelectedKitchenObjectChanged;
    }

    private void Show()
    {
        for (int i = 0; i < visualGameObjectArray.Length; i++)
        {
            //Debug.Log("Show - " + gameObject);
            if (visualGameObjectArray[i] == null)
            {
                Debug.LogError("Visual object is null | " + gameObject);
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
                Debug.LogError("Visual object is null | " + gameObject);
                continue;
            }

            visualGameObjectArray[i].SetActive(false);
        }
    }
}