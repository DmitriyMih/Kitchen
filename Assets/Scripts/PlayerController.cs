using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private Player player;
    [SerializeField] private GameInput gameInput;

    [Header("Move Settings")]
    [SerializeField] private bool isWalking;
    public bool IsWalking
    {
        get => isWalking;
        set
        {
            isWalking = value;
            moveEvent?.Invoke(IsWalking);
        }
    }

    [Header("Actions")]
    public Action<bool> moveEvent;

    public PlayerController() { }
    public PlayerController(float moveSpeed, float rotationSpeed)
    {
        player.moveSpeed = moveSpeed;
        player.rotationSpeed = rotationSpeed;
    }

    private void Awake()
    {
        gameInput = GetComponent<GameInput>();
    }

    private void Update()
    {
        if (gameInput == null)
            return;

        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * player.moveSpeed * Time.deltaTime;

        IsWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * player.rotationSpeed);
    }
}

[Serializable]
public class Player
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
}