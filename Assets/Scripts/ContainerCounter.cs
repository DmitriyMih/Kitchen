using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Connect Settings")]
    [SerializeField] private KitchenObject kitchenObject;

    private void Awake()
    {
        if (kitchenObjectSO != null)
            sprite.sprite = kitchenObjectSO.sprite;
        else
            Debug.LogError("Kitchen Object SO Is Null");
    }

    public override void Interact(PlayerController player)
    {
        Debug.Log("Interaction In | Container Counter");
        if (kitchenObject == null)
        {
            if (kitchenObjectSO == null)
                return;

            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
            kitchenObject.SetKitchenObjectParent(player);
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