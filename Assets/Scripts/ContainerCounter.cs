using System;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnPlayerGrapAnItem;
    public override void Interact(PlayerMovement player)
    {
        if (player.HasKitchenObject())
        {
            return;
        }
        Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrapAnItem?.Invoke(this, EventArgs.Empty);
    }
}
