using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerMovementActions playerMovementActions;
    public event EventHandler OnInteractAction;
    private void Awake() {
        playerMovementActions = new PlayerMovementActions();
        playerMovementActions.Player.Enable();

        // This is the syntex for the C# events
        playerMovementActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext callbackContext) {
            OnInteractAction?.Invoke(this, EventArgs.Empty);  
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerMovementActions.Player.Move.ReadValue<Vector2>().normalized;
    }
}
