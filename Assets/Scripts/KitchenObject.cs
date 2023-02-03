using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [Header("Data Settings")]
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

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
        {
            Debug.LogError("IKitchenParent Arleady Has A Kitchen Object");
            return;
        }

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        if (kitchenObjectSO == null || kitchenObjectParent == null)
            return null;

        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}