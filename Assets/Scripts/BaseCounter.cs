using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] protected Transform counterTopPoint;
    [SerializeField] protected KitchenObject kitchenObject;

    public virtual void Interact(PlayerController player) { }

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
        return null;
    }

    public bool HasKitchenObject()
    {
        return false;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    #endregion
}