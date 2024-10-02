using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIngredientUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform iconTemplate;
    void Awake()
    {
        plateKitchenObject.OnIngredientsAdded += PlateKitchenObject_OnIngredientsAdded;
        iconTemplate.gameObject.SetActive(false);
    }

    private void PlateKitchenObject_OnIngredientsAdded(object sender, PlateKitchenObject.OnIngredientsAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectsSO kitchenObjectsSO in plateKitchenObject.GetKitchenObjectSOs())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateSingleIngriendUI>().SetKitchenObject(kitchenObjectsSO);
        }
    }
}
