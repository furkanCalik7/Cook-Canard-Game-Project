using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaced;
    [SerializeField] protected Transform counterTopPoint;
    protected KitchenObject kitchenObject;
    public abstract void Interact(Player player);
    public virtual void InteractAlternate(Player player) {
        Debug.Log("BaseClass.InteractAlternate()");
    }

    public static void ResetStaticData() {
        OnAnyObjectPlaced = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null) {
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }

    public void CleanKitchenObject()
    {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
}
