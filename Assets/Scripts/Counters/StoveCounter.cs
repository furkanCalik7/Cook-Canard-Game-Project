using UnityEngine;

public class StoveCounter : BaseCounter
{
    enum StoveState
    {
        IDLE,
        COOKING,
        COOKED
    }

    [SerializeField] private StoveRecipeSO[] stoveRecipeSOs;
    private StoveState currentState;
    private float stovedProgress;
    private void Start()
    {
        currentState = StoveState.IDLE;
    }

    private void Update()
    {
        if (!HasKitchenObject())
        {
            return;
        }
        switch (currentState)
        {
            case StoveState.IDLE:
                if (!isKitchenObjectStovable(kitchenObject.GetKitchenObjectsSO()))
                {
                    return;
                }
                stovedProgress = 0f;
                currentState = StoveState.COOKING;
                break;
            case StoveState.COOKING:
                stovedProgress += Time.deltaTime;

                if (stovedProgress < findStoveRecipe(kitchenObject.GetKitchenObjectsSO()).requiredCookingTime) return;

                kitchenObject.DestroySelf();

                Transform stovedKitchenObjectTransform = Instantiate(findStoveRecipe(kitchenObject.GetKitchenObjectsSO()).output.prefab);
                stovedKitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

                currentState = StoveState.COOKED;
                break;
            case StoveState.COOKED:
                if (!isKitchenObjectStovable(kitchenObject.GetKitchenObjectsSO()))
                {
                    return;
                }
                stovedProgress = 0f;
                currentState = StoveState.COOKING;
                break;
        }
        Debug.Log(currentState);
    }
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                return;
            }
            GiveKitchenObjectToPlayer(player);
            return;
        }

        if (!player.HasKitchenObject())
        {
            return;
        }

        if (!isKitchenObjectStovable(player.GetKitchenObject().GetKitchenObjectsSO()))
        {
            return;
        }

        GetKitchenObjectFromPlayer(player);
    }
    private void GiveKitchenObjectToPlayer(IKitchenObjectParent kitchenObjectParent)
    {
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        kitchenObjectParent.GetKitchenObject().transform.localPosition = Vector3.zero;
    }

    private void GetKitchenObjectFromPlayer(IKitchenObjectParent kitchenObjectParent)
    {
        kitchenObjectParent.GetKitchenObject().SetKitchenObjectParent(this);
        kitchenObject.transform.localPosition = Vector3.zero;
    }



    private bool isKitchenObjectStovable(KitchenObjectsSO inputKitchenObjectSO)
    {
        StoveRecipeSO stoveRecipeSO = findStoveRecipe(inputKitchenObjectSO);
        return stoveRecipeSO != null;

    }

    private StoveRecipeSO findStoveRecipe(KitchenObjectsSO inputKitchenObject)
    {
        foreach (StoveRecipeSO stoveRecipeSO in stoveRecipeSOs)
        {
            if (stoveRecipeSO.input == inputKitchenObject)
            {
                return stoveRecipeSO;
            }
        }
        return null;
    }
}
