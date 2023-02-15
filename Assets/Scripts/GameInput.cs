using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private PlayerInput playerInput;

    private void Awake()
    {
        Instance = this;

        playerInput = new PlayerInput();
        playerInput.Player.Enable();

        playerInput.Player.Interact.performed += PlayerInput_InteractPerformed;
        playerInput.Player.InteractAlternate.performed += PlayerInput_InteractAlternatePerformed;
        playerInput.Player.Pause.performed += PlayerInput_PausePerformed;
    }

    private void OnDestroy()
    {
        playerInput.Player.Interact.performed -= PlayerInput_InteractPerformed;
        playerInput.Player.InteractAlternate.performed -= PlayerInput_InteractAlternatePerformed;
        playerInput.Player.Pause.performed -= PlayerInput_PausePerformed;

        playerInput.Dispose();
    }

    private void PlayerInput_PausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Pause");
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInput_InteractAlternatePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInput_InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}