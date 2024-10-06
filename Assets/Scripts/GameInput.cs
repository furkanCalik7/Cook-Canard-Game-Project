using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private static string PLAYER_PREFS_INPUT_BINDINGS = "InputBindings";

    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveRight,
        MoveLeft,
        Interact,
        InteractAlternate,
        Pause
    }
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

        if (PlayerPrefs.HasKey(PLAYER_PREFS_INPUT_BINDINGS))
        {
            playerMovementActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_INPUT_BINDINGS));
        }
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

    public string GetBindingString(Binding binding)
    {
        switch (binding)
        {
            case Binding.MoveUp:
                return playerMovementActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return playerMovementActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerMovementActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerMovementActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerMovementActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerMovementActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            default:
            case Binding.Pause:
                return playerMovementActions.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void Rebind(Binding binding, Action action)
    {
        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            case Binding.MoveUp:
                inputAction = playerMovementActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = playerMovementActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = playerMovementActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = playerMovementActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerMovementActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerMovementActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            default:
            case Binding.Pause:
                inputAction = playerMovementActions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        playerMovementActions.Player.Disable();
        inputAction.PerformInteractiveRebinding(bindingIndex)
        .OnComplete(callback =>
        {
            playerMovementActions.Player.Enable();
            action.Invoke();
            callback.Dispose();
            PlayerPrefs.SetString(PLAYER_PREFS_INPUT_BINDINGS, playerMovementActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        })
        .Start();
    }
}
