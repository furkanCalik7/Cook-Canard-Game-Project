using System;
using UnityEngine;

public class DeliverManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform orderTemplate;

    void Start()
    {
        orderTemplate.gameObject.SetActive(false);
        DeliveryManager.Instance.OnOrderSpawned += DeliveryManager_OnOrderSpawned;
        DeliveryManager.Instance.OnOrderFinished += DeliveryManager_OnOrderFinished;
    }
    private void DeliveryManager_OnOrderSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    private void DeliveryManager_OnOrderFinished(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (orderTemplate == child) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeList())
        {
            Transform recipeTransform = Instantiate(orderTemplate, container);
            recipeTransform.GetComponent<OrderTemplateSingleUI>().SetRecipe(recipeSO);
            recipeTransform.gameObject.SetActive(true);
        }
    }
}
