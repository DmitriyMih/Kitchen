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

            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}