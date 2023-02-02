using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO KitchenObjectSO => kitchenObjectSO;

    public string GetObjectName()
    {
        if (kitchenObjectSO == null)
            return null;

        return kitchenObjectSO.objectName;
    }
}