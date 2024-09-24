using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnCuttingActionPerformedEventArgs> OnCuttingActionPerformed;
    public class OnCuttingActionPerformedEventArgs : EventArgs
    {
        public float normalizedCuttingProgress;
    }
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
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
        OnCuttingActionPerformed?.Invoke(this, new OnCuttingActionPerformedEventArgs
        {
            normalizedCuttingProgress = (float)cuttingProgress / cuttingRecipeSO.requiredCuttingAction
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

        OnCuttingActionPerformed?.Invoke(this, new OnCuttingActionPerformedEventArgs
        {
            normalizedCuttingProgress = (float)cuttingProgress / cuttingRecipeSO.requiredCuttingAction
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
