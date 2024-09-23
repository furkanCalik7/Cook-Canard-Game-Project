using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedClearCounterChangedEventArgs> OnSelectedClearCounterChanged;
    public class OnSelectedClearCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float speed = 7f;
    [SerializeField] private float rotationField = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectGrapLocation;
    private bool isWalking = false;
    private Vector3 lastInteractedDir;
    private BaseCounter selectedBaseCounter;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one player!!");
        }
        Instance = this;
    }

    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedBaseCounter == null) return;
        selectedBaseCounter.Interact(this);
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedBaseCounter == null) return;
        selectedBaseCounter.InteractAlternate(this);

    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        float interactionDistance = 2f;
        bool isInteracted = Physics.Raycast(transform.position, lastInteractedDir, out RaycastHit hitInfo, interactionDistance, counterLayerMask);

        if (isInteracted)
        {
            if (hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (selectedBaseCounter != baseCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleMovement()
    {
        Vector2 movementVector = gameInput.GetMovementVectorNormalized();

        float moveDistance = speed * Time.deltaTime;

        Vector3 moveDir = new Vector3(movementVector.x, 0f, movementVector.y);
        float playerSize = 0.7f;
        float playerHeight = 2f;

        bool canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDir, moveDistance);
        if (canWalk)
        {
            transform.position += moveDir * moveDistance;
        }
        if (moveDir != Vector3.zero)
        {
            lastInteractedDir = moveDir;
        }
        isWalking = moveDir != Vector3.zero;
        // Look the direction you go
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationField);
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        selectedBaseCounter = selectedCounter;
        OnSelectedClearCounterChanged?.Invoke(this, new OnSelectedClearCounterChangedEventArgs
        {
            selectedCounter = selectedBaseCounter
        });
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
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
        return kitchenObjectGrapLocation;
    }
}
