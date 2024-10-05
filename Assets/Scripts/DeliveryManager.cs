using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    public event EventHandler OnOrderSpawned;
    public event EventHandler OnOrderFinished;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailed;

    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private float spawnRecipeTimerMax = 4f;
    [SerializeField] private int spawnRecipeAmountMax = 4;

    private List<RecipeSO> waitingRecipeList;
    private float spawnRecipeTimer = 0f;
    private int spawnRecipeAmount = 0;
    private int deliveredOrder = 0;

    void Awake()
    {
        Instance = this;
        waitingRecipeList = new List<RecipeSO>();
    }
    void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0f)
        {
            spawnRecipe();
            spawnRecipeTimer = spawnRecipeTimerMax;
        }
    }
    private void spawnRecipe()
    {
        if (spawnRecipeAmount >= spawnRecipeAmountMax) return;
        RecipeSO spawnedRecipe = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
        waitingRecipeList.Add(spawnedRecipe);
        OnOrderSpawned?.Invoke(this, EventArgs.Empty);
        spawnRecipeAmount++;
    }

    public void DeliverOrder(PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectsSO> ingredientsInPlate = plateKitchenObject.GetKitchenObjectSOs();
        foreach (RecipeSO recipeSO in waitingRecipeList)
        {
            if (recipeSO.ingredients.Count == ingredientsInPlate.Count)
            {
                bool doAnyRecipeIngredientsMatch = true;
                foreach (KitchenObjectsSO recipeIngredient in recipeSO.ingredients)
                {
                    if (!ingredientsInPlate.Any(p => p == recipeIngredient))
                    {
                        doAnyRecipeIngredientsMatch = false;
                        break;
                    }
                }
                if (doAnyRecipeIngredientsMatch)
                {
                    waitingRecipeList.Remove(recipeSO);
                    spawnRecipeAmount--;
                    deliveredOrder++;
                    OnOrderFinished?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeList()
    {
        return waitingRecipeList;
    }

    public int GetSuccessfullDeliveredOrderCount()
    {
        return deliveredOrder;
    }
}
