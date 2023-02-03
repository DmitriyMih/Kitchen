using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeArray;

    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
             if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
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
        Debug.Log("Interaction Alternate In | Counter");
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        for (int i = 0; i < cuttingRecipeArray.Length; i++)
        {
            if (cuttingRecipeArray[i].input == inputKitchenObjectSO)
                return true;
        }

        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        for (int i = 0; i < cuttingRecipeArray.Length; i++)
        {
            if (cuttingRecipeArray[i].input == inputKitchenObjectSO)
                return cuttingRecipeArray[i].output;
        }

        return null;
    }  
}