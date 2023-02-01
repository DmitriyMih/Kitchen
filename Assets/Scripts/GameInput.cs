using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInput playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInput();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        Debug.Log("Input - " + playerInputActions.Player.Move.ReadValue<Vector2>());
        return inputVector;
    }
}