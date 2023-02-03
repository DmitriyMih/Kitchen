using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [Header("Data Settings")]
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO KitchenObjectSO => kitchenObjectSO;

    [Header("Connect Settings")]
    [SerializeField] private IKitchenObjectParent kitchenObjectParent;

    public string GetObjectName()
    {
        if (kitchenObjectSO == null)
            return null;

        return kitchenObjectSO.objectName; 
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
            this.kitchenObjectParent.ClearKitchenObject();

        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent == null)
            return;

        if (kitchenObjectParent.HasKitchenObject())
            Debug.LogError("IKitchenParent Arleady Has A Kitchen Object");

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}