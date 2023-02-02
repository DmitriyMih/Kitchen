using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObject kitchenObject;
    [SerializeField] private Transform counterTopPoint;

    public void Interact()
    {
        if (kitchenObject == null)
            return;

        Debug.Log($"Interaction Object {kitchenObject.GetObjectName()}");
    }
}