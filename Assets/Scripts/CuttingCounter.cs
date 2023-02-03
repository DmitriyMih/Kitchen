using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(PlayerController player)
    {
        Debug.Log("Interaction In | Counter");
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
                player.GetKitchenObject().SetKitchenObjectParent(this);
            else
            {

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
                GetKitchenObject().SetKitchenObjectParent(player);
        }
    }

    public override void InteractAlternate(PlayerController player)
    {
        if(HasKitchenObject())
        {
            GetKitchenObject().DestroySelf();
        }
    }
}