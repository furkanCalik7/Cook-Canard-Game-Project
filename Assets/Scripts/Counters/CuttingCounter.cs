using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
    private int cuttingProgress;
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
                    return;
                }
                return;
            }
            GiveKitchenObjectToPlayer(player);
            return;
        }

        if (!player.HasKitchenObject())
        {
            Debug.Log("Player dont have a kitchen object.");
            return;
        }

        if (!isKitchenObjectCuttable(player.GetKitchenObject().GetKitchenObjectsSO()))
        {
            return;
        }
        GetKitchenObjectFromPlayer(player);
        CuttingRecipeSO cuttingRecipeSO = findCuttingRecipe(kitchenObject.GetKitchenObjectsSO());
        cuttingProgress = 0;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressChanged = (float)cuttingProgress / cuttingRecipeSO.requiredCuttingAction
        });
    }

    public override void InteractAlternate(Player player)
    {
        if (!HasKitchenObject())
        {
            return;
        }
        CuttingRecipeSO cuttingRecipeSO = findCuttingRecipe(kitchenObject.GetKitchenObjectsSO());
        if (cuttingRecipeSO == null) return;
        cuttingProgress++;

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressChanged = (float)cuttingProgress / cuttingRecipeSO.requiredCuttingAction
        });
        if (cuttingProgress < cuttingRecipeSO.requiredCuttingAction)
        {
            return;
        }
        kitchenObject.DestroySelf();

        Transform cuttedKitchenObjectTransform = Instantiate(cuttingRecipeSO.output.prefab);
        cuttedKitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
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

    private bool isKitchenObjectCuttable(KitchenObjectsSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = findCuttingRecipe(inputKitchenObjectSO);
        return cuttingRecipeSO != null;

    }

    private KitchenObjectsSO findRelatedCuttingObject(KitchenObjectsSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = findCuttingRecipe(inputKitchenObjectSO);
        if (cuttingRecipeSO == null) return null;
        return cuttingRecipeSO.output;
    }

    private CuttingRecipeSO findCuttingRecipe(KitchenObjectsSO inputKitchenObject)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOs)
        {
            if (cuttingRecipeSO.input == inputKitchenObject)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
