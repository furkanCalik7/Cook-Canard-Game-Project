using System;
using UnityEngine;

public class DeliveryCounter : BaseCounter, IKitchenObjectParent
{
    public static DeliveryCounter Instance {get; private set;}
    void Awake() {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            return;
        }

        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            DeliveryManager.Instance.DeliverOrder(plateKitchenObject);
            plateKitchenObject.DestroySelf();
        }
    }
}
