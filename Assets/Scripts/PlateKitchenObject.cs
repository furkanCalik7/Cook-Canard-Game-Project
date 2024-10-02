using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientsAddedEventArgs> OnIngredientsAdded;
    public class OnIngredientsAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO ingredient;
    }
    [SerializeField] private List<KitchenObjectsSO> validKitchenObjects;
    private List<KitchenObjectsSO> kitchenObjectSOs;

    private void Awake()
    {
        kitchenObjectSOs = new List<KitchenObjectsSO>();
    }
    public bool TryAddIngredients(KitchenObjectsSO kitchenObjectSO)
    {
        if (!validKitchenObjects.Contains(kitchenObjectSO))
        {
            return false;
        }
        if (kitchenObjectSOs.Contains(kitchenObjectSO))
        {
            return false;
        }
        kitchenObjectSOs.Add(kitchenObjectSO);
        OnIngredientsAdded?.Invoke(this, new OnIngredientsAddedEventArgs
        {
            ingredient = kitchenObjectSO
        });
        return true;
    }

    public List<KitchenObjectsSO> GetKitchenObjectSOs()
    {
        return kitchenObjectSOs;
    }

}
