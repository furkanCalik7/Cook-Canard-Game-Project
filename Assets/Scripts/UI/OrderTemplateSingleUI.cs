using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderTemplateSingleUI : MonoBehaviour
{
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private TextMeshProUGUI recipeTitle;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform recipeIcon;

    void Start()
    {
        recipeIcon.gameObject.SetActive(false);
    }

    public void SetRecipe(RecipeSO recipeSO)
    {
        recipeTitle.text = recipeSO.recipeName;
        foreach (KitchenObjectsSO kitchenObjectsSO in recipeSO.ingredients)
        {
            Transform imageTransform = Instantiate(recipeIcon, iconContainer);
            imageTransform.GetComponent<Image>().sprite = kitchenObjectsSO.sprite;
            imageTransform.gameObject.SetActive(true);
        }
    }
}
