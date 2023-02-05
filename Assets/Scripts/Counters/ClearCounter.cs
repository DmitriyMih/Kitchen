using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //  If Plate In Hand
                    if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        if (plateKitchenObject.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectSO()))
                            player.GetKitchenObject().DestroySelf();
                        else
                            Debug.Log("Not Find | " + player.GetKitchenObject().GetKitchenObjectSO());
                }
            }
            else
                GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
}