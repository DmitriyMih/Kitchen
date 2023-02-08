using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    [SerializeField] private float destroyTime = 0.5f;

    public override void Interact(PlayerController player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);

                player.GetKitchenObject().SetKitchenObjectParent(this);
                DeleteAfterWhile(destroyTime);
            }
        }
    }

    private void DeleteAfterWhile(float time = 0f)
    {
        if (HasKitchenObject())
            GetKitchenObject().DestroySelf(time);
    }
}