using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerMovementActions playerMovementActions;
    public event EventHandler OnPauseAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private void Awake()
    {
        Instance = this;
        playerMovementActions = new PlayerMovementActions();
        playerMovementActions.Player.Enable();

        playerMovementActions.Player.Interact.performed += Interact_performed;
        playerMovementActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerMovementActions.Player.Pause.performed += Pause_performed;
    }
    void OnDestroy()
    {
        playerMovementActions.Player.Interact.performed -= Interact_performed;
        playerMovementActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerMovementActions.Player.Pause.performed -= Pause_performed;
        playerMovementActions.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext callbackContext)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext callbackContext)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext callbackContext)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized()
    {
        return playerMovementActions.Player.Move.ReadValue<Vector2>().normalized;
    }
}
