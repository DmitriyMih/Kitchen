using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    [SerializeField] private List<KitchenObjectSO> kitchenObjectSOList;

    public event EventHandler OnIngredientCleared;
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    public bool TryAddIngridient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSO == null)
        {
            Debug.LogError("Kitchen Object Is Null | " + gameObject.name);
            return false;
        }

        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
            return false;

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
            return false;

        kitchenObjectSOList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(kitchenObjectSO, new OnIngredientAddedEventArgs
        {
            kitchenObjectSO = kitchenObjectSO
        });
        return true;
    }

    public bool FindElemetInKitchenList(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSO == null) { Debug.LogError("Kitchen Object Is Null | " + gameObject.name); return false; }

        //Debug.Log($"Element {kitchenObjectSO} | Start");
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
            return false;

        //Debug.Log($"Element {kitchenObjectSO} | Middle");
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
            return true;

        //Debug.Log($"Element {kitchenObjectSO} | End");
        return false;
    }

    public int GetKitchenObjectSOListCount()
    {
        return kitchenObjectSOList.Count;
    }

    public bool PlateIsNotEmpty()
    {
        return kitchenObjectSOList.Count != 0;
    }

    public void ClearThePlate()
    {
        if (!PlateIsNotEmpty())
            return;

        kitchenObjectSOList.Clear();
        OnIngredientCleared?.Invoke(this, EventArgs.Empty);
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}