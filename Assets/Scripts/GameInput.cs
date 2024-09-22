using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerMovementActions playerMovementActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private void Awake() {
        playerMovementActions = new PlayerMovementActions();
        playerMovementActions.Player.Enable();

        playerMovementActions.Player.Interact.performed += Interact_performed;
        playerMovementActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void Interact_performed(InputAction.CallbackContext callbackContext) {
            OnInteractAction?.Invoke(this, EventArgs.Empty);  
    } 

    private void InteractAlternate_performed(InputAction.CallbackContext callbackContext) {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);  
    }
    public Vector2 GetMovementVectorNormalized()
    {
        return playerMovementActions.Player.Move.ReadValue<Vector2>().normalized;
    }
}
