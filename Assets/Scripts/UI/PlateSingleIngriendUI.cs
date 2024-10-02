using UnityEngine;
using UnityEngine.UI;

public class PlateSingleIngriendUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetKitchenObject(KitchenObjectsSO kitchenObjectsSO)
    {
        image.sprite = kitchenObjectsSO.sprite;
    }
}
