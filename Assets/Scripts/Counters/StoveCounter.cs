using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStoveStateChangedEventArgs> OnStoveStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStoveStateChangedEventArgs : EventArgs
    {
        public StoveState currentState;
    }


    public enum StoveState
    {
        IDLE,
        COOKING,
        COOKED
    }

    [SerializeField] private StoveRecipeSO[] stoveRecipeSOs;
    private StoveState currentState;
    private float stovedProgress;
    private void Start()
    {
        currentState = StoveState.IDLE;
        OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
        {
            currentState = currentState
        });

    }

    private void Update()
    {
        if (!HasKitchenObject())
        {
            return;
        }
        switch (currentState)
        {
            case StoveState.IDLE:
                stovedProgress = 0f;


                if (!isKitchenObjectStovable(kitchenObject.GetKitchenObjectsSO()))
                {
                    return;
                }
                stovedProgress = 0f;
                currentState = StoveState.COOKING;
                OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
                {
                    currentState = currentState
                });
                break;
            case StoveState.COOKING:
                stovedProgress += Time.deltaTime;
                float requiredCookingTime = findStoveRecipe(kitchenObject.GetKitchenObjectsSO()).requiredCookingTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressChanged = stovedProgress / requiredCookingTime
                });

                if (stovedProgress < requiredCookingTime) return;

                kitchenObject.DestroySelf();

                Transform stovedKitchenObjectTransform = Instantiate(findStoveRecipe(kitchenObject.GetKitchenObjectsSO()).output.prefab);
                stovedKitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

                currentState = StoveState.COOKED;
                OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
                {
                    currentState = currentState
                });
                break;
            case StoveState.COOKED:
                if (!isKitchenObjectStovable(kitchenObject.GetKitchenObjectsSO()))
                {
                    return;
                }
                stovedProgress = 0f;
                currentState = StoveState.COOKING;
                OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
                {
                    currentState = currentState
                });
                break;
        }
    }
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredients(GetKitchenObject().GetKitchenObjectsSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    currentState = StoveState.IDLE;
                    OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
                    {
                        currentState = currentState
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressChanged = stovedProgress
                    });
                    return;
                }
                return;
            }
            GiveKitchenObjectToPlayer(player);
            currentState = StoveState.IDLE;
            OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
            {
                currentState = currentState
            });
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressChanged = stovedProgress
            });
            return;
        }

        if (!player.HasKitchenObject())
        {
            return;
        }

        if (!isKitchenObjectStovable(player.GetKitchenObject().GetKitchenObjectsSO()))
        {
            return;
        }
        currentState = StoveState.IDLE;
        OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
        {
            currentState = currentState
        });
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressChanged = stovedProgress
        });

        GetKitchenObjectFromPlayer(player);
    }
    private void GiveKitchenObjectToPlayer(IKitchenObjectParent kitchenObjectParent)
    {
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        kitchenObjectParent.GetKitchenObject().transform.localPosition = Vector3.zero;
    }

    private void GetKitchenObjectFromPlayer(IKitchenObjectParent kitchenObjectParent)
    {
        kitchenObjectParent.GetKitchenObject().SetKitchenObjectParent(this);
        kitchenObject.transform.localPosition = Vector3.zero;
    }



    private bool isKitchenObjectStovable(KitchenObjectsSO inputKitchenObjectSO)
    {
        StoveRecipeSO stoveRecipeSO = findStoveRecipe(inputKitchenObjectSO);
        return stoveRecipeSO != null;

    }

    private StoveRecipeSO findStoveRecipe(KitchenObjectsSO inputKitchenObject)
    {
        foreach (StoveRecipeSO stoveRecipeSO in stoveRecipeSOs)
        {
            if (stoveRecipeSO.input == inputKitchenObject)
            {
                return stoveRecipeSO;
            }
        }
        return null;
    }
}
