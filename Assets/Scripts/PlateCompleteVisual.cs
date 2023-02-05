using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        if (plateKitchenObject != null)
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
            plateKitchenObject.OnIngredientCleared += OnIngredientCleared;
        }

        HideAllIngredients();
    }

    private void OnIngredientCleared(object sender, EventArgs e)
    {
        HideAllIngredients();
    }

    private void HideAllIngredients()
    {
        for (int i = 0; i < kitchenObjectSOGameObjectList.Count; i++)
        {
            if (kitchenObjectSOGameObjectList[i].gameObject != null)
                kitchenObjectSOGameObjectList[i].gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        for (int i = 0; i < kitchenObjectSOGameObjectList.Count; i++)
        {
            if (kitchenObjectSOGameObjectList[i].kitchenObjectSO == null)
                continue;

            if (kitchenObjectSOGameObjectList[i].kitchenObjectSO == e.kitchenObjectSO)
                if (kitchenObjectSOGameObjectList[i].gameObject != null)
                    kitchenObjectSOGameObjectList[i].gameObject.SetActive(true);
        }
    }
}