using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        //Debug.Log("Interaction In | Counter");
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
                player.GetKitchenObject().SetKitchenObjectParent(this);
            else { }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject() is PlateKitchenObject)
                {
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject != null)
                    {
                        if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
                            GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
                GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
}