using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
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

        GetKitchenObjectFromPlayer(player);
    }

    public override void InteractAlternate(Player player)
    {
        if (!HasKitchenObject())
        {
            return;
        }
        KitchenObjectsSO cuttedKitchenObjectsSO = findRelatedCuttingObject(kitchenObject.GetKitchenObjectsSO());
        if (cuttedKitchenObjectsSO == null) return;
        kitchenObject.DestroySelf();

        Transform cuttedKitchenObjectTransform = Instantiate(cuttedKitchenObjectsSO.prefab);
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

    private KitchenObjectsSO findRelatedCuttingObject(KitchenObjectsSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOs)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }

}
