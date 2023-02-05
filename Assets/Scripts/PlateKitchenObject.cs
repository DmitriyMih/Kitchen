using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    [SerializeField] private List<KitchenObjectSO> kitchenObjectSOList;

    public bool TryAddIngridient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
            return false;

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
            return false;

        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }

    public bool PlateIsNotEmpty()
    {
        return kitchenObjectSOList.Count != 0;
    }

    public void ClearThePlate()
    {
        if (!PlateIsNotEmpty())
            return;

        for (int i = 0; i < kitchenObjectSOList.Count; i++)
        {
            if (kitchenObjectSOList[i] == null)
                continue;

            DestroyImmediate(kitchenObjectSOList[i], true);
        }

        kitchenObjectSOList.Clear();
    }
}