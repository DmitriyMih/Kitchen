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
                //  If Plate In Hand
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectInHand))
                {
                    //  Plate On Counter
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectInCounter))
                    {
                        //Debug.Log($"In Hand {plateKitchenObjectInHand.GetKitchenObjectSOListCount()}");
                        //Debug.Log($"In Counter {plateKitchenObjectInCounter.GetKitchenObjectSOListCount()}");

                        //  In Hand Empty
                        if (!plateKitchenObjectInHand.PlateIsNotEmpty())
                        {
                            Debug.Log(" Hand Empty");
                            ChangeTheContentsOfThePlates(plateKitchenObjectInHand, plateKitchenObjectInCounter);
                        }
                        //  Plate On Counter Not Null
                        else if (!plateKitchenObjectInCounter.PlateIsNotEmpty())
                        {
                            Debug.Log("Counter Empty");
                            ChangeTheContentsOfThePlates(plateKitchenObjectInHand, plateKitchenObjectInCounter);
                        }
                        else
                        {
                            if (TheKitchenObjectContentVaries(plateKitchenObjectInCounter, plateKitchenObjectInHand))
                                AddContentToKitchenPlates(plateKitchenObjectInCounter, plateKitchenObjectInHand);
                        }
                    }
                    //  Plate not on the table
                    else
                    {
                        if (plateKitchenObjectInHand.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
                            GetKitchenObject().DestroySelf();
                    }
                }
                //  In Hand No Plate
                else
                {
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectInCounter))
                    {
                        if (plateKitchenObjectInCounter.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectSO()))
                            player.GetKitchenObject().DestroySelf();
                        else
                            Debug.Log("Not Find | " + player.GetKitchenObject().GetKitchenObjectSO());
                    }
                }
            }
            else
                GetKitchenObject().SetKitchenObjectParent(player);
        }
    }

    private void AddContentToKitchenPlates(PlateKitchenObject firstKitchenObjet, PlateKitchenObject secondKitchenObjet)
    {
        List<KitchenObjectSO> tempSecondKitchenObjectSO = new List<KitchenObjectSO>(secondKitchenObjet.GetKitchenObjectSOList());
        secondKitchenObjet.ClearThePlate();

        foreach (KitchenObjectSO kitchenObjectSO in tempSecondKitchenObjectSO)
            firstKitchenObjet.TryAddIngridient(kitchenObjectSO);
    }

    private void ChangeTheContentsOfThePlates(PlateKitchenObject firstKitchenObjet, PlateKitchenObject secondKitchenObjet)
    {
        List<KitchenObjectSO> tempFirstKitchenObjectSO = new List<KitchenObjectSO>(firstKitchenObjet.GetKitchenObjectSOList());
        List<KitchenObjectSO> tempSecondKitchenObjectSO = new List<KitchenObjectSO>(secondKitchenObjet.GetKitchenObjectSOList());

        firstKitchenObjet.ClearThePlate();
        secondKitchenObjet.ClearThePlate();

        foreach (KitchenObjectSO kitchenObjectSO in tempFirstKitchenObjectSO)
            secondKitchenObjet.TryAddIngridient(kitchenObjectSO);

        foreach (KitchenObjectSO kitchenObjectSO in tempSecondKitchenObjectSO)
            firstKitchenObjet.TryAddIngridient(kitchenObjectSO);
    }

    private bool TheKitchenObjectContentVaries(PlateKitchenObject firstKitchenObjet, PlateKitchenObject secondKitchenObject)
    {
        List<KitchenObjectSO> tempKitchenObjectSO = firstKitchenObjet.GetKitchenObjectSOList();

        for (int i = 0; i < tempKitchenObjectSO.Count; i++)
        {
            if (tempKitchenObjectSO[i] == null)
            {
                Debug.Log($"Kitchien Object {i} Is Null");
                return false;
            }

            if (secondKitchenObject.FindElemetInKitchenList(tempKitchenObjectSO[i]))
            {
                Debug.Log($"Exit In {i} | {tempKitchenObjectSO[i]} Element");
                return false;
            }
        }

        return true;
    }
}