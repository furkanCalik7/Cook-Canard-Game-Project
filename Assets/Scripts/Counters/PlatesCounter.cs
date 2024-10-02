using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private int maxPlateAmount = 4;
    [SerializeField] private float secondsIntervalToSpawnPlates = 4f;
    [SerializeField] private KitchenObjectsSO plateKitchenObjectSO;
    private float plateSpawnTimer = 0f;
    private int plateAmount = 0;

    private void Update()
    {
        plateSpawnTimer += Time.deltaTime;
        TrySpawnPlate();
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            return;
        }
        TryGiveKitchenObjectToPlayer(player);
        return;
    }

    private bool TryGiveKitchenObjectToPlayer(IKitchenObjectParent kitchenObjectParent)
    {
        if (plateAmount < 1) {
            return false;
        }
        Transform kitchenObjectTransform = Instantiate(plateKitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<PlateKitchenObject>().SetKitchenObjectParent(kitchenObjectParent);
        kitchenObjectParent.GetKitchenObject().transform.localPosition = Vector3.zero;
        RemoveLastPlate();
        return true;
    }
    private void RemoveLastPlate()
    {
        plateAmount--;
        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
    }


    private bool TrySpawnPlate()
    {
        if (plateSpawnTimer >= secondsIntervalToSpawnPlates)
        {
            if (plateAmount < maxPlateAmount)
            {
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                plateSpawnTimer = 0f;
                plateAmount++;
                return true;
            }
            plateSpawnTimer = 0f;
            return false;
        }
        return false;
    }
}