using System;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    public event EventHandler OnPlayerGrapAnItem;
    public override void Interact(Player player)
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
