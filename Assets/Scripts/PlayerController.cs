using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKitchenObjectParent
{
    public static PlayerController Instance { get; private set; }

    [Header("Player Data")]
    [SerializeField] private Player player;
    private GameInput gameInput;

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
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObject kitchenObjectInHoldPoint;

    private Vector3 lastinteractDirection;
    private BaseCounter selectedCounter;

    [Header("Actions")]
    public Action<bool> moveEvent;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
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
        gameInput.OnInteractAlternateAction += GameInputOnInteractAlternateAction;
    }

    private void GameInputOnInteractAction(object sender, System.EventArgs e)
    {
        Debug.Log("Interact - " + selectedCounter);
        if (selectedCounter != null)
            selectedCounter.Interact(this);
    }

    private void GameInputOnInteractAlternateAction(object sender, System.EventArgs e)
    {
        Debug.Log("Interact Alternate - " + selectedCounter);
        if (selectedCounter != null)
            selectedCounter.InteractAlternate(this);
    }

    private void Update()
    {
        HandleMovement();
        HandleOnInteraction();
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                    SetSelectedCounter(baseCounter);
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
        transform.position += moveDirection * moveDistance;


        IsWalking = moveDirection != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * player.rotationSpeed);
    }

    #region Ikitchen Interface
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObjectInHoldPoint = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObjectInHoldPoint;
    }

    public bool HasKitchenObject()
    {
        return kitchenObjectInHoldPoint != null;
    }

    public void ClearKitchenObject()
    {
        kitchenObjectInHoldPoint = null;
    }
    #endregion
}

[Serializable]
public class Player
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
}