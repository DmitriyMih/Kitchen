using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class KitchenObject : MonoBehaviour
{
    private Rigidbody rigidbody;

    [Header("Data Settings")]
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [Header("Connect Settings")]
    [SerializeField] private IKitchenObjectParent kitchenObjectParent;

    [Header("Object Move Settings")]
    [SerializeField] private float moveTime = 0.3f;
    [SerializeField] private float rotationTime = 0.15f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public string GetObjectName()
    {
        if (kitchenObjectSO == null)
            return null;

        return kitchenObjectSO.objectName;
    }
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent, bool tweenMove = true)
    {
        if (this.kitchenObjectParent != null)
            this.kitchenObjectParent.ClearKitchenObject();

        this.kitchenObjectParent = kitchenObjectParent;

        rigidbody.isKinematic = kitchenObjectParent != null;

        if (kitchenObjectParent == null)
        {
            transform.parent = null;
            return;
        }

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenParent Arleady Has A Kitchen Object");
            return;
        }

        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();

        if (tweenMove)
        {
            transform.DOLocalMove(Vector3.zero, moveTime);
            transform.DOLocalRotateQuaternion(Quaternion.identity, rotationTime);
        }
        else
        {
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void DestroySelf(float destroyTime = 0f)
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject, destroyTime);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        if (kitchenObjectSO == null || kitchenObjectParent == null)
        {
            Debug.LogError("Kitchen Object Is Null");
            return null;
        }

        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        Debug.Log($"Spawn {kitchenObjectTransform.gameObject} | In {kitchenObjectParent}");
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent, false);
        return kitchenObject;
    }
}