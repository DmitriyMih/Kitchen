using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();

        playerInput.Player.Interact.performed += InteractionPerformed;
    }

    private void InteractionPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAction !=  null)
            OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}