using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IKitchenObjectParent
{
    public static PlayerController Instance { get; private set; }

    [Header("Player Data")]
    [SerializeField] private Player player;
    private GameInput gameInput;

    [Header("Move Settings")]
    private CharacterController characterController;
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
    private KitchenObject selectedKitchenObject;

    [Header("Actions")]
    public Action<bool> moveEvent;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    public event EventHandler<OnSelectedkitchenObjectChangedEventArgs> OnSelectedKitchenObjectChanged;
    public class OnSelectedkitchenObjectChangedEventArgs : EventArgs
    {
        public KitchenObject selectedKitchenObject;
    }

    public PlayerController() { }
    public PlayerController(float moveSpeed, float rotationSpeed)
    {
        player.moveSpeed = moveSpeed;
        player.rotationSpeed = rotationSpeed;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
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
        //Debug.Log("Interact - " + selectedCounter);
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
            return;
        }

        if (HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(null);
            SetSelectedKitchenObject(null);
        }

        if (selectedKitchenObject != null)
        {
            selectedKitchenObject.SetKitchenObjectParent(this);
            SetSelectedKitchenObject(null);
        }
    }

    private void GameInputOnInteractAlternateAction(object sender, System.EventArgs e)
    {
        //Debug.Log("Interact Alternate - " + selectedCounter);
        if (selectedCounter != null)
            selectedCounter.InteractAlternate(this);
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
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

    private void SetSelectedKitchenObject(KitchenObject selectedKitchenObject)
    {
        this.selectedKitchenObject = selectedKitchenObject;

        OnSelectedKitchenObjectChanged?.Invoke(this, new OnSelectedkitchenObjectChangedEventArgs
        {
            selectedKitchenObject = selectedKitchenObject
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

            if (raycastHit.transform.TryGetComponent(out KitchenObject kitchenObject))
            {
                if (!HasKitchenObject())
                    SetSelectedKitchenObject(kitchenObject);
            }
            else
                SetSelectedKitchenObject(null);
        }
        else
        {
            SetSelectedCounter(null);
            SetSelectedKitchenObject(null);
        }
    }

    private void HandleMovement()
    {
        if (gameInput == null)
            return;

        Vector2 inputVector = gameInput.GetMovementVector();
        IsWalking = inputVector != Vector2.zero;

        float handleGravity = HandleGravity();
        Vector3 moveDirection = new Vector3(inputVector.x, handleGravity, inputVector.y);

        characterController.Move(moveDirection * player.moveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (gameInput == null)
            return;

        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 positionToLookAt = new Vector3(inputVector.x, 0f, inputVector.y);
        Quaternion currentRotation = transform.rotation;

        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, player.rotationSpeed * Time.deltaTime);
        }
    }

    private float HandleGravity()
    {
        return characterController.isGrounded ? -0.05f : -9.8f;
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