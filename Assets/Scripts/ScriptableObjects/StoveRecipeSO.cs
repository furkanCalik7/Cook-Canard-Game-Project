using UnityEngine;

[CreateAssetMenu()]
public class StoveRecipeSO : ScriptableObject
{
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public float requiredCookingTime;
}
