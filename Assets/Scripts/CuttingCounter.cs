using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public override void Interact(PlayerMovement player)
    {
        if (HasKitchenObject())
        {
            Debug.Log("The clear counter already has a kitchen object!");
            if (player.HasKitchenObject())
            {
                Debug.Log("The player already has a kitchen object.");
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
}
