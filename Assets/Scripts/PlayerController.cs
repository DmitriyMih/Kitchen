using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private Player player;
    public PlayerController() { }
    public PlayerController(float moveSpeed, float rotationSpeed)
    {
        player.moveSpeed = moveSpeed;
        player.rotationSpeed = rotationSpeed;
    }

    [Header("Move Settings")]
    [SerializeField] private bool isWalking;
    public bool IsWalking => isWalking;

    private void Update()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * player.moveSpeed * Time.deltaTime;

        isWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * player.rotationSpeed);
    }
}

[Serializable]
public class Player
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
}