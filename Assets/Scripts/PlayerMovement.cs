using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 7f;
    [SerializeField] private float rotationField = 10f;
    [SerializeField] private bool isWalking = false;
    [SerializeField] private GameInput gameInput;
    private Vector3 lastInteractedDir;
    [SerializeField] private LayerMask counterLayerMask;

    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    { 
        float interactionDistance = 6f;
        bool isInteracted = Physics.Raycast(transform.position, lastInteractedDir, out RaycastHit hitInfo, interactionDistance, counterLayerMask);

        if (isInteracted)
        {
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    void Update()
    {
        HandleMovement();
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
        if (moveDir != Vector3.zero) {
            lastInteractedDir = moveDir;
        }
        isWalking = moveDir != Vector3.zero;
        // Look the direction you go
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationField);
    }

}
