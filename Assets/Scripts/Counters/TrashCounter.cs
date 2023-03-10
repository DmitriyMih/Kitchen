using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrashCounter : BaseCounter
{
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float objectMoveTime = 0.4f;
    [SerializeField] private float objectScaleTime = 0.4f;

    public static EventHandler OnAnyObjectTrashed;
    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject())
        {
            //  If Plate
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (plateKitchenObject.GetKitchenObjectSOListCount() > 0)
                {
                    OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
                    plateKitchenObject.ClearThePlate();
                }

                return;
            }
            else
            {
                player.GetKitchenObject().SetKitchenObjectParent(this, false);
                KitchenObject kitchenObject = GetKitchenObject();

                kitchenObject.transform.DOScale(Vector3.zero, objectScaleTime);
                kitchenObject.transform.DOMove(bottomPoint.position, objectMoveTime).OnComplete(() =>
                {
                    kitchenObject.DestroySelf();
                    OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
                });
            }
        }
    }

}