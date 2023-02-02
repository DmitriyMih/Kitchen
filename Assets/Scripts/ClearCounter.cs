using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    [Header("Connect Settings")]
    [SerializeField] private KitchenObject kitchenObject;

    public void Interact(PlayerController player)
    {
        if (kitchenObject == null)
        {
            return;
        }

        kitchenObject.SetKitchenObjectParent(player);
        Debug.Log($"Interaction Object {kitchenObject.GetObjectName()}");
    }

    #region Ikitchen Interface
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
  
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
 
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    #endregion
}