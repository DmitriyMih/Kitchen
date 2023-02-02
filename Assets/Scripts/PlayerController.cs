using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Player Data")]
    [SerializeField] private Player player;
    [SerializeField] private GameInput gameInput;

    [Header("Move Settings")]
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeigh = 2f;

    [SerializeField, Space] private bool isWalking;
    public bool IsWalking
    {
        get => isWalking;
        set
        {
            isWalking = value;
            moveEvent?.Invoke(IsWalking);
        }
    }

    [Header("Interaction Settings")]
    [SerializeField] private float interactDistance = 2f;
    private Vector3 lastinteractDirection;
    private ClearCounter selectedCounter;

    [SerializeField] private LayerMask countersLayerMask;

    [Header("Actions")]
    public Action<bool> moveEvent;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    public PlayerController() { }
    public PlayerController(float moveSpeed, float rotationSpeed)
    {
        player.moveSpeed = moveSpeed;
        player.rotationSpeed = rotationSpeed;
    }

    private void Awake()
    {
        gameInput = GetComponent<GameInput>();

        if (Instance != null)
            Debug.LogError("There is more than one Player instance");
        else
            Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInputOnInteractAction;
    }

    private void GameInputOnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
            selectedCounter.Interact();
    }

    private void Update()
    {
        HandleMovement();
        HandleOnInteraction();
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    private void HandleOnInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDirection != Vector3.zero)
            lastinteractDirection = moveDirection;

        if (Physics.Raycast(transform.position, lastinteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                    SetSelectedCounter(clearCounter);
            }
            else
                SetSelectedCounter(null);
        }
        else
            SetSelectedCounter(null);
    }

    private void HandleMovement()
    {
        if (gameInput == null)
            return;

        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = player.moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeigh, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeigh, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
                moveDirection = moveDirectionX;
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeigh, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                    moveDirection = moveDirectionZ;
            }
        }
        else
            transform.position += moveDirection * moveDistance;

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