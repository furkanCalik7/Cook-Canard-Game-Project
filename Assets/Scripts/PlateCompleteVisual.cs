using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{

    [Serializable]
    public struct KitchenObjectsSO_GameObject
    {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject gameObject;
    }
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] List<KitchenObjectsSO_GameObject> kitchenObjectsSO_GameObjects;
    private Dictionary<KitchenObjectsSO, GameObject> kitchenObjectToGameObjectsMap;

    void Awake()
    {
        plateKitchenObject.OnIngredientsAdded += PlateKitchenObject_OnIngredientsAdded;
        kitchenObjectToGameObjectsMap = new Dictionary<KitchenObjectsSO, GameObject>();
        foreach (KitchenObjectsSO_GameObject kitchenObjectsSO_GameObject in kitchenObjectsSO_GameObjects)
        {
            kitchenObjectToGameObjectsMap.Add(kitchenObjectsSO_GameObject.kitchenObjectsSO, kitchenObjectsSO_GameObject.gameObject);
        }
    }

    private void PlateKitchenObject_OnIngredientsAdded(object sender, PlateKitchenObject.OnIngredientsAddedEventArgs e)
    {
        kitchenObjectToGameObjectsMap[e.ingredient].SetActive(true);
    }
}
