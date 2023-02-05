using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;

    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;

                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = this.state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f,
                            instantFill = true
                        });
                    }
                    break;

                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = this.state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f,
                            instantFill = true
                        });
                    }
                    break;

                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(PlayerController player)
    {
        //  If Empty
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //  Plate In Hand
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectInHand))
                {
                    // In Hand Not Empty
                    if (plateKitchenObjectInHand.GetKitchenObjectSOListCount() == 1)
                    {
                        List<KitchenObjectSO> inputKitchenRecepySO = plateKitchenObjectInHand.GetKitchenObjectSOList();
                        foreach (KitchenObjectSO kitchenObjectSO in inputKitchenRecepySO)
                            if (HasRecipeWithInput(kitchenObjectSO))
                            {
                                plateKitchenObjectInHand.ClearThePlate();

                                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
                                StartFrying(GetKitchenObject().GetKitchenObjectSO());
                                return;
                            }
                    }
                }
                //  In Hand No Plate
                else
                {
                    KitchenObject inputKitchenRecepy = player.GetKitchenObject();
                    if (HasRecipeWithInput(inputKitchenRecepy.GetKitchenObjectSO()))
                    {
                        inputKitchenRecepy.SetKitchenObjectParent(this);
                        StartFrying(inputKitchenRecepy.GetKitchenObjectSO());
                    }
                }
            }
        }
        else
        {
            Debug.Log("Stove Empty");
            // If Objects In Player Not Null
            if (player.HasKitchenObject())
            {
                Debug.Log("Non");
                if (state == State.Frying)
                    return;

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        SetDefaultState();
                    }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                SetDefaultState();
            }
        }
    }

    private void StartFrying(KitchenObjectSO kitchenObjectSO)
    {
        fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        if (fryingRecipeSO == null) { Debug.LogError("Kitchen Object So Is Null"); return; }

        state = State.Frying;
        fryingTimer = 0;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = this.state
        }); ;
    }

    private void SetDefaultState()
    {
        state = State.Idle;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = this.state
        });

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
        });
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        if (inputKitchenObjectSO == null) { Debug.LogError("Input Kitchen Object So Is Null"); return false; }

        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
            return fryingRecipeSO.output;
        else
            return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        for (int i = 0; i < fryingRecipeSOArray.Length; i++)
            if (fryingRecipeSOArray[i].input == inputKitchenObjectSO)
                return fryingRecipeSOArray[i];

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        for (int i = 0; i < burningRecipeSOArray.Length; i++)
            if (burningRecipeSOArray[i].input == inputKitchenObjectSO)
                return burningRecipeSOArray[i];

        return null;
    }
}