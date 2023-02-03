using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private SpriteRenderer sprite;

    public event EventHandler OnPlayerGrabbedObject;

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
        if (!player.HasKitchenObject())
        {
            if (kitchenObjectSO == null)
                return;

            //KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            //kitchenObject.SetKitchenObjectParent(player, false);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}