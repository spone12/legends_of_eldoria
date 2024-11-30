using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    public event EventHandler OnPlayerAttack;

    private void Awake() {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Combat.Attack.started += PlayerAttack_started;
    }

    /**
     * Trigger to track a character's attack
     */
    private void PlayerAttack_started(InputAction.CallbackContext obj) {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    /**
     * Get movement vector
     */
    public Vector2 GetMovementVector() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    /**
     * Get mouse position
     */
    public Vector3 GetMousePosition() {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }
}
